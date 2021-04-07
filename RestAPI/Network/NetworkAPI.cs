using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Chetch.RestAPI.Network
{
    public class NetworkAPI : APIService
    {
        public const String CHETCH_MESSAGING_SERVICE = "Chetch Messaging";
        public const String DEFAULT_PROTOCOL = "http";
        public const String LOCAL_HOST = "127.0.0.1";
        public const int LOCAL_HOST_PORT = 8001;

        public class Service : DataObject
        {
            public String GetBaseURL(String protocol = null)
            {
                String domain = GetDomain() == null ? LOCAL_HOST : GetDomain();
                if(protocol == null)
                {
                    String[] protocols = GetProtocols();
                    protocol = protocols[0];
                }
                return protocol + "://" + domain + ":" + this["endpoint_port"] + "/" + this["endpoint"];
            }

            public String[] GetProtocols()
            {
                String[] protocols = this["protocols"].ToString().Split(',');
                return protocols;
            }

            public String GetDomain()
            {
                return this["domain"] == null ? null : this["domain"].ToString();
            }
        }

        public class Token : DataObject
        {
            public Token(){}

            public Token(int serviceID, String clientName, String tokenValue = null)
            {
                this["service_id"] = serviceID;
                this["client_name"] = clientName;
                this["token"] = tokenValue;
            }

            public String Value
            {
                get
                {
                    return ContainsKey("token") ? this["token"].ToString() : null;
                }

                set
                {
                    this["token"] = value;
                }
            }
        }


        public class Services : Dictionary<String, Service>, IRestAPIObject
        {
            static private readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

            public void Parse(string data)
            {
                var map = jsonSerializer.Deserialize<Dictionary<String, Service>>(data);
                foreach (var item in map)
                {
                    Add(item.Key, item.Value);
                }
            }

            public string Serialize()
            {
                throw new NotImplementedException();
            }
        }

        
        static private NetworkAPI _instance;

        static public NetworkAPI GetInstance()
        {
            if(_instance == null)
            {
                _instance = new NetworkAPI();
            }
            return _instance;
        }

        static public T CreateAPIService<T>(String serviceName) where T : APIService, new()
        {
            Service service = GetAPIService(serviceName);
            var api = new T();
            api.BaseURL = service.GetBaseURL();
            return api;
        }

        static public Service GetAPIService(String serviceName)
        {
            var api = GetInstance();
            return api.GetService(serviceName);
        }

        
        public NetworkAPI() : this(LOCAL_HOST_PORT) { }

        public NetworkAPI(int port) : this(DEFAULT_PROTOCOL + "://" + LOCAL_HOST + ":" + port + "/api") { }

        public NetworkAPI(String baseURL) : base(baseURL)
        {

        }

        public Services GetServices()
        {
            return Get<Services>("services");
        }

        public Service GetService(String serviceName)
        {
            Services services = GetServices();
            return services.ContainsKey(serviceName) ? services[serviceName] : null;
        }


        public Token GetToken(int serviceID, String clientName)
        {
            String stub = String.Format("token?service_id={0}&client_name={1}", serviceID, clientName);
            return Get<Token>(stub);
        }

        public Token GetToken(String serviceName, String clientName)
        {
            Service s = GetService(serviceName);
            return GetToken(s.ID, clientName);
        }

        public Token SaveToken(int serviceID, String clientName, String tokenValue)
        {
            Token token = new Token(serviceID, clientName, tokenValue);
            return SaveToken(token);
        }

        public Token SaveToken(Token token)
        {
            return Put<Token>("token", token);
        }
    }
}
