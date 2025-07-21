using Eskon.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Infrastructure.UnitOfWork
{
    public interface IRepositoryUnitOfWork
    {
        #region Properties
        public IBookingRepository BookingRepository { get; }
        public IFavouriteRepository FavouriteRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IPropertyRepository PropertyRepository { get; }
        public IPropertyTypeRepository PropertyTypeRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IReviewRepository ReviewRepository { get; }
        public ITicketRepository TicketRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public IUserRepository UserRepository { get; }
        #endregion

        #region Methods
        public Task<int> SaveChangesAsync();
        #endregion
    }
}
