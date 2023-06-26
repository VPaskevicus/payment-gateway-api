namespace Checkout.Payment.Gateway.Api.Contracts.Responses
{
    public class AuthenticateResponse
    {
        public string? User { get; set; }
        public string? StatusCode{ get; set; }
    }
}
