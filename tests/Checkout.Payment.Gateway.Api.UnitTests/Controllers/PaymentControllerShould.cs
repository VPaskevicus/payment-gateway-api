using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Controllers;
using Checkout.Payment.Gateway.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

using CardDetails = Checkout.Payment.Gateway.Api.Contracts.CardDetails;

namespace Checkout.Payment.Gateway.Api.UnitTests.Controllers
{
    public class PaymentControllerShould
    {
        private readonly PaymentController _paymentController;

        private readonly Mock<IPaymentService> _paymentServiceMock;

        public PaymentControllerShould() 
        {
            _paymentServiceMock = new Mock<IPaymentService>();

            _paymentController = new PaymentController(_paymentServiceMock.Object);
        }

        [Fact]
        public async Task Return200OKWhenPaymentCreatedSucessfully()
        {
            var paymentRequest = new PaymentRequest()
            {
                ShopperId = Guid.NewGuid(),
                MerchantId = Guid.NewGuid(),
                Currency = "gbp",
                Amount = 156.60m,
                ShopperCardDetails = new CardDetails()
                {
                    NameOnCard = "Vladimirs Paskevicus",
                    CardNumber = "1243123412341234",
                    ExpirationMonth = 3,
                    ExpirationYear = 2027,
                    SecurityCode = 555
                }
            };

            _paymentServiceMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<Models.Payment>())).ReturnsAsync(true);

            var response = await _paymentController.CreatePayment(paymentRequest);

            ((StatusCodeResult)response).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Return500InternalServerErrorIfPaymentServiceReturnsUnsucessful()
        {
            var paymentRequest = new PaymentRequest()
            {
                ShopperId = Guid.NewGuid(),
                MerchantId = Guid.NewGuid(),
                Currency = "gbp",
                Amount = 156.60m,
                ShopperCardDetails = new CardDetails()
                {
                    NameOnCard = "Vladimirs Paskevicus",
                    CardNumber = "1243123412341234",
                    ExpirationMonth = 3,
                    ExpirationYear = 2027,
                    SecurityCode = 555
                }
            };

            _paymentServiceMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<Models.Payment>())).ReturnsAsync(false);

            var response = await _paymentController.CreatePayment(paymentRequest);

            ((StatusCodeResult)response).StatusCode.Should().Be(400);
        }
    }
}
