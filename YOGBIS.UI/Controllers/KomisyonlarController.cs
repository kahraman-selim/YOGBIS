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

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KomisyonlarController : Controller
    {
        #region Değişkenler
        private readonly UserManager<Kullanici> _userManager;
        private readonly ILogger<KomisyonlarController> _logger; 
        private readonly IKomisyonlarBE _komisyonlarBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IUnitOfWork _unitOfWork;

        private static readonly object _lockObject = new object();
        #endregion

        #region Dönüştücüler
        public KomisyonlarController(
            UserManager<Kullanici> userManager,
            ILogger<KomisyonlarController> logger,
      
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
        [Route("KO10001", Name = "KomisyonlarIndexRoute")]
        public async Task<IActionResult> Index(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;
            
            
            if (id != null) 
            {
                var data = _komisyonlarBE.KomisyonGetir((Guid)id);
                return View(data);
            }
            else 
            {
                return View();
            }
            
        }
        #endregion

        #region KomisyonEkle(Get)
        public async Task<IActionResult> KomisyonEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;

            return View();
        }
        #endregion

        #region KomisyonEkle(Post)
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> KomisyonEkle(KomisyonlarVM model, Guid? KomisyonId)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

                var komisyon = await _kullaniciBE.KomisyonGetir();
                ViewBag.Komisyonlar = komisyon.Data;
                ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;

                if (KomisyonId != null)
                {
                    var data = _komisyonlarBE.KomisyonGuncelle(model, user);
                    //if (!data.IsSuccess)
                    //{
                    //    _logger.LogWarning($"Komisyon güncellenemedi: {data.Message}");
                    //    TempData["error"] = $"Komisyon güncellenemedi: {data.Message}";
                    //    return RedirectToAction("Index");
                    //}

                    _logger.LogInformation($"Komisyon başarıyla güncellendi: {model.KomisyonAdi}");
                    TempData["success"] = "Komisyon başarıyla güncellendi";
                    return RedirectToAction("Index");
                }
                else
                {
                    var data = _komisyonlarBE.KomisyonEkle(model, user);
                    //if (!data.IsSuccess)
                    //{
                    //    _logger.LogWarning($"Komisyon eklenemedi: {data.Message}");
                    //    TempData["error"] = $"Komisyon eklenemedi: {data.Message}";
                    //    return RedirectToAction("Index");
                    //}

                    _logger.LogInformation($"Komisyon başarıyla eklendi: {model.KomisyonAdi}");
                    TempData["success"] = "Komisyon başarıyla eklendi";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Komisyon işlemi sırasında hata: {ex.Message}");
                TempData["error"] = "İşlem sırasında bir hata oluştu";
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Guncelle
        [Route("KO10002", Name = "KomisyonlarGuncelleRoute")]
        public async Task<ActionResult> Guncelle(Guid? id, Guid? mulakatId)
        {
            try
            {
                _logger.LogInformation($"Komisyon güncelleme sayfası açılıyor. KomisyonId: {id}, MulakatId: {mulakatId}");

                var komisyon = await _kullaniciBE.KomisyonGetir();
                ViewBag.Komisyonlar = komisyon.Data;
                ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;
                ViewBag.MulakatId = mulakatId;

                if (id != null)
                {
                    var data = _komisyonlarBE.KomisyonGetir((Guid)id);
                    if (data.IsSuccess)
                    {
                        return View(data.Data);
                    }
                    else
                    {
                        _logger.LogWarning($"Komisyon bulunamadı. KomisyonId: {id}");
                        TempData["error"] = "Komisyon bulunamadı";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    _logger.LogWarning("Güncellenecek komisyon seçilmedi");
                    TempData["error"] = "Güncellenecek komisyon seçilmedi";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Komisyon güncelleme sayfası açılırken hata oluştu: {ex.Message}");
                TempData["error"] = "İşlem sırasında bir hata oluştu";
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region KomisyonSil
        [HttpDelete]
        public IActionResult KomisyonSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _komisyonlarBE.KomisyonSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

        #region KomisyonBilgileriYukle
        [HttpPost]
        public async Task<IActionResult> KomisyonBilgileriYukle(IFormFile file, Guid mulakatId)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    _logger.LogWarning("Excel dosyası boş veya seçilmedi");
                    return Json(new { success = false, error = "Lütfen bir Excel dosyası seçiniz!" });
                }

                if (mulakatId == Guid.Empty)
                {
                    _logger.LogWarning("Mülakat seçilmedi");
                    return Json(new { success = false, error = "Lütfen bir mülakat seçiniz!" });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                if (user == null)
                {
                    _logger.LogWarning("Oturum bilgisi bulunamadı");
                    return Json(new { success = false, error = "Oturum bilgisi bulunamadı!" });
                }

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            _logger.LogWarning("Excel dosyası boş veya geçersiz");
                            return Json(new { success = false, error = "Excel dosyası boş veya geçersiz!" });
                        }

                        var rowCount = worksheet.Dimension?.Rows ?? 0;
                        if (rowCount <= 1)
                        {
                            _logger.LogWarning("Excel dosyasında veri bulunamadı");
                            return Json(new { success = false, error = "Excel dosyasında veri bulunamadı!" });
                        }

                        var hatalar = new List<string>();
                        var basariliEklenen = 0;

                        // Kullanıcı adlarını toplu olarak kontrol et
                        var usernames = new HashSet<string>();
                        for (int row = 2; row <= rowCount; row++)
                        {
                            var username = worksheet.Cells[row, 3].Value?.ToString().Trim();
                            if (!string.IsNullOrEmpty(username))
                            {
                                usernames.Add(username);
                            }
                        }

                        // Kullanıcı ID'lerini al
                        var userIds = await _userManager.Users
                            .Where(u => usernames.Contains(u.UserName))
                            .ToDictionaryAsync(u => u.UserName, u => u.Id);

                        for (int row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                var komisyonuyedurum = worksheet.Cells[row, 4].Value?.ToString().Trim();
                                var komisyongorevdurum = true;

                                if (komisyonuyedurum == "Asıl")
                                {
                                    komisyongorevdurum = true;
                                }
                                else
                                {
                                    komisyongorevdurum = false;
                                }

                                var userName = worksheet.Cells[row, 3].Value?.ToString().Trim();
                                if (string.IsNullOrEmpty(userName))
                                {
                                    hatalar.Add($"Satır {row}: Kullanıcı adı boş olamaz!");
                                    _logger.LogWarning($"Satır {row}: Kullanıcı adı boş!");
                                    continue;
                                }

                                if (!userIds.ContainsKey(userName))
                                {
                                    hatalar.Add($"Satır {row}: {userName} kullanıcısı sistemde bulunamadı!");
                                    _logger.LogWarning($"Satır {row}: {userName} kullanıcısı bulunamadı!");
                                    continue;
                                }

                                var komisyonModel = new KomisyonlarVM
                                {
                                    KomisyonKullaniciId = userIds[userName],
                                    KomisyonSiraNo = Convert.ToInt32(worksheet.Cells[row, 1].Value?.ToString().Trim()),
                                    KomisyonAdi = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                                    KomisyonUyeDurum = komisyonuyedurum,
                                    KomisyonGorevDurum = komisyongorevdurum,
                                    KomisyonUyeSiraNo = Convert.ToInt32(worksheet.Cells[row, 5].Value?.ToString().Trim()),
                                    KomisyonUyeGorevi = worksheet.Cells[row, 6].Value?.ToString().Trim(),
                                    KomisyonUyeAdiSoyadi = worksheet.Cells[row, 7].Value?.ToString().Trim() + " " + worksheet.Cells[row, 8].Value?.ToString().Trim(),
                                    KomisyonUyeGorevYeri = worksheet.Cells[row, 9].Value?.ToString().Trim(),
                                    KomisyoUyeStatu = worksheet.Cells[row, 10].Value?.ToString().Trim(),
                                    KomisyonUlkeGrubu = worksheet.Cells[row, 11].Value?.ToString().Trim() ?? "",
                                    KomisyoUyeTel = "0",
                                    KomisyonUyeEPosta = "y@y.com.tr",
                                    KomisyonGorevBaslamaTarihi = DateTime.Parse(worksheet.Cells[row, 12].Value?.ToString().Trim()),
                                    KomisyonGorevBitisTarihi = DateTime.Parse(worksheet.Cells[row, 13].Value?.ToString().Trim()),
                                    MulakatId = mulakatId
                                };

                                if (string.IsNullOrEmpty(komisyonModel.KomisyonAdi))
                                {
                                    hatalar.Add($"Satır {row}: Komisyon adı boş olamaz!");
                                    _logger.LogWarning($"Satır {row}: Komisyon adı boş!");
                                    continue;
                                }
                                if (string.IsNullOrEmpty(komisyonModel.KomisyonUyeAdiSoyadi))
                                {
                                    hatalar.Add($"Satır {row}: Komisyon üye adı soyadı boş olamaz!");
                                    _logger.LogWarning($"Satır {row}: Komisyon üye adı soyadı boş!");
                                    continue;
                                }
                                if (string.IsNullOrEmpty(komisyonModel.KomisyonUyeGorevi))
                                {
                                    hatalar.Add($"Satır {row}: Komisyon üye görevi boş olamaz!");
                                    _logger.LogWarning($"Satır {row}: Komisyon üye görevi boş!");
                                    continue;
                                }

                                _logger.LogInformation($"Satır {row} için komisyon ekleniyor: KomisyonAdi={komisyonModel.KomisyonAdi}, UyeDurum={komisyonModel.KomisyonUyeDurum}, GorevDurum={komisyonModel.KomisyonGorevDurum}");
                                var result = _komisyonlarBE.KomisyonEkle(komisyonModel, user);
                                if (result.IsSuccess)
                                {
                                    basariliEklenen++;
                                    _logger.LogInformation($"Satır {row} başarıyla eklendi");
                                }
                                else
                                {
                                    var errorMessage = result.Message ?? "Bilinmeyen bir hata oluştu";
                                    hatalar.Add($"Satır {row}: {errorMessage}");
                                    _logger.LogWarning($"Satır {row}: {errorMessage}");
                                }
                            }
                            catch (Exception ex)
                            {
                                var errorMessage = $"Satır {row}: İşlem sırasında hata oluştu - {ex.Message}";
                                hatalar.Add(errorMessage);
                                _logger.LogError(errorMessage);
                                _logger.LogError($"Stack trace: {ex.StackTrace}");
                            }
                        }

                        if (basariliEklenen > 0)
                        {
                            var message = $"{basariliEklenen} adet komisyon kaydı başarıyla eklendi.";
                            if (hatalar.Any())
                            {
                                message += $" Ancak {hatalar.Count} adet hata oluştu: {string.Join(", ", hatalar)}";
                            }
                            return Json(new { success = true, message = message });
                        }
                        else
                        {
                            var errorMessage = "Komisyon kaydı eklenemedi. ";
                            if (hatalar.Any())
                            {
                                errorMessage += $"Hatalar: {string.Join(", ", hatalar)}";
                            }
                            else
                            {
                                errorMessage += "Lütfen Excel dosyasını kontrol ediniz.";
                            }
                            return Json(new { success = false, error = errorMessage });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Excel yükleme hatası: {ex.Message}");
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                return Json(new { success = false, error = $"Excel yükleme sırasında bir hata oluştu: {ex.Message}" });
            }
        }
        #endregion

        #region OrnekExcelIndir
        public IActionResult OrnekExcelIndir()
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Komisyonlar");

                    // Başlıkları ekle
                    var headers = new[]
                    {
                        "Komisyon Sıra No",
                        "Komisyon Adı",
                        "Kullanıcı Adı",
                        "Üye Durumu",
                        "Üye Sıra No",
                        "Üye Görevi",
                        "Üye Adı",
                        "Üye Soyadı",
                        "Görev Yeri",
                        "Üye Statüsü",
                        "Ülke Grubu",
                        "Görev Başlama Tarihi",
                        "Görev Bitiş Tarihi"
                    };

                    for (int i = 0; i < headers.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = headers[i];
                        worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                        worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        worksheet.Cells[1, i + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }

                    // Örnek veri ekle
                    var exampleData = new object[]
                    {
                        1,
                        "Örnek Komisyon",
                        "ornek.kullanici",
                        true,
                        1,
                        "Başkan",
                        "Ahmet",
                        "Yılmaz",
                        "Ankara MEM",
                        "Daire Başkanı",
                        "Avrupa",
                        DateTime.Now.ToString("dd/MM/yyyy"),
                        DateTime.Now.AddYears(1).ToString("dd/MM/yyyy")
                    };

                    for (int i = 0; i < exampleData.Length; i++)
                    {
                        worksheet.Cells[2, i + 1].Value = exampleData[i];
                        worksheet.Cells[2, i + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }

                    // Tarih formatını ayarla
                    worksheet.Cells["L2:M2"].Style.Numberformat.Format = "dd/mm/yyyy";

                    // Sütun genişliklerini otomatik ayarla
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Başlık satırını dondur
                    worksheet.View.FreezePanes(2, 1);

                    var fileBytes = package.GetAsByteArray();
                    var fileName = "KomisyonYukleme_Ornek.xlsx";

                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Örnek Excel oluşturma hatası: {ex.Message}");
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                TempData["Error"] = "Örnek Excel dosyası oluşturulurken bir hata oluştu!";
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region DurumDegistir
        [HttpPost]
        public async Task<IActionResult> DurumDegistir(Guid KomisyonId)
        {
            try
            {
                _logger.LogInformation($"Durum değiştirme isteği alındı: KomisyonId={KomisyonId}");
                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                
                var result = _komisyonlarBE.DurumDegistir(KomisyonId, user);

                if (result.IsSuccess)
                {
                    _logger.LogInformation($"Komisyon durumu başarıyla değiştirildi: KomisyonId={KomisyonId}");
                    TempData["success"] = result.Message;
                }
                else
                {
                    _logger.LogWarning($"Komisyon durumu değiştirilemedi: {result.Message}");
                    TempData["error"] = result.Message;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Durum değiştirme hatası: {ex.Message}, StackTrace: {ex.StackTrace}");
                TempData["error"] = "İşlem sırasında bir hata oluştu";
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region KomisyonListesiPartial
        public IActionResult KomisyonListesiPartial(Guid? mulakatId)
        {
            try
            {
                _logger.LogInformation($"Komisyon listesi getiriliyor. MulakatId: {mulakatId}");
                return ViewComponent("KomisyonListesi", new { mulakatId });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Komisyon listesi getirilirken hata oluştu: {ex.Message}");
                return Json(new { success = false, message = "Komisyon listesi getirilirken bir hata oluştu" });
            }
        }
        #endregion
    }
}
