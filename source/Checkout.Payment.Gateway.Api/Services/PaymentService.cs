using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Services
{
    public interface IPaymentService
    {
        Task<PaymentDetailsProcessResult> ProcessPaymentDetailsAsync(PaymentDetails paymentDetails);

        Task<PaymentDetailsProcessResult> GetPaymentDetailsAsync(Guid paymentId);
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


        public async Task<PaymentDetailsProcessResult> ProcessPaymentDetailsAsync(PaymentDetails paymentDetails)
        {
            var acquiringBankResponse = await _acquiringBank.ProcessPaymentAsync(paymentDetails);

            await _paymentRepository.AddPaymentDetailsAsync(acquiringBankResponse.PaymentId, paymentDetails);

            return new PaymentDetailsProcessResult(acquiringBankResponse, paymentDetails);
        }

        public async Task<PaymentDetailsProcessResult> GetPaymentDetailsAsync(Guid paymentId)
        {
            var acquiringBankResponseTask = _acquiringBank.GetPaymentStatusAsync(paymentId);

            var paymentDetailsTask = _paymentRepository.GetPaymentDetailsAsync(paymentId);

            await Task.WhenAll(acquiringBankResponseTask, paymentDetailsTask);

            return new PaymentDetailsProcessResult(acquiringBankResponseTask.Result, paymentDetailsTask.Result);
        }
    }
}
