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
        public async Task<ActionResult> CreatePayment([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                var payment = _paymentMapper.MapPaymentRequestToDomainModel(paymentRequest);

                var paymentProcessResult = await _paymentService.ProcessPaymentAsync(payment);

                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
