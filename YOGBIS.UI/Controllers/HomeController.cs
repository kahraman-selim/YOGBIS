using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.Common.ConstantsModels;

namespace YOGBIS.UI.Controllers
{
    //[Authorize] //(Roles = ResultConstant.Admin_Role)]
    public class HomeController : Controller
    {
       public IActionResult Index()
        {
            return View();
        }
    }
}
