﻿namespace Checkout.Payment.Gateway.Api.Models
{
    public class PaymentDetails
    {
        public Guid? ShopperId { get; set; }
        public Guid? MerchantId { get; set; }
        public string? Currency { get; set; }
        public decimal? Amount { get; set; }
        public CardDetails? CardDetails { get; set; }
    }
}
