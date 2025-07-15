using Microsoft.Extensions.DependencyInjection;
using Eskon.Service.Interfaces;
using Eskon.Service.Services;

namespace Eskon.Service
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection InjectingServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
