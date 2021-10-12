using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilities.RestApi;

namespace Utilities.Rest
{
    public interface IRestUtility
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object requestBodyObject, string contentType = HttpContentType.JSON);
        Task<T> PutAsync<T>(string url, object requestBodyObject, string contentType = HttpContentType.JSON);
        Task<T> DeleteAsync<T>(string url);
    }
}
