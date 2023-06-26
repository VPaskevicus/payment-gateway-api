using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;
using Checkout.Payment.Gateway.Api.Services;
using Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Fixtures;
using Moq;

namespace Checkout.Payment.Gateway.Api.UnitTests.Services
{
    [Collection("UnitTestFixtures")]
    public  class PaymentServiceShould
    {
        private readonly Mock<IAcquiringBank> _acquiringBankMock;
        private readonly Mock<IPaymentRepository> _paymentRepositoryMock;

        private readonly PaymentService _paymentService;

        private readonly PaymentDetailsFixture _paymentDetailsFixture;
        private readonly AcquiringBankResponseFixture _acquiringBankResponseFixture;

        public PaymentServiceShould(PaymentDetailsFixture paymentDetailsFixture, AcquiringBankResponseFixture acquiringBankResponseFixture)
        {
            _acquiringBankMock = new Mock<IAcquiringBank>();
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            
            _paymentService = new PaymentService(_acquiringBankMock.Object, _paymentRepositoryMock.Object);

            _paymentDetailsFixture = paymentDetailsFixture;
            _acquiringBankResponseFixture = acquiringBankResponseFixture;
        }

        [Fact]
        public async Task ReturnPaymentDetailsProcessResultWhenProcessPaymentAsyncIsCalled()
        {
            var basicAcquiringBankResponse = _acquiringBankResponseFixture.BasicAcquiringBankResponse;
            var basicPaymentDetails = _paymentDetailsFixture.BasicPaymentDetails;

            _acquiringBankMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<PaymentDetails>()))
                .ReturnsAsync(basicAcquiringBankResponse);

            _paymentRepositoryMock.Setup(m => m.AddPaymentDetailsAsync(It.IsAny<Guid>(), It.IsAny<PaymentDetails>()))
                .ReturnsAsync(true);

            var paymentDetailsProcessResult = await _paymentService.ProcessPaymentDetailsAsync(basicPaymentDetails);

            paymentDetailsProcessResult.AcquiringBankResponse.Should().BeSameAs(basicAcquiringBankResponse);
            paymentDetailsProcessResult.PaymentDetails.Should().BeSameAs(basicPaymentDetails);
        }

        [Fact]
        public async Task ReturnPaymentDetailsProcessResultWhenGetPaymentDetailsAsyncIsCalled()
        {
            var basicAcquiringBankResponse = _acquiringBankResponseFixture.BasicAcquiringBankResponse;
            var basicPaymentDetails = _paymentDetailsFixture.BasicPaymentDetails;

            _acquiringBankMock.Setup(m => m.GetPaymentStatusAsync(It.IsAny<Guid>()))
                .ReturnsAsync(basicAcquiringBankResponse);

            _paymentRepositoryMock.Setup(m => m.GetPaymentDetailsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(basicPaymentDetails);

            var paymentDetailsProcessResult = await _paymentService.GetPaymentDetailsAsync(basicAcquiringBankResponse.PaymentId);
            
            paymentDetailsProcessResult.AcquiringBankResponse.Should().BeSameAs(basicAcquiringBankResponse);
            paymentDetailsProcessResult.PaymentDetails.Should().BeSameAs(basicPaymentDetails);
        }

        [Fact]
        public async Task CallAddPaymentDetailsAsyncWhenProcessPaymentAsyncIsCalled()
        {
            var basicAcquiringBankResponse = _acquiringBankResponseFixture.BasicAcquiringBankResponse;
            var basicPaymentDetails = _paymentDetailsFixture.BasicPaymentDetails;

            _acquiringBankMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<PaymentDetails>()))
                .ReturnsAsync(basicAcquiringBankResponse);

            _paymentRepositoryMock.Setup(m => m.AddPaymentDetailsAsync(It.IsAny<Guid>(), It.IsAny<PaymentDetails>()))
                .ReturnsAsync(true);

            await _paymentService.ProcessPaymentDetailsAsync(basicPaymentDetails);

            _paymentRepositoryMock.Verify(m =>
                m.AddPaymentDetailsAsync(basicAcquiringBankResponse.PaymentId, basicPaymentDetails), Times.Once);
        }

        [Fact]
        public async Task CallGetPaymentDetailsAsyncWhenGetPaymentDetailsAsyncIsCalled()
        {
            var basicAcquiringBankResponse = _acquiringBankResponseFixture.BasicAcquiringBankResponse;
            var basicPaymentDetails = _paymentDetailsFixture.BasicPaymentDetails;

            _acquiringBankMock.Setup(m => m.GetPaymentStatusAsync(It.IsAny<Guid>()))
                .ReturnsAsync(basicAcquiringBankResponse);

            _paymentRepositoryMock.Setup(m => m.GetPaymentDetailsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(basicPaymentDetails);

            await _paymentService.GetPaymentDetailsAsync(basicAcquiringBankResponse.PaymentId);

            _paymentRepositoryMock.Verify(m => m.GetPaymentDetailsAsync(basicAcquiringBankResponse.PaymentId), Times.Once);
        }

        [Fact]
        public async Task CallAcquiringBankProcessPaymentAsyncWhenProcessPaymentAsyncIsCalled()
        {
            var basicAcquiringBankResponse = _acquiringBankResponseFixture.BasicAcquiringBankResponse;
            var basicPaymentDetails = _paymentDetailsFixture.BasicPaymentDetails;

            _acquiringBankMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<PaymentDetails>()))
                .ReturnsAsync(basicAcquiringBankResponse);

            _paymentRepositoryMock.Setup(m => m.AddPaymentDetailsAsync(It.IsAny<Guid>(), It.IsAny<PaymentDetails>()))
                .ReturnsAsync(true);

            await _paymentService.ProcessPaymentDetailsAsync(basicPaymentDetails);

            _acquiringBankMock.Verify(m => m.ProcessPaymentAsync(basicPaymentDetails), Times.Once);
        }

        [Fact]
        public async Task CallGetPaymentStatusAsyncWhenGetPaymentDetailsAsyncIsCalled()
        {
            var basicAcquiringBankResponse = _acquiringBankResponseFixture.BasicAcquiringBankResponse;
            var basicPaymentDetails = _paymentDetailsFixture.BasicPaymentDetails;

            _acquiringBankMock.Setup(m => m.GetPaymentStatusAsync(It.IsAny<Guid>()))
                .ReturnsAsync(basicAcquiringBankResponse);

            _paymentRepositoryMock.Setup(m => m.GetPaymentDetailsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(basicPaymentDetails);

            await _paymentService.GetPaymentDetailsAsync(basicAcquiringBankResponse.PaymentId);

            _acquiringBankMock.Verify(m => m.GetPaymentStatusAsync(basicAcquiringBankResponse.PaymentId), Times.Once);
        }

        [Fact]
        public async Task ThrowExceptionWhenPaymentRepositoryThrowsException()
        {
            _paymentRepositoryMock.Setup(m => m.AddPaymentDetailsAsync(It.IsAny<Guid>(), It.IsAny<PaymentDetails>()))
                .Throws(new Exception());

            Func<Task> function = async () => await _paymentService.ProcessPaymentDetailsAsync(_paymentDetailsFixture.BasicPaymentDetails);

            await function.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task ThrowExceptionWhenAcquiringBankThrowsException()
        {
            _acquiringBankMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<PaymentDetails>()))
                .Throws(new Exception());

            Func<Task> function = async () => await _paymentService.ProcessPaymentDetailsAsync(_paymentDetailsFixture.BasicPaymentDetails);

            await function.Should().ThrowAsync<Exception>();
        }
    }
}
