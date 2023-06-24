using System.Collections.Concurrent;

namespace Checkout.Payment.Gateway.Api.Repositories
{
    public class InMemoryDataStore : IPaymentRepository
    {
        private readonly ConcurrentDictionary<Guid, Models.Payment> _inMemoryStore;

        public InMemoryDataStore()
        {
            _inMemoryStore = new ConcurrentDictionary<Guid, Models.Payment>();
        }


        public Task<bool> AddPaymentAsync(Models.Payment payment)
        {
            var result = _inMemoryStore.TryAdd(payment.PaymentId, payment);

            return Task.FromResult(result);

        }
    }
}
