using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utilities.Rest;

namespace Utilities.RestApi
{
    public class RestUtility : IRestUtility
    {
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _client;
        string _clientName;

        public RestUtility(IHttpClientFactory httpClientFactory, string ClientName)
        {
            _httpClientFactory = httpClientFactory;
            _clientName = ClientName;
        }

        #region Generic, Async, static HTTP functions for GET, POST, PUT, and DELETE             

        public async Task<T> GetAsync<T>(string url)
        {
            T data;
            _client = _httpClientFactory.CreateClient(_clientName);

            using (HttpResponseMessage response = await _client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                string d = await content.ReadAsStringAsync();
                if (d != null)
                {
                    data = JsonConvert.DeserializeObject<T>(d);
                    return (T)data;
                }
            }

            Object o = new Object();
            return (T)o;
        }

        public async Task<T> PostAsync<T>(string url, object requestBodyObject, string contentType = HttpContentType.JSON)
        {
            T data;
            _client = _httpClientFactory.CreateClient(_clientName);

            if (requestBodyObject != null)
            {
                var requestBody = JsonConvert.SerializeObject(requestBodyObject);

                if (contentType == HttpContentType.XForm)
                {
                    Dictionary<string, string> requestContent = MapToDictionary(requestBodyObject);

                    using (HttpResponseMessage response = await _client.PostAsync(url, new FormUrlEncodedContent(requestContent)))
                    using (HttpContent content = response.Content)
                    {
                        string d = await content.ReadAsStringAsync();
                        if (d != null)
                        {
                            data = JsonConvert.DeserializeObject<T>(d);
                            return (T)data;
                        }
                    }
                }
                else
                {
                    using (HttpResponseMessage response = await _client.PostAsync(url, new StringContent(requestBody, Encoding.UTF8, contentType)))
                    using (HttpContent content = response.Content)
                    {
                        string d = await content.ReadAsStringAsync();
                        if (d != null)
                        {
                            data = JsonConvert.DeserializeObject<T>(d);
                            return (T)data;
                        }
                    }
                }

            }

            Object o = new Object();
            return (T)o;
        }

        public async Task<T> PutAsync<T>(string url, object requestBodyObject, string contentType = HttpContentType.JSON)
        {
            T data;
            _client = _httpClientFactory.CreateClient(_clientName);
            if (requestBodyObject != null)
            {
                var requestBody = JsonConvert.SerializeObject(requestBodyObject);

                using (HttpResponseMessage response = await _client.PutAsync(url, new StringContent(requestBody, Encoding.UTF8, contentType)))
                using (HttpContent content = response.Content)
                {
                    string d = await content.ReadAsStringAsync();
                    if (d != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(d);
                        return (T)data;
                    }
                }
            }

            Object o = new Object();
            return (T)o;
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            T newT;
            _client = _httpClientFactory.CreateClient(_clientName);

            using (HttpResponseMessage response = await _client.DeleteAsync(url))
            using (HttpContent content = response.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    newT = JsonConvert.DeserializeObject<T>(data);
                    return newT;
                }
            }
            Object o = new Object();
            return (T)o;
        }
        #endregion

        public Dictionary<string, string> MapToDictionary(object source)
        {
            var dictionary = new Dictionary<string, string>();
            MapToDictionaryInternal(dictionary, source);
            return dictionary;
        }

        private void MapToDictionaryInternal(
            Dictionary<string, string> dictionary, object source, string name = "")
        {
            var properties = source.GetType().GetProperties();
            foreach (var p in properties)
            {
                var key = string.IsNullOrEmpty(name) ? p.Name : name + "." + p.Name;
                object value = p.GetValue(source, null);
                Type valueType = value?.GetType();

                if (valueType == null || valueType.IsPrimitive || valueType == typeof(string) || valueType == typeof(int) || valueType == typeof(decimal) || valueType == typeof(long) || valueType == typeof(bool) || valueType == typeof(DateTime))
                {
                    dictionary[key] = value == null ? "" : value.ToString();
                }
                else if (value is IEnumerable)
                {
                    var i = 0;
                    foreach (object o in (IEnumerable)value)
                    {
                        MapToDictionaryInternal(dictionary, o, key + "[" + i + "]");
                        i++;
                    }
                }
                else
                {
                    MapToDictionaryInternal(dictionary, value, key);
                }
            }
        }

        public static class HttpContentType
        {
            public const string JSON = "application/json";
            public const string XForm = "application/x-www-form-urlencoded";
        }
    }
}
