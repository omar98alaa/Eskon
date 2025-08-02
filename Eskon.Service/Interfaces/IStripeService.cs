using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IStripeService
    {
        public decimal CalculateEskonBookingFees(decimal BookingAmount);

        public string CreateStripeAccountForOwner(User user);

        public string CreateStripeConnectedAccountLinkForOwner(string stripeAccountId, string refreshUrl, string returnUrl);

        public string CreateStripeCheckoutUrl(Booking booking, string successUrl, string cancelUrl);
    }
}
