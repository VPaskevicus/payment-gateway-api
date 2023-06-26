using System.Collections.Concurrent;
using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Repositories
{
    public class InMemoryPaymentDetailsDataStore : IPaymentDetailsRepository
    {
        private readonly ConcurrentDictionary<Guid, PaymentDetails> _inMemoryPaymentDetailsDataStore;

        public InMemoryPaymentDetailsDataStore()
        {
            _inMemoryPaymentDetailsDataStore = new ConcurrentDictionary<Guid, PaymentDetails>();
        }


        public Task<bool> AddPaymentDetailsAsync(Guid paymentId, PaymentDetails paymentDetails)
        {
            try
            {
                var result = _inMemoryPaymentDetailsDataStore.TryAdd(paymentId, paymentDetails);

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

        public Task<PaymentDetails?> GetPaymentDetailsAsync(Guid? paymentId)
        {
            if (!paymentId.HasValue)
                return Task.FromResult<PaymentDetails?>(null);
            
            try
            {
                var found = _inMemoryPaymentDetailsDataStore.TryGetValue(paymentId.Value, out var result);
                return found ? Task.FromResult(result) : Task.FromResult<PaymentDetails?>(null);
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
