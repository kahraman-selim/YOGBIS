using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
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
using YOGBIS.Common.VModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator,CommissionerHead")]
    public class MulakatController : Controller
    {

        #region Değişkenler
        private readonly IMulakatSorulariBE _mulakatSorulariBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MulakatController> _logger;
        private readonly IAdaylarBE _adaylarBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly UserManager<Kullanici> _userManager;
        #endregion

        #region Dönüştürücüler
        public MulakatController(IMulakatSorulariBE mulakatSorulariBE, IDerecelerBE derecelerBE, ISoruKategorileriBE soruKategorileriBE, 
            IUnitOfWork unitOfWork, ILogger<MulakatController> logger, IAdaylarBE adaylarBE, IKullaniciBE kullaniciBE,
    UserManager<Kullanici> userManager)
        {
            _mulakatSorulariBE = mulakatSorulariBE;
            _derecelerBE = derecelerBE;
            _soruKategorileriBE = soruKategorileriBE;
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
            if (data != null)
                return Json(new { success = true, message = "Aday çağrı durumu güncellendi" });
            else
                return Json(new { success = false, message = "Aday çağrı durumu güncellenirken hata oluştu" });
        }
        #endregion

        #region AdaySinavOdaKabulGuncelle
        [HttpPost]
        public IActionResult AdaySinavOdaKabulGuncelle(Guid id)
        {
            if (id == Guid.Empty)
                return Json(new { success = false, message = "Güncellemek için Kayıt Seçiniz" });

            var data = _adaylarBE.AdaySinavOdaKabulGuncelle(id);
            if (data != null)
                return Json(new { success = true, message = "Aday çağrı durumu güncellendi" });
            else
                return Json(new { success = false, message = "Aday çağrı durumu güncellenirken hata oluştu" });
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

                if (result != null)
                {
                    return Json(new { success = true, data = result.Data });
                }
                else
                {
                    return Json(new { success = false, message = "Aday listesi getirilemedi!" });
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
                if (result != null)
                {
                    return Json(new
                    {
                        isSuccess = true,
                        adayAdiSoyadi = result.Data.AdayAdiSoyadi,
                        dereceAdi = result.Data.DereceAdi,
                        dereceId = result.Data.DereceId,
                        mulakatId = result.Data.MulakatId,
                        bransAdi = result.Data.BransAdi,
                        ulkeTercihAdi = result.Data.UlkeTercihAdi
                    });
                }

                return Json(new { isSuccess = false, message = "Aday bilgileri bulunamadı" });
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, message = "Aday bilgileri getirilirken hata oluştu: " + ex.Message });
            }
        }
        #endregion

        #region AdayBasvuruBilgileriGetir
        [HttpGet]
        public async Task<IActionResult> AdayBasvuruBilgileriGetir(Guid adayId)
        {
            Result<AdayBasvuruBilgileriVM> result = _adaylarBE.GetirAdayBasvuruBilgileri(adayId);
            Console.WriteLine($"GetirAdayBasvuruBilgileri sonucu: {result.IsSuccess}, Mesaj: {result.Message}, AdayId: {adayId}");
            
            if (result.IsSuccess && result.Data != null)
            {
                var viewComponentResult = await this.RenderViewComponentToStringAsync("AdayBilgi", result.Data);
                return Json(new
                {
                    success = true,
                    data = viewComponentResult
                });
            }
            return Json(new { success = false, message = result.Message });
        }
        #endregion

        #region SinavIptal
        [HttpPost]
        public async Task<IActionResult> SinavIptal(Guid id, string aciklama)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

                var adayMYS = await _adaylarBE.GetirAdayMYSSBilgileri(id);
                if (adayMYS != null)
                {
                    adayMYS.SinavIptal = true;
                    adayMYS.SinavIptalAck = aciklama;
                    var data = _adaylarBE.AdaySinavIptalGuncelle(adayMYS,user);
                    TempData["Success"] = "Sınav iptal işlemi başarıyla gerçekleştirildi.";
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Kayıt bulunamadı." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Sınav iptal işlemi sırasında bir hata oluştu." });
            }
        }
        #endregion

        #region AdayMulakatSoruGetir
        [HttpGet]
        public async Task<IActionResult> AdayMulakatSoruGetir(int soruSiraNo, Guid mulakatId, Guid dereceId)
        {
            try
            {
                var viewComponentResult = await this.RenderViewComponentToStringAsync("MulakatSoru", 
                    new { sorusirano = soruSiraNo, mulakatId = mulakatId, dereceId = dereceId });
                
                return Json(new
                {
                    isSuccess = true,
                    data = viewComponentResult,
                    message = "Soru başarıyla getirildi."
                });
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, message = "Soru getirme işlemi sırasında bir hata oluştu: " + ex.Message });
            }
        }
        #endregion
    }

    public static class ControllerExtensions
    {
        public static async Task<string> RenderViewComponentToStringAsync(this Controller controller, string componentName, object parameters = null)
        {
            var context = controller.ControllerContext;
            var viewComponentHelper = context.HttpContext.RequestServices
                .GetRequiredService<IViewComponentHelper>();

            ((IViewContextAware)viewComponentHelper).Contextualize(new ViewContext(
                context,
                new NullView(),
                controller.ViewData,
                controller.TempData,
                TextWriter.Null,
                new HtmlHelperOptions()));

            var viewComponentResult = await viewComponentHelper.InvokeAsync(componentName, parameters);
            using var writer = new StringWriter();
            viewComponentResult.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            return writer.ToString();
        }

        private class NullView : IView
        {
            public string Path => "";
            public async Task RenderAsync(ViewContext context)
            {
                await Task.CompletedTask;
            }
        }
    }
}
