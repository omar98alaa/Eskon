using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Eskon.Core.Mapping.Users;
using Eskon.Core.Mapping.CityMapping;
using Eskon.Core.Mapping.PropertyImageMapping;
using Eskon.Core.Mapping.CountryMapping;

namespace Eskon.Core
{
    public static class CoreDependencyInjection
    {
        public static IServiceCollection InjectingCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(conf => conf.AddProfile<UserProfileMapping>());
            services.AddAutoMapper(conf=>conf.AddProfile<CityMapping>());
            services.AddAutoMapper(conf => conf.AddProfile<CountryMapper>());
            services.AddAutoMapper(conf => conf.AddProfile<PropertyImageMapping>());
            return services;
        }
    }
}
