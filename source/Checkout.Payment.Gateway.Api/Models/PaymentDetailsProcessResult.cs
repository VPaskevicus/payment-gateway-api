namespace Checkout.Payment.Gateway.Api.Models
{
    public class PaymentDetailsProcessResult
    {
        public PaymentDetailsProcessResult(AcquiringBankResponse? acquiringBankResponse, PaymentDetails? paymentDetails)
        {
            AcquiringBankResponse = acquiringBankResponse;
            PaymentDetails = paymentDetails;
        }

        public AcquiringBankResponse? AcquiringBankResponse { get; }

        public PaymentDetails? PaymentDetails { get; }
    }
}
