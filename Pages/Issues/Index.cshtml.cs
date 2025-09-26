using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Web.Pages.Issues
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public IndexModel(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public List<Issue> Issues { get; set; } = new();
        public async Task OnGetAsync()
        {
            var client = _httpFactory.CreateClient("MyApi");
            Issues = await client.GetFromJsonAsync<List<Issue>>("api/issues") ?? new List<Issue>();
        }
    }
}
