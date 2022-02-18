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
        #endregion

        #region EtkinlikEkleGet
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
        #endregion

        #region EtkinlikEklePost
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
                    string klasorler = "img/EtkinlikKapakFoto";
                    model.EtkinlikKapakResimUrl = await FotoYukle(klasorler, model.EtkinlikKapakResim);
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
                            FotoURL = await FotoYukle(fotoklasorler, file),
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
        #endregion

        #region GuncellePost
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
        #endregion

        #region EtkinlikSil
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
        #endregion

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

        #region FotoYukle
        [Obsolete]
        private async Task<string> FotoYukle(string dosyaYolu, IFormFile dosya)
        {

            dosyaYolu += Guid.NewGuid().ToString() + "_" + dosya.FileName;

            string dosyaKlasor = Path.Combine(_hostingEnvironment.WebRootPath, dosyaYolu);

            await dosya.CopyToAsync(new FileStream(dosyaKlasor, FileMode.Create));

            return "/" + dosyaYolu;
        }
        #endregion
    }
}
