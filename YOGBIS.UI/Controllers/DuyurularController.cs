using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class DuyurularController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
