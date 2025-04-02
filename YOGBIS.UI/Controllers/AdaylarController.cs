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
    
    public class AdaylarController : Controller
    {
        #region Değişkenler
        private readonly IAdaylarBE _adaylarBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly IUlkeTercihleriBE _ulkeTercihleriBE;
        #endregion

        #region Dönüştürücüler
        public AdaylarController(IAdaylarBE adaylarBE, IKullaniciBE kullaniciBE, IUlkeTercihleriBE ulkeTercihleriBE)
        {
            _adaylarBE = adaylarBE;
            _kullaniciBE = kullaniciBE;
            _ulkeTercihleriBE = ulkeTercihleriBE;
        }
        #endregion

        #region Index
        [Authorize(Roles = "Administrator")]
        [Route("AD10002", Name = "AdaylarIndexRoute")]
        public async Task<IActionResult> Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;

            

            return View();
            
        }
        #endregion

        #region AdayEkle(Get)
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult AdayEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }
        #endregion

        #region AdayEkle(Post)
        [Authorize(Roles = "Administrator")]
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
        [Route("AD10005", Name = "AdayGuncelleRoute")]
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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

        #region AdayNotBilgileri
        [Authorize(Roles = "Administrator, CommissionerHead")]
        [Route("AD10003", Name = "AdayNotBilgileriRoute")]
        public IActionResult AdayNotBilgileri()
        {
            return View();
        } 
        #endregion
    }
}
