using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Checkout.Payment.Gateway.Api.Contracts.Requests;
using Checkout.Payment.Gateway.Api.Contracts.Responses;
using Checkout.Payment.Gateway.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Checkout.Payment.Gateway.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<ActionResult<string>> Authenticate(RegistrationRequest registrationRequest)
        {
            var added = await _userRepository.AddUserAsync(
                registrationRequest.Username, registrationRequest.Password);

            if (added)
            {
                return Created(string.Empty,
                    new AuthenticateResponse { User = registrationRequest.Username, StatusCode = "c_001" });
            }

            return BadRequest();
        }


        [HttpPost]
        [Route("/authenticate")]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequest authenticationRequest)
        {
            var user = await _userRepository.GetUserAsync(
                authenticationRequest.Username, authenticationRequest.Password);

            if (user == null)
            {
                return Unauthorized();
            }

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

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(token);
        }
    }
}
