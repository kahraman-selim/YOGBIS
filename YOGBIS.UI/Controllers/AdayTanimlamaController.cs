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
using System.Diagnostics;
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

        #region EkTanımalalar
        private static readonly object _lockObject = new object();
        private static Dictionary<string, ProgressInfo> _progressData = new Dictionary<string, ProgressInfo>();
        #endregion

        #region ProgressInfo
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
        #endregion

        #region UpdateProgress
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
        #endregion

        #region GetProgress
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
        #endregion

        #region AdayTemelBilgileriYukle
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
                    TempData["Error"] = "Lütfen bir dosya seçin!";
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
                                DogumTarihi2 = worksheet.Cells[row, 6].Value?.ToString()?.Replace("/", "."),
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
        #endregion

        #region TamamlandiOnayla
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
        #endregion

        #region AdayBasvuruBilgileriYukle
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> AdayBasvuruBilgileriYukle(IFormFile file)
        {
            var sessionId = HttpContext.Session.Id;
            try
            {
                if (file == null || file.Length == 0)
                {
                    TempData["Error"] = "Lütfen bir dosya seçin!";
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

                        // AŞAMA 1: Kayıt İşlemi
                        islemYapilan = 0;
                        var kayitEdilecekToplam = toplamKayit;

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
                            var aday = new AdayBasvuruBilgileriVM
                            {
                                TC = worksheet.Cells[row, 1].Value?.ToString(),
                                Askerlik= worksheet.Cells[row, 2].Value?.ToString(),
                                KurumKod= worksheet.Cells[row, 3].Value?.ToString(),
                                KurumAdi= worksheet.Cells[row, 4].Value?.ToString(),
                                Ogrenim= worksheet.Cells[row, 5].Value?.ToString(),
                                MezunOkulKodu= worksheet.Cells[row, 6].Value?.ToString(),
                                Mezuniyet= worksheet.Cells[row, 7].Value?.ToString(),
                                HizmetYil= worksheet.Cells[row, 8].Value?.ToString(),
                                HizmetAy= worksheet.Cells[row, 9].Value?.ToString(),
                                HizmetGun= worksheet.Cells[row, 10].Value?.ToString(),
                                Derece= worksheet.Cells[row, 11].Value?.ToString(),
                                Kademe= worksheet.Cells[row, 12].Value?.ToString(),
                                Enaz5Yil= worksheet.Cells[row, 13].Value?.ToString(),
                                DahaOnceYDGorev= worksheet.Cells[row, 14].Value?.ToString(),
                                YIciGorevBasTar= worksheet.Cells[row, 15].Value?.ToString(),
                                YabanciDilBasvuru= worksheet.Cells[row, 16].Value?.ToString(),
                                YabanciDilAdi= worksheet.Cells[row, 17].Value?.ToString(),
                                YabanciDilTuru= worksheet.Cells[row, 18].Value?.ToString(),
                                YabanciDilTarihi= worksheet.Cells[row, 19].Value?.ToString(),
                                YabanciDilPuan= worksheet.Cells[row, 20].Value?.ToString(),
                                YabanciDilSeviye= worksheet.Cells[row, 21].Value?.ToString(),
                                IlTercihi1= worksheet.Cells[row, 22].Value?.ToString(),
                                IlTercihi2= worksheet.Cells[row, 23].Value?.ToString(),
                                IlTercihi3= worksheet.Cells[row, 24].Value?.ToString(),
                                IlTercihi4= worksheet.Cells[row, 25].Value?.ToString(),
                                IlTercihi5= worksheet.Cells[row, 26].Value?.ToString(),
                                BasvuruTarihi= worksheet.Cells[row, 27].Value?.ToString(),
                                SonDegisiklikTarihi= worksheet.Cells[row, 28].Value?.ToString(),
                                OnayDurumu= worksheet.Cells[row, 29].Value?.ToString(),
                                OnayDurumuAck= worksheet.Cells[row, 30].Value?.ToString(),
                                MYYSTarihi= worksheet.Cells[row, 31].Value?.ToString(),
                                MYYSSinavTedbiri= worksheet.Cells[row, 32].Value?.ToString(),
                                MYYSTedbirAck= worksheet.Cells[row, 33].Value?.ToString(),
                                MYYSPuan= worksheet.Cells[row, 34].Value?.ToString(),
                                MYYSSonuc= worksheet.Cells[row, 35].Value?.ToString(),
                                MYSSDurum= worksheet.Cells[row, 36].Value?.ToString(),
                                MYSSDurumAck= worksheet.Cells[row, 37].Value?.ToString(),
                                IlMemGorus= worksheet.Cells[row, 38].Value?.ToString(),
                                Referans= worksheet.Cells[row, 39].Value?.ToString(),
                                ReferansAck= worksheet.Cells[row, 40].Value?.ToString(),
                                GorevIptalAck= worksheet.Cells[row, 41].Value?.ToString(),
                                GorevIptalBrans= worksheet.Cells[row, 42].Value?.ToString(),
                                GorevIptalYil= worksheet.Cells[row, 43].Value?.ToString(),
                                GorevIptalBAOK= worksheet.Cells[row, 44].Value?.ToString(),
                                IlkGorevKaydi= worksheet.Cells[row, 45].Value?.ToString(),
                                YabanciDilALM= worksheet.Cells[row, 46].Value?.ToString(),
                                YabanciDilING= worksheet.Cells[row, 47].Value?.ToString(),
                                YabanciDilFRS= worksheet.Cells[row, 48].Value?.ToString(),
                                YabanciDilDiger= worksheet.Cells[row, 49].Value?.ToString(),
                                GorevdenUzaklastirma= worksheet.Cells[row, 50].Value?.ToString(),
                                EDurum= worksheet.Cells[row, 51].Value?.ToString(),
                                MDurum= worksheet.Cells[row, 52].Value?.ToString(),
                                PDurum= worksheet.Cells[row, 53].Value?.ToString(),
                                Sendika= worksheet.Cells[row, 54].Value?.ToString(),
                                SendikaAck= worksheet.Cells[row, 55].Value?.ToString(),
                                MYYSSoruItiraz= worksheet.Cells[row, 56].Value?.ToString(),
                                MYYSSonucItiraz= worksheet.Cells[row, 57].Value?.ToString(),
                                BasvuruBrans= worksheet.Cells[row, 58].Value?.ToString(),
                                BransId=Guid.Parse(worksheet.Cells[row, 59].Value?.ToString()),
                                DereceId=Guid.Parse(worksheet.Cells[row, 60].Value?.ToString()),
                                DereceAdi= worksheet.Cells[row, 61].Value?.ToString(),
                                Unvan= worksheet.Cells[row, 62].Value?.ToString(),
                                UlkeTercihId=Guid.Parse(worksheet.Cells[row, 63].Value?.ToString()),
                                MulakatId=Guid.Parse(worksheet.Cells[row, 64].Value?.ToString()),
                            };

                            var result = _adaylarBE.AdayBasvuruBilgileriEkle(aday, user);
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
        #endregion

        #region AdayBasvuruBilgileriniGetir
        public IActionResult AdayBasvuruBilgileriniGetir(string TC)
        {
            System.Diagnostics.Debug.WriteLine($"Controller - TC ile sorgu başladı: {TC}");
            var result = _adaylarBE.AdayBasvuruBilgileriniGetir(TC);

            if (result.IsSuccess && result.Data != null && result.Data.Any())
            {
                System.Diagnostics.Debug.WriteLine($"Controller - {result.Data.Count} adet veri bulundu");
                return ViewComponent("AdayBasvuruBilgileri", result.Data);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Controller - Hata: {result.Message}");
                return Json(new { success = false, message = result.Message });
            }
        }
        #endregion

        #region AdayIletisimBilgileriYukle
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> AdayIletisimBilgileriYukle(IFormFile file)
        {
            var sessionId = HttpContext.Session.Id;
            try
            {
                if (file == null || file.Length == 0)
                {
                    TempData["Error"] = "Lütfen bir dosya seçin!";
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

                        // AŞAMA 1: Kayıt İşlemi
                        islemYapilan = 0;
                        var kayitEdilecekToplam = toplamKayit;

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
                            var aday = new AdayIletisimBilgileriVM
                            {
                                TC = worksheet.Cells[row, 1].Value?.ToString(),
                                CepTelNo= worksheet.Cells[row, 2].Value?.ToString(),
                                EPosta= worksheet.Cells[row, 3].Value?.ToString(),
                                NufusIl= worksheet.Cells[row, 4].Value?.ToString(),
                                NufusIlce= worksheet.Cells[row, 5].Value?.ToString(),
                                IkametAdres= worksheet.Cells[row, 6].Value?.ToString(),
                                IkametIl= worksheet.Cells[row, 7].Value?.ToString(),
                                IkametIlce= worksheet.Cells[row, 8].Value?.ToString(),
                                MulakatId= Guid.Parse(worksheet.Cells[row, 9].Value?.ToString()),
                            };

                            var result = _adaylarBE.AdayIletisimBilgileriEkle(aday, user);
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
        #endregion

        #region AdayIletisimBilgileriniGetir
        public IActionResult AdayIletisimBilgileriniGetir(string TC)
        {
            System.Diagnostics.Debug.WriteLine($"Controller - TC ile sorgu başladı: {TC}");
            var result = _adaylarBE.AdayIletisimBilgileriniGetir(TC);

            if (result.IsSuccess && result.Data != null && result.Data.Any())
            {
                System.Diagnostics.Debug.WriteLine($"Controller - {result.Data.Count} adet veri bulundu");
                return ViewComponent("AdayIletisimBilgileri", result.Data);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Controller - Hata: {result.Message}");
                return Json(new { success = false, message = result.Message });
            }
        }
        #endregion

        #region GetAdliSicilBelge
        [HttpGet]
        public IActionResult GetAdliSicilBelge(Guid id)
        {
            try
            {
                var result = _adaylarBE.AdayBasvuruBilgileriniGetirById(id);
                if (result.IsSuccess && result.Data?.AdliSicilBelge != null)
                {
                    var base64String = Convert.ToBase64String(result.Data.AdliSicilBelge);
                    return Json(new { isSuccess = true, data = base64String });
                }
                
                return Json(new { isSuccess = false, message = "Belge bulunamadı" });
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, message = "Belge gösterilirken hata oluştu: " + ex.Message });
            }
        }
        #endregion

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
