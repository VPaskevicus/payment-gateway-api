using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Gateway.Api.Contracts.Requests
{
    public class GetPaymentDetailsRequest
    {
        [Required]
        public Guid? PaymentId { get; set; }
    }
}
