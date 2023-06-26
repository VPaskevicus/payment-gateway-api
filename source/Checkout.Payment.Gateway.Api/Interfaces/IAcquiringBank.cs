using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Interfaces
{
    public interface IAcquiringBank
    {
        Task<AcquiringBankResponse> ProcessPaymentAsync(PaymentDetails paymentDetails);
        
        Task<AcquiringBankResponse> GetPaymentStatus(Guid paymentId);
    }
}