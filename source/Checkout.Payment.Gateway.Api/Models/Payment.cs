namespace Checkout.Payment.Gateway.Api.Models
{
    public class Payment
    {
        public Guid PaymentId => Guid.NewGuid();
        public Guid ShopperId { get; set; }
        public Guid MerchantId { get; set; }
        public string? Currency { get; set; }
        public decimal Amount { get; set; }
        public CardDetails? ShopperCardDetails { get; set; }

    }
}
