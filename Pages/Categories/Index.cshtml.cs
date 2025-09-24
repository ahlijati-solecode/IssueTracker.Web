using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Web.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public IndexModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        public List<Category> Categories { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpFactory.CreateClient("MyApi");
            Categories = await client.GetFromJsonAsync<List<Category>>("api/categories") ?? new List<Category>();
        }
    }
}
