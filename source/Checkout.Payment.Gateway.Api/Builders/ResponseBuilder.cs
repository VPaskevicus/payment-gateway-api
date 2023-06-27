using Checkout.Payment.Gateway.Api.Contracts.Responses;
using Checkout.Payment.Gateway.Api.Extensions;
using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Builders
{
    public interface IResponseBuilder
    {
        CreatePaymentResponse BuildCreatePaymentResponse(PaymentDetailsProcessResult paymentDetailsProcessResult);
        GetPaymentDetailsResponse BuildGetPaymentDetailsResponse(PaymentDetailsProcessResult paymentDetailsProcessResult);
    }

    public class ResponseBuilder : IResponseBuilder
    {
        public CreatePaymentResponse BuildCreatePaymentResponse(PaymentDetailsProcessResult paymentDetailsProcessResult)
        {
            return new CreatePaymentResponse
            {
                Payment = new Contracts.Responses.Payment
                {
                    Id = paymentDetailsProcessResult.AcquiringBankResponse?.PaymentId,
                    StatusCode = paymentDetailsProcessResult.AcquiringBankResponse?.StatusCode
                }
            };
        }

        public GetPaymentDetailsResponse BuildGetPaymentDetailsResponse(PaymentDetailsProcessResult paymentDetailsProcessResult)
        {
            return new GetPaymentDetailsResponse
            {
                Payment = new Contracts.Responses.Payment
                {
                    Id = paymentDetailsProcessResult.AcquiringBankResponse?.PaymentId,
                    StatusCode = paymentDetailsProcessResult.AcquiringBankResponse?.StatusCode
                },
                ShopperId = paymentDetailsProcessResult.PaymentDetails?.ShopperId,
                MerchantId = paymentDetailsProcessResult.PaymentDetails?.MerchantId,
                Currency = paymentDetailsProcessResult.PaymentDetails?.Currency,
                Amount = paymentDetailsProcessResult.PaymentDetails?.Amount,
                CardDetails = new Contracts.Responses.CardDetails
                {
                    NameOnCard = paymentDetailsProcessResult.PaymentDetails?.CardDetails?.NameOnCard,
                    CardNumber = paymentDetailsProcessResult.PaymentDetails?.CardDetails?.CardNumber.Mask(),
                    ExpirationMonth = paymentDetailsProcessResult.PaymentDetails?.CardDetails?.ExpirationMonth,
                    ExpirationYear = paymentDetailsProcessResult.PaymentDetails?.CardDetails?.ExpirationYear,
                    SecurityCode = paymentDetailsProcessResult.PaymentDetails?.CardDetails?.SecurityCode
                }
            };
        }
    }
}
