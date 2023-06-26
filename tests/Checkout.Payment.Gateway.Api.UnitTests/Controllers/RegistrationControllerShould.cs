using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Contracts.Responses;
using Checkout.Payment.Gateway.Api.Controllers;
using Checkout.Payment.Gateway.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Checkout.Payment.Gateway.Api.UnitTests.Controllers
{
    public class RegistrationControllerShould
    {
        private static readonly RegistrationRequest DefaultRegistrationRequest = new() { Username = "vpaskevicus", Password = "123456" };

        private readonly Mock<IUserRepository> _userRepositoryMock;

        private readonly Mock<ILogger<RegistrationController>> _registrationControllerLoggerMock;

        private readonly RegistrationController _registrationController;

        public RegistrationControllerShould()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            _registrationControllerLoggerMock = new Mock<ILogger<RegistrationController>>();

            _registrationController = new RegistrationController(
                _userRepositoryMock.Object,
                _registrationControllerLoggerMock.Object);
        }

        [Fact]
        public async Task Return201CreatedWhenRegistrationIsSuccessful()
        {
            _userRepositoryMock.Setup(m => m.AddUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            var registrationResponse = await _registrationController.Registration(DefaultRegistrationRequest);

            var response = (CreatedResult)registrationResponse;

            response.StatusCode.Should().Be(201);

            var responseValue = response.Value as RegistrationResponse;

            responseValue?.Username.Should().Be(DefaultRegistrationRequest.Username);
            responseValue?.StatusCode.Should().Be("c_001");
        }

        [Fact]
        public async Task Return400BadRequestWhenRegistrationIsUnsuccessful()
        {
            _userRepositoryMock.Setup(m => m.AddUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            var registrationResponse = await _registrationController.Registration(DefaultRegistrationRequest);

            ((BadRequestResult)registrationResponse).StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Return500InternalServerErrorWhenUserRepositoryAddUserAsyncThrowsException()
        {
            _userRepositoryMock.Setup(m => m.AddUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            var registrationResponse = await _registrationController.Registration(new RegistrationRequest());

            ((StatusCodeResult)registrationResponse).StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task CallUserRepositoryAddUserAsyncWhenRegisterIsCalled()
        {
            await _registrationController.Registration(DefaultRegistrationRequest);

            _userRepositoryMock.Verify(m => m.AddUserAsync(It.Is<string>(s => s.Equals(DefaultRegistrationRequest.Username)),
                It.Is<string>(s => s.Equals(DefaultRegistrationRequest.Password))), Times.Once);
        }

        [Fact]
        public async Task LogExceptionWhenOccurredOnRegistrationCall()
        {
            _userRepositoryMock.Setup(m => m.AddUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            await _registrationController.Registration(DefaultRegistrationRequest);

            _registrationControllerLoggerMock.Verify(logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.Is<EventId>(eventId => eventId.Id == 0),
                    It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Exception occurred while registering the user." && @type.Name == "FormattedLogValues"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }
    }
}
