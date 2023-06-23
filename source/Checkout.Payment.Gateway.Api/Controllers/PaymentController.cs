using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Models;
using Checkout.Payment.Gateway.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payment.Gateway.Api.Controllers
{
    [Route("/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePayment(PaymentRequest paymentRequest)
        {
            try
            {
                var paymentDetails = new Models.Payment()
                {
                    ShopperId = paymentRequest.ShopperId,
                    MerchantId = paymentRequest.MerchantId,
                    Amount = paymentRequest.Amount,
                    Currency = paymentRequest.Currency,
                    ShopperCardDetails = new CardDetails()
                    {
                        CardNumber = paymentRequest.ShopperCardDetails.CardNumber,
                        NameOnCard = paymentRequest.ShopperCardDetails.NameOnCard,
                        SecurityCode = paymentRequest.ShopperCardDetails.SecurityCode,
                        ExpirationMonth = paymentRequest.ShopperCardDetails.ExpirationMonth,
                        ExpirationYear = paymentRequest.ShopperCardDetails.ExpirationYear

                    }
                };

                var result = await _paymentService.ProcessPaymentAsync(paymentDetails);

                if (result)
                {
                    return Ok();
                }
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
