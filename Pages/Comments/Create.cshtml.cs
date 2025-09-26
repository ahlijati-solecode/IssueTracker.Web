using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;

namespace IssueTracker.Web.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public CreateModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        [BindProperty]
        public Comment Comments { get; set; } = new();

        public List<SelectListItem> IssueList { get; set; } = new List<SelectListItem>();

        public async Task OnGet()
        {
            var client = _httpFactory.CreateClient("MyApi");
            var issues = await client.GetFromJsonAsync<List<Issue>>("api/issues") ?? new List<Issue>();
            IssueList = issues
                .Select(p => new SelectListItem(p.Title, p.id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var client = _httpFactory.CreateClient("MyApi");

            Comments.IssueId = 1;
            Comments.UserId = "Aryes";
            Comments.CreatedDate = DateTime.Now;
            var resp = await client.PostAsJsonAsync("api/Comments", Comments);

            if (resp.IsSuccessStatusCode) return RedirectToPage("Index");

            //handle errors 
            ModelState.AddModelError(string.Empty, "Failed to Create Comments.");
            return Page();


        }
    }
}
