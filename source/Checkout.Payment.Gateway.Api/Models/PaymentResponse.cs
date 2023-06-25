namespace Checkout.Payment.Gateway.Api.Models
{
    public  class PaymentResponse
    {
        public Guid PaymentId { get; set; }

        public string StatusCode { get; set; } = string.Empty;
    }
}
