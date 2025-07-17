using Microsoft.Extensions.DependencyInjection;
using Eskon.Service.Interfaces;
using Eskon.Service.Services;
using Eskon.Service.Services.Country_City;
using Eskon.Service.Interfaces.Country_City;

namespace Eskon.Service
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection InjectingServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICityService, CityService>();

            return services;
        }
    }
}
