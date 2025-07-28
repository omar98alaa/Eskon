using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Interfaces;
using Eskon.Infrastructure.Repositories;

namespace Eskon.Infrastructure.UnitOfWork
{
    public class RepositoryUnitOfWork : IRepositoryUnitOfWork
    {
        #region Fields
        private readonly MyDbContext context;

        private IBookingRepository bookingRepository;

        private IChatRepository chatRepository;
        
        private IChatMessageRepository chatMessageRepository;
        
        private ICityRepository cityRepository;
        
        private ICountryRepository countryRepository;

        private IFavouriteRepository favouriteRepository;
        
        private IImageRepository imageRepository;
        
        private INotificationRepository notificationRepository;
        
        private INotificationTypeRepository notificationTypeRepository;

        private IPaymentRepository paymentRepository;

        private IPropertyRepository propertyRepository;

        private IPropertyTypeRepository propertyTypeRepository;

        private IRefreshTokenRepository refreshTokenRepository;

        private IReviewRepository reviewRepository;

        private ITicketRepository ticketRepository;

        private ITransactionRepository transactionRepository;

        private IUserRepository userRepository;
        #endregion

        #region Properties
        public IBookingRepository BookingRepository => bookingRepository == null ? new BookingRepository(context) : bookingRepository;

        public IChatRepository ChatRepository => chatRepository == null ? new ChatRepository(context) : chatRepository;
        
        public IChatMessageRepository ChatMessageRepository => chatMessageRepository == null ? new ChatMessageRepository(context) : chatMessageRepository;
        
        public ICityRepository CityRepository => cityRepository== null? new CityRepository(context): cityRepository;

        public ICountryRepository CountryRepository => countryRepository == null ? new CountryRepository(context) : countryRepository;

        public IFavouriteRepository FavouriteRepository => favouriteRepository == null ? new FavouriteRepository(context) : favouriteRepository;

        public IImageRepository ImageRepository => imageRepository == null ? new ImageRepository(context) : imageRepository;

        public INotificationRepository NotificationRepository => notificationRepository == null ? new NotificationRepositroy(context) : notificationRepository;
        
        public INotificationTypeRepository NotificationTypeRepository => notificationTypeRepository == null ? new NotificationTypeRepository(context) : notificationTypeRepository;

        public IPaymentRepository PaymentRepository => paymentRepository == null ? new PaymentRepository(context) : paymentRepository;

        public IPropertyRepository PropertyRepository => propertyRepository == null ? new PropertyRepository(context) : propertyRepository;

        public IPropertyTypeRepository PropertyTypeRepository => propertyTypeRepository == null ? new PropertyTypeRepository(context) : propertyTypeRepository;

        public IRefreshTokenRepository RefreshTokenRepository => refreshTokenRepository == null ? new RefreshTokenRepository(context) : refreshTokenRepository;

        public IReviewRepository ReviewRepository => reviewRepository == null ? new ReviewRepository(context) : reviewRepository;

        public ITicketRepository TicketRepository => ticketRepository == null ? new TicketRepository(context) : ticketRepository;

        public ITransactionRepository TransactionRepository => transactionRepository == null ? new TransactionRepository(context) : transactionRepository;

        public IUserRepository UserRepository => userRepository == null ? new UserRepository(context) : userRepository;
        #endregion

        #region Constructors
        public RepositoryUnitOfWork(MyDbContext context)
        {
            this.context = context;
        }
        #endregion

        #region Methods
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
        #endregion
    }
}
