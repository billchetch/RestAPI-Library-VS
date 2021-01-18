using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Chetch.RestAPI.Network
{
    public class NetworkAPI : APIService
    {
        public const String CHETCH_MESSAGING_SERVICE = "Chetch Messaging";

        public class Service : DataObject
        {
        }

        public class Token : DataObject
        {
            public Token()
            {

            }

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
