﻿using Checkout.Payment.Gateway.Api.Builders;
using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Mappers;
using Checkout.Payment.Gateway.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payment.Gateway.Api.Controllers
{
    [Route("/paymentDetails")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IRequestMapper _requestMapper;
        private readonly IPaymentService _paymentService;
        private readonly IResponseBuilder _responseBuilder;

        public PaymentController(
            IRequestMapper requestMapper, 
            IPaymentService paymentService, 
            IResponseBuilder responseBuilder)
        {
            _requestMapper = requestMapper;
            _paymentService = paymentService;
            _responseBuilder = responseBuilder;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePayment([FromBody] CreatePaymentRequest createPaymentRequest)
        {
            try
            {
                var paymentDetails = _requestMapper.MapToDomainModel(createPaymentRequest);

                var paymentDetailsProcessResult = await _paymentService.ProcessPaymentDetailsAsync(paymentDetails);

                var createPaymentResponse = _responseBuilder.BuildResponse(paymentDetailsProcessResult);

                return Ok(createPaymentResponse);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
