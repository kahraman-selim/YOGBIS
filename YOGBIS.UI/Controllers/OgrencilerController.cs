using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.Extentsion;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    //[Authorize]
    public class OgrencilerController : Controller
    {
        private readonly IOgrencilerBE _ogrencilerBE;
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IOkullarBE _okullarBE;

        public OgrencilerController(IOgrencilerBE ogrencilerBE, IUlkelerBE ulkelerBE, IOkullarBE okullarBE)
        {
            _ogrencilerBE = ogrencilerBE;
            _ulkelerBE = ulkelerBE;
            _okullarBE = okullarBE;
        }
        
        [Authorize(Roles = "Administrator,Manager,Teacher")]
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

        [Authorize(Roles = "Administrator,Manager,Teacher")]
        [HttpGet]
        public IActionResult OgrenciEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;
            return View();
        }

        [Authorize(Roles = "Administrator,Manager,Teacher")]
        [HttpPost]
        public IActionResult OgrenciEkle(OgrencilerVM model)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            var data = _ogrencilerBE.OgrenciEkle(model, user);
            if (data.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator,Manager,Teacher")]
        public ActionResult Guncelle(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            if (id > 0)
            {
                var data = _ogrencilerBE.OgrenciGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }

        [Authorize(Roles = "Administrator,Manager,Teacher")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Guncelle(OgrencilerVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            var data = _ogrencilerBE.OgrenciGuncelle(model, user);
            if (data.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator,Manager,Teacher")]
        [HttpDelete]
        public IActionResult OgrenciSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _ogrencilerBE.OgrenciSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }

        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult OgrenciGetir(int? ulkeId)
        {            
            
            if (ulkeId > 0)
            {
                var data = _ogrencilerBE.OgrenciGetirUlkeId(ulkeId);
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;                

                if (data.IsSuccess)
                {
                    return View(data.Data);
                }

                return View();
            }
            else
            {
                var requestmodel = _ogrencilerBE.OgrencileriGetir();
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = _okullarBE.OkullariGetir().Data;

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }

                return View();
            }

        }

        [Authorize(Roles = "Administrator,Manager")]
        public ActionResult OgrencileriGetirUlkeId(int? ulkeId) 
        {
            var data = _ogrencilerBE.OgrenciGetirUlkeId(ulkeId);
            if (data.IsSuccess)
            {
                return Json(new { isSucces = data.IsSuccess, message = data.Message, data = data.Data });
            }
            else
            {
                return RedirectToAction("OgrenciGetir", new { ulkeId = ulkeId});
            }            
        }
        
        [Authorize(Roles = "Administrator,Manager")]
        public ActionResult OgrencileriGetirOkulId(int? OkulId)
        {
            var data = _ogrencilerBE.OgrenciGetirOkulId(OkulId);
            if (data.IsSuccess)
            {
                return Json(new { isSucces = data.IsSuccess, message = data.Message, data = data.Data });
            }
            else
            {
                return RedirectToAction("OgrenciGetir", new { okulKodu = OkulId });
            }
        }

        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult OgrencileriGetir(int? ulkeId)
        {

            if (ulkeId > 0)
            {
                var data = _ogrencilerBE.OgrenciGetirUlkeId(ulkeId);
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;
                //ViewBag.Aylar = EnumExtension<EnumAylar>.GetDisplayValues((EnumAylar)int.Parse(EnumAylar).ToString()); //GetDisplayValue(EnumAylar).ToList();

                if (data.IsSuccess)
                {
                    return View(data.Data);
                }

                return View();
            }
            else
            {
                var requestmodel = _ogrencilerBE.OgrencileriGetir();
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;                

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }

                return View();
            }

        }

    }
}
