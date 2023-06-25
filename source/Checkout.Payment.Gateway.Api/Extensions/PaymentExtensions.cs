namespace Checkout.Payment.Gateway.Api.Extensions
{
    public static class PaymentExtensions
    {
        public static void SetPaymentId(this Models.Payment payment, Guid paymentId)
        {
            payment.PaymentId = paymentId;
        }
    }
}
