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
        public class Service : DataObject
        {
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
    }
}
