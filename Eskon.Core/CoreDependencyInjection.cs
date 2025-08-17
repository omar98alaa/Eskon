using Eskon.Core.Mapping;
using Eskon.Core.Mapping.BookingMapping;
using Eskon.Core.Mapping.Chats;
using Eskon.Core.Mapping.CityMapping;
using Eskon.Core.Mapping.CountryMapping;
using Eskon.Core.Mapping.FavouriteMapping;
using Eskon.Core.Mapping.Properties;
using Eskon.Core.Mapping.ReviewMapping;
using Eskon.Core.Mapping.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Eskon.Core
{
    public static class CoreDependencyInjection
    {
        public static IServiceCollection InjectingCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(conf => conf.AddProfile<UserProfileMapping>());
            services.AddAutoMapper(conf => conf.AddProfile<ChatMessageMapping>());
            services.AddAutoMapper(conf => conf.AddProfile<CityMapping>());
            services.AddAutoMapper(conf => conf.AddProfile<CountryMapper>());
            services.AddAutoMapper(conf => conf.AddProfile<PropertyMappings>());
            services.AddAutoMapper(conf => conf.AddProfile<BookingMappings>());
            services.AddAutoMapper(conf => conf.AddProfile<FavouriteMapping>());
            services.AddAutoMapper(conf => conf.AddProfile<ReviewMappings>());
            services.AddAutoMapper(conf => conf.AddProfile<NotificationProfile>());
            return services;
        }
    }
}
