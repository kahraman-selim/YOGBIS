﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OkutmanlarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OkutmanDetay() 
        {
            return View();
        }
    }
}
