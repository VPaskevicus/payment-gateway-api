using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Contracts.Responses;
using Checkout.Payment.Gateway.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payment.Gateway.Api.Controllers
{
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly ILogger<RegistrationController> _registrationControllerLogger;

        public RegistrationController(
            IUserRepository userRepository,
            ILogger<RegistrationController> registrationControllerLogger)
        {
            _userRepository = userRepository;
            _registrationControllerLogger = registrationControllerLogger;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<ActionResult> Registration(RegistrationRequest registrationRequest)
        {
            try
            {
                var added = await _userRepository.AddUserAsync(
                                registrationRequest.Username, registrationRequest.Password);

                if (added)
                {
                    return Created(string.Empty,
                        new RegistrationResponse
                        { Username = registrationRequest.Username, StatusCode = "c_001" });
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _registrationControllerLogger.LogError(
                    message: "Exception occurred while registering the user.", exception: ex);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
