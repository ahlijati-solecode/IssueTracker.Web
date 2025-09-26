using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IssueTracker.Web.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Cryptography.X509Certificates;

namespace IssueTracker.Web.Pages.Comments
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public EditModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

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
            if (!ModelState.IsValid) return Page();

            var client = _httpFactory.CreateClient("MyApi");
            var resp = await client.PutAsJsonAsync($"api/Comments/{Comment.Id}", Comment);
            if (resp.IsSuccessStatusCode) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Failed to update commenets.");
            return Page();
        }

    }
}
