using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DotNetBasics.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync("Identity.Login");
            return RedirectToPage("/Home");
        }
    }
}
