using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Checkout.Payment.Gateway.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        private readonly ILogger<AuthenticationController> _authenticationControllerLogger;

        public AuthenticationController(
            IConfiguration configuration, 
            IUserRepository userRepository,
            ILogger<AuthenticationController> authenticationControllerLogger)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _authenticationControllerLogger = authenticationControllerLogger;
        }

        [HttpPost]
        [Route("/authenticate")]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequest authenticationRequest)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(
                    authenticationRequest.Username, authenticationRequest.Password);

                if (user == null)
                {
                    return Unauthorized();
                }

                var userToken = CreateUserToken(user);

                return Ok(userToken);
            }
            catch (Exception ex)
            {
                _authenticationControllerLogger.LogError(
                    message: "Exception occurred while authenticating the user.", exception: ex);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        private string CreateUserToken(User user)
        {
            var securityKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecurityKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new("user_name", user.Username),
                new("user_password", user.Password)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.Now,
                DateTime.Now.AddHours(2),
                signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
