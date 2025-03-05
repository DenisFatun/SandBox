using Proxy.Config;
using Proxy.GRPC;
using Proxy.Proxies;
using SandBoxLib.Extensions;

namespace Proxy.Extensions
{
    public static class InitService
    {
        public static void InitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwagger();

            services.AddHttpClient();

            var httpProducerProxyUrl = new Uri(configuration["RestClients:UsersProxy"]);
            services.AddHttpClient(UsersProxy.HttpClientName, httpClient => httpClient.BaseAddress = httpProducerProxyUrl);

            services.AddServicesByInterface<IProxy>();
            services.AddServicesByInterface<IGrpc>();

            services.AddGrpcClient<Auth.AuthClient>(o =>
            {
                o.Address = new Uri(configuration["GRPC:TokensGrpc"]);
            });
        }

        public static void InitApplication(this WebApplication app)
        {
            app.UseConfiguredSwagger();
            app.UseHttpsRedirection();

            app.AddUserEndpoints();
        }        
    }
}
