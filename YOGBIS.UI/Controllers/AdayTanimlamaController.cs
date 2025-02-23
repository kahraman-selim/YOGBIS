using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdayTanimlamaController : Controller
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdaylarBE _adaylarBE;
        private readonly ILogger<AdayTanimlamaController> _logger;
        #endregion

        #region Dönüştürücüler
        public AdayTanimlamaController(IUnitOfWork unitOfWork, IAdaylarBE adaylarBE, ILogger<AdayTanimlamaController> logger)
        {
            _unitOfWork = unitOfWork;
            _adaylarBE = adaylarBE;
            _logger = logger;
        }
        #endregion

        #region Index
        [Route("Adaylar/AD10001", Name = "AdayBilgiIndexRoute")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            return View();
        } 
        #endregion

        private static readonly object _lockObject = new object();
        private static Dictionary<string, ProgressInfo> _progressData = new Dictionary<string, ProgressInfo>();

        private class ProgressInfo
        {
            public int IslemYapilan { get; set; }
            public int ToplamKayit { get; set; }
            public int Yuzde { get; set; }
            public string IslemAsamasi { get; set; }
            public string Warning { get; set; }
            public string Success { get; set; }
            public int BasariliEklenen { get; set; }
        }

        private void UpdateProgress(string sessionId, Action<ProgressInfo> updateAction)
        {
            lock (_lockObject)
            {
                if (!_progressData.ContainsKey(sessionId))
                {
                    _progressData[sessionId] = new ProgressInfo();
                }

                updateAction(_progressData[sessionId]);
            }
        }

        [HttpGet]
        public IActionResult GetProgress()
        {
            var sessionId = HttpContext.Session.Id;
            lock (_lockObject)
            {
                if (_progressData.ContainsKey(sessionId))
                {
                    var progress = _progressData[sessionId];
                    return Json(new
                    {
                        islemYapilan = progress.IslemYapilan,
                        toplamKayit = progress.ToplamKayit,
                        yuzde = progress.Yuzde,
                        islemAsamasi = progress.IslemAsamasi,
                        warning = progress.Warning,
                        success = progress.Success,
                        basariliEklenen = progress.BasariliEklenen
                    });
                }
                return Json(new { });
            }
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> AdayTemelBilgileriYukle(IFormFile file)
        {
            var sessionId = HttpContext.Session.Id;
            try
            {
                if (file == null || file.Length <= 0)
                {
                    return Json(new { success = false });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                if (user == null)
                {
                    return Json(new { success = false });
                }

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension?.Rows ?? 0;

                        if (rowCount <= 1)
                        {
                            return Json(new { success = false });
                        }

                        var toplamKayit = rowCount - 1;
                        var islemYapilan = 0;

                        UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Baslatiliyor";
                            p.ToplamKayit = toplamKayit;
                            p.IslemYapilan = 0;
                            p.Yuzde = 0;
                            p.BasariliEklenen = 0;
                        });

                        await Task.Delay(500);

                        // AŞAMA 1: TC Kontrol
                        UpdateProgress(sessionId, p => 
                        {
                            p.IslemAsamasi = "TCKontrol";
                            p.IslemYapilan = 0;
                        });

                        var tcListesi = new List<string>();
                        var gecerliTcler = new List<string>(); // Geçerli TC'leri sakla
                        
                        for (int row = 2; row <= rowCount; row++)
                        {
                            var tc = worksheet.Cells[row, 1].Value?.ToString();
                            if (!string.IsNullOrEmpty(tc))
                            {
                                if (!_adaylarBE.TCKontrol(tc))
                                {
                                    gecerliTcler.Add(tc); // Geçerli TC'yi listeye ekle
                                }
                                islemYapilan++;
                                UpdateProgress(sessionId, p =>
                                {
                                    p.IslemYapilan = islemYapilan;
                                    p.Yuzde = (int)((double)islemYapilan / toplamKayit * 100);
                                });
                                await Task.Delay(10);
                            }
                        }

                        var tekrarEdenTCSayisi = toplamKayit - gecerliTcler.Count;

                        // Kayıt aşamasına geçiş
                        UpdateProgress(sessionId, p => 
                        {
                            p.IslemAsamasi = "KayitBasliyor";
                            p.IslemYapilan = 0;
                            p.ToplamKayit = 0;
                            p.Yuzde = 0;
                        });

                        await Task.Delay(500); // Geçiş animasyonu için bekle

                        // AŞAMA 2: Kayıt İşlemi
                        islemYapilan = 0;
                        var kayitEdilecekToplam = gecerliTcler.Count;

                        UpdateProgress(sessionId, p => 
                        {
                            p.IslemAsamasi = "Kayit";
                            p.IslemYapilan = 0;
                            p.ToplamKayit = kayitEdilecekToplam;
                            p.Yuzde = 0;
                        });

                        var basariliEklenen = 0;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var tc = worksheet.Cells[row, 1].Value?.ToString();
                            if (string.IsNullOrEmpty(tc) || !gecerliTcler.Contains(tc)) 
                            {
                                continue;
                            }

                            var aday = new AdaylarVM
                            {
                                TC = tc,
                                Ad = worksheet.Cells[row, 2].Value?.ToString(),
                                Soyad = worksheet.Cells[row, 3].Value?.ToString(),
                                BabaAd = worksheet.Cells[row, 4].Value?.ToString(),
                                AnaAd = worksheet.Cells[row, 5].Value?.ToString(),
                                DogumTarihi = worksheet.Cells[row, 6].Value?.ToString(),
                                DogumTarihi2 = worksheet.Cells[row, 6].Value?.ToString()?.Replace("/","."),
                                DogumYeri = worksheet.Cells[row, 7].Value?.ToString(),
                                Cinsiyet = worksheet.Cells[row, 8].Value?.ToString(),
                            };

                            var result = _adaylarBE.AdayEkle(aday, user);
                            if (result.IsSuccess)
                            {
                                basariliEklenen++;
                            }

                            islemYapilan++;
                            UpdateProgress(sessionId, p =>
                            {
                                p.IslemYapilan = islemYapilan;
                                p.BasariliEklenen = basariliEklenen;
                                p.Yuzde = kayitEdilecekToplam > 0 ? (int)((double)islemYapilan / kayitEdilecekToplam * 100) : 100;
                            });
                            await Task.Delay(10);
                        }

                        // İşlem Tamamlandı
                        UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Tamamlandi";
                            p.IslemYapilan = kayitEdilecekToplam;
                            p.Yuzde = 100;
                            if (tekrarEdenTCSayisi > 0)
                            {
                                p.Warning = $"{tekrarEdenTCSayisi} adet TC numarası sistemde mevcuttur!";
                            }
                            if (basariliEklenen > 0)
                            {
                                p.Success = $"{basariliEklenen} adet kayıt başarıyla eklenmiştir.";
                            }
                        });

                        await Task.Delay(500); // Tamamlandı mesajının görünmesi için kısa bir bekleme

                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel'den aday yüklenirken hata oluştu: {Message}", ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
            finally
            {
                // İşlem bittiğinde progress datasını temizle
                lock (_lockObject)
                {
                    _progressData.Remove(sessionId);
                }
            }
        }

        [HttpPost]
        public IActionResult TamamlandiOnayla()
        {
            var sessionId = HttpContext.Session.Id;
            lock (_lockObject)
            {
                _progressData.Remove(sessionId);
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> AdayBasvuruBilgileriYukle(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    TempData["Error"] = "Lütfen bir dosya seçin!";
                    return Json(new { success = false });
                }

                // Burada başvuru bilgileri yükleme işlemleri yapılacak
                // Şimdilik sadece başarılı döndürelim
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Bir hata oluştu: " + ex.Message;
                return Json(new { success = false });
            }
        }

        #region TCKimlikNoDogrula
        private bool TCKimlikNoDogrula(string tcKimlikNo)
        {
            if (string.IsNullOrEmpty(tcKimlikNo) || tcKimlikNo.Length != 11)
                return false;

            int[] digits = tcKimlikNo.Select(c => c - '0').ToArray();

            // İlk hane 0 olamaz
            if (digits[0] == 0)
                return false;

            // 1, 3, 5, 7, 9. hanelerin toplamının 7 katından, 2, 4, 6, 8. hanelerin toplamı çıkartıldığında,
            // elde edilen sonucun 10'a bölümünden kalan, yani Mod10'u 10. haneyi vermelidir.
            int tekler = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            int ciftler = digits[1] + digits[3] + digits[5] + digits[7];
            
            int hane10 = (tekler * 7 - ciftler) % 10;
            if (hane10 != digits[9])
                return false;

            // 1'den 10'uncu haneye kadar olan rakamların toplamından elde edilen sonucun
            // 10'a bölümünden kalan, yani Mod10'u 11. haneyi vermelidir.
            int toplam = digits.Take(10).Sum();
            int hane11 = toplam % 10;
            if (hane11 != digits[10])
                return false;

            return true;
        }
        #endregion
    }
}
