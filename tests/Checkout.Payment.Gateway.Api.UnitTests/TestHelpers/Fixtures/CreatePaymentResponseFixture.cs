using Checkout.Payment.Gateway.Api.Contracts.Responses;

namespace Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures
{
    public  class CreatePaymentResponseFixture
    {
        public CreatePaymentResponse BasicCreatePaymentResponse => new()
        {
            Payment = new Api.Contracts.Responses.Payment()
            {
                Id = new Guid("01f887cd-ba91-475f-9f16-b7c22ad0ebac"),
                StatusCode = "001"
            }
        };
    }
}