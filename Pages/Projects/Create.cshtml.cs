using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace IssueTracker.Web.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public CreateModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        [BindProperty]
        public Models.Project Project { get; set; }
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            Project.CreatedDate = DateTime.Now;
            Project.CreatedByUserId = "2";
            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.PostAsJsonAsync("api/projects", Project);
            if (resp.IsSuccessStatusCode) return RedirectToPage("Index");
            //handle errors
            ModelState.AddModelError(string.Empty, "Failed to create project");
            return Page();
        }
    }
}
