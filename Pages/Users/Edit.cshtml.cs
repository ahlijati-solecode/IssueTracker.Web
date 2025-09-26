using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IssueTracker.Web.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public EditModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        [BindProperty]
        public User User { get; set; } = new();
        public List<SelectListItem> RoleList { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            RoleList = new List<SelectListItem>
            {
                new SelectListItem("Admin", "Admin"),
                new SelectListItem("Manager", "Manager"),
                new SelectListItem("Developer", "Developer")
            };
            var client = _httpFactory.CreateClient("MyApi");
            User = await client.GetFromJsonAsync<User>($"api/users/{id}") ?? new User();
            if (User == null || User.Id == "") return RedirectToPage("Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.PutAsJsonAsync($"api/users/{User.Id}", User);
            if (resp.IsSuccessStatusCode) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Failed to update users.");
            return Page();
        }
    }
}
