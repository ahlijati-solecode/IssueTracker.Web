using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Web.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public DeleteModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

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
            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.DeleteAsync($"api/categories/{Category.Id}");
            if (resp.IsSuccessStatusCode) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Failed to delete category.");
            return Page();
        }
    }
}
