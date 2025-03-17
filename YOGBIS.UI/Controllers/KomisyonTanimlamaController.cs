using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.VModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;
using System.Runtime.InteropServices;
using YOGBIS.Common.ResultModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KomisyonTanimlamaController : Controller
    {
        #region Değişkenler
        private readonly UserManager<Kullanici> _userManager;
        private readonly ILogger<KomisyonTanimlamaController> _logger;
        private readonly IKomisyonlarBE _komisyonlarBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Dönüştücüler
        public KomisyonTanimlamaController(
            UserManager<Kullanici> userManager,
            ILogger<KomisyonTanimlamaController> logger,
            IKomisyonlarBE komisyonlarBE,
            IKullaniciBE kullaniciBE,
            IMulakatOlusturBE mulakatOlusturBE,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _logger = logger;
            _komisyonlarBE = komisyonlarBE;
            _kullaniciBE = kullaniciBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Index
        [Route("KT10001", Name = "KomisyonPersonelTanimlamaIndexRoute")]
        public async Task<IActionResult> Index(Guid? id)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                
                var personel = await _kullaniciBE.KomisyonSorumluGetir();
                ViewBag.KomisyonPersoneller = personel.Data;

                var komisyon = await _kullaniciBE.KomisyonGetir();
                ViewBag.Komisyonlar = komisyon.Data;

                

                Result<List<KullaniciVM>> personelResult = await _kullaniciBE.KomisyonSorumluGetir();
                if (!personelResult.Success)
                {
                    TempData["ErrorMessage"] = personelResult.Message;
                    return View();
                }
                ViewBag.KomisyonPersoneller = personelResult.Data;

                Result<List<KomisyonlarVM>> komisyonlarResult = _komisyonlarBE.KomisyonlariGetir();
                if (!komisyonlarResult.Success)
                {
                    TempData["ErrorMessage"] = komisyonlarResult.Message;
                    return View();
                }
                ViewBag.Komisyonlar = komisyonlarResult.Data;

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Index sayfası görüntülenirken hata oluştu: {ex.Message}");
                TempData["ErrorMessage"] = "Bir hata oluştu. Lütfen tekrar deneyiniz.";
                return View();
            }
        }
        #endregion

        //#region KomisyonKaydet
        //[HttpPost]
        //public IActionResult KomisyonKaydet(KomisyonlarVM model)
        //{
        //    try
        //    {
        //        if (model == null)
        //        {
        //            return Json(new { success = false, message = "Geçersiz veri" });
        //        }

        //        var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

        //        foreach (var komisyonId in model.KomisyonId)
        //        {
        //            var komisyon = new KomisyonlarVM
        //            {
        //                IlgiliPersonelId = model.IlgiliPersonelId,                        
        //            };

        //            Result<KomisyonlarVM> result = _komisyonlarBE.KomisyonGuncelle(komisyon, user);
        //            if (!result.Success)
        //            {
        //                return Json(new { success = false, message = result.Message });
        //            }
        //        }

        //        return Json(new { success = true, message = "Komisyon ataması başarıyla kaydedildi" });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Komisyon kaydedilirken hata oluştu: {ex.Message}");
        //        return Json(new { success = false, message = "Bir hata oluştu. Lütfen tekrar deneyiniz." });
        //    }
        //}
        //#endregion
    }
}
