using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class EtkinlikController : Controller
    {
        private readonly IEtkinliklerBE _etkinliklerBE;
        private readonly IOkullarBE _okullarBE;
        private readonly IUlkelerBE _ulkelerBE;
        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;

        [Obsolete]
        public EtkinlikController(IEtkinliklerBE etkinliklerBE, IOkullarBE okullarBE, IUlkelerBE ulkelerBE, IWebHostEnvironment hostingEnvironment)
        {
            _etkinliklerBE = etkinliklerBE;
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

            var requestmodel = _etkinliklerBE.EtkinlikGetirKullaniciId(user.LoginId);

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View(user);
        }

        [Authorize(Roles = "Administrator,Manager,Teacher")]
        [HttpGet]
        [Route("Etkinlikler")]
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
        public async Task<IActionResult> EtkinlikEkle(EtkinliklerVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            if (ModelState.IsValid)
            {
                if (model.EtkinlikKapakResim != null)
                {
                    string dosyalar = "img/EtkinlikKapakFoto";
                    model.EtkinlikKapakResimUrl = await DosyaYukle(dosyalar, model.EtkinlikKapakResim);
                }

                if (model.FotoGaleri != null)
                {
                    string dosyalar = "img/EtkinlikFoto";
                    model.FotoGaleri = new List<FotoGaleriVM>();
                    foreach (var dosya in model.FotoGaleri)
                    {
                        var galeri = new FotoGaleriVM()
                        {
                            FotoAdi = dosya.FotoAdi,
                            FotoURL = await DosyaYukle(dosyalar, dosya)

                        };
                        model.FotoGaleri.Add(galeri);
                    }
                }

                if (model.EtkinlikDosya != null)
                {
                    string dosyalar = "img/EtkinlikDosyalar";
                    model.EtkinlikDosyaUrl = await DosyaYukle(dosyalar, model.EtkinlikDosya);
                }

                var data = _etkinliklerBE.EtkinlikEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }

            return View();
        }

        private Task<string> DosyaYukle(string dosyalar, FotoGaleriVM dosya)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Administrator,Manager,Teacher")]
        public ActionResult Guncelle(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            if (id > 0)
            {
                var data = _etkinliklerBE.EtkinlikGetir((int)id);
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
        public ActionResult Guncelle(EtkinliklerVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            var data = _etkinliklerBE.EtkinlikGuncelle(model, user);
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

            var data = _etkinliklerBE.EtkinlikSil(id);
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
                var data = _etkinliklerBE.EtkinlikGetirOkulId(OkulId);
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
                var requestmodel = _etkinliklerBE.EtkinlikleriGetir();
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
            var data = _etkinliklerBE.EtkinlikGetirOkulId(Id);
            if (data.IsSuccess)
            {
                return Json(new { isSucces = data.IsSuccess, message = data.Message, data = data.Data });
            }
            else
            {
                return RedirectToAction("EtkinliklerGetirOkulId", new { okulId = Id });
            }
        }

        [Authorize(Roles = "Administrator,Manager")]
        public ActionResult Detay(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            if (id > 0)
            {
                var data = _etkinliklerBE.EtkinlikGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }

        [Obsolete]
        private async Task<string> DosyaYukle(string dosyaYolu, IFormFile dosya) 
        {
            dosyaYolu += Guid.NewGuid().ToString() + "_" + dosya.FileName;
            string Dosyalar = Path.Combine(_hostingEnvironment.WebRootPath, dosyaYolu);
            await dosya.CopyToAsync(new FileStream(Dosyalar, FileMode.Create));
            return "/" + dosyaYolu;
        }
    }
}
