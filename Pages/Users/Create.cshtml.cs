using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IssueTracker.Web.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public CreateModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        [BindProperty]
        public User User { get; set; } = new();
        public List<SelectListItem> RoleList { get; set; } = new List<SelectListItem>();

        public void OnGet()
        {
            RoleList = new List<SelectListItem>
            {
                new SelectListItem("Admin", "Admin"),
                new SelectListItem("Manager", "Manager"),
                new SelectListItem("Developer", "Developer")
            };
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.PostAsJsonAsync("api/users", User);
            if (resp.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Issue created successfully!";
                return RedirectToPage("Index");
            }


            // handle errors (optional: read error content)
            ModelState.AddModelError(string.Empty, "Failed to create issue.");
            return Page();
        }
    }
}
