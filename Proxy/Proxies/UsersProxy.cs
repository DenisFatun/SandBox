using RestSharp;
using SandBoxLib.Implemention.Services;
using SandBoxLib.Models;
using SandBoxLib.Models.RRServices.users;

namespace Proxy.Proxies
{
    public interface IUsersProxy : IProxy
    {
        Task<BaseResponse> UsersCreateAsync(UsersCreateRequest request);
        Task<UsersGetByIdResponse> UsersGetByIdAsync(int id);
        Task<UsersGetByIdResponse> UsersAuthUserAsync(AuthUserRequest request);
    }

    public class UsersProxy : BaseProxy, IUsersProxy
    {
        public static string HttpClientName = "HttpProducerProxy";
        public UsersProxy(IHttpClientFactory httpClientFactory)
        {
            var httpClient = httpClientFactory.CreateClient(HttpClientName);
            _restClient = new RestClient(httpClient);
        }

        public async Task<BaseResponse> UsersCreateAsync(UsersCreateRequest request)
        {
            return await PostAsync<BaseResponse>("api/users", request);
        }

        public async Task<UsersGetByIdResponse> UsersGetByIdAsync(int id)
        {
            return await GetAsync<UsersGetByIdResponse>($"api/users/{id}");
        }

        public async Task<UsersGetByIdResponse> UsersAuthUserAsync(AuthUserRequest request)
        {
            return await PostAsync<UsersGetByIdResponse>("api/users/login", request);
        }
    }    
}