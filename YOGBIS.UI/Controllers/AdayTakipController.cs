using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles ="Administrator,Follower")]
    public class AdayTakipController : Controller
    {
        #region Değişkenler     
        private readonly IAdaylarBE _adaylarBE;
        private readonly ILogger<AdayTakipController> _logger;
        #endregion

        #region Dönüştürücüler
        public AdayTakipController(IAdaylarBE adaylarBE, ILogger<AdayTakipController> logger)
        {
            _adaylarBE = adaylarBE;
            _logger = logger;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            try
            {
                _logger.LogInformation("AdayTakip/Index - Başlangıç");
                var result = _adaylarBE.AdayTakipMulakatListesi();
                if (!result.IsSuccess)
                {
                    _logger.LogWarning($"AdayTakip/Index - {result.Message}");
                    ViewBag.ErrorMessage = result.Message;
                    return View(new List<AdayMYSSVM>());
                }

                _logger.LogInformation($"AdayTakip/Index - {result.Data.Count} kayıt bulundu");
                return View(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdayTakip/Index - Hata: {ex.Message}", ex);
                ViewBag.ErrorMessage = "Bir hata oluştu: " + ex.Message;
                return View(new List<AdayMYSSVM>());
            }
        }
        #endregion

        #region AdayIletisimBilgileriGetir
        [HttpGet]
        public IActionResult AdayIletisimBilgileriGetir(Guid adayId)
        {
            try
            {
                _logger.LogInformation($"AdayIletisimBilgileriGetir başladı - AdayId: {adayId}");

                if (adayId == Guid.Empty)
                {
                    _logger.LogWarning("AdayIletisimBilgileriGetir - Geçersiz AdayId");
                    return Json(new { success = false, message = "Geçersiz aday bilgisi", data = "" });
                }

                var result = _adaylarBE.AdayIletisimBilgileriGetir(adayId);
                _logger.LogInformation($"AdayIletisimBilgileriGetir sonuç - Success: {result.Success}, Message: {result.Message}, Data: {result.Data}");

                return Json(new { success = result.IsSuccess, message = result.Message, data = result.Data });
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdayIletisimBilgileriGetir hata - {ex.Message}", ex);
                return Json(new { success = false, message = "İletişim bilgileri alınırken bir hata oluştu", data = (string)null });
            }
        }
        #endregion
    }
}
