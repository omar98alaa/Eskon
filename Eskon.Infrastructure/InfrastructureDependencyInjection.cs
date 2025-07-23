using Eskon.Infrastructure.Interfaces;
using Eskon.Infrastructure.Repositories;
using Eskon.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Eskon.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection InjectingInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryUnitOfWork, RepositoryUnitOfWork>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            //services.AddTransient<IImageRepository, ImageRepository>();

            return services;
        }
    }
}
