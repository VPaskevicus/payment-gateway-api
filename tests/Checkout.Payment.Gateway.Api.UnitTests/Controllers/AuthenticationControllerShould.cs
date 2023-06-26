using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Controllers;
using Checkout.Payment.Gateway.Api.Generators;
using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Checkout.Payment.Gateway.Api.UnitTests.Controllers
{
    public class AuthenticationControllerShould
    {
        private static readonly AuthenticationRequest DefaultAuthenticationRequest = new() { Username = "vpaskevicus", Password = "123456" };
        private static readonly User DefaultUser = new("vpaskevicus", "123456");

        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IAuthenticationTokenGenerator> _authenticationTokenGeneratorMock;

        private readonly Mock<ILogger<AuthenticationController>> _authenticationControllerLoggerMock;

        private readonly AuthenticationController _authenticationController;

        public AuthenticationControllerShould()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authenticationTokenGeneratorMock = new Mock<IAuthenticationTokenGenerator>();

            _authenticationControllerLoggerMock = new Mock<ILogger<AuthenticationController>>();

            _authenticationController = new AuthenticationController(
                _userRepositoryMock.Object,
                _authenticationTokenGeneratorMock.Object,
                _authenticationControllerLoggerMock.Object);
        }

        [Fact]
        public async Task Return200OkWhenAuthenticationIsSuccessful()
        {
            _userRepositoryMock.Setup(m => m.GetUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(DefaultUser);
            _authenticationTokenGeneratorMock.Setup(m => m.GenerateToken(DefaultUser)).Returns("defaultToken");
            var authenticationResponse = await _authenticationController.Authenticate(DefaultAuthenticationRequest);

            var response = (OkObjectResult)authenticationResponse;

            response.StatusCode.Should().Be(200);

            var responseValue = response.Value as string;

            responseValue?.Should().Be("defaultToken");
        }

        [Fact]
        public async Task Return401UnauthorizedWhenUserRecordNotFoundInUserDataStore()
        {
            User? user = null;
            
            _userRepositoryMock.Setup(m => m.GetUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);
            
            var authenticationResponse = await _authenticationController.Authenticate(DefaultAuthenticationRequest);

            var response = (UnauthorizedResult)authenticationResponse;

            ((UnauthorizedResult)authenticationResponse).StatusCode.Should().Be(401);
        }

        [Fact]
        public async Task Return500InternalServerErrorWhenUserRepositoryGetUserAsyncThrowsException()
        {
            _userRepositoryMock.Setup(m => m.GetUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            var authenticationResponse = await _authenticationController.Authenticate(new AuthenticationRequest());

            ((StatusCodeResult)authenticationResponse).StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task CallUserRepositoryGetUserAsyncWhenAuthenticateIsCalled()
        {
            await _authenticationController.Authenticate(DefaultAuthenticationRequest);

            _userRepositoryMock.Verify(m => m.GetUserAsync(
                It.Is<string>(s => s.Equals(DefaultAuthenticationRequest.Username)),
                It.Is<string>(s => s.Equals(DefaultAuthenticationRequest.Password))), 
                Times.Once);
        }

        [Fact]
        public async Task LogExceptionWhenOccurredOnAuthenticateCall()
        {
            _userRepositoryMock.Setup(m => m.GetUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            await _authenticationController.Authenticate(DefaultAuthenticationRequest);

            _authenticationControllerLoggerMock.Verify(logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.Is<EventId>(eventId => eventId.Id == 0),
                    It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Exception occurred while authenticating the user." && @type.Name == "FormattedLogValues"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }
    }
}
