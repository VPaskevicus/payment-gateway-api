namespace Checkout.Payment.Gateway.Api.Models
{
    public class PaymentProcessResult
    {
        public PaymentProcessResult(PaymentResponse paymentResponse, Payment payment)
        {
            PaymentResponse = paymentResponse;
            Payment = payment;
        }

        public PaymentResponse PaymentResponse { get; }

        public Payment Payment { get; }
    }
}
