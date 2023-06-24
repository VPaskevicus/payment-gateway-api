using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.UnitTests.Builders;
using Checkout.Payment.Gateway.Api.UnitTests.Fixtures;

namespace Checkout.Payment.Gateway.Api.UnitTests.Contracts.Validators
{
    [Collection("UnitTestFixtures")]
    public class CardDetailsValidatorShould
    {
        private readonly PaymentRequest _basicPaymentRequest;

        public CardDetailsValidatorShould(PaymentRequestFixture paymentRequestFixtures)
        {
            _basicPaymentRequest = paymentRequestFixtures.BasicPaymentRequest;
        }

        [Fact]
        public void ReturnValidWhenModelIsValid()
        {
            var validationResult = ModelValidator.Validate(_basicPaymentRequest.ShopperCardDetails);

            validationResult.Should().BeEmpty();
        }

        [Theory]
        [InlineData("four", "must be a string with a minimum length of 5 and a maximum length of 70")]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod",
            "must be a string with a minimum length of 5 and a maximum length of 70")]
        [InlineData("", "field is required")]
        [InlineData(null, "field is required")]
        public void ReturnInvalidModelWhenNameOnCardIsInvalid(string? invalidNameOnCard, string expectedValidationError)
        {
            var cardDetails = new CardDetailsBuilder()
                .WithNameOnCard(invalidNameOnCard)
                .Create();

            var validationResult = ModelValidator.Validate(cardDetails);

            validationResult.Should().NotBeEmpty();

            validationResult.ElementAt(0).ErrorMessage.Should().Contain(expectedValidationError);
        }

        [Theory]
        [InlineData("12341234", "must be a string with a minimum length of 16 and a maximum length of 16")]
        [InlineData("Lorem ipsum dolor", "must be a string with a minimum length of 16 and a maximum length of 16")]
        [InlineData("", "field is required")]
        [InlineData(null, "field is required")]
        public void ReturnInvalidModelWhenCardNumberIsInvalid(string? invalidCardNumber, string expectedValidationError)
        {
            var cardDetails = new CardDetailsBuilder()
                .WithCardNumber(invalidCardNumber)
                .Create();

            var validationResult = ModelValidator.Validate(cardDetails);

            validationResult.Should().NotBeEmpty();

            validationResult.ElementAt(0).ErrorMessage.Should().Contain(expectedValidationError);
        }

        [Theory]
        [InlineData(0, "must be between 1 and 12")]
        [InlineData(13, "must be between 1 and 12")]
        [InlineData(-1, "must be between 1 and 12")]
        public void ReturnInvalidModelWhenExpirationMonthIsInvalid(int invalidExpirationMonth,
            string expectedValidationError)
        {
            var cardDetails = new CardDetailsBuilder()
                .WithExpirationMonth(invalidExpirationMonth)
                .Create();

            var validationResult = ModelValidator.Validate(cardDetails);

            validationResult.Should().NotBeEmpty();

            validationResult.ElementAt(0).ErrorMessage.Should().Contain(expectedValidationError);
        }


        [Theory]
        [InlineData(123, "must be from the current year onwards", 2)]
        [InlineData(1235, "must be from the current year onwards", 1)]
        [InlineData(2000, "must be from the current year onwards", 1)]
        public void ReturnInvalidModelWhenExpirationYearIsInvalid(int invalidExpirationYear,
            string expectedValidationError, int expectedErrorsCount)
        {
            var cardDetails = new CardDetailsBuilder()
                .WithExpirationYear(invalidExpirationYear)
                .Create();

            var validationResult = ModelValidator.Validate(cardDetails);

            validationResult.Should().NotBeEmpty();
            
            validationResult.Count.Should().Be(expectedErrorsCount);

            validationResult.ElementAt(0).ErrorMessage.Should().Contain(expectedValidationError);
        }

        [Theory]
        [InlineData(99, "must be between 100 and 999")]
        [InlineData(1000, "must be between 100 and 999")]
        public void ReturnInvalidModelWhenSecurityCodeIsInvalid(int invalidSecurityCode,
            string expectedValidationError)
        {
            var cardDetails = new CardDetailsBuilder()
                .WithSecurityCode(invalidSecurityCode)
                .Create();

            var validationResult = ModelValidator.Validate(cardDetails);

            validationResult.Should().NotBeEmpty();

            validationResult.ElementAt(0).ErrorMessage.Should().Contain(expectedValidationError);
        }
    }
}
