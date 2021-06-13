using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YOGBIS.Common.ConstantsModels;

namespace YOGBIS.UI.Areas.Identity.Pages.Account
{
    //[AllowAnonymous]
    [Authorize(Roles = ResultConstant.Admin_Role)]
    public class ForgotPasswordConfirmation : PageModel
    {
        public void OnGet()
        {
        }
    }
}
