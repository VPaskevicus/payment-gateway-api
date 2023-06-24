using Checkout.Payment.Gateway.Api.Repositories;

namespace Checkout.Payment.Gateway.Api.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(Models.Payment payment);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }


        public async Task<bool> ProcessPaymentAsync(Models.Payment payment)
        {
            return await _paymentRepository.AddPaymentAsync(payment);
        }
    }
}
