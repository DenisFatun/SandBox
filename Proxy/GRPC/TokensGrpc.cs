using Proxy.Models;
using SandBoxLib.Models.RRServices.users;

namespace Proxy.GRPC
{
    public interface ITokensGrpc : IGrpc
    {
        Task<ProxyAuthUserResponse> LoginAsync(AuthUserRequest request);
        Task<TokenResponse> GetRefreshTokenAsync(TokenRequest request);
        Task<TokenResponse> GetAccessTokenAsync(TokenRequest request);
    }

    public class TokensGrpc : ITokensGrpc
    {
        private readonly Auth.AuthClient _client;

        public TokensGrpc(Auth.AuthClient client)
        {
            _client = client;
        }

        public async Task<ProxyAuthUserResponse> LoginAsync(AuthUserRequest request)
        {
            var reply = await _client.LoginAsync(new LoginRequest { 
                LoginOrEmail = request.LoginOrEmail, Password = request.Password });

            return new ProxyAuthUserResponse { IsSuccess = string.IsNullOrEmpty(reply.Message), 
                Message = reply.Message, RefreshToken = reply.RefreshToken, AccessToken = reply.AccessToken };
        }

        public async Task<TokenResponse> GetRefreshTokenAsync(TokenRequest request)
        {
            var reply = await _client.GetRefreshTokenAsync(new GetTokenRequest
            {
                RefreshToken = request.RefreshToken
            });

            return new TokenResponse
            {
                IsSuccess = string.IsNullOrEmpty(reply.Message),
                Message = reply.Message,
                Token = reply.Token
            };
        }

        public async Task<TokenResponse> GetAccessTokenAsync(TokenRequest request)
        {
            var reply = await _client.GetAccessTokenAsync(new GetTokenRequest
            {
                RefreshToken = request.RefreshToken
            });

            return new TokenResponse
            {
                IsSuccess = string.IsNullOrEmpty(reply.Message),
                Message = reply.Message,
                Token = reply.Token
            };
        }
    }
}
