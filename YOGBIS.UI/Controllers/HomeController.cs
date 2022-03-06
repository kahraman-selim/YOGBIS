using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        #region Değişkenler
        private readonly ILogger<HomeController> _logger;
        private readonly IUlkelerBE _ulkelerBE;
        #endregion

        #region Dönüştürücüler
        public HomeController(ILogger<HomeController> logger, IUlkelerBE ulkelerBE)
        {
            _logger = logger;
            _ulkelerBE = ulkelerBE;
        }
        #endregion

        #region Index
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
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
