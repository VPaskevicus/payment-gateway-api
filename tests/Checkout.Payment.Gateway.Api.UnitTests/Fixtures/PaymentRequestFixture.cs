using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.UnitTests.Builders;

namespace Checkout.Payment.Gateway.Api.UnitTests.Fixtures
{
    public  class PaymentRequestFixture
    {
        public PaymentRequest BasicPaymentRequest => new PaymentRequestBuilder().Create();
    }
}