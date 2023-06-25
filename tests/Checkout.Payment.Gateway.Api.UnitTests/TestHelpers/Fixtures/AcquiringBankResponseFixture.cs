using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures
{
    public  class AcquiringBankResponseFixture
    {
        public AcquiringBankResponse BasicAcquiringBankResponse = new()
        {
            PaymentId = new Guid("bd97d5db-e8f4-4f13-9ad0-2f1d5cb431ff"),
            StatusCode = "001"
        };
    }
}
