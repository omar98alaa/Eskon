using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Domian.Stripe;
using Eskon.Service.Interfaces;
using Stripe;
using Stripe.Checkout;

namespace Eskon.Service.Services
{
    public class StripeService : IStripeService
    {
        #region Fields
        private readonly StripeSettings _stripeSettings;
        #endregion

        #region Constructors
        public StripeService(StripeSettings stripeSettings)
        {
            _stripeSettings = stripeSettings;
        }
        #endregion

        public decimal CalculateEskonBookingFees(decimal BookingAmount)
        {
            return BookingAmount * 0.1m;
        }

        #region Stripe Accounts
        public string CreateStripeAccountForOwner(User user)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var options = new AccountCreateOptions
            {
                Email = user.Email,
                Controller = new AccountControllerOptions
                {
                    Losses = new AccountControllerLossesOptions { Payments = "application" }, //application, stripe
                    Fees = new AccountControllerFeesOptions { Payer = "application" }, //application, account
                    StripeDashboard = new AccountControllerStripeDashboardOptions
                    {
                        Type = "express",
                    },
                },
            };
            var service = new AccountService();

            // ToDo : Check account
            Account account = service.Create(options);

            return account.Id;
        }

        public string CreateStripeConnectedAccountLinkForOwner(string stripeAccountId, string refreshUrl, string returnUrl)
        {
            var accountURLOptions = new AccountLinkCreateOptions
            {
                Account = stripeAccountId,
                RefreshUrl = refreshUrl,
                ReturnUrl = returnUrl,
                Type = "account_onboarding",
            };
            var accountLinkService = new AccountLinkService();

            // ToDo : Check account link
            AccountLink accountLink = accountLinkService.Create(accountURLOptions);

            return accountLink.Url;
        }
        #endregion

        #region Stripe Checkout
        public Session CreateStripeCheckoutSession(Booking booking, string successUrl, string cancelUrl)
        {
            try
            {
                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

                var options = new SessionCreateOptions
                {
                    Mode = "payment",
                    SuccessUrl = successUrl,
                    CancelUrl = cancelUrl,
                    LineItems = new List<SessionLineItemOptions>

                {
                    new SessionLineItemOptions
                    {

                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Reservation for {booking.Property.Title} from {booking.StartDate} to {booking.EndDate}",
                            },
                            UnitAmount = (long)booking.TotalPrice * 100,
                        },
                        Quantity = 1,
                    },
                },

                    PaymentIntentData = new SessionPaymentIntentDataOptions
                    {
                        ApplicationFeeAmount = (long)CalculateEskonBookingFees(booking.TotalPrice) * 100,
                        TransferData = new SessionPaymentIntentDataTransferDataOptions
                        {
                            Destination = booking.Property.Owner.stripeAccountId,
                        },
                        Metadata = new Dictionary<string, string>
                        {
                           { "BookingId", booking.Id.ToString() }
                        }
                    }
                };

                var service = new SessionService();
                Session session = service.Create(options);
                return session;
            }
            catch (StripeException ex)
            {
                throw new ApplicationException($"Stripe error occurred: {ex.StripeError?.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the Stripe Checkout session.", ex);
            }
        }
        #endregion

        #region Stripe Refund

        public void CreateStripeRefund(string chargeId)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;


            try
            {
                var options = new RefundCreateOptions
                {
                    Charge = chargeId,
                    RefundApplicationFee = true,
                    ReverseTransfer = true,
                };

                var service = new RefundService();
                var refund = service.Create(options); //pending, requires_action, succeeded, failed or canceled
            }
            catch (StripeException ex)
            {
                throw new ApplicationException($"Stripe error occurred: {ex.StripeError?.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the Stripe Checkout session.", ex);
            }
        }

        #endregion
    }
}
