using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(string? registrationRequestUsername, string? registrationRequestPassword);
        Task<User?> GetUserAsync(string username, string password);
        
    }
}
