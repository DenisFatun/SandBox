using Microsoft.IdentityModel.Tokens;
using SandBoxLib.Implemention.Services;
using SandBoxLib.Models.RRServices.tokens;
using System.IdentityModel.Tokens.Jwt;
using TokensApp.Abstractions.Services;

namespace TokensApp.Implemention.Services
{
    public class JWTService : IJWTService
    {
        public string Generate(TokenClaims data, TypeToken typeToken = TypeToken.AccessToken)
        {
            var config = Utils.AccessTokenConfig;
            if (typeToken == TypeToken.RefreshToken)
                config = Utils.RefreshTokenConfig;

            var currentDate = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: config.Issuer,
                audience: config.Audience,
                claims: Utils.GenerateClaimsList(data),
                notBefore: currentDate,
                expires: currentDate.Add(TimeSpan.FromMinutes(config.LifeTimeMinutes)),
                signingCredentials: new SigningCredentials(Utils.GetSymmetricSecurityKey(config.SecretKey),
                    SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }                
    }       
}
