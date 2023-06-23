using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Mappers;
using Checkout.Payment.Gateway.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payment.Gateway.Api.Controllers
{
    [Route("/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentMapper _paymentMapper;
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentMapper paymentMapper, IPaymentService paymentService)
        {
            _paymentMapper = paymentMapper;
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePayment(PaymentRequest paymentRequest)
        {
            try
            {
                var payment = _paymentMapper.MapPaymentRequestToDomainModel(paymentRequest);

                var result = await _paymentService.ProcessPaymentAsync(payment);

                if (result)
                {
                    return Ok();
                }
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
