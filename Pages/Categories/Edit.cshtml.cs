using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Web.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public EditModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        [BindProperty]
        public Category Category { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpFactory.CreateClient("MyApi");
            Category = await client.GetFromJsonAsync<Category>($"api/categories/{id}") ?? new Category();
            if (Category == null || Category.Id == 0) return RedirectToPage("Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.PutAsJsonAsync($"api/categories/{Category.Id}", Category);
            if (resp.IsSuccessStatusCode) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Failed to update category.");
            return Page();
        }
    }
}
