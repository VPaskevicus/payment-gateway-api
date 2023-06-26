using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Fakes
{
    public class FakeAcquiringBank : IAcquiringBank
    {
        public Task<AcquiringBankResponse> ProcessPaymentAsync(PaymentDetails paymentDetails)
        {
            return Task.FromResult(new AcquiringBankResponse
            {
                PaymentId = Guid.NewGuid(),
                StatusCode = "001"
            });
        }

        public Task<AcquiringBankResponse> GetPaymentStatusAsync(Guid paymentId)
        {
            return Task.FromResult(new AcquiringBankResponse
            {
                PaymentId = paymentId,
                StatusCode = "001"
            });
        }
    }
}
