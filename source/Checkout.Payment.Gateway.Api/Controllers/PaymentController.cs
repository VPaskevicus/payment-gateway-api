using Checkout.Payment.Gateway.Api.Builders;
using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Mappers;
using Checkout.Payment.Gateway.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payment.Gateway.Api.Controllers
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IRequestMapper _requestMapper;
        private readonly IPaymentService _paymentService;
        private readonly IResponseBuilder _responseBuilder;

        private readonly ILogger<PaymentController> _paymentControllerLogger;

        public PaymentController(
            IRequestMapper requestMapper,
            IPaymentService paymentService,
            IResponseBuilder responseBuilder,
            
            ILogger<PaymentController> paymentControllerLogger)
        {
            _requestMapper = requestMapper;
            _paymentService = paymentService;
            _responseBuilder = responseBuilder;

            _paymentControllerLogger = paymentControllerLogger;
        }

        [HttpPost]
        [Route("/payment")]
        public async Task<ActionResult> CreatePayment([FromBody] CreatePaymentRequest createPaymentRequest)
        {
            try
            {
                var paymentDetails = _requestMapper.MapToDomainModel(createPaymentRequest);

                var paymentDetailsProcessResult = await _paymentService.ProcessPaymentDetailsAsync(paymentDetails);

                var createPaymentResponse = _responseBuilder.BuildCreatePaymentResponse(paymentDetailsProcessResult);

                return Ok(createPaymentResponse);
            }
            catch (Exception ex)
            {
                _paymentControllerLogger.LogCritical(
                    message: "Exception occurred while processing payment request with id.", exception: ex);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("/payment/{PaymentId}")]
        public async Task<ActionResult> GetPaymentDetails([FromRoute] GetPaymentDetailsRequest getPaymentDetailsRequest)
        {
            try
            {
                var paymentDetailsProcessResult =
                    await _paymentService.GetPaymentDetailsAsync(getPaymentDetailsRequest.PaymentId);

                if (paymentDetailsProcessResult.NotFound())
                {
                    _paymentControllerLogger.LogInformation(
                        $"Payment with {getPaymentDetailsRequest.PaymentId} was not found when accessing payments.");
                    return NotFound();
                }

                var getPaymentDetailsResponse =
                    _responseBuilder.BuildGetPaymentDetailsResponse(paymentDetailsProcessResult);

                return Ok(getPaymentDetailsResponse);
            }
            catch (Exception ex)
            {
                _paymentControllerLogger.LogCritical(
                    message:
                    $"Exception occurred while getting payment details with id {getPaymentDetailsRequest.PaymentId}.",
                    exception: ex);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
