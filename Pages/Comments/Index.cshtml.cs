using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace IssueTracker.Web.Pages.Comments
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public IndexModel(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }
        public List<Comment> Comments { get; set; } = new();
        public async Task OnGetAsync()
        {
            var client = _httpFactory.CreateClient("MyApi");
            Comments = await client.GetFromJsonAsync<List<Comment>>("api/Comments") ?? new List<Comment>();
        }
    }
}
