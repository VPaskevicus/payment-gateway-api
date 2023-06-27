using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Contracts.Responses;
using Checkout.Payment.Gateway.Api.Generators;
using Checkout.Payment.Gateway.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payment.Gateway.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationTokenGenerator _authenticationTokenGenerator;

        private readonly ILogger<AuthenticationController> _authenticationControllerLogger;

        public AuthenticationController(
            IUserRepository userRepository,
            IAuthenticationTokenGenerator authenticationTokenGenerator,
            ILogger<AuthenticationController> authenticationControllerLogger)
        {
            _userRepository = userRepository;
            _authenticationTokenGenerator = authenticationTokenGenerator;
            _authenticationControllerLogger = authenticationControllerLogger;
        }

        [HttpPost]
        [Route("/authenticate")]
        public async Task<ActionResult> Authenticate(AuthenticationRequest authenticationRequest)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(
                    authenticationRequest.Username, authenticationRequest.Password);

                if (user == null)
                {
                    return Unauthorized();
                }

                var userToken = _authenticationTokenGenerator.GenerateToken(user);

                return Ok(new AuthenticationResponse { Token = userToken });
            }
            catch (Exception ex)
            {
                _authenticationControllerLogger.LogError(
                    message: "Exception occurred while authenticating the user.", exception: ex);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
