
namespace Eskon.Service.Interfaces
{
    public interface IStripePaymentService
    {
        Task<string> CreatePaymentIntentAsync(decimal amount, string currency = "USD");
    }
}
