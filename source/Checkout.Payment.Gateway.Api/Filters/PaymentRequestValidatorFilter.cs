using Microsoft.AspNetCore.Mvc.Filters;

namespace Checkout.Payment.Gateway.Api.Filters
{
    public class PaymentRequestValidatorFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
