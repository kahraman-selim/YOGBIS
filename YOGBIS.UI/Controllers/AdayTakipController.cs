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
        public IActionResult AdayIletisimBilgileriGetir(string adayId)
        {
            try
            {
                _logger.LogInformation($"AdayIletisimBilgileriGetir - İstek alındı - AdayId: {adayId}");

                if (string.IsNullOrEmpty(adayId))
                {
                    _logger.LogWarning("AdayIletisimBilgileriGetir - AdayId boş geldi");
                    return Json(new { success = false, message = "Aday bilgisi bulunamadı" });
                }

                if (!Guid.TryParse(adayId, out Guid adayGuid))
                {
                    _logger.LogWarning($"AdayIletisimBilgileriGetir - Geçersiz AdayId formatı: {adayId}");
                    return Json(new { success = false, message = "Geçersiz aday bilgisi" });
                }

                var result = _adaylarBE.AdayIletisimBilgileriGetir(adayGuid);
                _logger.LogInformation($"AdayIletisimBilgileriGetir - İşlem sonucu - AdayId: {adayId}, Başarılı: {result.IsSuccess}, Mesaj: {result.Message}");

                return Json(new { 
                    success = result.IsSuccess, 
                    message = result.Message, 
                    telefon = result.Data ?? "Telefon bilgisi bulunamadı" 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdayIletisimBilgileriGetir - Hata: {ex.Message}", ex);
                return Json(new { success = false, message = "İşlem sırasında bir hata oluştu" });
            }
        }
        #endregion
    }
}
