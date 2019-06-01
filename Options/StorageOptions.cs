namespace crgolden.Shared
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class StorageOptions
    {
        public string ImageContainer { get; set; }

        public string ThumbnailContainer { get; set; }

        public AzureBlobStorageOptions AzureBlobStorageOptions { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class AzureBlobStorageOptions
    {
        public string AccountName { get; set; }

        public string AccountKey { get; set; }
    }
}
