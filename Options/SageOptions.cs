namespace crgolden.Shared
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SageOptions
    {
        public string Path { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Company { get; set; }
    }
}
