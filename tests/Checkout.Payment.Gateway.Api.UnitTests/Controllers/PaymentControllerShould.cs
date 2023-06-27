using Checkout.Payment.Gateway.Api.Builders;
using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Controllers;
using Checkout.Payment.Gateway.Api.Mappers;
using Checkout.Payment.Gateway.Api.Models;
using Checkout.Payment.Gateway.Api.Services;
using Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Checkout.Payment.Gateway.Api.UnitTests.Controllers
{
    [Collection("UnitTestFixtures")]
    public class PaymentControllerShould
    {
        private const string ErrorMessage = "Something went wrong!";

        private readonly PaymentController _paymentController;

        private readonly CreatePaymentRequestFixture _createPaymentRequestFixture;
        private readonly CreatePaymentResponseFixture _createPaymentResponseFixture;
        private readonly GetPaymentDetailsResponseFixture _getPaymentDetailsResponseFixture;
        private readonly PaymentProcessResultFixture _paymentProcessResultFixture;

        private readonly Mock<IRequestMapper> _requestMapperMock;
        private readonly Mock<IPaymentService> _paymentServiceMock;
        private readonly Mock<IResponseBuilder> _responseBuilderMock;

        private readonly Mock<ILogger<PaymentController>> _paymentControllerLoggerMock;

        public PaymentControllerShould(
            CreatePaymentRequestFixture createPaymentRequestFixture,
            CreatePaymentResponseFixture createPaymentResponseFixture,
            GetPaymentDetailsResponseFixture getPaymentDetailsResponseFixture,
            PaymentProcessResultFixture paymentProcessResultFixture) 
        {
            _requestMapperMock = new Mock<IRequestMapper>();
            _paymentServiceMock = new Mock<IPaymentService>();
            _responseBuilderMock = new Mock<IResponseBuilder>();

            _paymentControllerLoggerMock = new Mock<ILogger<PaymentController>>();

            _paymentController = new PaymentController(
                _requestMapperMock.Object, 
                _paymentServiceMock.Object,
                _responseBuilderMock.Object,
                _paymentControllerLoggerMock.Object);

            _createPaymentRequestFixture = createPaymentRequestFixture;
            _createPaymentResponseFixture = createPaymentResponseFixture;
            _getPaymentDetailsResponseFixture = getPaymentDetailsResponseFixture;
            _paymentProcessResultFixture = paymentProcessResultFixture;
        }

        [Fact]
        public async Task Return200OkWhenCreatePaymentIsSuccessful()
        {
            _responseBuilderMock.Setup(m => m.BuildCreatePaymentResponse(It.IsAny<PaymentDetailsProcessResult>()))
                .Returns(_createPaymentResponseFixture.BasicCreatePaymentResponse);

            var createPaymentResponse =
                await _paymentController.CreatePayment(_createPaymentRequestFixture.BasicCreatePaymentRequest);

            ((OkObjectResult)createPaymentResponse).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Return500InternalServerErrorWhenRequestMapperThrowsException()
        {
            _requestMapperMock.Setup(m => m.MapToDomainModel(It.IsAny<CreatePaymentRequest>()))
                .Throws(new Exception(ErrorMessage));

            var createPaymentResponse =
                await _paymentController.CreatePayment(_createPaymentRequestFixture.BasicCreatePaymentRequest);

            ((StatusCodeResult)createPaymentResponse).StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Return500InternalServerErrorWhenPaymentServiceProcessPaymentDetailsAsyncThrowsException()
        {
            _paymentServiceMock.Setup(m => m.ProcessPaymentDetailsAsync(It.IsAny<PaymentDetails>()))
                .Throws(new Exception(ErrorMessage));

            var createPaymentResponse =
                await _paymentController.CreatePayment(_createPaymentRequestFixture.BasicCreatePaymentRequest);

            ((StatusCodeResult)createPaymentResponse).StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Return500InternalServerErrorWhenResponseBuilderBuildCreatePaymentResponseThrowsException()
        {
            _responseBuilderMock.Setup(m => m.BuildCreatePaymentResponse(It.IsAny<PaymentDetailsProcessResult>()))
                .Throws(new Exception(ErrorMessage));

            var createPaymentResponse =
                await _paymentController.CreatePayment(_createPaymentRequestFixture.BasicCreatePaymentRequest);

            ((StatusCodeResult)createPaymentResponse).StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Return200OkWhenGetPaymentDetailsIsSuccessful()
        {
            _paymentServiceMock.Setup(m => m.GetPaymentDetailsAsync(It.IsAny<Guid?>()))
                .ReturnsAsync(_paymentProcessResultFixture.BasicPaymentDetailsProcessResult);

            _responseBuilderMock.Setup(m => m.BuildGetPaymentDetailsResponse(It.IsAny<PaymentDetailsProcessResult>()))
                .Returns(_getPaymentDetailsResponseFixture.BasicGetPaymentDetailsResponse);

            var createPaymentResponse =
                await _paymentController.GetPaymentDetails(new GetPaymentDetailsRequest());

            ((OkObjectResult)createPaymentResponse).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Return404NotFoundWhenPaymentDetailsProcessResultNotFound()
        {
            _paymentServiceMock.Setup(m => m.GetPaymentDetailsAsync(It.IsAny<Guid?>()))
                .ReturnsAsync(_paymentProcessResultFixture.EmptyPaymentDetailsProcessResult);

            var createPaymentResponse =
                await _paymentController.GetPaymentDetails(new GetPaymentDetailsRequest());

            ((StatusCodeResult)createPaymentResponse).StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Return500InternalServerErrorWhenPaymentServiceGetPaymentDetailsAsyncThrowsException()
        {
            _paymentServiceMock.Setup(m => m.GetPaymentDetailsAsync(It.IsAny<Guid?>()))
                .Throws(new Exception(ErrorMessage));

            var createPaymentResponse =
                await _paymentController.GetPaymentDetails(new GetPaymentDetailsRequest());

            ((StatusCodeResult)createPaymentResponse).StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Return500InternalServerErrorWhenResponseBuilderBuildGetPaymentDetailsResponseThrowsException()
        {
            _responseBuilderMock.Setup(m => m.BuildGetPaymentDetailsResponse(It.IsAny<PaymentDetailsProcessResult>()))
                .Throws(new Exception(ErrorMessage));

            var createPaymentResponse =
                await _paymentController.GetPaymentDetails(new GetPaymentDetailsRequest());

            ((StatusCodeResult)createPaymentResponse).StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task LogExceptionWhenOccurredOnCreatePaymentCall()
        {
            _requestMapperMock.Setup(m => m.MapToDomainModel(It.IsAny<CreatePaymentRequest>()))
                .Throws(new Exception(ErrorMessage));

            await _paymentController.CreatePayment(_createPaymentRequestFixture.BasicCreatePaymentRequest);

            _paymentControllerLoggerMock.Verify(logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.Is<EventId>(eventId => eventId.Id == 0),
                    It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Exception occurred while processing payment request." && @type.Name == "FormattedLogValues"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task LogExceptionWhenOccurredOnGetPaymentDetailsCall()
        {
            var getPaymentDetailsRequest = new GetPaymentDetailsRequest { PaymentId = Guid.NewGuid() };

            _paymentServiceMock.Setup(m => m.GetPaymentDetailsAsync(It.IsAny<Guid>()))
                .Throws(new Exception(ErrorMessage));

            await _paymentController.GetPaymentDetails(getPaymentDetailsRequest);

            _paymentControllerLoggerMock.Verify(logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.Is<EventId>(eventId => eventId.Id == 0),
                    It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == $"Exception occurred while getting payment details with id {getPaymentDetailsRequest.PaymentId}." && @type.Name == "FormattedLogValues"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task LogInformationWhenPaymentServiceGetPaymentDetailsAsyncReturnsNull()
        {
            var getPaymentDetailsRequest = new GetPaymentDetailsRequest { PaymentId = Guid.NewGuid() };

            _paymentServiceMock.Setup(m => m.GetPaymentDetailsAsync(It.IsAny<Guid?>()))
                .ReturnsAsync(_paymentProcessResultFixture.EmptyPaymentDetailsProcessResult);

            await _paymentController.GetPaymentDetails(getPaymentDetailsRequest);

            _paymentControllerLoggerMock.Verify(logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                    It.Is<EventId>(eventId => eventId.Id == 0),
                    It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == $"Payment with id {getPaymentDetailsRequest.PaymentId} was not found when accessing payments." && @type.Name == "FormattedLogValues"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }
    }
}
