using Azure.Storage.Blobs.Models;

namespace lab1.Services
{
    public interface IAzureBlobStorageService
    {
        Task AddBlobAsync(Stream blob, string name);
        Task<Stream> GetBlobAsync(string name);
        Task DeleteBlobAsync(string name);
        IAsyncEnumerable<BlobItem> GetBlobItemsAsync();
    }
}
