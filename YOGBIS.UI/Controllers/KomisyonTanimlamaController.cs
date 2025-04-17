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

                var personelilgili = await _kullaniciBE.KomisyonSorumluGetir();
                ViewBag.KomisyonIlgiliPersoneller = personelilgili.Data;

                var personelyardimci = await _kullaniciBE.KomisyonYardimciGetir();
                ViewBag.KomisyonYardimciPersoneller = personelyardimci.Data;

                var komisyon = await _kullaniciBE.KomisyonGetir();
                ViewBag.Komisyonlar = komisyon.Data.ToList();

                var komisyonId = ViewBag.Komisyonlar;

                var komisyonIDs = (List<KullaniciVM>)komisyonId;

                var data = komisyonIDs.Select(x => x.Id).ToList();


                var model = new KomisyonPersonellerVM();
                model.KomisyonIdForView = data;

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Index sayfası görüntülenirken hata oluştu: {ex.Message}");
                TempData["ErrorMessage"] = "Bir hata oluştu. Lütfen tekrar deneyiniz.";
                return View();
            }
        }
        #endregion

        #region KomisyonKullaniciIdGetir
        [HttpGet]
        public JsonResult KomisyonKullaniciIdGetir(string komisyonId)
        {
            try
            {
                var komisyon = _kullaniciBE.KomisyonKullaniciIDGetir(komisyonId);
                if (komisyon.IsSuccess)
                {
                    return Json(new { success = true, Id = komisyon.Data.ToString() });
                }
                return Json(new { success = false, message = "Komisyon bulunamadı" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Komisyon kullanıcı ID getirme hatası: {ex.Message}");
                return Json(new { success = false, message = "Bir hata oluştu" });
            }
        }
        #endregion

        #region KomisyonBilgileriGetir
        [HttpGet]
        public JsonResult KomisyonBilgileriGetir(string komisyonAdi)
        {
            try
            {
                _logger.LogInformation($"Gelen komisyon adı: {komisyonAdi}");

                // 1. Adım: Komisyonlar tablosundan komisyon bilgilerini al
                var komisyonlar = _komisyonlarBE.KomisyonlariGetir().Data
                    .Where(k => k.KomisyonAdi == komisyonAdi)
                    .ToList();

                _logger.LogInformation($"Bulunan komisyon sayısı: {komisyonlar.Count}");

                if (!komisyonlar.Any())
                {
                    return Json(new { success = false, message = "Komisyon bulunamadı" });
                }

                var komisyon = komisyonlar.First();
                _logger.LogInformation($"Seçilen komisyon adı: {komisyon.KomisyonAdi}");

                // 2. Adım: KomisyonAdi ile kullanıcı adını al
                var kullaniciResult = _kullaniciBE.KomisyonKullaniciIDGetir(komisyon.KomisyonAdi);
                _logger.LogInformation($"Kullanıcı bulundu mu: {kullaniciResult.IsSuccess}, Mesaj: {kullaniciResult.Message}");

                if (!kullaniciResult.IsSuccess || kullaniciResult.Data == null)
                {
                    _logger.LogWarning("Kullanıcı bulunamadı veya Data null");
                    return Json(new
                    {
                        success = true,
                        kullaniciAdi = komisyon.KomisyonAdi.Replace("-", ""),  // Komisyon-1 -> Komisyon1
                        komisyonAdi = komisyon.KomisyonAdi  // Komisyon-1
                    });
                }

                _logger.LogInformation($"Bulunan kullanıcı adı: {kullaniciResult.Data}");
                return Json(new
                {
                    success = true,
                    kullaniciAdi = kullaniciResult.Data,  // Komisyon1
                    komisyonAdi = komisyon.KomisyonAdi    // Komisyon-1
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Komisyon bilgileri getirme hatası: {ex.Message}");
                return Json(new { success = false, message = "Bir hata oluştu" });
            }
        }
        #endregion

        #region KomisyonPersonelKaydet
        [HttpPost]
        public JsonResult KomisyonPersonelKaydet([FromBody] List<KomisyonPersonellerVM> data)
        {
            try
            {
                //if (model == null)
                //{
                //    return Json(new { success = false, message = "Model boş olamaz" });
                //}

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                var datalar = _komisyonlarBE.KomisyonPersonelKaydet(data, user);

                return Json("200");

                //if (data.IsSuccess)
                //{
                //    return Json(new { success = true, message = data.Message});
                //}
                //else
                //{
                //    return Json(new { success = false, message = data.Message });
                //}
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Hata oluştu: {ex.Message}" });
            }
        }
        #endregion

        #region PersonelKomisyonGetir
        [HttpGet]
        public IActionResult PersonelKomisyonGetir(string personelId)
        {
            var data = _komisyonlarBE.KomisyonPersonelleriGetir(personelId);
            return Json(data.Data);
        } 
        #endregion
    }
}
