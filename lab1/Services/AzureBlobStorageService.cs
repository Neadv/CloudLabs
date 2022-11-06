using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using lab1.Models;

namespace lab1.Services
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _client;

        public AzureBlobStorageService(AzureStorageSettings azureStorageSettings)
        {
            if (azureStorageSettings.UseDevEmulator)
            {
                _blobServiceClient = new BlobServiceClient("UseDevelopmentStorage=true");
            }
            else
            {
                if (string.IsNullOrEmpty(azureStorageSettings.ConnectionString))
                    throw new ArgumentNullException(nameof(azureStorageSettings.ConnectionString));

                _blobServiceClient = new BlobServiceClient(azureStorageSettings.ConnectionString);
            }

            _client = _blobServiceClient.GetBlobContainerClient(azureStorageSettings.ContainerName);
            _client.CreateIfNotExists();
        }

        public async Task AddBlobAsync(Stream blob, string name)
        {
            var blobClient = _client.GetBlobClient(name);
            await blobClient.UploadAsync(blob);
        }

        public async Task DeleteBlobAsync(string name)
        {
            var blobClient = _client.GetBlobClient(name);
            await blobClient.DeleteAsync();
        }

        public async Task<Stream> GetBlobAsync(string name)
        {
            var blobClient = _client.GetBlobClient(name);

            var result = new MemoryStream();
            await blobClient.DownloadToAsync(result);
            result.Seek(0, SeekOrigin.Begin);

            return result;
        }

        public IAsyncEnumerable<BlobItem> GetBlobItemsAsync()
        {
            return _client.GetBlobsAsync();
        }
    }
}
