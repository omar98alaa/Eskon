using Eskon.Service.Interfaces;
using Eskon.Service.Services;
using Eskon.Service.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Eskon.Service
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection InjectingServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IServiceUnitOfWork, ServiceUnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
