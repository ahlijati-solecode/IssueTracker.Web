using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IssueTracker.Web.Models;

namespace IssueTracker.Web.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public IndexModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        public List<Models.Project> Projects { get; set; } = new();
        public async Task OnGetAsync()
        {
            var Client = _httpFactory.CreateClient("MyApi");
            Projects = await Client.GetFromJsonAsync<List<Models.Project>>("api/projects") ?? new List<Models.Project>();
        }
    }
}
