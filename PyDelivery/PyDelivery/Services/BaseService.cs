using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace PyDelivery.Services
{
    public enum ServiceType
    {
        Merchant,
        Auth,
        Product,
        Order
    }

    public class BaseService
    {
        HttpClient _client;

        public const string BASE_URL = "https://getpymobileapp.azurewebsites.net/";
        public BaseService()
        {
            _client = new HttpClient();
        }
        protected string getUrl(ServiceType serviceType, string endpoint)
        {
            switch (serviceType)
            {
                case ServiceType.Merchant:
                    return string.Format("{0}{1}{2}{3}", BASE_URL, "api/v2/", "merchant/", endpoint);
                case ServiceType.Order:
                    return string.Format("{0}{1}{2}{3}", BASE_URL, "api/v2/", "order/", endpoint);
                case ServiceType.Product:
                    return string.Format("{0}{1}{2}{3}", BASE_URL, "api/v2/", "prod/", endpoint);
                default:
                    return string.Format("{0}{1}{2}{3}", BASE_URL, "api/v2/", "auth/", endpoint);
            }
            return null;
        }

        public string getAuthUrl(string endpoint)
        {
            return getUrl(ServiceType.Auth, endpoint);
        }
        public string getOrderUrl(string endpoint)
        {
            return getUrl(ServiceType.Order, endpoint);
        }
        public string getProducetUrl(string endpoint)
        {
            return getUrl(ServiceType.Product, endpoint);
        }
        public string getMerchantUrl(string endpoint)
        {
            return getUrl(ServiceType.Merchant, endpoint);
        }

        private void addHeaders(HttpRequestMessage requestMessage, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var keyValue in headers)
                {
                    requestMessage.Headers.Add(keyValue.Key, keyValue.Value);
                }
            }
        }
        private string getQueryParamsUrl(string uri, Dictionary<string, string> queryParams)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach (var keyPair in queryParams)
            {
                query[keyPair.Key] = keyPair.Value;
            }
            string queryString = query.ToString();
            var url = uri + "?" + queryString;
            return url;
        }
        public async Task<T> Get<T>(string uri, Dictionary<string, string> queryParams, Dictionary<string, string> headers)
        {
            HttpResponseMessage response = null;
            try
            {
                string url = getQueryParamsUrl(uri, queryParams);
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    addHeaders(requestMessage, headers);
                    response = await _client.SendAsync(requestMessage);
                }
                string rcontent = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    T resObj = JsonConvert.DeserializeObject<T>(rcontent);
                    response = null;
                    return resObj;
                }
                else
                {
                    //Log.Debug("[BaseService]-[Post]", string.Format("Post request failed with {0} -Message:{1}", response.StatusCode.ToString(), rcontent));
                }
                return default;
            }
            catch (Exception e)
            {
                response = null;
                await Task.Delay(100);
                return default;
            }

        }
        public async Task<T> Post<T>(string url, Dictionary<string, dynamic> payload, Dictionary<string, string> headers)
        {
            return await Post<T>(url, JsonConvert.SerializeObject(payload), headers);
        }
        public async Task<T> Post<T>(string url, string json, Dictionary<string, string> headers)
        {
            HttpResponseMessage response = null;
            try
            {
                //Log.Debug("[BaseService]-[Post]", string.Format("Request for {0} initiated", url));
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    addHeaders(requestMessage, headers);
                    requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await _client.SendAsync(requestMessage);
                }
                string rcontent = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    T resObj = JsonConvert.DeserializeObject<T>(rcontent);
                    response = null;
                    return resObj;
                }
                else
                {
                    //Log.Debug("[BaseService]-[Post]", string.Format("Post request failed with {0} -Message:{1}", response.StatusCode.ToString(), rcontent));
                }
                return default;
            }
            catch (Exception e)
            {
                //Log.Debug("[BaseService]-[Post]", "Exception raised while invoking post " + e.Message);
                response = null;
                await Task.Delay(100);
                return default;
            }
        }
        public async Task<T> Put<T>(string url, Dictionary<string, string> payload, Dictionary<string, string> headers)
        {
            return await Put<T>(url, JsonConvert.SerializeObject(payload), headers);
        }
        public async Task<T> Put<T>(string url, string json, Dictionary<string, string> headers)
        {
            HttpResponseMessage response = null;
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, url))
                {
                    addHeaders(requestMessage, headers);
                    requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await _client.SendAsync(requestMessage);
                }
                string rcontent = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    T resObj = JsonConvert.DeserializeObject<T>(rcontent);
                    response = null;
                    return resObj;
                }
                else
                {
                    //Log.Debug("[BaseService]-[Post]", string.Format("Post request failed with {0} -Message:{1}", response.StatusCode.ToString(), rcontent));
                }
                return default;
            }
            catch (Exception e)
            {
                //Log.Debug("[BaseService]-[Post]", "Exception raised while invoking post " + e.Message);
                response = null;
                await Task.Delay(100);
                return default;
            }
        }

    }
}
