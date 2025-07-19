using Microsoft.EntityFrameworkCore;
using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Eskon.Domian.Models;

namespace Eskon.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepositoryAsync<Payment>, IPaymentRepository
    {
        #region Fields
        private readonly DbSet<Payment> _paymentDbSet;
        #endregion

        #region Constructors
        public PaymentRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _paymentDbSet = myDbContext.Set<Payment>();
        }
        #endregion

        #region Methods

        #endregion
    }
}
