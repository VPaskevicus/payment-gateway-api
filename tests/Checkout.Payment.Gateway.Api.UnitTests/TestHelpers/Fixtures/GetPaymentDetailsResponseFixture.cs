using Checkout.Payment.Gateway.Api.Contracts.Responses;

namespace Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures
{
    public  class GetPaymentDetailsResponseFixture
    {
        public GetPaymentDetailsResponse BasicGetPaymentDetailsResponse = new()
        {
            Payment = new Api.Contracts.Responses.Payment
            {
                Id = new Guid("93f5915a-d0bf-4cde-bf53-d0df49ce4437"),
                StatusCode = "001"
            },
            ShopperId = new Guid("683fa16a-d9e8-4347-9d3b-3c3365408a62"),
            MerchantId = new Guid("00e3e886-cdc9-45d1-9dc5-b3cc1c112978"),
            Currency = "gbp",
            Amount = 32.75m,
            CardDetails = new CardDetails
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
