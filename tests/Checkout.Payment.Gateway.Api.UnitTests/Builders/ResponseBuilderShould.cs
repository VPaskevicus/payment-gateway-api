using Checkout.Payment.Gateway.Api.Builders;
using Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures;

namespace Checkout.Payment.Gateway.Api.UnitTests.Builders
{
    [Collection("UnitTestFixtures")]
    public class ResponseBuilderShould
    {
        private readonly PaymentProcessResultFixture _paymentProcessResultFixture;

        public ResponseBuilderShould(PaymentProcessResultFixture paymentProcessResultFixture)
        {
            _paymentProcessResultFixture = paymentProcessResultFixture;
        }

        [Fact]
        public void ReturnCreatePaymentResponse()
        {
            var paymentResponseBuilder = new ResponseBuilder();
            var basicPaymentProcessResult = _paymentProcessResultFixture.BasicPaymentDetailsProcessResult;

            var createPaymentResponse = paymentResponseBuilder.BuildResponse(basicPaymentProcessResult);

            createPaymentResponse.Payment.Should().NotBeNull();
            createPaymentResponse.Payment?.Id.Should().Be(basicPaymentProcessResult.AcquiringBankResponse.PaymentId);
            createPaymentResponse.Payment?.StatusCode.Should().Be(basicPaymentProcessResult.AcquiringBankResponse.StatusCode);
        }
    }
}
