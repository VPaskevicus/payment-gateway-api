using Checkout.Payment.Gateway.Api.Mappers;
using Checkout.Payment.Gateway.Api.UnitTests.Builders;
using Checkout.Payment.Gateway.Api.UnitTests.Fixtures;

namespace Checkout.Payment.Gateway.Api.UnitTests.Mappers
{
    [Collection("UnitTestFixtures")]
    public class PaymentMapperShould
    {
        private readonly PaymentRequestFixture _paymentRequestFixtures;

        private readonly IPaymentMapper _paymentMapper;

        public PaymentMapperShould(PaymentRequestFixture paymentRequestFixtures)
        {
            _paymentRequestFixtures = paymentRequestFixtures;

            _paymentMapper = new PaymentMapper();
        }

        [Fact]
        public void ReturnMappedPayment()
        {
            var paymentRequest = _paymentRequestFixtures.BasicPaymentRequest;

            var payment = _paymentMapper.MapPaymentRequestToDomainModel(paymentRequest);

            payment.ShopperId.Should().Be(paymentRequest.ShopperId);
            payment.MerchantId.Should().Be(paymentRequest.MerchantId);
            payment.Currency.Should().Be(paymentRequest.Currency);
            payment.Amount.Should().Be(paymentRequest.Amount);
            payment.CardDetails.Should().NotBeNull();
            payment.CardDetails!.NameOnCard.Should().Be(paymentRequest.ShopperCardDetails!.NameOnCard);
            payment.CardDetails.CardNumber.Should().Be(paymentRequest.ShopperCardDetails.CardNumber);
            payment.CardDetails.ExpirationMonth.Should().Be(paymentRequest.ShopperCardDetails.ExpirationMonth);
            payment.CardDetails.ExpirationYear.Should().Be(paymentRequest.ShopperCardDetails.ExpirationYear);
            payment.CardDetails.SecurityCode.Should().Be(paymentRequest.ShopperCardDetails.SecurityCode);
        }

        [Fact]
        public void ReturnMappedPaymentId()
        {
            var paymentRequest = new PaymentRequestBuilder().WithPaymentId(null).Create();

            var payment = _paymentMapper.MapPaymentRequestToDomainModel(paymentRequest);

            payment.PaymentId.Should().Be(Guid.Empty);

        }
    }
}
