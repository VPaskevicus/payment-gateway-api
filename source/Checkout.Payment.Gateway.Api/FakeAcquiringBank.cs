using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api
{
    public class FakeAcquiringBank : IAcquiringBank
    {
        public Task<PaymentResponse> ProcessPaymentAsync(Models.Payment payment)
        {
            return Task.FromResult(new PaymentResponse
            {
                PaymentId = Guid.NewGuid(),
                StatusCode = "001"
            });
        }
    }
}
