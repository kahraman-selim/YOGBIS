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
        private readonly IKomisyonlarBE _komisyonlarBE;
        #endregion

        #region Dönüştürücüler
        public AdaylarController(IAdaylarBE adaylarBE, IKullaniciBE kullaniciBE, IUlkeTercihleriBE ulkeTercihleriBE, IKomisyonlarBE komisyonlarBE)
        {
            _adaylarBE = adaylarBE;
            _kullaniciBE = kullaniciBE;
            _ulkeTercihleriBE = ulkeTercihleriBE;
            _komisyonlarBE = komisyonlarBE;
        }
        #endregion

        #region Index        
        [Route("AD10002", Name = "AdaylarIndexRoute")]
        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            
            //var komisyon = await _kullaniciBE.KomisyonGetir();
            var komisyon = _komisyonlarBE.KomisyonAdlariGetir();
            ViewBag.Komisyonlar = komisyon.Data;

            return View();
            
        }
        #endregion

        #region AdayEkle(Get)        
        [HttpGet]
        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult AdayEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            
            //var komisyon = await _kullaniciBE.KomisyonGetir();
            var komisyon = _komisyonlarBE.KomisyonAdlariGetir();
            ViewBag.Komisyonlar = komisyon.Data;
            
            return View();
        }
        #endregion

        #region AdayEkle(Post)        
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult AdayEkle(AdayMYSSVM model, Guid? Id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            
            //var komisyon = await _kullaniciBE.KomisyonGetir();
            var komisyon = _komisyonlarBE.KomisyonAdlariGetir();
            ViewBag.Komisyonlar = komisyon.Data;

            if (Id != null)
            {
                var data = _adaylarBE.AdayMYSSGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                //var data = _adaylarBE.AdayMYSSGuncelle(model, user);
                //if (data.IsSuccess)
                //{
                //    return RedirectToAction("Index");
                //}
                return View(model);
            }
        }
        #endregion

        #region Guncelle
        [Route("AD10005", Name = "AdayGuncelleRoute")]     
        [Authorize(Roles = "Administrator,Manager")]
        public ActionResult Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            
            //var komisyon = await _kullaniciBE.KomisyonGetir();
            var komisyon = _komisyonlarBE.KomisyonAdlariGetir();
            ViewBag.Komisyonlar = komisyon.Data;

            if (id != null)
            {
                var data = _adaylarBE.MYSSAdayGetir((Guid)id);
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
        [Authorize(Roles = "Administrator,Manager")]
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
        [Authorize(Roles = "Administrator, CommissionerHead, Manager")]
        [Route("AD10003", Name = "AdayNotBilgileriRoute")]
        public IActionResult AdayNotBilgileri()
        {
            return View();
        } 
        #endregion
    }
}
