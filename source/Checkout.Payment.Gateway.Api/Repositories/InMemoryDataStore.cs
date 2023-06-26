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
            try
            {
                var result = _inMemoryDataStore.TryAdd(paymentId, paymentDetails);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                // add code to track dependency using telemetry client
            }
        }

        public Task<PaymentDetails?> GetPaymentDetailsAsync(Guid? paymentId)
        {
            if (paymentId.HasValue)
            {
                try
                {
                    var found = _inMemoryDataStore.TryGetValue(paymentId.Value, out var result);
                    return found ? Task.FromResult(result) : Task.FromResult<PaymentDetails?>(null);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    // add code to track dependency using telemetry client
                }

            }
            return Task.FromResult<PaymentDetails?>(null);
        }
    }
}
