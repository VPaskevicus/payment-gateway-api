namespace Checkout.Payment.Gateway.Api.Models
{
    public class CardDetails
    {
        public string? NameOnCard { get; set; }
        public string? CardNumber { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int SecurityCode { get; set; }
    }
}
