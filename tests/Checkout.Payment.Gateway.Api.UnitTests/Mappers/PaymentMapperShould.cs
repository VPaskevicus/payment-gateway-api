using Checkout.Payment.Gateway.Api.UnitTests.Fixtures;

namespace Checkout.Payment.Gateway.Api.UnitTests.Mappers
{
    [Collection("UnitTestFixtures")]
    public  class PaymentMapperShould
    {
        private readonly PaymentRequestFixture _paymentRequestFixtures;

        public PaymentMapperShould(PaymentRequestFixture paymentRequestFixtures)
        {
            _paymentRequestFixtures = paymentRequestFixtures;
        }

        [Fact]
        public void RetrunMappedPayment()
        {
            var paymentMapper = new PaymentMapper();

            var paymentRequest = _paymentRequestFixtures.BasicPaymentRequest;

            Models.Payment payment = paymentMapper.MapPaymentRequestToDomainModel(paymentRequest);

            payment.PaynmentId.Should().NotBeEmpty();
            payment.ShopperId.Should().Be(paymentRequest.ShopperId);
            payment.MerchantId.Should().Be(paymentRequest.MerchantId);
            payment.Currency.Should().Be(paymentRequest.Currency);
            payment.Amount.Should().Be(paymentRequest.Amount);
            payment.ShopperCardDetails.Should().NotBeNull();
            payment.ShopperCardDetails.NameOnCard.Should().Be(paymentRequest.ShopperCardDetails.NameOnCard);
            payment.ShopperCardDetails.CardNumber.Should().Be(paymentRequest.ShopperCardDetails.NameOnCard);
            payment.ShopperCardDetails.ExpirationMonth.Should().Be(paymentRequest.ShopperCardDetails.ExpirationMonth);
            payment.ShopperCardDetails.ExpirationYear.Should().Be(paymentRequest.ShopperCardDetails.ExpirationYear);
            payment.ShopperCardDetails.SecurityCode.Should().Be(paymentRequest.ShopperCardDetails.SecurityCode);

        }
    }
}
