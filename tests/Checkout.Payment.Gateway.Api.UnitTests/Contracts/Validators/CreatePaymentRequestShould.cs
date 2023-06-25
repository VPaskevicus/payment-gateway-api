using Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Builders;
using Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures;

namespace Checkout.Payment.Gateway.Api.UnitTests.Contracts.Validators
{
    [Collection("UnitTestFixtures")]
    public  class CreatePaymentRequestShould
    {
        private readonly CreatePaymentRequestFixture _createPaymentRequestFixture;

        public CreatePaymentRequestShould(CreatePaymentRequestFixture createPaymentRequestFixture)
        {
            _createPaymentRequestFixture = createPaymentRequestFixture;
        }

        [Fact]
        public void ReturnValidResultWhenModelIsValid()
        {
            var validationResult = ModelValidator.Validate(_createPaymentRequestFixture.BasicCreatePaymentRequest);
            
            validationResult.Should().BeEmpty();
        }

        [Fact]
        public void ReturnInvalidResultWhenShopperIdIsInvalid()
        {
            var createPaymentRequest = new CreatePaymentRequestBuilder()
                .WithShopperId(null)
                .Create();

            var validationResult = ModelValidator.Validate(createPaymentRequest);

            validationResult.Should().NotBeEmpty();
            validationResult.ElementAt(0).ErrorMessage.Should().Contain("field is required");
        }

        [Fact]
        public void ReturnInvalidResultWhenMerchantIdIsInvalid()
        {
            var createPaymentRequest = new CreatePaymentRequestBuilder()
                .WithMerchantId(null)
                .Create();

            var validationResult = ModelValidator.Validate(createPaymentRequest);

            validationResult.Should().NotBeEmpty();
            validationResult.ElementAt(0).ErrorMessage.Should().Contain("field is required");
        }

        [Theory]
        [InlineData("gbpp", "must be a string with a minimum length of 3 and a maximum length of 3")]
        [InlineData("gb", "must be a string with a minimum length of 3 and a maximum length of 3")]
        [InlineData("", "field is required")]
        [InlineData(null, "field is required")]

        public void ReturnInvalidResultWhenCurrencyIsInvalid(string? invalidCurrency, string expectedValidationError)
        {
            var createPaymentRequest = new CreatePaymentRequestBuilder()
                .WithCurrency(invalidCurrency)
                .Create();

            var validationResult = ModelValidator.Validate(createPaymentRequest);

            validationResult.Should().NotBeEmpty();
            validationResult.ElementAt(0).ErrorMessage.Should().Contain(expectedValidationError);
        }

        [Fact]

        public void ReturnInvalidResultWhenAmountIsNull()
        {
            var createPaymentRequest = new CreatePaymentRequestBuilder()
                .WithAmount(null)
                .Create();

            var validationResult = ModelValidator.Validate(createPaymentRequest);

            validationResult.Should().NotBeEmpty();
            validationResult.ElementAt(0).ErrorMessage.Should().Contain("field is required");
        }

        [Fact]
        public void ReturnInvalidResultWhenShopperCardDetailsIsNull()
        {
            var createPaymentRequest = new CreatePaymentRequestBuilder()
                .WithCardDetails(null!)
                .Create();

            var validationResult = ModelValidator.Validate(createPaymentRequest);

            validationResult.Should().NotBeEmpty();
            validationResult.ElementAt(0).ErrorMessage.Should().Contain("field is required");
        }
    }
}
