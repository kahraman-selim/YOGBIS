using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class EtkinlikController : Controller
    {
        private readonly IAktivitelerBE _aktivitelerBE;
        private readonly IOkullarBE _okullarBE;
        private readonly IUlkelerBE _ulkelerBE;
        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;

        [Obsolete]
        public EtkinlikController(IAktivitelerBE aktivitelerBE, IOkullarBE okullarBE, IUlkelerBE ulkelerBE, IWebHostEnvironment hostingEnvironment)
        {
            _aktivitelerBE = aktivitelerBE;
            _okullarBE = okullarBE;
            _ulkelerBE = ulkelerBE;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "Administrator,Manager,Teacher,Follower")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            var requestmodel = _aktivitelerBE.EtkinlikGetirKullaniciId(user.LoginId);

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View(user);
        }

        [Authorize(Roles = "Administrator,Manager,Teacher")]
        [HttpGet]
        public IActionResult EtkinlikEkle() 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;
            return View();
        }

        [Authorize(Roles = "Administrator,Manager,Teacher")]
        [HttpPost]
        [Obsolete]
        public IActionResult EtkinlikEkle(AktivitelerVM model, string etkinlikresimdosya) 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            //string etkinlikresimdosyaup = null;
            //if (model.Resim1 != null)
            //{
            //    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
            //    etkinlikresimdosya = Guid.NewGuid().ToString() + "-" + model.Resim1.FileName;
            //    string dosyaYolu = Path.Combine(uploadsFolder, etkinlikresimdosya);
            //    model.Resim1.CopyTo(new FileStream(dosyaYolu, FileMode.Create));
            //}

            //var data = _aktivitelerBE.EtkinlikEkle(model, user, etkinlikresimdosya);
            //if (data.IsSuccess)
            //{
            //    return RedirectToAction("Index");
            //}
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
                var data = _aktivitelerBE.EtkinlikGetir((int)id);
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
        public ActionResult Guncelle(AktivitelerVM model, string etkinlikresimdosya)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            var data = _aktivitelerBE.EtkinlikGuncelle(model, user, etkinlikresimdosya);
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
        public IActionResult EtkinlikSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _aktivitelerBE.EtkinlikSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }

        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult Etkinlikler(int OkulId)
        {

            if (OkulId > 0)
            {
                var data = _aktivitelerBE.EtkinlikGetirOkulId(OkulId);
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
                var requestmodel = _aktivitelerBE.EtkinlikleriGetir();
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = _okullarBE.OkullariGetir().Data;

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }

                return View();
            }

        }
        public ActionResult EtkinlikGetirOkulId(int Id)
        {
            var data = _aktivitelerBE.EtkinlikGetirOkulId(Id);
            if (data.IsSuccess)
            {
                return Json(new { isSucces = data.IsSuccess, message = data.Message, data = data.Data });
            }
            else
            {
                return RedirectToAction("EtkinliklerGetirOkulId", new { okulId = Id });
            }
        }
        
    }
}
