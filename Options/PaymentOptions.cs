namespace crgolden.Shared
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class PaymentOptions
    {
        public StripeOptions StripeOptions { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class StripeOptions
    {
        public string SecretKey { get; set; }
    }
}