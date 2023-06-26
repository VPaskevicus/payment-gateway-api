namespace Checkout.Payment.Gateway.Api.Contracts.Requests
{
    public class RegistrationRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
