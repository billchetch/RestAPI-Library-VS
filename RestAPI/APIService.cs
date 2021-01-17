using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Chetch.RestAPI
{
    /*var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();


            try
            {

            } catch (Exception e)
{
    Console.WriteLine(e.Message);
}


int serviceID = 2;
Dictionary<String, Object> token = new Dictionary<String, Object>();
token["service_id"] = serviceID;
token["client_name"] = cn;
token["token"] = "xsdfasdfa";
String json = jsonSerializer.Serialize(token);
var data = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    var response = await httpClient.PutAsync("http://127.0.0.1:8001/api/token", data);
    int k = 3;
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
} */

    public class APIService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly JavaScriptSerializer _jsonSerializer = new JavaScriptSerializer();

        public String BaseURL { get; internal set;  }
        public APIService(String baseURL)
        {
            BaseURL = baseURL + (baseURL.Last() == '/' ? "" : "/");
        }

        protected String ApiURL(String stub)
        {
            return BaseURL + stub;
        }

        async public Task<T> GetAsync<T>(String stub) where T : IRestAPIObject, new()
        {
            try
            {
                String url = ApiURL(stub);

                //HttpResponseMessage response = await _httpClient.PutAsync("http://127.0.0.1:8001/api/token", data);
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                String responseBody = await response.Content.ReadAsStringAsync();

                T t = new T();
                t.Parse(responseBody);

                return t;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return default(T);
        }

        public T Get<T>(String stub) where T: IRestAPIObject, new()
        {
            return Task.Run(() => GetAsync<T>(stub)).GetAwaiter().GetResult();
        }

    }
}
