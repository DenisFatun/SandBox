namespace Proxy.Proxies
{
    public interface IProxy
    {
        Task<TResponse> PostAsync<TResponse>(string url, object body, bool disableLog = false, Dictionary<string, string> headers = null);
        Task<TResponse> GetAsync<TResponse>(string url, bool disableLog = false, Dictionary<string, string> headers = null);
    }
}
