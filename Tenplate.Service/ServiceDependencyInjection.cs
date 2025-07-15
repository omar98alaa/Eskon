using Microsoft.Extensions.DependencyInjection;
using Template.Service.Interfaces;
using Template.Service.Services;

namespace Template.Service
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
