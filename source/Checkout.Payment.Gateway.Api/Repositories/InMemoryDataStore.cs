using System.Collections.Concurrent;
using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Repositories
{
    public class InMemoryDataStore : IPaymentRepository
    {
        private readonly ConcurrentDictionary<Guid, PaymentDetails> _inMemoryDataStore;

        public InMemoryDataStore()
        {
            _inMemoryDataStore = new ConcurrentDictionary<Guid, PaymentDetails>();
        }


        public Task<bool> AddPaymentDetailsAsync(Guid paymentId, PaymentDetails paymentDetails)
        {
            var result = _inMemoryDataStore.TryAdd(paymentId, paymentDetails);

            return Task.FromResult(result);

        }
    }
}
