using Checkout.Payment.Gateway.Api.Extensions;

namespace Checkout.Payment.Gateway.Api.UnitTests.Extensions
{
    public  class PaymentExtensionsShould
    {

        [Fact]
        public void SetPaymentIdOnThePaymentDomainModel()
        {
            var paymentId = new Guid();
            var payment = new Models.Payment();

            payment.SetPaymentId(paymentId);

            payment.PaymentId.Should().Be(paymentId);
        }
    }
}
