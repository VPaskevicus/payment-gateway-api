using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Gateway.Api.UnitTests.Contracts.Validators
{
    public static class ModelValidator
    {
        public static IList<ValidationResult> Validate(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
