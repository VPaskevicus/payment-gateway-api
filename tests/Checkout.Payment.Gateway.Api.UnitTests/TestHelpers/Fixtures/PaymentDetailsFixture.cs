using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures
{
    public class PaymentDetailsFixture
    {
        public PaymentDetails BasicPaymentDetails => new()
        {
            ShopperId = new Guid("83d29ff5-0735-4428-9a02-67d83f4599c8"),
            MerchantId = new Guid("b92a095e-a730-49c5-a2a9-1f1e5377355f"),
            Currency = "gbp",
            Amount = 156.60m,
            CardDetails = new CardDetails()
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