using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Stripe.Checkout;

namespace Eskon.Service.Interfaces
{
    public interface IStripeService
    {
        public decimal CalculateEskonBookingFees(decimal BookingAmount);

        public string CreateStripeAccountForOwner(User user);

        public string CreateStripeConnectedAccountLinkForOwner(string stripeAccountId, string refreshUrl, string returnUrl);

        public Session CreateStripeCheckoutSession(Booking booking, string successUrl, string cancelUrl);

        public void CreateStripeRefund(string chargeId);
    }
}
