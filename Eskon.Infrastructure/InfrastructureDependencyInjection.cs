using Microsoft.Extensions.DependencyInjection;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Eskon.Infrastructure.Repositories;

namespace Eskon.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection InjectingInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            
            services.AddTransient<IFavouriteRepository, FavouriteRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            return services;
        }
    }
}
