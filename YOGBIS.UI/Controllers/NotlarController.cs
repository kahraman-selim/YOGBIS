using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class NotlarController : Controller
    {
        private readonly INotlarBE _notlarBE;
        public NotlarController(INotlarBE notlarBE)
        {
            _notlarBE = notlarBE;
        }
        public IActionResult Index(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (id > 0)
            {
                var data = _notlarBE.NotGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult NotEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult NotEkle(NotlarVM model, int? NotId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (NotId > 0)
            {
                var data = _notlarBE.NotGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _notlarBE.NotEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }

        [HttpDelete]
        public IActionResult NotSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _notlarBE.NotSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
    }
}
