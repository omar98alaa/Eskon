using Eskon.Infrastructure.Interfaces;

namespace Eskon.Infrastructure.UnitOfWork
{
    public interface IRepositoryUnitOfWork
    {
        #region Properties
        public IBookingRepository BookingRepository { get; }
        public IChatRepository ChatRepository { get; }
        public IChatMessageRepository ChatMessageRepository { get; }
        public ICityRepository CityRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IFavouriteRepository FavouriteRepository { get; }
        public INotificationRepository NotificationRepository { get; }
        public INotificationTypeRepository NotificationTypeRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IPropertyRepository PropertyRepository { get; }
        public IPropertyTypeRepository PropertyTypeRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IReviewRepository ReviewRepository { get; }
        public ITicketRepository TicketRepository { get; }
        public IUserRepository UserRepository { get; }
        #endregion

        #region Methods
        public Task<int> SaveChangesAsync();
        #endregion
    }
}
