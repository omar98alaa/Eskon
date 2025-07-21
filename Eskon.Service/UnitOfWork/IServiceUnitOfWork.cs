using Eskon.Service.Interfaces;

namespace Eskon.Service.UnitOfWork
{
    public interface IServiceUnitOfWork
    {
        #region Properties
        public IAuthenticationService AuthenticationService { get; }
        public IBookingService BookingService { get; }
        public IFavouriteService FavouriteService { get; }
        public IPaymentService PaymentService { get; }
        public IPropertyService PropertyService { get; }
        public IPropertyTypeService PropertyTypeService { get; }
        public IRefreshTokenService RefreshTokenService { get; }
        public IReviewService ReviewService { get; }
        public ITicketService TicketService { get; }
        public ITransactionService TransactionService { get; }
        public IUserService UserService { get; }
        public INotificationTypeService NotificationTypeService { get; }
        public INotificationService NotificationService { get; }
        #endregion

        #region Methods
        public Task<int> SaveChangesAsync();
        #endregion
    }
}
