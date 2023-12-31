﻿using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures
{
    public class PaymentProcessResultFixture
    {
        public PaymentDetailsProcessResult BasicPaymentDetailsProcessResult => new(
            new AcquiringBankResponse { PaymentId = new Guid("2aea7cd0-c3ed-4d96-bd34-673f3210c955"), 
                StatusCode = "001" },
            new PaymentDetailsFixture().BasicPaymentDetails);

        public PaymentDetailsProcessResult EmptyPaymentDetailsProcessResult => new(null, null);
    }
}
