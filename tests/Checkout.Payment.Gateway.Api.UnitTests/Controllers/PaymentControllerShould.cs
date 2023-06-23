using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Controllers;
using Checkout.Payment.Gateway.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

using CardDetails = Checkout.Payment.Gateway.Api.Contracts.CardDetails;

namespace Checkout.Payment.Gateway.Api.UnitTests.Controllers
{
    public class PaymentControllerShould
    {
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

            var paymentServiceMock = new Mock<IPaymentService>();

            paymentServiceMock.Setup(m => m.ProcessPaymentAsync(It.IsAny<Models.Payment>())).ReturnsAsync(true);

            var paymentController = new PaymentController(paymentServiceMock.Object);

            var response = await paymentController.CreatePayment(paymentRequest);

            ((StatusCodeResult)response).StatusCode.Should().Be(200);

        }
    }
}
