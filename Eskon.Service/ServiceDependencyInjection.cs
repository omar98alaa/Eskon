using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Context;
using Eskon.Service.Interfaces;
using Eskon.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Eskon.Service
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection InjectingServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IFavouriteService, FavouriteService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<IReviewService, ReviewService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            #region Identity Configurations

            // Configure Identity Account 
            services.AddIdentityApiEndpoints<User>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);
            })
             .AddRoles<IdentityRole<Guid>>()
             .AddEntityFrameworkStores<MyDbContext>()
             .AddDefaultTokenProviders();
            #endregion


            #region Authentication Configurations
            //Authorization
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.SaveToken = true;
                  options.RequireHttpsMetadata = false;
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = jwtSettings.ValidateIssuer,
                      ValidateAudience = jwtSettings.ValidateAudience,
                      ValidateLifetime = jwtSettings.ValidateLifeTime,
                      ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                      ValidIssuer = jwtSettings.Issuer,
                      ValidAudience = jwtSettings.Audience,
                      IssuerSigningKey = new SymmetricSecurityKey(
                          Encoding.ASCII.GetBytes(jwtSettings.Secret)
                      )
                  };
              }); 
            #endregion
            return services;
        }
    }
}
