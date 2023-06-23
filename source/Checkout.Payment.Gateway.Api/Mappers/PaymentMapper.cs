using Checkout.Payment.Gateway.Api.Contracts;
using Checkout.Payment.Gateway.Api.Contracts.Requests;

namespace Checkout.Payment.Gateway.Api.Mappers
{
    public interface IPaymentMapper
    {
        Models.Payment MapPaymentRequestToDomainModel(PaymentRequest paymentRequest);
    }

    public class PaymentMapper : IPaymentMapper
    {
        public Models.Payment MapPaymentRequestToDomainModel(PaymentRequest paymentRequest)
        {
            return new Models.Payment()
            {
                ShopperId = paymentRequest.ShopperId,
                MerchantId = paymentRequest.MerchantId,
                Currency = paymentRequest.Currency,
                Amount = paymentRequest.Amount,
                ShopperCardDetails = MapCardDetails(paymentRequest.ShopperCardDetails)
            };
        }

        private static Models.CardDetails MapCardDetails(CardDetails cardDetails)
        {
            return new Models.CardDetails()
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
