using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class SSSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
