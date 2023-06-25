using Checkout.Payment.Gateway.Api.Builders;
using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Controllers;
using Checkout.Payment.Gateway.Api.Mappers;
using Checkout.Payment.Gateway.Api.Models;
using Checkout.Payment.Gateway.Api.Services;
using Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Checkout.Payment.Gateway.Api.UnitTests.Controllers
{
    [Collection("UnitTestFixtures")]
    public class PaymentControllerShould
    {
        private readonly PaymentController _paymentController;

        private readonly PaymentRequest _basicPaymentRequest;

        private readonly Mock<IPaymentMapper> _paymentMapperMock;
        private readonly Mock<IPaymentService> _paymentServiceMock;
        private readonly Mock<IPaymentResponseBuilder> _paymentResponseBuilder;

        public PaymentControllerShould(PaymentRequestFixture paymentRequestFixtures) 
        {
            _paymentMapperMock = new Mock<IPaymentMapper>();
            _paymentServiceMock = new Mock<IPaymentService>();
            _paymentResponseBuilder = new Mock<IPaymentResponseBuilder>();

            _paymentController = new PaymentController(_paymentMapperMock.Object, _paymentServiceMock.Object, _paymentResponseBuilder.Object);

            _basicPaymentRequest = paymentRequestFixtures.BasicPaymentRequest;
        }

        [Fact]
        public async Task Return200OkWhenPaymentProcessedSuccessfully()
        {
            _paymentServiceMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<Models.Payment>())).ReturnsAsync(new PaymentProcessResult(new PaymentResponse(), new Models.Payment()));

            var response = await _paymentController.CreatePayment(_basicPaymentRequest);

            ((OkObjectResult)response).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Return500InternalServerErrorIfPaymentServiceThrowsException()
        {
            _paymentServiceMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<Models.Payment>())).Throws(new Exception("something went wrong!"));

            var response = await _paymentController.CreatePayment(_basicPaymentRequest);

            ((StatusCodeResult)response).StatusCode.Should().Be(500);
        }
    }
}
