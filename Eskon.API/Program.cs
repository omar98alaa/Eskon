using Eskon.API.Hubs;
using Eskon.Core;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Stripe;
using Eskon.Infrastructure;
using Eskon.Infrastructure.Context;
using Eskon.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Eskon.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<MyDbContext>(op => op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("dev")));
            builder.Services.Configure<IdentitySeeder>(builder.Configuration.GetSection("IdentitySettings"));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            // Injecting Dependencies
            builder.Services.InjectingInfrastructureDependencies();
            builder.Services.InjectingServiceDependencies();
            builder.Services.InjectingCoreDependencies();

            //signalR 
            builder.Services.AddSignalR();

            #region JWT Settings
            var jwtSettings = new JwtSettings();
            builder.Configuration.GetRequiredSection(nameof(jwtSettings)).Bind(jwtSettings);
            builder.Services.AddSingleton(jwtSettings);
            #endregion


            #region Stripe Settings
            var stripeSettings = new StripeSettings();
            builder.Configuration.GetRequiredSection(nameof(stripeSettings)).Bind(stripeSettings);
            builder.Services.AddSingleton(stripeSettings);
            #endregion


            #region Identity Configurations
            // Configure Identity Account 
            builder.Services.AddIdentityApiEndpoints<User>(options =>
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
             .AddRoles<Role>()
             .AddEntityFrameworkStores<MyDbContext>()
             .AddDefaultTokenProviders();
            #endregion


            #region Authentication Configurations
            //Authorization
            builder.Services.AddAuthentication(options =>
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
                  options.Events = new JwtBearerEvents
                  {
                      OnMessageReceived = context =>
                      {
                          var accessToken = context.Request.Query["access_token"];
                          var path = context.HttpContext.Request.Path;


                          if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/api/chatHub"))
                          {
                              context.Token = accessToken;
                          }

                          return Task.CompletedTask;
                      }
                  };
              });
            #endregion


            #region FluentEmail Configuration
            builder.Services
                .AddFluentEmail(builder.Configuration.GetRequiredSection("MailGun")["FromEmail"])
                .AddMailGunSender(
                    builder.Configuration.GetRequiredSection("MailGun")["Domain"],
                    builder.Configuration.GetRequiredSection("MailGun")["API_KEY"],
                    FluentEmail.Mailgun.MailGunRegion.USA
                );
            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }

            // Seeding the Identity Roles at the Program Start
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                await IdentitySeeder.SeedRolesAsync(roleManager);
            }

            app.UseRouting();

            app.UseCors("AllowLocalhost");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHub<ChatHub>("/api/chatHub");

            app.Run();
        }
    }
}