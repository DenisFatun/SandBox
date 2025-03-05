using MediatR;
using SandBoxLib.Decorators;
using SandBoxLib.Extensions;
using System.Reflection;
using TokensApp.Abstractions.Proxy;
using TokensApp.Abstractions.Services;
using TokensApp.Implemention.Proxy;

namespace TokensApp.Config
{
    public static class InitService
    {
        public static void InitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            var httpProducerProxyUrl = new Uri(configuration["RestClients:UsersProxy"]);
            services.AddHttpClient(UsersProxy.HttpClientName, httpClient => httpClient.BaseAddress = httpProducerProxyUrl);

            services.AddServicesByInterface<IProxy>();
            services.AddServicesByInterface<IService>();

            services.AddGrpc();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingDecorator<,>));
        }
    }
}
