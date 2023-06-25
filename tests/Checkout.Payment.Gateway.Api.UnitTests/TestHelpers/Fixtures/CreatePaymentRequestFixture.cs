using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Builders;

namespace Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures
{
    public  class CreatePaymentRequestFixture
    {
        public CreatePaymentRequest BasicCreatePaymentRequest => new CreatePaymentRequestBuilder().Create();
    }
}