using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YOGBIS.UI.Areas.Identity.Pages.Account
{

    [Authorize(Roles = "Administrator")]
    public class ForgotPasswordConfirmation : PageModel
    {
        public void OnGet()
        {
        }
    }
}
