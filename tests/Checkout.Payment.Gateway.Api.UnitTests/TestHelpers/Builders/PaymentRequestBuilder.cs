using Checkout.Payment.Gateway.Api.Contracts.Requests;
using CardDetails = Checkout.Payment.Gateway.Api.Contracts.CardDetails;

namespace Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Builders
{
    public class PaymentRequestBuilder
    {
        private Guid? _paymentId = new("44c23956-9f44-4776-b445-0e7fc56a5da6");
        private Guid? _shopperId = new("83d29ff5-0735-4428-9a02-67d83f4599c8");
        private Guid? _merchantId = new("b92a095e-a730-49c5-a2a9-1f1e5377355f");
        private string? _currency = "gbp";
        private decimal? _amount = 156.60m;
        private CardDetails _cardDetails  = DefaultCardDetails();

        private static CardDetails DefaultCardDetails()
        {
            return new CardDetailsBuilder().Create();
        }

        public PaymentRequestBuilder WithPaymentId(Guid? paymentId)
        {
            _paymentId = paymentId;
            return this;
        }

        public PaymentRequestBuilder WithShopperId(Guid? shopperId)
        {
            _shopperId = shopperId;
            return this;
        }

        public PaymentRequestBuilder WithMerchantId(Guid? merchantId)
        {
            _merchantId = merchantId;
            return this;
        }

        public PaymentRequestBuilder WithCurrency(string? currency)
        {
            _currency = currency;
            return this;
        }

        public PaymentRequestBuilder WithAmount(decimal? amount)
        {
            _amount = amount;
            return this;
        }

        public PaymentRequestBuilder WithCardDetails(CardDetails cardDetails)
        {
            _cardDetails = cardDetails;
            return this;
        }

        public PaymentRequest Create()
        {
            return new PaymentRequest
            {
                ShopperId = _shopperId,
                MerchantId = _merchantId,
                Currency = _currency,
                Amount = _amount,
                ShopperCardDetails = _cardDetails
            };
        }
    }
}
