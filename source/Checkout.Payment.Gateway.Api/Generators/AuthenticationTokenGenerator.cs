using Checkout.Payment.Gateway.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Checkout.Payment.Gateway.Api.Generators
{
    public interface IAuthenticationTokenGenerator
    {
        string GenerateToken(User user);
    }

    public class AuthenticationTokenGenerator : IAuthenticationTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public AuthenticationTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var securityKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecurityKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new("username", user.Username),
                new("password", user.Password)
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
