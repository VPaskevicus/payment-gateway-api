namespace Checkout.Payment.Gateway.Api.Contracts.Responses
{
    public class GetPaymentDetailsResponse
    {
        public Payment? Payment { get; set; }

        public Guid? ShopperId { get; set; }

        public Guid? MerchantId { get; set; }

        public string? Currency { get; set; }

        public decimal? Amount { get; set; }

        public CardDetails? CardDetails { get; set; }
    }
}
