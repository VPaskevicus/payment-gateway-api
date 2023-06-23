using Checkout.Payment.Gateway.Api.Contracts;
using Checkout.Payment.Gateway.Api.Contracts.Requests;

namespace Checkout.Payment.Gateway.Api.UnitTests.Fixtures
{
    public  class PaymentRequestFixture
    {
        public PaymentRequest BasicPaymentRequest => new PaymentRequest()
        {
            ShopperId = Guid.NewGuid(),
            MerchantId = Guid.NewGuid(),
            Currency = "gbp",
            Amount = 156.60m,
            ShopperCardDetails = new CardDetails()
            {
                NameOnCard = "Vladimirs Paskevicus",
                CardNumber = "1243123412341234",
                ExpirationMonth = 3,
                ExpirationYear = 2027,
                SecurityCode = 555
            }
        };
    }
}