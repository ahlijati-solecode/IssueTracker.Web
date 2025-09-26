using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IssueTracker.Web.Pages.Issues
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public CreateModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;
        public List<SelectListItem> StatusList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> PriorityList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ProjectList { get; set; } = new List<SelectListItem>();
        public async Task OnGet()
        {

            StatusList = new List<SelectListItem>
            {
                new SelectListItem("Open", "Open"),
                new SelectListItem("In Progress", "In Progress"),
                new SelectListItem("Closed", "Closed")
            };

            PriorityList = new List<SelectListItem>
            {
                new SelectListItem("Low", "Low"),
                new SelectListItem("Medium", "Medium"),
                new SelectListItem("High", "High")
            };

            var client = _httpFactory.CreateClient("MyApi");
            var projects = await client.GetFromJsonAsync<List<Project>>("api/projects") ?? new List<Project>();
            ProjectList = projects
                .Select(p => new SelectListItem(p.Name, p.Id.ToString()))
                .ToList();


        }
    }
}
