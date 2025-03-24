using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;
using Microsoft.AspNetCore.Identity;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator,CommissionerHead")]
    public class MulakatController : Controller
    {

        #region Değişkenler
        private readonly IMulakatSorulariBE _mulakatSorulariBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MulakatController> _logger;
        private readonly IAdaylarBE _adaylarBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly UserManager<Kullanici> _userManager;
        #endregion

        #region Dönüştürücüler
        public MulakatController(IMulakatSorulariBE mulakatSorulariBE, IDerecelerBE derecelerBE, ISoruKategorileriBE soruKategorileriBE,
    IMulakatOlusturBE mulakatOlusturBE, IUnitOfWork unitOfWork, ILogger<MulakatController> logger, IAdaylarBE adaylarBE, IKullaniciBE kullaniciBE,
    UserManager<Kullanici> userManager)
        {
            _mulakatSorulariBE = mulakatSorulariBE;
            _derecelerBE = derecelerBE;
            _soruKategorileriBE = soruKategorileriBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _adaylarBE = adaylarBE;
            _kullaniciBE = kullaniciBE;
            _userManager = userManager;
        }
        #endregion

        #region Index
        [Route("MU10001", Name = "MulakatIndexRoute")]
        public async Task<IActionResult> Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;

            return View();
        } 
        #endregion

        #region AdayCagriDurumGuncelle
        [HttpPost]
        public IActionResult AdayCagriDurumGuncelle(Guid id)
        {
            if (id == Guid.Empty)
                return Json(new { success = false, message = "Güncellemek için Kayıt Seçiniz" });

            var data = _adaylarBE.AdayCagriDurumGuncelle(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });
        }
        #endregion

        #region KomisyonAdaylariniGetir
        [HttpGet]
        public IActionResult KomisyonAdaylariniGetir(string komisyonUserName)
        {
            try
            {
                if (string.IsNullOrEmpty(komisyonUserName))
                {
                    return Json(new { success = false, message = "Lütfen komisyon seçiniz!" });
                }

                var mulakatTarihi = "15.04.2024"; // daha sonra dinamik datetime.now.day bilgisi getirilecek
                var result = _adaylarBE.GetirKomisyonMulakatListesi(komisyonUserName, mulakatTarihi);

                if (result.IsSuccess)
                {
                    return Json(new { success = true, data = result.Data });
                }
                else
                {
                    return Json(new { success = false, message = result.Message ?? "Aday listesi getirilemedi!" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"KomisyonAdaylariniGetir hatası: {ex}");
                return Json(new { success = false, message = "İşlem sırasında bir hata oluştu!" });
            }
        } 
        #endregion

        #region GetirAdayBilgileri
        [HttpGet]
        public IActionResult GetirAdayBilgileri(Guid id)
        {
            try
            {
                var result = _adaylarBE.GetirAdayMYSSBilgileri(id);
                if (result.IsSuccess)
                {
                    return Json(new
                    {
                        isSuccess = true,
                        adayAdiSoyadi = result.Data.AdayAdiSoyadi,
                        dereceAdi = result.Data.DereceAdi,
                        bransAdi = result.Data.BransAdi,
                        ulkeTercihAdi = result.Data.UlkeTercihAdi
                    });
                }

                return Json(new { isSuccess = false, message = result.Message });
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, message = ex.Message });
            }
        }
        #endregion

        #region AdayBasvuruBilgileriGetir
        [HttpGet]
        public JsonResult AdayBasvuruBilgileriGetir(Guid adayId)
        {
            var result = _adaylarBE.GetirAdayBasvuruBilgileri(adayId);

            if (result.IsSuccess)
            {
                return Json(new
                {
                    success = true,
                    data = new
                    {
                        Id = result.Data.Id,
                        AdayId = result.Data.AdayId,
                        TC = result.Data.TC,
                        MulakatId = result.Data.MulakatId,
                        MulakatYil = result.Data.MulakatYil,
                        Dogumyeri = result.Data.Dogumyeri,
                        Yas = result.Data.Yas,
                        IkametBilgisi = result.Data.IkametBilgisi,
                        HizmetAy = result.Data.HizmetAy,
                        HizmetYil = result.Data.HizmetYil,
                        HizmetGun = result.Data.HizmetGun,
                        Derece = result.Data.Derece,
                        Kademe = result.Data.Kademe,
                        IlkGorevKaydi = result.Data.IlkGorevKaydi,
                        GorevdenUzaklastirma = result.Data.GorevdenUzaklastirma,
                        GorevIptalAck = result.Data.GorevIptalAck,
                        GorevIptalBAOK = result.Data.GorevIptalBAOK,
                        GorevIptalBrans = result.Data.GorevIptalBrans,
                        GorevIptalYil = result.Data.GorevIptalYil,
                        AdliSicilBelge = result.Data.AdliSicilBelge,
                        YabanciDilAdi = result.Data.YabanciDilAdi,
                        YabanciDilALM = result.Data.YabanciDilALM,
                        YabanciDilBasvuru = result.Data.YabanciDilBasvuru,
                        YabanciDilDiger = result.Data.YabanciDilDiger,
                        YabanciDilFRS = result.Data.YabanciDilFRS,
                        YabanciDilING = result.Data.YabanciDilING,
                        YabanciDilPuan = result.Data.YabanciDilPuan,
                        YabanciDilSeviye = result.Data.YabanciDilSeviye,
                        YabanciDilTarihi = result.Data.YabanciDilTarihi,
                        YabanciDilTuru = result.Data.YabanciDilTuru,
                        YLisans = result.Data.YLisans,
                        Doktora = result.Data.Doktora,
                        GenelDurum = result.Data.GenelDurum,
                        BilgiFormu = result.Data.BilgiFormu,
                        UlkeTercihAdi = result.Data.UlkeTercihAdi
                    }
                });
            }

            return Json(new { success = false, message = result.Message });
        }
        #endregion
    }
}
