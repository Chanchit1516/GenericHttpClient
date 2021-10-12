using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            try
            {

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
            }
            catch (Exception ex)
            {
                throw;
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

    }

    public static class HttpContentType
    {
        public const string JSON = "application/json";
        public const string XForm = "application/x-www-form-urlencoded";
    }
}
