using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(Models.Payment payment);
        Models.Payment GetPayment(Guid paymentId);

    }
}
