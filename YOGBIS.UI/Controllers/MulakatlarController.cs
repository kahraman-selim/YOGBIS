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
    [Authorize(Roles = "Administrator,Manager")]
    public class MulakatlarController : Controller
    {
        #region Değişkenler
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly IKullaniciBE _kullaniciBE;
        #endregion

        #region Dönüştücüler
        public MulakatlarController(IMulakatOlusturBE mulakatOlusturBE, IDerecelerBE derecelerBE, IKullaniciBE kullaniciBE)
        {
            _mulakatOlusturBE = mulakatOlusturBE;
            _derecelerBE = derecelerBE;
            _kullaniciBE = kullaniciBE;
        } 
        #endregion
        
        public IActionResult Index(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (id != null)
            {
                var data = _mulakatOlusturBE.MulakatGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult MulakatEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult MulakatEkle(MulakatlarVM model, Guid? MulakatId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (MulakatId != null)
            {
                var data = _mulakatOlusturBE.MulakatGuncelle(model, user);
                return RedirectToAction("Index");
            }
            else
            {
                var data = _mulakatOlusturBE.MulakatEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }

        public IActionResult Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (id != null)
            {
                var data = _mulakatOlusturBE.MulakatGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }

        [HttpDelete]
        public IActionResult MulakatSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _mulakatOlusturBE.MulakatSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
    }
}
