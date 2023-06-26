using System.Collections.Concurrent;
using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Repositories
{
    public class InMemoryUserDataStore : IUserRepository
    {
        private readonly ConcurrentDictionary<string, User> _inMemoryUserDataStore;

        public InMemoryUserDataStore()
        {
            _inMemoryUserDataStore = new ConcurrentDictionary<string, User>();
        }

        public Task<bool> AddUserAsync(string? username, string? password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) 
                return Task.FromResult(false);
            
            try
            {
                var result = _inMemoryUserDataStore.TryAdd(username, new User(username, password));

                return Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // add code to track dependency using telemetry client
            }
        }

        public Task<User?> GetUserAsync(string? username, string? password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) 
                return Task.FromResult<User?>(null);
            
            try
            {
                var found = _inMemoryUserDataStore.TryGetValue(username, out var user);
                if (found && user!.Password.Equals(password))
                {
                    return Task.FromResult<User?>(user);
                }

                return Task.FromResult<User?>(null);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // add code to track dependency using telemetry client
            }
        }
    }
}
