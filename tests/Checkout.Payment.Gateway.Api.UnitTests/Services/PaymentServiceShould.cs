using Checkout.Payment.Gateway.Api.Repositories;
using Checkout.Payment.Gateway.Api.Services;
using Moq;

namespace Checkout.Payment.Gateway.Api.UnitTests.Services
{
    public  class PaymentServiceShould
    {
        private readonly PaymentService _paymentService;

        private readonly Mock<IPaymentRepository> _paymentRepositoryMock;

        public PaymentServiceShould()
        {
            _paymentRepositoryMock = new Mock<IPaymentRepository>();

            _paymentService = new PaymentService(_paymentRepositoryMock.Object);
        }

        [Fact]
        public async Task CallPaymentRepositoryWhenProcessPaymentAsyncIsCalled()
        {
            _paymentRepositoryMock.Setup(m => m.AddPaymentAsync(It.IsAny<Models.Payment>())).ReturnsAsync(true);

            var payment = new Models.Payment();

            var result = await _paymentService.ProcessPaymentAsync(payment);

            _paymentRepositoryMock.Verify(m => m.AddPaymentAsync(payment));

            result.Should().BeTrue();

        }

        [Fact]
        public async Task ThrowExceptionWhenPaymentRepositoryThrowsException()
        {
            _paymentRepositoryMock.Setup(m => m.AddPaymentAsync(It.IsAny<Models.Payment>())).Throws(new Exception());

            Func<Task> function = async () => await _paymentService.ProcessPaymentAsync(new Models.Payment());

            await function.Should().ThrowAsync<Exception>();
        }
    }
}
