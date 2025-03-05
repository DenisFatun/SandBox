using AutoMapper;
using SandBoxLib.Extensions;
using UsersApp.Abstractions.Repositories;
using UsersApp.Abstractions.Services;
using UsersApp.Config;

namespace UsersApp.Extensions
{
    public static class InitService
    {
        public static void InitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwagger();

            services.AddServicesByInterface<IRepository>();
            services.AddServicesByInterface<IService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingEntity());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void InitApplication(this WebApplication app)
        {
            app.UseConfiguredSwagger();

            app.AddUserEndpoints();
        }        
    }
}
