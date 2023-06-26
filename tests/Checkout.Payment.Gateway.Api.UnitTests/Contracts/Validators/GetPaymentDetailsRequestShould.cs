using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures;

namespace Checkout.Payment.Gateway.Api.UnitTests.Contracts.Validators
{
    public class GetPaymentDetailsRequestShould
    {
        [Fact]
        public void ReturnValidResultWhenModelIsValid()
        {
            var validationResult = ModelValidator.Validate(new GetPaymentDetailsRequest{PaymentId = Guid.NewGuid()});

            validationResult.Should().BeEmpty();
        }

        [Fact]
        public void ReturnInvalidResultWhenPaymentIdIsInvalid()
        {
            var validationResult = ModelValidator.Validate(new GetPaymentDetailsRequest { PaymentId = null });

            validationResult.Should().NotBeEmpty();
            validationResult.ElementAt(0).ErrorMessage.Should().Contain("field is required");
        }
    }
}
