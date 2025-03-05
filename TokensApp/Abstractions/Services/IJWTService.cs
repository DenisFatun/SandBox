using SandBoxLib.Models.RRServices.tokens;
using TokensApp.Implemention.Services;

namespace TokensApp.Abstractions.Services
{
    public interface IJWTService : IService
    {
        string Generate(TokenClaims data, TypeToken typeToken);
    }
}
