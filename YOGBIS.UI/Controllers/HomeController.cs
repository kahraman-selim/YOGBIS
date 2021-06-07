using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.Common.ConstantsModels;

namespace YOGBIS.UI.Controllers
{
    /*[Authorize] *///(Roles = ResultConstant.Admin_Role)]
    [Authorize(Roles = ResultConstant.Admin_Role)]
    public class HomeController : Controller
    {
       public IActionResult Index()
        {
            if (User.IsInRole(ResultConstant.Admin_Role))
                return RedirectToAction("Index", "Kullanicilar");
            if (User.IsInRole(ResultConstant.Kullanici_Role))
                return RedirectToAction("Index", "Kullanicilar");

            return View();
        }
    }
}
