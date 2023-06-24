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

            payment.PaymentId.ToString().Should().Be(paymentRequest.PaymentId.ToString());
            payment.ShopperId.Should().Be(paymentRequest.ShopperId);
            payment.MerchantId.Should().Be(paymentRequest.MerchantId);
            payment.Currency.Should().Be(paymentRequest.Currency);
            payment.Amount.Should().Be(paymentRequest.Amount);
            payment.ShopperCardDetails.Should().NotBeNull();
            payment.ShopperCardDetails!.NameOnCard.Should().Be(paymentRequest.ShopperCardDetails!.NameOnCard);
            payment.ShopperCardDetails.CardNumber.Should().Be(paymentRequest.ShopperCardDetails.CardNumber);
            payment.ShopperCardDetails.ExpirationMonth.Should().Be(paymentRequest.ShopperCardDetails.ExpirationMonth);
            payment.ShopperCardDetails.ExpirationYear.Should().Be(paymentRequest.ShopperCardDetails.ExpirationYear);
            payment.ShopperCardDetails.SecurityCode.Should().Be(paymentRequest.ShopperCardDetails.SecurityCode);
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
