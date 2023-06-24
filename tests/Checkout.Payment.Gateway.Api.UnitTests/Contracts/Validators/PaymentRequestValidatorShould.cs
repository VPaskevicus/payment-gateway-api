using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.UnitTests.Builders;
using Checkout.Payment.Gateway.Api.UnitTests.Fixtures;

namespace Checkout.Payment.Gateway.Api.UnitTests.Contracts.Validators
{
    [Collection("UnitTestFixtures")]
    public  class PaymentRequestValidatorShould
    {
        private readonly PaymentRequest _basicPaymentRequest;

        public PaymentRequestValidatorShould(PaymentRequestFixture paymentRequestFixtures)
        {
            _basicPaymentRequest = paymentRequestFixtures.BasicPaymentRequest;
        }

        [Fact]
        public void ReturnValidWhenModelIsValid()
        {
            var validationResult = ModelValidator.Validate(_basicPaymentRequest);

            validationResult.Should().BeEmpty();
        }

        [Fact]
        public void ReturnInvalidModelWhenPaymentIdIsInvalid()
        {
            var paymentRequest = new PaymentRequestBuilder()
                .WithPaymentId(null)
                .Create();

            var validationResult = ModelValidator.Validate(paymentRequest);

            validationResult.Should().NotBeEmpty();

            validationResult.ElementAt(0).ErrorMessage.Should().Be($"The {nameof(PaymentRequest.PaymentId)} field is required.");
        }

        [Fact]
        public void ReturnInvalidModelWhenShopperIdIsInvalid()
        {
            var paymentRequest = new PaymentRequestBuilder()
                .WithShopperId(null)
                .Create();

            var validationResult = ModelValidator.Validate(paymentRequest);

            validationResult.Should().NotBeEmpty();

            validationResult.ElementAt(0).ErrorMessage.Should().Be($"The {nameof(PaymentRequest.ShopperId)} field is required.");
        }

        [Fact]
        public void ReturnInvalidModelWhenMerchantIdIsInvalid()
        {
            var paymentRequest = new PaymentRequestBuilder()
                .WithMerchantId(null)
                .Create();

            var validationResult = ModelValidator.Validate(paymentRequest);

            validationResult.Should().NotBeEmpty();

            validationResult.ElementAt(0).ErrorMessage.Should().Be($"The {nameof(PaymentRequest.MerchantId)} field is required.");
        }

        [Theory]
        [InlineData("gbpp", "must be a string with a minimum length of 3 and a maximum length of 3")]
        [InlineData("gb", "must be a string with a minimum length of 3 and a maximum length of 3")]
        [InlineData("", "field is required")]
        [InlineData(null, "field is required")]

        public void ReturnInvalidModelWhenCurrencyIsInvalid(string? invalidCurrency, string expectedValidationError)
        {
            var paymentRequest = new PaymentRequestBuilder()
                .WithCurrency(invalidCurrency)
                .Create();

            var validationResult = ModelValidator.Validate(paymentRequest);

            validationResult.Should().NotBeEmpty();

            validationResult.ElementAt(0).ErrorMessage.Should().Contain(expectedValidationError);
        }

        [Fact]

        public void ReturnInvalidModelWhenAmountIsNull()
        {
            var paymentRequest = new PaymentRequestBuilder()
                .WithAmount(null)
                .Create();

            var validationResult = ModelValidator.Validate(paymentRequest);

            validationResult.Should().NotBeEmpty();

            validationResult.ElementAt(0).ErrorMessage.Should().Contain("field is required");
        }

        [Fact]
        public void ReturnInvalidModelWhenShopperCardDetailsIsNull()
        {
            var paymentRequest = new PaymentRequestBuilder()
                .WithCardDetails(null!)
                .Create();

            var validationResult = ModelValidator.Validate(paymentRequest);

            validationResult.Should().NotBeEmpty();

            validationResult.ElementAt(0).ErrorMessage.Should().Contain("field is required");
        }
    }
}
