using AutoMapper;
using Eskon.Core.Features.Country_CityFeatures.Queries.Models;
using Eskon.Core.Mapping.Country_CityMapping;
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
            services.AddAutoMapper(cfg => cfg.AddProfile<GetCityMapper>());
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(GetCityByNameQuery).Assembly));
            services.AddAutoMapper(cfg => cfg.AddProfile<GetCountryMapper>());
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(GetCountryByNameQuery).Assembly));


            return services;
        }
    }
}
