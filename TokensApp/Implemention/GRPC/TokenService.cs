using Grpc.Core;
using SandBoxLib.Implemention.Services;
using SandBoxLib.Models.RRServices.tokens;
using SandBoxLib.Models.RRServices.users;
using TokensApp.Abstractions.Services;
using TokensApp.Implemention.Proxy;

namespace TokensApp.Implemention.GRPC;

public class TokenService : Auth.AuthBase
{
    private readonly IUsersProxy _usersProxy;
    private readonly IJWTService _jwtService;
    public TokenService(IUsersProxy usersProxy, IJWTService jwtService)
    {
        _usersProxy = usersProxy;
        _jwtService = jwtService;
    }

    public override async Task<LoginReply> Login(LoginRequest request, ServerCallContext context)
    {
        var response = await _usersProxy.UsersAuthUserAsync(new AuthUserRequest { LoginOrEmail = request.LoginOrEmail, Password = request.Password });
        var refreshToken = string.Empty;
        var accessToken = string.Empty;
        if (response.IsSuccess)
        {
            refreshToken = _jwtService.Generate(new TokenClaims { UserId = response.Id }, TypeToken.RefreshToken);
            accessToken = _jwtService.Generate(new TokenClaims { UserId = response.Id }, TypeToken.AccessToken);
        }
        return new LoginReply { Message = response.Message ?? string.Empty, RefreshToken = refreshToken, AccessToken = accessToken };
    }

    public override async Task<GetTokenReply> GetRefreshToken(GetTokenRequest request, ServerCallContext context)
    {
        var reply = new GetTokenReply { Token = string.Empty, Message = string.Empty };
        var claims = Utils.Validate(request.RefreshToken, TypeToken.RefreshToken);
        if (claims == null)
            reply.Message = "Invalid token";
        else
            reply.Token = _jwtService.Generate(claims, TypeToken.RefreshToken);
        return reply;
    }

    public override async Task<GetTokenReply> GetAccessToken(GetTokenRequest request, ServerCallContext context)
    {
        var reply = new GetTokenReply { Token = string.Empty, Message = string.Empty };
        var claims = Utils.Validate(request.RefreshToken, TypeToken.RefreshToken);
        if (claims == null)
            reply.Message = "Invalid token";
        else
            reply.Token = _jwtService.Generate(claims, TypeToken.AccessToken);
        return reply;
    }
}
