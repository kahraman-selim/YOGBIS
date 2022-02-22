﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class OkullarController : Controller
    {
        
        #region Değişkenler
        private readonly IOkullarBE _okullarBE;
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IKullaniciBE _kullaniciBE;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Dönüştürücüler
        [Obsolete]
        public OkullarController(IOkullarBE okullarBE, IUlkelerBE ulkelerBE, IKullaniciBE kullaniciBE, IHostingEnvironment hostingEnvironment)
        {
            _okullarBE = okullarBE;
            _ulkelerBE = ulkelerBE;
            _kullaniciBE = kullaniciBE;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Index
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (User.IsInRole(EnumsKullaniciRolleri.Administrator.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Manager.ToString()))
            {

                var requestmodel = _okullarBE.OkullariGetir();
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }
                return View(user);
            }
            else
            {
                var requestmodel = _okullarBE.OkulGetirYoneticiId(user.LoginId);                

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }
                return View(user);
            }
        }
        #endregion

        #region OkulEkleGet
        [Authorize(Roles = "Administrator")]
        [HttpGet]

        [Route("Okullar/OC10002", Name = "OkulEkleRoute")]
        public async Task<IActionResult> OkulEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            var okulmudur = await _kullaniciBE.OkulMuduruGetir();
            ViewBag.OkulMuduru = okulmudur.Data;

            return View();
        }
        #endregion

        #region OkulEklePost
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Obsolete]
        [Route("Okullar/OC10002", Name = "OkulEkleRoute")]
        public async Task<IActionResult> OkulEkle(OkullarVM model, Guid? OkulId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            var okulmudur = await _kullaniciBE.OkulMuduruGetir();
            ViewBag.OkulMuduru = okulmudur.Data;

            if (okulmudur.Data == null)
            {
                model.OkulMudurId = null;
            }
            //if (model.OkulLogo != null)
            //{
            //    string klasorler = "img/Okullar/";
            //    model.OkulLogoURL = await FotoYukle(klasorler, model.OkulLogo);
            //}

            if (OkulId != null)
            {
                //if (model.OkulMudurId==user.LoginId)
                //{
                //    var datamd = _okullarBE.OkulGuncelle(model, user);

                //    if (datamd.IsSuccess)
                //    {
                //        return RedirectToAction("OC10001","Okullar");
                //    }
                //}

                var data = _okullarBE.OkulGuncelle(model, user);

                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var data = _okullarBE.OkulEkle(model, user);

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
        [Authorize(Roles = "Administrator")]
        [Route("Okullar/OC10003", Name = "OkulGuncelle")]
        public async Task<ActionResult> Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            var okulmudur = await _kullaniciBE.OkulMuduruGetir();
            ViewBag.OkulMuduru = okulmudur.Data;

            if (id != null)
            {
                var data = _okullarBE.OkulGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region OkulSil
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public IActionResult OkulSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _okullarBE.OkulSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

        #region OkulDetay
        [Authorize(Roles = "Administrator,Manager")]
        [Route("Okullar/OC10004", Name = "OkulDetayById")]
        public IActionResult OkulDetay()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var requestmodel = _okullarBE.OkullariGetir();
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View(user);
        }
        #endregion

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
