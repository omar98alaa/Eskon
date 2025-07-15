using Microsoft.Extensions.DependencyInjection;
using Template.Infrastructure.Generics;
using Template.Infrastructure.Interfaces;
using Template.Infrastructure.Repositories;

namespace Template.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection InjectingInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}
