using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Interfaces
{
    public interface IPaymentDetailsRepository
    {
        Task<bool> AddPaymentDetailsAsync(Guid paymentId, PaymentDetails paymentDetails);
        
        Task<PaymentDetails?> GetPaymentDetailsAsync(Guid? paymentId);
    }
}
