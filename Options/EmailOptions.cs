namespace crgolden.Shared
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class EmailOptions
    {
        public SendGridOptions SendGridOptions { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class SendGridOptions
    {
        public string ApiKey { get; set; }
    }
}