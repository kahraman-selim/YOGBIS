using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator,Commissioner")]
    public class AdayKabulController : Controller
    {

        #region Değişkenler
        private readonly IAdaylarBE _adaylarBE;
        private readonly ILogger<AdayKabulController> _logger;
        #endregion

        #region Dönüştürücüler
        public AdayKabulController(IAdaylarBE adaylarBE, ILogger<AdayKabulController> logger)
        {
            _adaylarBE = adaylarBE;
            _logger = logger;
        }
        #endregion

        #region Index
        [Route("AK10002", Name = "AdayKabulIndexRoute")]
        public IActionResult Index()
        {
            try
            {
                _logger.LogInformation("AdayKabul/Index - Başlangıç");
                var result = _adaylarBE.AdayKabulMulakatListesi();
                if (!result.IsSuccess)
                {
                    _logger.LogWarning($"AdayKabul/Index - {result.Message}");
                    ViewBag.ErrorMessage = result.Message;
                    return View(new List<AdayMYSSVM>());
                }

                _logger.LogInformation($"AdayKabul/Index - {result.Data.Count} kayıt bulundu");
                return View(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdayKabul/Index - Hata: {ex.Message}", ex);
                ViewBag.ErrorMessage = "Bir hata oluştu: " + ex.Message;
                return View(new List<AdayMYSSVM>());
            }
        }
        #endregion

        #region AdaySinavKabulGuncelle
        [HttpPost]
        public IActionResult AdayKabulOnay(Guid id)
        {

            if (id == Guid.Empty)
                return Json(new { success = false, message = "Güncellemek için Kayıt Seçiniz" });
            var data = _adaylarBE.AdaySinavKabulGuncelle(id);
            if (data.IsSuccess)
                return Json(new { success = true, message = data.Message });
            else
                return Json(new { success = false, message = data.Message });

        }
        #endregion

        #region AdaySinavOdaAlindiGuncelle
        [HttpPost]
        public IActionResult AdaySinavOdaAlindiGuncelle(Guid id)
        {

            if (id == Guid.Empty)
                return Json(new { success = false, message = "Güncellemek için Kayıt Seçiniz" });
            var data = _adaylarBE.AdaySinavOdaAlindiGuncelle(id);
            if (data.IsSuccess)
                return Json(new { success = true, message = data.Message });
            else
                return Json(new { success = false, message = data.Message });

        }
        #endregion
    }
}
