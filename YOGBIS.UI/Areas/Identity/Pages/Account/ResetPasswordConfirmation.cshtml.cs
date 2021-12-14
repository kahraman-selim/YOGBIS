using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YOGBIS.UI.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Administrator")]
    public class ResetPasswordConfirmationModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
