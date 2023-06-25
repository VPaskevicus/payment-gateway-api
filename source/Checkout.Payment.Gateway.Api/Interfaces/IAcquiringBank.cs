using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Interfaces
{
    public interface IAcquiringBank
    {
        Task<PaymentResponse> ProcessPaymentAsync(Models.Payment payment);
    }
}