using IssueTracker.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Security.Claims;

namespace IssueTracker.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        public LoginModel(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        [BindProperty]
        public User Input { get; set; } = new User();

        public string? ErrorMessage { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var client = _httpFactory.CreateClient("MyApi");

            //var response = await client.PostAsJsonAsync("/api/auth/login", Input);
            var response = await client.PostAsync(
                $"/api/auth/login?email={Input.UserName}&password={Input.Password}",
            null);


            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            }

            var user = await response.Content.ReadFromJsonAsync<User>();
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Login failed");
                return Page();
            }

            // Buat claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage("/Dashboard/Index");
        }
    }
}
