using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class OkulBilgiController : Controller
    {
        private readonly IOkulBilgiBE _okulBilgiBE;
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IOkullarBE _okullarBE;

        public OkulBilgiController(IOkulBilgiBE okulBilgiBE, IUlkelerBE ulkelerBE, IOkullarBE okullarBE)
        {
            _okulBilgiBE = okulBilgiBE;
            _ulkelerBE = ulkelerBE;
            _okullarBE = okullarBE;
        }
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetir().Data;

            var requestmodel = _okulBilgiBE.OkulBilgiGetirKullaniciId(user.LoginId);

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult OkulBilgiEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetir().Data;
            return View();
        }

        [HttpPost]
        public IActionResult OkulBilgiEkle(OkulBilgiVM model)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetir().Data;

            //if (OkulId > 0)
            //{
            //    var data = _okulBilgiBE.OkulBilgiGuncelle(model, user);

            //    return RedirectToAction("Index");
            //}
            //else
            //{
            var data = _okulBilgiBE.OkulBilgiEkle(model, user);
            if (data.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(model);
            //}
        }

        public ActionResult Guncelle(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetir().Data;

            if (id > 0)
            {
                var data = _okulBilgiBE.OkulBilgiGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Guncelle(OkulBilgiVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetir().Data;

            //if (id > 0)
            //{
            var data = _okulBilgiBE.OkulBilgiGuncelle(model, user);
            if (data.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

            //}
            //else
            //{
            //    return View();
            //}

        }

        [HttpDelete]
        public IActionResult OkulBilgiSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _okulBilgiBE.OkulBilgiSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }

        public IActionResult OkulBilgileriGetir(int ulkeId)
        {

            if (ulkeId > 0)
            {
                var data = _okulBilgiBE.OkulBilgiGetirUlkeId(ulkeId);
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = _okullarBE.OkullariGetir().Data;

                if (data.IsSuccess)
                {
                    return View(data.Data);
                }

                return View();
            }
            else
            {
                var requestmodel = _okulBilgiBE.OkulBilgileriGetir();
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = _okullarBE.OkullariGetir().Data;

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }

                return View();
            }

        }

        public ActionResult OkulBilgiGetirUlkeId(int Id) 
        {
            var data = _okulBilgiBE.OkulBilgiGetirUlkeId(Id);
            if (data.IsSuccess)
            {
                return Json(new { isSucces = data.IsSuccess, message = data.Message, data = data.Data });
            }
            else
            {
                return RedirectToAction("OkulBilgileriGetir", new { ulkeId = Id });
            }            
        }
       
    }
}
