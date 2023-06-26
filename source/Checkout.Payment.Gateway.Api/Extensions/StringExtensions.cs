namespace Checkout.Payment.Gateway.Api.Extensions
{
    public static class StringExtensions
    {
        public static string? Mask(this string? input)
        {
            return input == null ? input: new string('*', input.Length);
        }
    }
}
