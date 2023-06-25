using Checkout.Payment.Gateway.Api.Extensions;
using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;
using Checkout.Payment.Gateway.Api.Repositories;

namespace Checkout.Payment.Gateway.Api.Services
{
    public interface IPaymentService
    {
        Task<PaymentProcessResult> ProcessPaymentAsync(Models.Payment payment);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IAcquiringBank _acquiringBank;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IAcquiringBank acquiringBank, IPaymentRepository paymentRepository)
        {
            _acquiringBank = acquiringBank;
            _paymentRepository = paymentRepository;
        }


        public async Task<PaymentProcessResult> ProcessPaymentAsync(Models.Payment payment)
        {
            var response = await _acquiringBank.ProcessPaymentAsync(payment);

            payment.SetPaymentId(response.PaymentId);

            await _paymentRepository.AddPaymentAsync(payment);

            return new PaymentProcessResult(response, payment);
        }
    }
}
