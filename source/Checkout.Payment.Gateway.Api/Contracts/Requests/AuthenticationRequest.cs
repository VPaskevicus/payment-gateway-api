using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Gateway.Api.Contracts.Requests
{
    public class AuthenticationRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string? Username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string? Password { get; set; }
    }
}
