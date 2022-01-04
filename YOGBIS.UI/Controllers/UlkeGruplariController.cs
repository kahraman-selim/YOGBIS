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
    [Authorize(Roles = "Administrator,Manager")]
    public class UlkeGruplariController : Controller
    {
        private readonly IUlkeGruplariBE _ulkeGruplariBE;
        private readonly IKitalarBE _kitalarBE;
        public UlkeGruplariController(IUlkeGruplariBE ulkeGruplariBE, IKitalarBE kitalarBE)
        {
            _ulkeGruplariBE = ulkeGruplariBE;
            _kitalarBE = kitalarBE;
        }
        public IActionResult Index(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;

            if (id > 0)
            {
                var data = _ulkeGruplariBE.UlkeGrupGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult UlkeGrupEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UlkeGrupEkle(UlkeGruplariVM model, int? UlkeGrupId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;

            if (UlkeGrupId > 0)
            {
                var data = _ulkeGruplariBE.UlkeGrupGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _ulkeGruplariBE.UlkeGrupEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }

        [HttpDelete]
        public IActionResult UlkeGrupSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _ulkeGruplariBE.UlkeGrupSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
    }
}
