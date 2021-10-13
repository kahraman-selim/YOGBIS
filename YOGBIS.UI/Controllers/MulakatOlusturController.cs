using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MulakatOlusturController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
