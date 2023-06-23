using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payment.Gateway.Api.Filters
{
    public class PaymentRequestValidatorAttribute : TypeFilterAttribute
    {
        public PaymentRequestValidatorAttribute() : base(typeof(PaymentRequestValidatorFilter))
        {
        }
    }
}
