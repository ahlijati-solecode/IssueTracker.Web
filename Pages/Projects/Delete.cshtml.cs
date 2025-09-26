using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace IssueTracker.Web.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public DeleteModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        [BindProperty]
        public Project Project { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpFactory.CreateClient("MyApi");
            Project = await
                client.GetFromJsonAsync<Project>($"api/projects/{id}") ?? new Project();
            if (Project == null || Project.Id == 0) return RedirectToPage("Index");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.DeleteAsync($"api/projects/{Project.Id}");
            if (resp.IsSuccessStatusCode) return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Failed to delete project");
            return Page();
        }
    }
}
