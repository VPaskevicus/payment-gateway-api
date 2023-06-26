using Checkout.Payment.Gateway.Api.Contracts.Requests;

namespace Checkout.Payment.Gateway.Api.Mappers
{
    public interface IRequestMapper
    {
        Models.PaymentDetails MapToDomainModel(CreatePaymentRequest createPaymentRequest);
    }

    public class RequestMapper : IRequestMapper
    {
        public Models.PaymentDetails MapToDomainModel(CreatePaymentRequest createPaymentRequest)
        {
            return new Models.PaymentDetails
            {
                ShopperId = createPaymentRequest.ShopperId,
                MerchantId = createPaymentRequest.MerchantId,
                Currency = createPaymentRequest.Currency,
                Amount = createPaymentRequest.Amount,
                CardDetails = MapCardDetails(createPaymentRequest.CardDetails)
            };
        }

        private static Models.CardDetails MapCardDetails(CardDetails cardDetails)
        {
            return new Models.CardDetails
            {
                NameOnCard = cardDetails.NameOnCard,
                CardNumber = cardDetails.CardNumber,
                ExpirationMonth = cardDetails.ExpirationMonth,
                ExpirationYear = cardDetails.ExpirationYear,
                SecurityCode = cardDetails.SecurityCode
            };
        }
    }
}
