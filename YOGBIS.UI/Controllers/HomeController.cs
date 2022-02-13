using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;

namespace YOGBIS.UI.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUlkelerBE _ulkelerBE;
        public HomeController(ILogger<HomeController> logger, IUlkelerBE ulkelerBE)
        {
            _logger = logger;
            _ulkelerBE = ulkelerBE;
        }
        public IActionResult Index()
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (User.IsInRole(EnumsKullaniciRolleri.Administrator.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Manager.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Follower.ToString()))
            {
                
                var requestmodel = _ulkelerBE.UlkeleriGetir();                

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }

                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Duyurular");
            }
        }
    }
}
