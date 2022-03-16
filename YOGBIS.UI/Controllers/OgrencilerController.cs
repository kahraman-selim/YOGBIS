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

    public class OgrencilerController : Controller
    {
        
        #region Değişkenler
        private readonly IOgrencilerBE _ogrencilerBE;
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IOkullarBE _okullarBE;
        private readonly ISubelerBE _subelerBE;
        private readonly ISiniflarBE _siniflarBE;
        #endregion

        #region Dönüştürücüler
        public OgrencilerController(IOgrencilerBE ogrencilerBE, IUlkelerBE ulkelerBE, IOkullarBE okullarBE, ISubelerBE subelerBE, ISiniflarBE siniflarBE)
        {
            _ogrencilerBE = ogrencilerBE;
            _ulkelerBE = ulkelerBE;
            _okullarBE = okullarBE;
            _subelerBE = subelerBE;
            _siniflarBE = siniflarBE;
        }
        #endregion

        #region Index
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;
            //ViewBag.Aylar = EnumExtension<EnumAylar>.GetDisplayValue(EnumAylar.Agustos);

            var requestmodel = _ogrencilerBE.OgrenciGetirKullaniciId(user.LoginId);

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View(user);
        }
        #endregion

        #region OgrenciEkleGet
        [Authorize(Roles = "Administrator,SubManager")]
        [HttpGet]
        [Route("Ogrenciler/OGC10002", Name = "OgrenciEkleRoute")]
        public IActionResult OgrenciEkle(Guid OkulId, Guid UlkeId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            var ulkeid = _ulkelerBE.UlkeIdGetir(UlkeId).Data;
            ViewBag.UlkeAdi = _ulkelerBE.UlkeGetir(ulkeid).Data;
            ViewBag.OkulAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;
            ViewBag.SubeAdi = _subelerBE.SubeleriGetirOkulId((Guid)OkulId).Data;
            ViewBag.SinifAdi = _siniflarBE.SiniflariGetirOkulId((Guid)OkulId).Data;

            return View();
        }
        #endregion

        #region OgrenciEklePost
        [Authorize(Roles = "Administrator,SubManager")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Ogrenciler/OGC10002", Name = "OgrenciEkleRoute")]
        public IActionResult OgrenciEkle(OgrencilerVM model, Guid OkulId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));



            if (OkulId != null)
            {
                var data = _ogrencilerBE.OgrenciGuncelle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var data = _ogrencilerBE.OgrenciEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }

            return View();
        }
        #endregion

        #region Guncelle
        [Authorize(Roles = "Administrator,SubManager")]
        [Route("Ogrenciler/OGC10003", Name = "OgrenciGuncelle")]
        public ActionResult Guncelle(Guid? id, Guid UlkeId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            if (id != null)
            {
                var data = _ogrencilerBE.OgrenciGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region OgrenciSil
        [Authorize(Roles = "Administrator,SubManager")]
        [HttpDelete]
        public IActionResult OgrenciSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _ogrencilerBE.OgrenciSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        } 
        #endregion

    }
}
