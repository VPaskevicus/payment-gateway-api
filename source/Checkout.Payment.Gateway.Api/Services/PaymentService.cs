using Checkout.Payment.Gateway.Api.Interfaces;
using Checkout.Payment.Gateway.Api.Models;

namespace Checkout.Payment.Gateway.Api.Services
{
    public interface IPaymentService
    {
        Task<PaymentDetailsProcessResult> ProcessPaymentDetailsAsync(PaymentDetails paymentDetails);

        Task<PaymentDetailsProcessResult> GetPaymentDetailsAsync(Guid? paymentId);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IAcquiringBank _acquiringBank;
        private readonly IPaymentDetailsRepository _paymentDetailsRepository;

        public PaymentService(IAcquiringBank acquiringBank, IPaymentDetailsRepository paymentDetailsRepository)
        {
            _acquiringBank = acquiringBank;
            _paymentDetailsRepository = paymentDetailsRepository;
        }


        public async Task<PaymentDetailsProcessResult> ProcessPaymentDetailsAsync(PaymentDetails paymentDetails)
        {
            var acquiringBankResponse = await _acquiringBank.ProcessPaymentAsync(paymentDetails);

            await _paymentDetailsRepository.AddPaymentDetailsAsync(acquiringBankResponse.PaymentId, paymentDetails);

            return new PaymentDetailsProcessResult(acquiringBankResponse, paymentDetails);
        }

        public async Task<PaymentDetailsProcessResult> GetPaymentDetailsAsync(Guid? paymentId)
        {
            var acquiringBankResponseTask = _acquiringBank.GetPaymentStatusAsync(paymentId);

            var paymentDetailsTask = _paymentDetailsRepository.GetPaymentDetailsAsync(paymentId);

            await Task.WhenAll(acquiringBankResponseTask, paymentDetailsTask);

            return new PaymentDetailsProcessResult(acquiringBankResponseTask.Result, paymentDetailsTask.Result);
        }
    }
}
