using Microsoft.Extensions.DependencyInjection;
using Eskon.Service.Interfaces;
using Eskon.Service.Services;
using Eskon.Domian.Entities.Identity;
using Microsoft.Extensions.Configuration;

namespace Eskon.Service
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection InjectingServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IFavouriteService, FavouriteService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<IReviewService, ReviewService>();

            //Authentication
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);
            return services;
        }
    }
}
