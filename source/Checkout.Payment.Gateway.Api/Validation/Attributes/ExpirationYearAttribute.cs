using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Gateway.Api.Validation.Attributes
{
    public class ExpirationYearAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var expirationYear = Convert.ToInt32(value);

            if (expirationYear > DateTime.Now.Year)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult
                ("The field ExpirationYear must be from the current year onwards.");
        }
    }
}
