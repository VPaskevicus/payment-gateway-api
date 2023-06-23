using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Controllers;
using Checkout.Payment.Gateway.Api.Services;
using Checkout.Payment.Gateway.Api.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Checkout.Payment.Gateway.Api.UnitTests.Controllers
{
    [Collection("UnitTestFixtures")]
    public class PaymentControllerShould
    {
        private readonly PaymentController _paymentController;

        private readonly PaymentRequest _basicPaymentRequest;

        private readonly Mock<IPaymentService> _paymentServiceMock;

        public PaymentControllerShould(PaymentRequestFixture paymentRequestFixtures) 
        {
            _paymentServiceMock = new Mock<IPaymentService>();

            _paymentController = new PaymentController(_paymentServiceMock.Object);

            _basicPaymentRequest = paymentRequestFixtures.BasicPaymentRequest;
        }

        [Fact]
        public async Task Return200OKWhenPaymentCreatedSucessfully()
        {
            _paymentServiceMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<Models.Payment>())).ReturnsAsync(true);

            var response = await _paymentController.CreatePayment(_basicPaymentRequest);

            ((StatusCodeResult)response).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Return500InternalServerErrorIfPaymentServiceReturnsUnsucessful()
        {
            _paymentServiceMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<Models.Payment>())).ReturnsAsync(false);

            var response = await _paymentController.CreatePayment(_basicPaymentRequest);

            ((StatusCodeResult)response).StatusCode.Should().Be(500);
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
