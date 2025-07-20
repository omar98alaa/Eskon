using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Repositories;
using Eskon.Infrastructure.UnitOfWork;
using Eskon.Service.Interfaces;
using Eskon.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Service.UnitOfWork
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        #region Fields
        private readonly IRepositoryUnitOfWork repositoryUnitOfWork;

        private readonly JwtSettings jwtSettings;

        private IAuthenticationService authenticationService;

        private IBookingService bookingService;

        private IFavouriteService favouriteService;

        private IPaymentService paymentService;

        private IPropertyService propertyService;

        private IPropertyTypeService propertyTypeService;

        private IRefreshTokenService refreshTokenService;

        private IReviewService reviewService;

        private ITicketService ticketService;

        private ITransactionService transactionService;

        private IUserService userService;
        #endregion

        #region Properties
        public IAuthenticationService AuthenticationService => authenticationService == null ? new AuthenticationService(jwtSettings) : authenticationService;

        public IBookingService BookingService => bookingService == null? new BookingService(repositoryUnitOfWork.BookingRepository): bookingService;

        public IFavouriteService FavouriteService => favouriteService == null? new FavouriteService(repositoryUnitOfWork.FavouriteRepository) : favouriteService;

        public IPaymentService PaymentService => paymentService == null? new PaymentService(repositoryUnitOfWork.PaymentRepository) : paymentService;

        public IPropertyService PropertyService => propertyService == null? new PropertyService(repositoryUnitOfWork.PropertyRepository) : propertyService;

        public IPropertyTypeService PropertyTypeService => propertyTypeService == null? new PropertyTypeService(repositoryUnitOfWork.PropertyTypeRepository) : propertyTypeService;

        public IRefreshTokenService RefreshTokenService => refreshTokenService == null? new RefreshTokenService(repositoryUnitOfWork.RefreshTokenRepository) : refreshTokenService;

        public IReviewService ReviewService => reviewService == null? new ReviewService(repositoryUnitOfWork.ReviewRepository) : reviewService;

        public ITicketService TicketService => ticketService == null? new TicketService(repositoryUnitOfWork.TicketRepository) : ticketService;

        public ITransactionService TransactionService => transactionService == null? new TransactionService(repositoryUnitOfWork.TransactionRepository) : transactionService;

        public IUserService UserService => userService == null? new UserService(repositoryUnitOfWork.UserRepository) : userService;
        #endregion

        #region Constructors
        public ServiceUnitOfWork(IRepositoryUnitOfWork repositoryUnitOfWork, JwtSettings jwtSettings)
        {
            this.repositoryUnitOfWork = repositoryUnitOfWork;
            this.jwtSettings = jwtSettings;
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
