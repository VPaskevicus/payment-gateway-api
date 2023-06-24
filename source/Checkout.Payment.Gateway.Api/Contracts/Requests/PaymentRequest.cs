﻿using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Gateway.Api.Contracts.Requests
{
    public class PaymentRequest
    {
        [Required]
        public Guid? PaymentId { get; set; }

        [Required]
        public Guid? ShopperId { get; set; }

        [Required]
        public Guid? MerchantId { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]

        public string? Currency { get; set; }

        [Required]
        public decimal? Amount { get; set; }

        [Required]
        public CardDetails ShopperCardDetails { get; set; } = new();
    }
}
