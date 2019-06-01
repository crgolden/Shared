namespace crgolden.Shared
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class IndexOptions
    {
        public ElasticsearchOptions ElasticsearchOptions { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ElasticsearchOptions
    {
        public string AdminUsername { get; set; }

        public string AdminPassword { get; set; }

        public string ReadUsername { get; set; }

        public string ReadPassword { get; set; }

        public string KibanaUsername { get; set; }

        public string KibanaPassword { get; set; }

        public string LogstashUsername { get; set; }

        public string LogstashPassword { get; set; }

        public string BeatsUsername { get; set; }

        public string BeatsPassword { get; set; }

        public string[] DataNodes { get; set; }

        public string[] LogNodes { get; set; }
    }
}
