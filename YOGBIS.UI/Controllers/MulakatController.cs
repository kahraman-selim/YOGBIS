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
using YOGBIS.UI.Extensions;

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

            // Bugünün tarihini kontrol et
            var today = DateTime.Now.ToString("dd.MM.yyyy");
            var defaultDate = mulakatTarihleri.Contains(today) ? today : mulakatTarihleri.FirstOrDefault();

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
                    // İlk açılışta varsayılan tarihi kullan
                    var result = _adaylarBE.GetirKomisyonMulakatListesi(selectedKomisyon, defaultDate);
                    viewModel.AdayListesi = result.IsSuccess ? result.Data : Enumerable.Empty<YOGBIS.Common.VModels.AdayMYSSVM>();
                    TempData["CurrentDate"] = defaultDate;
                    TempData["Message"] = result.IsSuccess && result.Data.Any() ? null : "Bu tarihe ait aday listesi bulunamadı!";
                }
            }
            else
            {
                // Diğer kullanıcılar varsayılan gün için kendi listelerini görecek
                var result = _adaylarBE.GetirKomisyonMulakatListesi(currentUser.Ad, defaultDate);
                viewModel.AdayListesi = result.IsSuccess ? result.Data : Enumerable.Empty<YOGBIS.Common.VModels.AdayMYSSVM>();
                TempData["Message"] = result.IsSuccess && result.Data.Any() ? null : "Bu tarihe ait aday listesi bulunamadı!";
            }

            // DataTable özelliklerini kullanıcı rolüne göre ayarla
            ViewBag.UseDataTable = !userRoles.Contains("Administrator");

            // TempData özelliklerini ayarla
            var currentDate = mulakatTarihleri[0];
            TempData["CurrentDate"] = currentDate;
            TempData["Message"] = viewModel.AdayListesi.Any() ? null : "Bu tarihe ait aday listesi bulunamadı!";
            TempData["HasNext"] = mulakatTarihleri.IndexOf(currentDate) < mulakatTarihleri.Count - 1;
            TempData["HasPrev"] = mulakatTarihleri.IndexOf(currentDate) > 0;

            return View(viewModel);
        }
        #endregion

        #region GetirTarihliListe
        [HttpGet]
        public async Task<IActionResult> GetirTarihliListe(string currentDate, string direction, string selectedKomisyon = null)
        {
            // Eğer selectedKomisyon null ise TempData'dan al
            if (string.IsNullOrEmpty(selectedKomisyon))
            {
                selectedKomisyon = TempData.Peek("SelectedKomisyon")?.ToString();
            }
            else
            {
                TempData["SelectedKomisyon"] = selectedKomisyon;
            }
            var viewModel = new AdayMulakatListeViewModel();
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            var currentUser = await _userManager.FindByNameAsync(userName);
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            // DataTable özelliklerini kullanıcı rolüne göre ayarla
            ViewBag.UseDataTable = !userRoles.Contains("Administrator");

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

            // Bugünün tarihini kontrol et
            var today = DateTime.Now.ToString("dd.MM.yyyy");
            var defaultDate = mulakatTarihleri.Contains(today) ? today : mulakatTarihleri.FirstOrDefault();

            // Eğer currentDate boş ise varsayılan tarihi kullan
            if (string.IsNullOrEmpty(currentDate))
            {
                currentDate = defaultDate;
            }

            // Mevcut tarihin indeksini bul
            var currentIndex = mulakatTarihleri.IndexOf(currentDate);

            // Yöne göre yeni tarihi belirle
            var newIndex = direction == "next" ?
                Math.Min(currentIndex + 1, mulakatTarihleri.Count - 1) :
                Math.Max(currentIndex - 1, 0);

            if (newIndex >= 0 && newIndex < mulakatTarihleri.Count)
            {
                var newDate = mulakatTarihleri[newIndex];

                if (userRoles.Contains("Administrator"))
                {
                    // Administrator için tüm CommissionerHead rolündeki kullanıcıları getir
                    var commissionHeads = await _userManager.GetUsersInRoleAsync("CommissionerHead");
                    viewModel.KomisyonBaskanları = commissionHeads.Select(u => new KomisyonBaskanViewModel
                    {
                        Id = u.Id,
                        AdSoyad = $"{u.Ad}",
                        UserName = u.Ad,
                        TcKN = int.Parse(u.TcKimlikNo),
                        Aktif = u.Aktif ?? false
                    }).Where(x => x.Aktif == true).OrderBy(x => x.TcKN).ToList();

                    if (!string.IsNullOrEmpty(selectedKomisyon))
                    {
                        // Seçilen komisyon için ilk tarihi kullan
                        var tarihToUse = direction == null ? mulakatTarihleri.FirstOrDefault() : newDate;
                        var result = _adaylarBE.GetirKomisyonMulakatListesi(selectedKomisyon, tarihToUse);
                        viewModel.AdayListesi = result.IsSuccess ? result.Data : Enumerable.Empty<YOGBIS.Common.VModels.AdayMYSSVM>();
                        TempData["CurrentDate"] = tarihToUse;
                        TempData["Message"] = result.IsSuccess && result.Data.Any() ? null : "Bu tarihe ait aday listesi bulunamadı!";
                    }
                    else
                    {
                        viewModel.AdayListesi = Enumerable.Empty<YOGBIS.Common.VModels.AdayMYSSVM>();
                        TempData["Message"] = "Lütfen bir komisyon seçiniz.";
                    }
                }
                else
                {
                    var result = _adaylarBE.GetirKomisyonMulakatListesi(currentUser.Ad, newDate);
                    viewModel.AdayListesi = result.IsSuccess ? result.Data : Enumerable.Empty<YOGBIS.Common.VModels.AdayMYSSVM>();
                    TempData["Message"] = result.IsSuccess && result.Data.Any() ? null : "Bu tarihe ait aday listesi bulunamadı!";
                }

                TempData["CurrentDate"] = newDate;
                TempData["HasNext"] = newIndex < mulakatTarihleri.Count - 1;
                TempData["HasPrev"] = newIndex > 0;
            }
            else
            {
                TempData["Message"] = "Geçersiz tarih!";
                viewModel.AdayListesi = Enumerable.Empty<YOGBIS.Common.VModels.AdayMYSSVM>();
            }

            return View("Index", viewModel);
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

        #region AdaySinavOdaAlındıGuncelle
        [HttpPost]
        public IActionResult AdaySinavOdaAlindiGuncelle(Guid id)
        {
            if (id == Guid.Empty)
                return Json(new { success = false, message = "Güncellemek için Kayıt Seçiniz" });

            var data = _adaylarBE.AdaySinavOdaAlindiGuncelle(id);
            if (data != null)
                return Json(new { success = true, message = "Aday çağrı durumu güncellendi" });
            else
                return Json(new { success = false, message = "Aday çağrı durumu güncellenirken hata oluştu" });
        }
        #endregion

        #region SinavBaslamaTarihiGuncelle
        [HttpPost]
        public IActionResult SinavBaslamaTarihiGuncelle(Guid id)
        {
            if (id == Guid.Empty)
                return Json(new { success = false, message = "Güncellemek için Kayıt Seçiniz" });

            var data = _adaylarBE.SinavBaslamaTarihiGuncelle(id);
            if (data != null)
                return Json(new { success = true, message = "Aday çağrı durumu güncellendi" });
            else
                return Json(new { success = false, message = "Aday çağrı durumu güncellenirken hata oluştu" });
        }
        #endregion

        #region SinavBitisTarihiGuncelle
        [HttpPost]
        public IActionResult SinavBitisTarihiGuncelle(Guid id)
        {
            if (id == Guid.Empty)
                return Json(new { success = false, message = "Güncellemek için Kayıt Seçiniz" });

            var data = _adaylarBE.SinavBitisTarihiGuncelle(id);
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
                        adayId = result.Data.AdayId,
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
                if (mulakatId == Guid.Empty || dereceId == Guid.Empty)
                {
                    return Json(new { isSuccess = false, message = "Soru bulunamadı!" });
                }

                var viewComponentResult = await this.RenderViewComponentToStringAsync("MulakatSoru",
                    new { sorusirano = soruSiraNo, mulakatId, dereceId });

                return Json(new { isSuccess = true, data = viewComponentResult });
            }
            catch (Exception)
            {
                return Json(new { isSuccess = false, message = "Soru getirilirken bir hata oluştu!" });
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
            catch (Exception)
            {
                return NotFound("Dosya gösterilirken bir hata oluştu!");
            }
        }
        #endregion

        #region MulakatDetay
        [HttpGet]
        public async Task<IActionResult> MulakatDetay(string id, string adayId, string adayAdiSoyadi, string dereceAdi, string bransAdi, string ulkeTercihAdi, string tc, string mulakatId, string dereceId)
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            var currentUser = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KullaniciId = currentUser?.LoginId;

            ViewBag.AdayAdiSoyadi = adayAdiSoyadi;
            ViewBag.DereceAdi = dereceAdi;
            ViewBag.BransAdi = bransAdi;
            ViewBag.UlkeTercihAdi = ulkeTercihAdi;
            ViewBag.AdayId = adayId;
            ViewBag.Id = id;
            ViewBag.TC = tc;
            ViewBag.MulakatId = mulakatId;
            ViewBag.DereceId = dereceId;
            return View();
        }
        #endregion

        #region AdaySinavNotuKaydet
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AdaySinavNotuKaydet(AdaySinavNotlarVM model, Guid? Id)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

                if (Id != null)
                {
                    return Json(new { success = false, message = "Güncellemek için Kayıt Seçiniz" });
                }
                
                var data = _adaylarBE.AdaySinavNotuKaydet(model, user);
                if (data.IsSuccess)
                {
                    return Json(new { success = true, message = data.Message });
                }
                else
                {
                    return Json(new { success = false, message = data.Message });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdaySinavNotu kaydedilirken hata oluştu: {ex.Message}");
                _logger.LogError($"Hata detayı: {ex}");
                return Json(new { success = false, message = $"Aday sınav notu kaydedilirken bir hata oluştu: {ex.Message}" });
            }
        }
        #endregion

        #region AdayMYYSPuanGetir
        [HttpGet]
        public IActionResult AdayMYYSPuanGetir(string tcKimlikNo)
        {
            try
            {
                var aday = _unitOfWork.adayBasvuruBilgileriRepository.GetFirstOrDefault(x => x.TC == tcKimlikNo);
                if (aday != null)
                {
                    return Json(new { success = true, myys = aday.MYYSPuan });
                }
                return Json(new { success = false, message = "Aday bulunamadı" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"MYYS Puan getirme hatası: {ex.Message}");
                return Json(new { success = false, message = "MYYS Puanı getirilirken bir hata oluştu" });
            }
        } 
        #endregion

    }
}
