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
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Dönüştürücüler
        public MulakatController(IMulakatSorulariBE mulakatSorulariBE, IDerecelerBE derecelerBE, ISoruKategorileriBE soruKategorileriBE,
            IUnitOfWork unitOfWork, ILogger<MulakatController> logger, IAdaylarBE adaylarBE, IKullaniciBE kullaniciBE,
    UserManager<Kullanici> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _mulakatSorulariBE = mulakatSorulariBE;
            _derecelerBE = derecelerBE;
            _soruKategorileriBE = soruKategorileriBE;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _adaylarBE = adaylarBE;
            _kullaniciBE = kullaniciBE;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Index
        [Route("MU10001", Name = "MulakatIndexRoute")]
        public async Task<IActionResult> Index(string selectedKomisyon = null)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            // Aktif mülakatları getir
            var aktifMulakatlar = _unitOfWork.mulakatlarRepository.GetAll(x => x.Durumu == true).ToList();
            var mulakatTarihleri = new List<string>();

            // Başlangıç ve bitiş tarihleri arasındaki günleri ekle
            foreach (var mulakat in aktifMulakatlar)
            {
                for (var tarih = mulakat.BaslamaTarihi; tarih <= mulakat.BitisTarihi; tarih = tarih.AddDays(1))
                {
                    mulakatTarihleri.Add(tarih.ToString("dd.MM.yyyy"));
                }
            }
            
            // Tarihleri tekilleştir ve sırala
            mulakatTarihleri = mulakatTarihleri.Distinct().OrderBy(x => DateTime.ParseExact(x, "dd.MM.yyyy", null)).ToList();
            var currentUser = await _userManager.FindByNameAsync(userName);
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var viewModel = new AdayMulakatListeViewModel();

            if (userRoles.Contains("Administrator")) // Administrator rolüne sahip kullanıcılar görebilsin
            {
                // Administrator için tüm CommissionerHead rolündeki kullanıcıları getir
                var commissionHeads = await _userManager.GetUsersInRoleAsync("CommissionerHead");
                viewModel.KomisyonBaskanları = commissionHeads.Select(u => new KomisyonBaskanViewModel
                {
                    Id = u.Id,
                    AdSoyad = $"{u.Ad}",
                    UserName = u.Ad,// Komisyon adını UserName olarak kullan
                    TcKN = int.Parse(u.TcKimlikNo),
                    Aktif = u.Aktif ?? false
                }).Where(x=>x.Aktif==true).OrderBy(x=>x.TcKN).ToList();

                // Eğer seçili komisyon varsa onun listesini getir
                if (!string.IsNullOrEmpty(selectedKomisyon))
                {
                    var tumAdaylar = new List<YOGBIS.Common.VModels.AdayMYSSVM>();
                    foreach (var tarih in mulakatTarihleri)
                    {
                        var result = _adaylarBE.GetirKomisyonMulakatListesi(selectedKomisyon, tarih);
                        if (result.IsSuccess)
                        {
                            tumAdaylar.AddRange(result.Data);
                        }
                    }
                    viewModel.AdayListesi = tumAdaylar;
                }
                else
                {
                   // İlk açılışta boş liste göster
                    viewModel.AdayListesi = Enumerable.Empty<YOGBIS.Common.VModels.AdayMYSSVM>();
                }
            }
            else
            {
                // Diğer kullanıcılar sadece ilk gün için kendi listelerini görecek
                var result = _adaylarBE.GetirKomisyonMulakatListesi(currentUser.Ad, mulakatTarihleri[0]);
                viewModel.AdayListesi = result.IsSuccess ? result.Data : Enumerable.Empty<YOGBIS.Common.VModels.AdayMYSSVM>();
            }

            return View(viewModel);
        }
        #endregion

        public IActionResult MulakatDetay(MulakatDetayVM model)
        {
            return View();
        }

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

                var mulakatTarihi = "07.04.2025";//"15.04.2024"; // daha sonra dinamik datetime.now.day bilgisi getirilecek
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
                        ulkeTercihAdi = result.Data.UlkeTercihAdi,

                        tC = result.Data.TC
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
        public async Task<IActionResult> AdayBasvuruBilgileriGetir(string TC)
        {
            try
            {
                if (TC == null)
                {
                    return Json(new { isSuccess = false, message = "Geçersiz parametreler!" });
                }

                var viewComponentResult = await this.RenderViewComponentToStringAsync("AdayBilgi", new { TC });
                return Json(new { isSuccess = true, data = viewComponentResult });
            }
            catch (Exception ex)
            {

                return Json(new { isSuccess = false, message = "Bir hata oluştu: " + ex.Message });
            }

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
                    var data = _adaylarBE.AdaySinavIptalGuncelle(adayMYS, user);
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
                if (soruSiraNo > 0 && mulakatId == Guid.Empty && dereceId == Guid.Empty)
                {
                    return Json(new { isSuccess = false, message = "Geçersiz parametreler!" });
                }

                var viewComponentResult = await this.RenderViewComponentToStringAsync("MulakatSoru",
                    new { sorusirano = soruSiraNo, mulakatId, dereceId });

                return Json(new { isSuccess = true, data = viewComponentResult });
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }
        #endregion

        #region GosterBilgiFormu
        public IActionResult GosterBilgiFormu(Guid id)
        {
            try
            {
                var adayBasvuru = _unitOfWork.adayBasvuruBilgileriRepository.GetFirstOrDefault(x => x.Id == id);
                if (adayBasvuru?.BilgiFormu == null)
                {
                    return NotFound("Dosya bulunamadı.");
                }

                return File(adayBasvuru.BilgiFormu, "application/pdf");
            }
            catch (Exception ex)
            {
                return NotFound("Dosya gösterilirken bir hata oluştu: " + ex.Message);
            }
        }
        #endregion
    }

    #region ControllerExtensions
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
    #endregion
}
