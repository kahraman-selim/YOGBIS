using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SSSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult SSSEkle() 
        {
            return View();
        }
    }
}
