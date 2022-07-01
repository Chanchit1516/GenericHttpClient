using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilities.RestApi;
using static Utilities.RestApi.RestUtility;

namespace Utilities.Rest
{
    public interface IRestUtility
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object requestBodyObject, string contentType = HttpContentType.JSON);
        Task<T> PutAsync<T>(string url, object requestBodyObject, string contentType = HttpContentType.JSON);
        Task<T> DeleteAsync<T>(string url);
        Task PostLineNotiAsync(string lineToken, string message, int stickerPackageID, int stickerID, string pictureUrl);
    }
}
