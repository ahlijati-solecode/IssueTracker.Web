using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Web.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public CreateModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        [BindProperty]
        public Category Category { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
                return Page();
            

            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.PostAsJsonAsync("api/categories", Category);
            if (resp.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = await resp.Content.ReadAsStringAsync();
                return RedirectToPage("Index");
            }

            // handle errors (optional: read error content)
            ModelState.AddModelError(string.Empty, "failed");
            return Page();
        }
    }
}
