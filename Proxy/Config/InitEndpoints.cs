using Microsoft.AspNetCore.Http;
using Proxy.GRPC;
using Proxy.Models;
using Proxy.Proxies;
using SandBoxLib.Filters;
using SandBoxLib.Models.RRServices.tokens;
using SandBoxLib.Models.RRServices.users;
using System.Net;

namespace Proxy.Config
{
    public static class InitEndpoints
    {
        public static void AddUserEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/users").WithTags("users");

            group.MapPost("/", async (IUsersProxy proxy, UsersCreateRequest request) =>
            {
                return await proxy.UsersCreateAsync(request);
            }).Validate<UsersCreateRequest>();

            group.MapGet("/", async (IUsersProxy proxy, HttpContext context) =>
            {
                var user = (TokenClaims)context.Items["User"];
                return await proxy.UsersGetByIdAsync(user.UserId);
            }).AddEndpointFilter<AuthorizeFilter>();

            group.MapPost("/login", async (ITokensGrpc tokensGrpc, AuthUserRequest request) =>
            {
                return await tokensGrpc.LoginAsync(request);
            });

            group.MapPost("/refresh-token", async (ITokensGrpc tokensGrpc, TokenRequest request) =>
            {
                return await tokensGrpc.GetRefreshTokenAsync(request);
            });

            group.MapPost("/access-token", async (ITokensGrpc tokensGrpc, TokenRequest request) =>
            {
                return await tokensGrpc.GetAccessTokenAsync(request);
            });
        }
    }
}
