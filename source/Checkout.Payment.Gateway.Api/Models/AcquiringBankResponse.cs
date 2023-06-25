namespace Checkout.Payment.Gateway.Api.Models
{
    public  class AcquiringBankResponse
    {
        public Guid PaymentId { get; set; }

        public string StatusCode { get; set; } = string.Empty;
    }
}
