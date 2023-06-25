using Checkout.Payment.Gateway.Api.Builders;
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
        private readonly IPaymentResponseBuilder _paymentResponseBuilder;

        public PaymentController(
            IPaymentMapper paymentMapper, 
            IPaymentService paymentService, 
            IPaymentResponseBuilder paymentResponseBuilder)
        {
            _paymentMapper = paymentMapper;
            _paymentService = paymentService;
            _paymentResponseBuilder = paymentResponseBuilder;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePayment([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                var payment = _paymentMapper.MapPaymentRequestToDomainModel(paymentRequest);

                var paymentProcessResult = await _paymentService.ProcessPaymentAsync(payment);

                var response = _paymentResponseBuilder.BuildCreatePaymentResponse(paymentProcessResult);

                return Ok(response);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
