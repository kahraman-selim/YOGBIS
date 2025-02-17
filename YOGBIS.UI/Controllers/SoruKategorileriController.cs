using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SoruKategorileriController : Controller
    {

        #region Değişkenler
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IDerecelerBE _derecelerBE;
        #endregion

        #region Dönüştürücüler
        public SoruKategorileriController(ISoruKategorileriBE soruKategorileriBE, IDerecelerBE derecelerBE)
        {
            _soruKategorileriBE = soruKategorileriBE;
            _derecelerBE = derecelerBE;
        }
        #endregion

        #region Index
        public IActionResult Index(Guid? id)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            try
            {
                var result = _derecelerBE.DereceleriGetir();
                if (result.IsSuccess)
                {
                    //ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
                    ViewBag.Dereceler = result.Data; //?? new List<SoruDerecelerVM>();

                    if (id != null)
                    {
                        var data = _soruKategorileriBE.SoruKategoriGetir((Guid)id);
                        return View(data.Data);
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    // Başarısız ise hata mesajını TempData veya ViewBag ile view'e gönder
                    TempData["ErrorMessage"] = result.Message;
                    return View(); // Hata durumunda başka bir sayfaya yönlendir
                }
                
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Dereceler getirilirken bir hata oluştu.";                
            }

            return View();

        }
        #endregion

        #region SoruKategoriEkleGet
        [HttpGet]
        public IActionResult SoruKategoriEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            return View();
        }
        #endregion

        #region SoruKategoriEklePost
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SoruKategoriEkle(SoruKategorilerVM model, Guid? SoruKategorilerId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (SoruKategorilerId != null)
            {
                var data = _soruKategorileriBE.SoruKategoriGuncelle(model, user);
                return RedirectToAction("Index");
            }
            else
            {
                var data = _soruKategorileriBE.SoruKategoriEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        #endregion

        #region Guncelle
        public IActionResult Guncelle(Guid? id)
        {
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (id != null)
            {
                var data = _soruKategorileriBE.SoruKategoriGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region SoruKategoriSil
        [HttpDelete]
        public IActionResult SoruKategoriSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _soruKategorileriBE.SoruKategoriSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        } 
        #endregion

    }
}
