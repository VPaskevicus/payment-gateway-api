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


        public Task<bool> AddUserAsync(string userName, string password)
        {
            try
            {
                var result = _inMemoryUserDataStore.TryAdd(userName, new User(userName, password));

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

        public Task<User?> GetUserAsync(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    var found = _inMemoryUserDataStore.TryGetValue(userName, out var user);
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
            return Task.FromResult<User?>(null);
        }
    }
}
