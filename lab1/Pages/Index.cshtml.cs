using Azure.Storage.Blobs.Models;
using lab1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab1.Pages
{
    [ValidateAntiForgeryToken]
    public class IndexModel : PageModel
    {
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public IAsyncEnumerable<BlobItem> BlobItems { get; set; } = null!;

        public IndexModel(IAzureBlobStorageService azureBlobStorageService)
        {
            this._azureBlobStorageService = azureBlobStorageService;
        }

        public void OnGet()
        {
            BlobItems = _azureBlobStorageService.GetBlobItemsAsync();
        }

        public async Task<ActionResult> OnPostDeleteAsync(string name)
        {
            await _azureBlobStorageService.DeleteBlobAsync(name);

            BlobItems = _azureBlobStorageService.GetBlobItemsAsync();
            return Page();
        }

        public async Task<ActionResult> OnPostDownloadAsync(string name)
        {
            var stream = await _azureBlobStorageService.GetBlobAsync(name);
            return File(stream, "application/octet-stream", name);
        }
    }
}
