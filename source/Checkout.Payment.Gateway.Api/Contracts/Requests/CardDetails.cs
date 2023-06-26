using System.ComponentModel.DataAnnotations;
using Checkout.Payment.Gateway.Api.Validation.Attributes;

namespace Checkout.Payment.Gateway.Api.Contracts.Requests
{
    public class CardDetails
    {
        [Required, StringLength(70, MinimumLength = 5)]
        public string? NameOnCard { get; set; }

        [Required, StringLength(16, MinimumLength = 16)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Card number must be numeric")]
        public string? CardNumber { get; set; }

        [Required, Range(1, 12)]
        public int ExpirationMonth { get; set; }

       
        [Required, ExpirationYear]
        [RegularExpression("^\\d{4}$", ErrorMessage = "Expiration year must be in 4 digits format")]
        public int ExpirationYear { get; set; }

        [Required, Range(100, 999)]
        public int SecurityCode { get; set; }
    }
}
