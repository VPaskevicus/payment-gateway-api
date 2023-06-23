namespace Checkout.Payment.Gateway.Api.Contracts.Requests
{
    public class PaymentRequest
    {
        public Guid ShopperId { get; set; }
        public Guid MerchantId { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public CardDetails ShopperCardDetails { get; set; }
    }
}
