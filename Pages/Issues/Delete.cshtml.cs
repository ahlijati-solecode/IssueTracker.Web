using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Web.Pages.Issues
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public DeleteModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        [BindProperty]
        public Issue Issue { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpFactory.CreateClient("MyApi");
            Issue = await client.GetFromJsonAsync<Issue>($"api/issues/{id}") ?? new Issue();

            if (Issue == null || Issue.id == 0)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.DeleteAsync($"api/issues/{Issue.id}");

            if (resp.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Issue deleted successfully!";
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Failed to delete issue");

            return Page();
        }
    }
}
