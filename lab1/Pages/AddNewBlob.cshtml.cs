using lab1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab1.Pages
{
    [ValidateAntiForgeryToken]
    public class AddNewBlobModel : PageModel
    {
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public AddNewBlobModel(IAzureBlobStorageService azureBlobStorageService)
        {
            this._azureBlobStorageService = azureBlobStorageService;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public string Name { get; set; } = null!;

        [BindProperty]
        public IFormFile FormFile { get; set; } = null!;

        public async Task<IActionResult> OnPostAsync()
        {
            await _azureBlobStorageService.AddBlobAsync(FormFile.OpenReadStream(), Name);
            return RedirectToPage("Index");
        }
    }
}
