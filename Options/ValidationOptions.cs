namespace Clarity.Shared
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ValidationOptions
    {
        public SmartyStreetsOptions SmartyStreetsOptions { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class SmartyStreetsOptions
    {
        public string AuthId { get; set; }

        public string AuthToken { get; set; }
    }
}