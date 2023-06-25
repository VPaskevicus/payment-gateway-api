using Checkout.Payment.Gateway.Api.Builders;
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

        private readonly CreatePaymentRequestFixture _createPaymentRequestFixture;

        private readonly Mock<IPaymentService> _paymentServiceMock;

        public PaymentControllerShould(CreatePaymentRequestFixture createPaymentRequestFixture) 
        {
            Mock<IRequestMapper> paymentMapperMock = new();
            _paymentServiceMock = new Mock<IPaymentService>();
            Mock<IResponseBuilder> paymentResponseBuilder = new();

            _paymentController = new PaymentController(paymentMapperMock.Object, _paymentServiceMock.Object,
                paymentResponseBuilder.Object);

            _createPaymentRequestFixture = createPaymentRequestFixture;
        }

        [Fact]
        public async Task Return200OkWhenPaymentProcessedSuccessfully()
        {
            _paymentServiceMock.Setup(m => m.ProcessPaymentDetailsAsync(It.IsAny<PaymentDetails>()))
                .ReturnsAsync(new PaymentDetailsProcessResult(new AcquiringBankResponse(), new PaymentDetails()));

            var createPaymentResponse =
                await _paymentController.CreatePayment(_createPaymentRequestFixture.BasicCreatePaymentRequest);

            ((OkObjectResult)createPaymentResponse).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Return500InternalServerErrorIfPaymentServiceThrowsException()
        {
            _paymentServiceMock.Setup(m => m.ProcessPaymentDetailsAsync(It.IsAny<PaymentDetails>())).Throws(new Exception("something went wrong!"));

            var createPaymentResponse =
                await _paymentController.CreatePayment(_createPaymentRequestFixture.BasicCreatePaymentRequest);

            ((StatusCodeResult)createPaymentResponse).StatusCode.Should().Be(500);
        }
    }
}
