using Eskon.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Eskon.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection InjectingInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryUnitOfWork, RepositoryUnitOfWork>();

            return services;
        }
    }
}
