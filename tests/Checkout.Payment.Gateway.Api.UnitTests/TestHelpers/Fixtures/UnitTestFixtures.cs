namespace Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures
{
    [CollectionDefinition("UnitTestFixtures")]
    public class UnitTestFixtures :
        ICollectionFixture<CreatePaymentRequestFixture>, 
        ICollectionFixture<PaymentDetailsFixture>,
        ICollectionFixture<PaymentProcessResultFixture>,
        ICollectionFixture<AcquiringBankResponseFixture>,
        ICollectionFixture<CreatePaymentResponseFixture>,
        ICollectionFixture<GetPaymentDetailsResponseFixture>
    { }
}
