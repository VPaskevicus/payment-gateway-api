namespace Checkout.Payment.Gateway.Api.Contracts.Responses
{
    public class Payment
    {
        public Guid Id { get; set; }

        public string StatusCode { get; set; } = string.Empty;
    }
}
