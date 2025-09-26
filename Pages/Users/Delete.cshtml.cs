using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Web.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public DeleteModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        [BindProperty]
        public User User { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var client = _httpFactory.CreateClient("MyApi");
            User = await client.GetFromJsonAsync<User>($"api/users/{id}") ?? new User();
            if (User == null || User.Id == "") return RedirectToPage("Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.DeleteAsync($"api/users/{User.Id}");
            if (resp.IsSuccessStatusCode) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Failed to delete user.");
            return Page();
        }
    }
}
