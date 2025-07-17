using Microsoft.Extensions.DependencyInjection;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Eskon.Infrastructure.Repositories;
using Eskon.Infrastructure.Interfaces.Country_City;
using Eskon.Infrastructure.Repositories.Country_CityRepo;

namespace Eskon.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection InjectingInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IFavouriteRepository, FavouriteRepository>();
          
            return services;
        }
    }
}
