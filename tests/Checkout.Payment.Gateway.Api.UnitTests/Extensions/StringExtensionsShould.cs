using Checkout.Payment.Gateway.Api.Extensions;

namespace Checkout.Payment.Gateway.Api.UnitTests.Extensions
{
    public  class StringExtensionsShould
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("test", "****")]
        [InlineData(null, null)]
        public void MaskString(string? input, string? expectedResult)
        {
            var result = input.Mask();

            result.Should().Be(expectedResult);
        }
    }
}
