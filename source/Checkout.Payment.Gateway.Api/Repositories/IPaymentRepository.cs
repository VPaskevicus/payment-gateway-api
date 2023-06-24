namespace Checkout.Payment.Gateway.Api.Repositories
{
    public interface IPaymentRepository
    {
        Task<bool> AddPaymentAsync(Models.Payment payment);
    }
}
