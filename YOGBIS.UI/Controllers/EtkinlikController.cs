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
        
        #region Değişkenler
        private readonly IEtkinliklerBE _etkinliklerBE;
        private readonly IOkullarBE _okullarBE;
        private readonly IUlkelerBE _ulkelerBE;
        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;
        #endregion

        #region Dönüştürücüler
        [Obsolete]
        public EtkinlikController(IEtkinliklerBE etkinliklerBE, IOkullarBE okullarBE, IUlkelerBE ulkelerBE, IWebHostEnvironment hostingEnvironment)
        {
            _etkinliklerBE = etkinliklerBE;
            _okullarBE = okullarBE;
            _ulkelerBE = ulkelerBE;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Index
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
        #endregion

        #region EtkinlikEkleGet
        [HttpGet]
        [Route("Etkinlikler")]
        public IActionResult EtkinlikEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;
            return View();
        }
        #endregion

        #region EtkinlikEklePost
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> EtkinlikEkle(EtkinliklerVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;

            if (ModelState.IsValid)
            {
                if (model.EtkinlikKapakResim != null)
                {
                    string klasorler = "img/EtkinlikKapakFoto";
                    model.EtkinlikKapakResimUrl = FotoYukle(klasorler, model.EtkinlikKapakResim);
                }

                if (model.FotoGaleri != null)
                {
                    string fotoklasorler = "img/EtkinlikFoto";
                    model.FotoGaleri = new List<FotoGaleriVM>();

                    foreach (var file in model.FotoGaleris)
                    {
                        var galeri = new FotoGaleriVM()
                        {
                            FotoAdi = file.FileName,
                            FotoURL = FotoYukle(fotoklasorler, file),
                            KaydedenId=user.LoginId,
                            KayitTarihi=model.KayitTarihi

                        };
                        model.FotoGaleri.Add(galeri);
                    }
                }

                //if (model.EtkinlikDosya != null)
                //{
                //    string dosyalar = "img/EtkinlikDosyalar";
                //    model.EtkinlikDosyaUrl = await DosyaYukle(dosyalar, model.EtkinlikDosya);
                //}

                var data = _etkinliklerBE.EtkinlikEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }

            return View();
        }
        #endregion

        #region GuncelleGet
        public ActionResult Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;

            if (id != null)
            {
                var data = _etkinliklerBE.EtkinlikGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region GuncellePost
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Guncelle(EtkinliklerVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;

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
        #endregion

        #region EtkinlikSil
        [HttpDelete]
        public IActionResult EtkinlikSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _etkinliklerBE.EtkinlikSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

        #region Etkinlikler
        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult Etkinlikler(Guid OkulId)
        {

            if (OkulId != null)
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
        #endregion

        #region EtkinlikGetirOkulId
        [Authorize(Roles = "Administrator,Manager")]
        public ActionResult EtkinlikGetirOkulId(Guid Id)
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
        #endregion

        #region EtkinlikDetay
        [Authorize(Roles = "Administrator,Manager")]
        public ActionResult Detay(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            if (id != null)
            {
                var data = _etkinliklerBE.EtkinlikGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region FotoYukle
        [Obsolete]
        private string FotoYukle(string dosyaAdi, IFormFile dosya)
        {

            dosyaAdi += Guid.NewGuid().ToString() + "_" + dosya.FileName;

            string dosyaKlasor = Path.Combine(_hostingEnvironment.WebRootPath, dosyaAdi);

            //await dosya.CopyToAsync(new FileStream(dosyaKlasor, FileMode.Create));

            //return "/" + dosyaAdi;

            using (FileStream fs = new FileStream(dosyaKlasor, FileMode.Create))
            {
                dosya.CopyToAsync(fs);
                return "/" + dosyaAdi;
            }
        }
        #endregion
    }
}
