using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Stripe;
using Eskon.Infrastructure.UnitOfWork;
using Eskon.Service.Interfaces;
using Eskon.Service.Services;

namespace Eskon.Service.UnitOfWork
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        #region Fields
        private readonly IRepositoryUnitOfWork repositoryUnitOfWork;

        private readonly JwtSettings jwtSettings;

        private readonly StripeSettings stripeSettings;

        private IAuthenticationService authenticationService;

        private IBookingService bookingService;

        private IChatService chatService;

        private IChatMessagesService chatMessagesService;

        private IFavouriteService favouriteService;
        
        private INotificationService notificationService;
        
        private INotificationTypeService notificationTypeService;

        private IPaymentService paymentService;

        private IPropertyService propertyService;

        private IPropertyTypeService propertyTypeService;

        private IRefreshTokenService refreshTokenService;

        private IReviewService reviewService;

        private ITicketService ticketService;

        private IUserService userService;

        private ICityService cityService;

        private ICountryService countryService;

        private IImageService imageService;

        private IFileService fileService;

        private IStripeService stripeService;
        #endregion

        #region Properties
        public IAuthenticationService AuthenticationService => authenticationService == null ? new AuthenticationService(jwtSettings) : authenticationService;

        public IBookingService BookingService => bookingService == null ? new BookingService(repositoryUnitOfWork.BookingRepository) : bookingService;

        public IChatService ChatService => chatService == null ? new ChatService(repositoryUnitOfWork.ChatRepository) : chatService;

        public IChatMessagesService ChatMessagesService => chatMessagesService == null ? new ChatMessageService(repositoryUnitOfWork.ChatMessageRepository) : chatMessagesService;

        public IFavouriteService FavouriteService => favouriteService == null ? new FavouriteService(repositoryUnitOfWork.FavouriteRepository) : favouriteService;
        
        public INotificationService NotificationService => notificationService == null ? new NotificationService(repositoryUnitOfWork.NotificationRepository) : notificationService;
        
        public INotificationTypeService NotificationTypeService => notificationTypeService == null? new NotificationTypeService(repositoryUnitOfWork.NotificationTypeRepository) : notificationTypeService;

        public IPaymentService PaymentService => paymentService == null ? new PaymentService(repositoryUnitOfWork.PaymentRepository) : paymentService;

        public IPropertyService PropertyService => propertyService == null ? new PropertyService(repositoryUnitOfWork.PropertyRepository) : propertyService;

        public IPropertyTypeService PropertyTypeService => propertyTypeService == null ? new PropertyTypeService(repositoryUnitOfWork.PropertyTypeRepository) : propertyTypeService;

        public IRefreshTokenService RefreshTokenService => refreshTokenService == null ? new RefreshTokenService(repositoryUnitOfWork.RefreshTokenRepository) : refreshTokenService;

        public IReviewService ReviewService => reviewService == null ? new ReviewService(repositoryUnitOfWork.ReviewRepository) : reviewService;

        public ITicketService TicketService => ticketService == null ? new TicketService(repositoryUnitOfWork.TicketRepository) : ticketService;

        public IUserService UserService => userService == null? new UserService(repositoryUnitOfWork.UserRepository) : userService;

        public ICityService CityService => cityService == null ? new CityService(repositoryUnitOfWork.CityRepository) : cityService;

        public ICountryService CountryService => countryService == null ? new CountryService(repositoryUnitOfWork.CountryRepository) : countryService;

        public IStripeService StripeService => stripeService == null ? new StripeService(stripeSettings) : stripeService;

        #endregion

        #region Constructors
        public ServiceUnitOfWork(IRepositoryUnitOfWork repositoryUnitOfWork, JwtSettings jwtSettings, StripeSettings stripeSettings)
        {
            this.repositoryUnitOfWork = repositoryUnitOfWork;
            this.jwtSettings = jwtSettings;
            this.stripeSettings = stripeSettings;
        }
        #endregion

        #region Methods
        public async Task<int> SaveChangesAsync()
        {
            return await repositoryUnitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}
