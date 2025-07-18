using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Eskon.Core;
using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure;
using Eskon.Infrastructure.Context;
using Eskon.Service;

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
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Injecting Dependencies
            builder.Services.InjectingInfrastructureDependencies();
            builder.Services.InjectingServiceDependencies(builder.Configuration);
            builder.Services.InjectingCoreDependencies();
          





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
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                await IdentitySeeder.SeedRolesAsync(roleManager);
            }


            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}