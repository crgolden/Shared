namespace Clarity.Shared
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ServiceBusOptions
    {
        public string SharedAccessKeyName { get; set; }

        public string PrimaryKey { get; set; }

        public string SecondaryKey { get; set; }

        public string Endpoint { get; set; }

        public string EmailQueueName { get; set; }

        public string IndexQueueName { get; set; }
    }
}
