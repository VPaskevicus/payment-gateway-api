using Checkout.Payment.Gateway.Api.Contracts.Responses;
using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Builders
{
    public interface IPaymentResponseBuilder
    {
        CreatePaymentResponse BuildCreatePaymentResponse(PaymentProcessResult paymentProcessResult);
    }

    public class PaymentResponseBuilder : IPaymentResponseBuilder
    {
        public CreatePaymentResponse BuildCreatePaymentResponse(PaymentProcessResult paymentProcessResult)
        {
            return new CreatePaymentResponse
            {
                Payment = new Contracts.Responses.Payment
                {
                    Id = paymentProcessResult.Payment.PaymentId,
                    StatusCode = paymentProcessResult.PaymentResponse.StatusCode
                }
            };
        }
    }
}
