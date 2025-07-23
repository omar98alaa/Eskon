using AutoMapper;
using Eskon.Core.Mapping.EscrowTransactions;
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
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EscrowTransactionProfileMapping>();
                cfg.AddProfile<UserProfileMapping>();
            });
            return services;
        }
    }
}
