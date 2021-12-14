using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TemsilciliklerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
