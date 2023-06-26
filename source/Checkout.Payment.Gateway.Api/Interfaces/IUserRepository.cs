using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(string? registrationRequestUserName, string? registrationRequestPassword);
        Task<User?> GetUserAsync(string userName, string password);
        
    }
}
