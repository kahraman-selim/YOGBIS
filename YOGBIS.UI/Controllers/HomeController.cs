using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YOGBIS.Common.ConstantsModels;

namespace YOGBIS.UI.Controllers
{

    [Authorize] //*///(Roles = ResultConstant.Admin_Role)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {

            //var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (User.IsInRole(EnumsKullaniciRolleri.Administrator.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Manager.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Follower.ToString()))
            {
                //return View(user);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Notlar");
            }
        }
    }
}
