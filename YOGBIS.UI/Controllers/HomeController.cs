using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Controllers
{

    [Authorize] //*///(Roles = ResultConstant.Admin_Role)]
    public class HomeController : Controller
    {
       public IActionResult Index()
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (User.IsInRole(ResultConstant.Admin_Role))
            {
                return View(user);
                //return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }            
        }
    }
}
