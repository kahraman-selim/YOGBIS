using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YOGBIS.Common.ConstantsModels;

namespace YOGBIS.UI.Areas.Identity.Pages.Account
{
    
    [Authorize(Roles = "Administrator")]
    public class LockoutModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
