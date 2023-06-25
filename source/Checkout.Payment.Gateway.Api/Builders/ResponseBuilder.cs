using Checkout.Payment.Gateway.Api.Contracts.Responses;
using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Builders
{
    public interface IResponseBuilder
    {
        CreatePaymentResponse BuildResponse(PaymentDetailsProcessResult paymentDetailsProcessResult);
    }

    public class ResponseBuilder : IResponseBuilder
    {
        public CreatePaymentResponse BuildResponse(PaymentDetailsProcessResult paymentDetailsProcessResult)
        {
            return new CreatePaymentResponse
            {
                Payment = new Contracts.Responses.Payment
                {
                    Id = paymentDetailsProcessResult.AcquiringBankResponse.PaymentId,
                    StatusCode = paymentDetailsProcessResult.AcquiringBankResponse.StatusCode
                }
            };
        }
    }
}
