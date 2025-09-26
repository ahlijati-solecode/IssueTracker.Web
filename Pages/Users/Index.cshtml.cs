using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Web.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public IndexModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        public List<User> Users { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpFactory.CreateClient("MyApi");
            Users = await client.GetFromJsonAsync<List<User>>("api/users") ?? new List<User>();
        }
    }
}
