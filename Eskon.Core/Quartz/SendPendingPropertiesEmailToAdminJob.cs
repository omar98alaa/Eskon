
using Eskon.Domian.Entities.Identity;
using Eskon.Service.Interfaces;
using Eskon.Service.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Quartz;

namespace Eskon.Core.Quartz
{
    public class SendPendingPropertiesEmailToAdminJob : IJob
    {
        #region Fields
        private readonly IEmailService _emailService;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public SendPendingPropertiesEmailToAdminJob(IEmailService emailService, IServiceUnitOfWork serviceUnitOfWork, UserManager<User> userManager)
        {
            _emailService = emailService;
            _serviceUnitOfWork = serviceUnitOfWork;
            _userManager = userManager;
        }
        #endregion

        public async Task Execute(IJobExecutionContext context)
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");

            foreach (var admin in admins)
            {
                var propertiesCount = await _serviceUnitOfWork.PropertyService
                    .GetAllPendingPropertiesCountPerAdminAsync(admin.Id);

                if (propertiesCount == 0)
                {
                    continue;
                }

                string subject = "Pending Properties Notification";
                string body = $"Dear {admin.Email},\n\nYou have {propertiesCount} pending properties:\n";

                 _emailService.SendEmailAsync(
                    To: admin.Email,
                    Subject: subject,
                    Body: body
                );
            }
        }

    }
}
