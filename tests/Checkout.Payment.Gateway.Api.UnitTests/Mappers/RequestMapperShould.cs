using Checkout.Payment.Gateway.Api.Mappers;
using Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures;

namespace Checkout.Payment.Gateway.Api.UnitTests.Mappers
{
    [Collection("UnitTestFixtures")]
    public class RequestMapperShould
    {
        private readonly CreatePaymentRequestFixture _createPaymentRequestFixture;

        private readonly IRequestMapper _requestMapper;

        public RequestMapperShould(CreatePaymentRequestFixture createPaymentRequestFixture)
        {
            _createPaymentRequestFixture = createPaymentRequestFixture;

            _requestMapper = new RequestMapper();
        }

        [Fact]
        public void ReturnMappedPaymentDetails()
        {
            var createPaymentRequest = _createPaymentRequestFixture.BasicCreatePaymentRequest;

            var paymentDetails = _requestMapper.MapToDomainModel(createPaymentRequest);

            paymentDetails.ShopperId.Should().Be(createPaymentRequest.ShopperId);
            paymentDetails.MerchantId.Should().Be(createPaymentRequest.MerchantId);
            paymentDetails.Currency.Should().Be(createPaymentRequest.Currency);
            paymentDetails.Amount.Should().Be(createPaymentRequest.Amount);
            paymentDetails.CardDetails.Should().NotBeNull();
            paymentDetails.CardDetails!.NameOnCard.Should().Be(createPaymentRequest.CardDetails.NameOnCard);
            paymentDetails.CardDetails.CardNumber.Should().Be(createPaymentRequest.CardDetails.CardNumber);
            paymentDetails.CardDetails.ExpirationMonth.Should().Be(createPaymentRequest.CardDetails.ExpirationMonth);
            paymentDetails.CardDetails.ExpirationYear.Should().Be(createPaymentRequest.CardDetails.ExpirationYear);
            paymentDetails.CardDetails.SecurityCode.Should().Be(createPaymentRequest.CardDetails.SecurityCode);
        }
    }
}
