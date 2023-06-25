﻿using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Interfaces
{
    public interface IPaymentRepository
    {
        Task<bool> AddPaymentDetailsAsync(Guid paymentId, PaymentDetails paymentDetails);
    }
}
