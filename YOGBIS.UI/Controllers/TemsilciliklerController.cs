using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TemsilciliklerController : Controller
    {
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly ISSSBE _sSSBE;
        public TemsilciliklerController(ISoruKategorileriBE soruKategorileriBE, IDerecelerBE derecelerBE, ISSSBE sSSBE)
        {
            _soruKategorileriBE = soruKategorileriBE;
            _derecelerBE = derecelerBE;
            _sSSBE = sSSBE;
        }
        public IActionResult Index()
        {
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            return View();
        }
        public IActionResult Test()
        {
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = string.Empty;// _soruKategorileriBE.SoruKategorileriGetir().Data;
            return View();
        }
        
        [HttpGet]
        public IActionResult SSSEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SSSEkle(SSSVM model, Guid? SSSId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (SSSId != null)
            {
                var data = _sSSBE.SSSEkle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _sSSBE.SSSEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        public JsonResult SoruKategoriGetir(Guid dereceId)
        {
            if (dereceId == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _soruKategorileriBE.SoruKategorileriGetirDereceId(dereceId);
            if (data.IsSuccess)
            {
                return Json(new { success = data.IsSuccess, message = data.Message });
                ViewBag.Kategoriler = data;
            }
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
    }
}
