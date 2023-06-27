using Checkout.Payment.Gateway.Api.Contracts.Requests;

namespace Checkout.Payment.Gateway.Api.UnitTests.TestHelpers.Builders
{
    public  class CardDetailsBuilder
    {
        private string? _nameOnCard = "Vladimirs Paskevicus";
        private string? _cardNumber = "1243123412341234";
        private int _expirationMonth = 3;
        private int _expirationYear = 2027;
        private int _securityCode = 555;

        public CardDetailsBuilder WithNameOnCard(string? nameOnCard)
        {
            _nameOnCard = nameOnCard;
            return this;
        }

        public CardDetailsBuilder WithCardNumber(string? cardNumber)
        {
            _cardNumber = cardNumber;
            return this;
        }

        public CardDetailsBuilder WithExpirationMonth(int expirationMonth)
        {
            _expirationMonth = expirationMonth;
            return this;
        }

        public CardDetailsBuilder WithExpirationYear(int expirationYear)
        {
            _expirationYear = expirationYear;
            return this;
        }

        public CardDetailsBuilder WithSecurityCode(int securityCode)
        {
            _securityCode = securityCode;
            return this;
        }

        public CardDetails Create()
        {
            return new CardDetails
            {
                NameOnCard = _nameOnCard,
                CardNumber = _cardNumber,
                ExpirationMonth = _expirationMonth,
                ExpirationYear = _expirationYear,
                SecurityCode = _securityCode
            };
        }
    }
}
