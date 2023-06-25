using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;
using Checkout.Payment.Gateway.Api.Repositories;
using Checkout.Payment.Gateway.Api.Services;
using Checkout.Payment.Gateway.Api.UnitTests.Fixtures;
using Moq;

namespace Checkout.Payment.Gateway.Api.UnitTests.Services
{
    [Collection("UnitTestFixtures")]
    public  class PaymentServiceShould
    {
        private readonly Mock<IAcquiringBank> _acquiringBankMock;
        private readonly Mock<IPaymentRepository> _paymentRepositoryMock;

        private readonly PaymentService _paymentService;

        private readonly PaymentFixture _paymentFixture;



        public PaymentServiceShould(PaymentFixture paymentFixture)
        {
            _acquiringBankMock = new Mock<IAcquiringBank>();
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            
            _paymentService = new PaymentService(_acquiringBankMock.Object, _paymentRepositoryMock.Object);

            _paymentFixture = paymentFixture;
        }

        [Fact]
        public async Task AddPaymentRecordToDataStoreWhenProcessPaymentAsyncIsCalled()
        {
            _acquiringBankMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<Models.Payment>())).ReturnsAsync(
                new PaymentResponse { PaymentId = _paymentFixture.BasicPayment.PaymentId, StatusCode = "001" });

            _paymentRepositoryMock.Setup(m => m.AddPaymentAsync(It.IsAny<Models.Payment>())).ReturnsAsync(true);

            var payment = _paymentFixture.BasicPayment;

            await _paymentService.ProcessPaymentAsync(payment);

            _paymentRepositoryMock.Verify(m => m.AddPaymentAsync(payment));
        }

        [Fact]
        public async Task CallAcquiringBankProcessPaymentAsyncWhenProcessPaymentAsyncIsCalled()
        {
            _acquiringBankMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<Models.Payment>())).ReturnsAsync(
                new PaymentResponse { PaymentId = _paymentFixture.BasicPayment.PaymentId, StatusCode = "001" });

            _paymentRepositoryMock.Setup(m => m.AddPaymentAsync(It.IsAny<Models.Payment>())).ReturnsAsync(true);

            var payment = _paymentFixture.BasicPayment;

            await _paymentService.ProcessPaymentAsync(payment);

            _acquiringBankMock.Verify(m => m.ProcessPaymentAsync(payment));
        }

        [Fact]
        public async Task ThrowExceptionWhenPaymentRepositoryThrowsException()
        {
            _paymentRepositoryMock.Setup(m => m.AddPaymentAsync(It.IsAny<Models.Payment>())).Throws(new Exception());

            Func<Task> function = async () => await _paymentService.ProcessPaymentAsync(_paymentFixture.BasicPayment);

            await function.Should().ThrowAsync<Exception>();
        }
    }
}
