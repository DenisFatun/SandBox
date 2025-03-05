using RestSharp;
using SandBoxLib.Implemention.Services;
using SandBoxLib.Models.RRServices.users;
using TokensApp.Abstractions.Proxy;

namespace TokensApp.Implemention.Proxy
{
    public interface IUsersProxy : IProxy
    {
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

        public async Task<UsersGetByIdResponse> UsersAuthUserAsync(AuthUserRequest request)
        {
            return await PostAsync<UsersGetByIdResponse>("api/users/login", request);
        }
    }
}
