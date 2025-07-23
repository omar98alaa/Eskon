using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Eskon.Core.Mapping.Users;
using Eskon.Core.Mapping.Properties;

namespace Eskon.Core
{
    public static class CoreDependencyInjection
    {
        public static IServiceCollection InjectingCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(conf => conf.AddProfile<UserProfileMapping>());
            services.AddAutoMapper(conf => conf.AddProfile<PropertyMappings>());
            return services;
        }
    }
}
