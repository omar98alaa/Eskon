
using Eskon.Domian.Entities.Identity;
using Eskon.Service.Interfaces;
using Eskon.Service.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Quartz;

namespace Eskon.Core.Quartz
{
    public class SendPendingBookingsEmailToOwnerJob : IJob
    {
        #region Fields
        private readonly IEmailService _emailService;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public SendPendingBookingsEmailToOwnerJob(IEmailService emailService, IServiceUnitOfWork serviceUnitOfWork, UserManager<User> userManager)
        {
            _emailService = emailService;
            _serviceUnitOfWork = serviceUnitOfWork;
            _userManager = userManager;
        }
        #endregion

        public async Task Execute(IJobExecutionContext context)
        {
            var owners = await _userManager.GetUsersInRoleAsync("Owner");

            foreach (var owner in owners)
            {
                var bookingsCount = await _serviceUnitOfWork.BookingService.GetPendingBookingsCountPerOwnerAsync(owner.Id);

                if (bookingsCount == 0)
                {
                    continue;
                }

                string subject = "Pending Bookings Notification";
                string body = $"Dear {owner.Email},\n\nYou have {bookingsCount} pending bookings:\n";

                _emailService.SendEmailAsync(
                   To: owner.Email,
                   Subject: subject,
                   Body: body
               );
            }
        }
    }
}
