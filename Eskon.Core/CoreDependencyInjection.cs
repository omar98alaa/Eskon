using Eskon.Core.Mapping;
using Eskon.Core.Mapping.BookingMapping;
using Eskon.Core.Mapping.Chats;
using Eskon.Core.Mapping.CityMapping;
using Eskon.Core.Mapping.CountryMapping;
using Eskon.Core.Mapping.FavouriteMapping;
using Eskon.Core.Mapping.Properties;
using Eskon.Core.Mapping.ReviewMapping;
using Eskon.Core.Mapping.Users;
using Eskon.Core.Quartz;
using Eskon.Domian.Models;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
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


            #region Quartz
            services.AddQuartz(static q =>
            {

                // Job 1 - Delete unpaid bookings
                q.AddJob<DeleteUnpaidBookingsJob>(opts => opts.WithIdentity(nameof(DeleteUnpaidBookingsJob)));
                q.AddTrigger(opts => opts
                    .ForJob(nameof(DeleteUnpaidBookingsJob))
                    .WithIdentity("DeleteUnpaidBookingsTrigger")
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(2, 0)) // 2:00 AM daily
                );


                // Job 2 - Send daily email by pending properties to its assigned admin
                q.AddJob<SendPendingPropertiesEmailToAdminJob>(opts => opts.WithIdentity(nameof(SendPendingPropertiesEmailToAdminJob)));
                q.AddTrigger(opts => opts
                    .ForJob(nameof(SendPendingPropertiesEmailToAdminJob))
                    .WithIdentity("SendPendingPropertiesEmailToAdminTrigger")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(1, 0)) // 1:00 AM daily
                //.WithSimpleSchedule(x => x
                //    .WithIntervalInMinutes(2)
                //    .RepeatForever())
                );

                // Job 3 - Send daily email by pending bookings to its owner
                q.AddJob<SendPendingBookingsEmailToOwnerJob>(opts => opts.WithIdentity(nameof(SendPendingBookingsEmailToOwnerJob)));
                q.AddTrigger(opts => opts
                    .ForJob(nameof(SendPendingBookingsEmailToOwnerJob))
                    .WithIdentity("SendPendingBookingsEmailToOwnerTrigger")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(3, 0)) // 3:00 AM daily
                //.WithSimpleSchedule(x => x
                //    .WithIntervalInMinutes(2)
                //    .RepeatForever())
                );

            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            #endregion
            services.AddAutoMapper(conf => conf.AddProfile<FavouriteMapping>());
            services.AddAutoMapper(conf => conf.AddProfile<ReviewMappings>());
            services.AddAutoMapper(conf => conf.AddProfile<NotificationMapping>());
            return services;
        }
    }
}
