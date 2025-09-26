using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IssueTracker.Web.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IssueTracker.Web.Pages.Comments
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public DeleteModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        [BindProperty]
        public Comment Comment { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int Id)
        {
            var client = _httpFactory.CreateClient("MyApi");
            Comment = await client.GetFromJsonAsync<Comment>($"Api/Comments/{Id}") ?? new Comment();
            if (Comment == null || Comment.Id == 0) return RedirectToPage("Index");
            return Page();


        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.DeleteAsync($"Api/Comments/{Comment.Id}");
            if (resp.IsSuccessStatusCode) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Failed to delete comments.");
            return Page();
        }
    }
}
