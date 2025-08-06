
using Eskon.Service.UnitOfWork;
using Quartz;

namespace Eskon.Core.Quartz
{
    public class DeleteUnpaidBookingsJob : IJob
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;

        public DeleteUnpaidBookingsJob(IServiceUnitOfWork serviceUnitOfWork)
        {
           _serviceUnitOfWork = serviceUnitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var unPaidBookings = await _serviceUnitOfWork.BookingService.GetUnpaidPassedAcceptedDateBookingsAsync();
            await _serviceUnitOfWork.BookingService.RemoveBookingRangeAsync(unPaidBookings);
            await _serviceUnitOfWork.SaveChangesAsync();
        }
    }
}
