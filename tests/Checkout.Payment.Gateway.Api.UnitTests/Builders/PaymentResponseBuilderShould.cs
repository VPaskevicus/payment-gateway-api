using Checkout.Payment.Gateway.Api.Builders;
using Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures;

namespace Checkout.Payment.Gateway.Api.UnitTests.Builders
{
    [Collection("UnitTestFixtures")]
    public class PaymentResponseBuilderShould
    {
        private readonly PaymentProcessResultFixture _paymentProcessResultFixture;

        public PaymentResponseBuilderShould(PaymentProcessResultFixture paymentProcessResultFixture)
        {
            _paymentProcessResultFixture = paymentProcessResultFixture;
        }

        [Fact]
        public void ReturnCreatePaymentResponse()
        {
            var paymentResponseBuilder = new PaymentResponseBuilder();
            var basicPaymentProcessResult = _paymentProcessResultFixture.BasicPaymentProcessResult;

            var createPaymentResponse = paymentResponseBuilder.BuildCreatePaymentResponse(basicPaymentProcessResult);

            createPaymentResponse.Payment.Should().NotBeNull();
            createPaymentResponse.Payment?.Id.Should().Be(basicPaymentProcessResult.Payment.PaymentId);
            createPaymentResponse.Payment?.StatusCode.Should().Be(basicPaymentProcessResult.PaymentResponse.StatusCode);
        }
    }
}
