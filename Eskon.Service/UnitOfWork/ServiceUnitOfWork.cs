using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Stripe;
using Eskon.Infrastructure.UnitOfWork;
using Eskon.Service.Interfaces;
using Eskon.Service.Services;
using Microsoft.AspNetCore.Hosting;

namespace Eskon.Service.UnitOfWork
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        #region Fields
        private readonly IRepositoryUnitOfWork repositoryUnitOfWork;

        private readonly JwtSettings jwtSettings;
        
        private readonly StripeSettings stripeSettings;
        
        private readonly IWebHostEnvironment env;

        private IAuthenticationService authenticationService;

        private IBookingService bookingService;

        private IChatService chatService;

        private IChatMessagesService chatMessagesService;

        private ICityService cityService;

        private ICountryService countryService;

        private IFavouriteService favouriteService;

        private IFileService fileService;

        private IImageService imageService;

        private INotificationService notificationService;
        
        private INotificationTypeService notificationTypeService;

        private IPaymentService paymentService;

        private IPropertyService propertyService;

        private IPropertyTypeService propertyTypeService;

        private IRefreshTokenService refreshTokenService;

        private IReviewService reviewService;
        
        private IStripeService stripeService;

        private ITicketService ticketService;

        private IUserService userService;
        #endregion

        #region Properties
        public IAuthenticationService AuthenticationService => authenticationService == null ? new AuthenticationService(jwtSettings) : authenticationService;

        public IBookingService BookingService => bookingService == null ? new BookingService(repositoryUnitOfWork.BookingRepository) : bookingService;

        public IChatService ChatService => chatService == null ? new ChatService(repositoryUnitOfWork.ChatRepository) : chatService;

        public IChatMessagesService ChatMessagesService => chatMessagesService == null ? new ChatMessageService(repositoryUnitOfWork.ChatMessageRepository) : chatMessagesService;

        public ICityService CityService => cityService == null ? new CityService(repositoryUnitOfWork.CityRepository) : cityService;

        public ICountryService CountryService => countryService == null ? new CountryService(repositoryUnitOfWork.CountryRepository) : countryService;

        public IFavouriteService FavouriteService => favouriteService == null ? new FavouriteService(repositoryUnitOfWork.FavouriteRepository) : favouriteService;

        public IFileService FileService => fileService == null ? new FileService(env) : fileService;

        public IImageService ImageService => imageService == null ? new ImageService(repositoryUnitOfWork.ImageRepository) : imageService;

        public INotificationService NotificationService => notificationService == null ? new NotificationService(repositoryUnitOfWork.NotificationRepository) : notificationService;
        
        public INotificationTypeService NotificationTypeService => notificationTypeService == null? new NotificationTypeService(repositoryUnitOfWork.NotificationTypeRepository) : notificationTypeService;

        public IPaymentService PaymentService => paymentService == null ? new PaymentService(repositoryUnitOfWork.PaymentRepository) : paymentService;

        public IPropertyService PropertyService => propertyService == null ? new PropertyService(repositoryUnitOfWork.PropertyRepository) : propertyService;

        public IPropertyTypeService PropertyTypeService => propertyTypeService == null ? new PropertyTypeService(repositoryUnitOfWork.PropertyTypeRepository) : propertyTypeService;

        public IRefreshTokenService RefreshTokenService => refreshTokenService == null ? new RefreshTokenService(repositoryUnitOfWork.RefreshTokenRepository) : refreshTokenService;

        public IReviewService ReviewService => reviewService == null ? new ReviewService(repositoryUnitOfWork.ReviewRepository) : reviewService;

        public IStripeService StripeService => stripeService == null ? new StripeService(stripeSettings) : stripeService;
        
        public ITicketService TicketService => ticketService == null ? new TicketService(repositoryUnitOfWork.TicketRepository) : ticketService;

        public IUserService UserService => userService == null? new UserService(repositoryUnitOfWork.UserRepository) : userService;
        #endregion


        #region Constructors
        public ServiceUnitOfWork(IRepositoryUnitOfWork repositoryUnitOfWork, JwtSettings jwtSettings, StripeSettings stripeSettings, IWebHostEnvironment env)
        {
            this.repositoryUnitOfWork = repositoryUnitOfWork;
            this.jwtSettings = jwtSettings;
            this.stripeSettings = stripeSettings;
            this.env = env;
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
