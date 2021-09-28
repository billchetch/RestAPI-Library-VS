using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Chetch.RestAPI
{
    public class APIService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly JavaScriptSerializer _jsonSerializer = new JavaScriptSerializer();

        public static void SetHTTPTimeout(int timeout)
        {
            if (timeout != _httpClient.Timeout.TotalMilliseconds)
            {
                _httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
            }
        }

        private String _baseURL;
        public String BaseURL { get
            {
                return _baseURL;
            }
            set 
            {
                _baseURL = value + (value.Last() == '/' ? "" : "/");
            }
        }

        public APIService()
        {

        }

        public APIService(String baseURL)
        {
            BaseURL = baseURL;
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

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                String responseBody = await response.Content.ReadAsStringAsync();

                T t = new T();
                t.Parse(responseBody);

                return t;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public T Get<T>(String stub) where T: IRestAPIObject, new()
        {
            return Task.Run(() => GetAsync<T>(stub)).GetAwaiter().GetResult();
        }


        async public Task<T> PutAsync<T>(String stub, T t) where T : IRestAPIObject, new()
        {
            try
            {
                String url = ApiURL(stub);

                String json = t.Serialize();
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync(url, data);
                response.EnsureSuccessStatusCode();
                String responseBody = await response.Content.ReadAsStringAsync();

                T newT = new T();
                newT.Parse(responseBody);

                return newT;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public T Put<T>(String stub, T t) where T : IRestAPIObject, new()
        {
            return Task.Run(() => PutAsync<T>(stub, t)).GetAwaiter().GetResult();
        }
    }
}
