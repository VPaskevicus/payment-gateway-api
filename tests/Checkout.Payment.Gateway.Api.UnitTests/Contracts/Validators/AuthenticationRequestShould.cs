using Checkout.Payment.Gateway.Api.Contracts.Requests;

namespace Checkout.Payment.Gateway.Api.UnitTests.Contracts.Validators
{
    [Collection("UnitTestFixtures")]
    public class AuthenticationRequestShould
    {
        [Fact]
        public void ReturnValidResultWhenModelIsValid()
        {
            var validationResult = ModelValidator.Validate(new AuthenticationRequest()
            {
                Username = "vpaskevicus", 
                Password = "123456"
            });

            validationResult.Should().BeEmpty();
        }

        [Theory]
        [InlineData("vpask", "must be a string with a minimum length of 6 and a maximum length of 30")]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit biamda.", "must be a string with a minimum length of 6 and a maximum length of 30")]
        public void ReturnInvalidResultWhenUsernameIsInvalid(string invalidUsername, string expectedValidationError)
        {
            var authenticationRequest = new AuthenticationRequest
            {
                Username = invalidUsername,
                Password = "123456"
            };

            var validationResult = ModelValidator.Validate(authenticationRequest);

            validationResult.Should().NotBeEmpty();
            validationResult.ElementAt(0).ErrorMessage.Should().Contain(expectedValidationError);
        }

        [Theory]
        [InlineData("12345", "must be a string with a minimum length of 6 and a maximum length of 30")]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit biamda.", "must be a string with a minimum length of 6 and a maximum length of 30")]
        public void ReturnInvalidResultWhenPasswordIsInvalid(string invalidPassword, string expectedValidationError)
        {
            var authenticationRequest = new AuthenticationRequest
            {
                Username = invalidPassword,
                Password = "123456"
            };

            var validationResult = ModelValidator.Validate(authenticationRequest);

            validationResult.Should().NotBeEmpty();
            validationResult.ElementAt(0).ErrorMessage.Should().Contain(expectedValidationError);
        }
    }
}
