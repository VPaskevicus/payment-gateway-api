namespace Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures
{
    [CollectionDefinition("UnitTestFixtures")]
    public class UnitTestFixtures :
        ICollectionFixture<PaymentRequestFixture>, 
        ICollectionFixture<PaymentFixture>,
        ICollectionFixture<PaymentProcessResultFixture>
    { }
}
