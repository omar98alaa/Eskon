using Microsoft.Extensions.DependencyInjection;
using Eskon.Service.Interfaces;
using Eskon.Service.Services;
using Eskon.Service.Services.Country_City;
using Eskon.Service.Interfaces.Country_City;
using Eskon.Domian.Entities.Identity;
using Microsoft.Extensions.Configuration;

namespace Eskon.Service
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection InjectingServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IFavouriteService, FavouriteService>();

            //Authentication
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);
            
            return services;
        }
    }
}
