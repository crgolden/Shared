namespace Clarity.Shared
{
    public class PaymentOptions
    {
        public StripeOptions StripeOptions { get; set; }
    }

    public class StripeOptions
    {
        public string SecretKey { get; set; }
    }
}