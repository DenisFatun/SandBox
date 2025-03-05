using SandBoxLib.Models;

namespace Proxy.Models
{
    public class ProxyAuthUserResponse: BaseResponse
    {
        public string RefreshToken { get;set; }
        public string AccessToken { get; set; }
    }
}
