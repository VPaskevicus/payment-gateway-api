using Checkout.Payment.Gateway.Api.Builders;
using Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures;

namespace Checkout.Payment.Gateway.Api.UnitTests.Builders
{
    [Collection("UnitTestFixtures")]
    public class ResponseBuilderShould
    {
        private readonly PaymentProcessResultFixture _paymentProcessResultFixture;

        private readonly ResponseBuilder _responseBuilder;

        public ResponseBuilderShould(PaymentProcessResultFixture paymentProcessResultFixture)
        {
            _paymentProcessResultFixture = paymentProcessResultFixture;

            _responseBuilder = new ResponseBuilder();
        }

        [Fact]
        public void ReturnCreatePaymentResponse()
        {
            var basicPaymentDetailsProcessResult = _paymentProcessResultFixture.BasicPaymentDetailsProcessResult;

            var createPaymentResponse = _responseBuilder.BuildCreatePaymentResponse(basicPaymentDetailsProcessResult);

            createPaymentResponse.Payment.Should().NotBeNull();
            createPaymentResponse.Payment?.Id.Should().Be(basicPaymentDetailsProcessResult.AcquiringBankResponse?.PaymentId);
            createPaymentResponse.Payment?.StatusCode.Should().Be(basicPaymentDetailsProcessResult.AcquiringBankResponse?.StatusCode);
        }

        [Fact]
        public void ReturnGetPaymentDetailsResponse()
        {
            var basicPaymentDetailsProcessResult = _paymentProcessResultFixture.BasicPaymentDetailsProcessResult;

            var getPaymentDetailsResponse = _responseBuilder.BuildGetPaymentDetailsResponse(basicPaymentDetailsProcessResult);

            getPaymentDetailsResponse.Payment.Should().NotBeNull();
            getPaymentDetailsResponse.Payment?.Id.Should().Be(basicPaymentDetailsProcessResult.AcquiringBankResponse?.PaymentId);
            getPaymentDetailsResponse.Payment?.StatusCode.Should().Be(basicPaymentDetailsProcessResult.AcquiringBankResponse?.StatusCode);
            getPaymentDetailsResponse.ShopperId.Should().Be(basicPaymentDetailsProcessResult.PaymentDetails?.ShopperId);
            getPaymentDetailsResponse.MerchantId.Should().Be(basicPaymentDetailsProcessResult.PaymentDetails?.MerchantId);
            getPaymentDetailsResponse.Currency.Should().Be(basicPaymentDetailsProcessResult.PaymentDetails?.Currency);
            getPaymentDetailsResponse.Amount.Should().Be(basicPaymentDetailsProcessResult.PaymentDetails?.Amount);
        }
    }
}
