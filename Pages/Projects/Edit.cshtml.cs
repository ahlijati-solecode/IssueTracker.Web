using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Web.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public EditModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;
        [BindProperty]
        public Project Project { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpFactory.CreateClient("MyApi");
            Project = await client.GetFromJsonAsync<Project>($"api/projects/{id}") ?? new Project();

            if (Project == null || Project.Id == 0) return RedirectToPage("Index");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.PutAsJsonAsync($"api/projects/{Project.Id}", Project);
            if (resp.IsSuccessStatusCode) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Failed to update project");
            return Page();
        }

    }
}
