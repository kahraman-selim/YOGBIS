using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AdaylarController : Controller
    {
        #region Değişkenler
        private readonly IAdaylarBE _adaylarBE;
        private readonly IKullaniciBE _kullaniciBE;
        #endregion

        #region Dönüştürücüler
        public AdaylarController(IAdaylarBE adaylarBE, IKullaniciBE kullaniciBE)
        {
            _adaylarBE = adaylarBE;
            _kullaniciBE = kullaniciBE;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;

            if (id != null)
            {
                var data = _adaylarBE.AdayGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region AdayEkle(Get)
        [HttpGet]
        public IActionResult AdayEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }
        #endregion

        #region AdayEkle(Post)
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AdayEkle(AdaylarVM model, Guid? AdayId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (AdayId != null)
            {
                var data = _adaylarBE.AdayGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _adaylarBE.AdayEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        #endregion

        #region Guncelle
        public ActionResult Guncelle(Guid? id)
        {

            if (id != null)
            {
                var data = _adaylarBE.AdayGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region AdaySil
        [HttpDelete]
        public IActionResult AdaySil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _adaylarBE.AdaySil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        } 
        #endregion
    }
}
