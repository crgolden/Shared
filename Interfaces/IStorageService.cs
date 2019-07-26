namespace crgolden.Shared
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IStorageService
    {
        Task<Uri> UploadStreamToStorageAsync(
            Stream stream,
            string fileName,
            string containerName,
            CancellationToken token);

        Task<Uri> UploadByteArrayToStorageAsync(
            byte[] buffer,
            string fileName,
            string containerName,
            CancellationToken token);

        string GetSharedAccessSignature(
            string fileName,
            string containerName);

        Task<bool> DeleteFileFromStorageAsync(Uri blobUri, CancellationToken token);

        Task DeleteAllFromStorageAsync(string containerName, CancellationToken token);
    }
}