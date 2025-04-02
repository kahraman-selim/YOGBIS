using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdayTanimlamaController : Controller
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdaylarBE _adaylarBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly ILogger<AdayTanimlamaController> _logger;
        private readonly IBranslarBE _branslarBE;
        private readonly IUlkeTercihleriBE _ulkeTercihleriBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly IKomisyonlarBE _komisyonlarBE;
        private readonly IProgressService _progressService;
        #endregion

        #region Dönüştürücüler
        public AdayTanimlamaController(
            IUnitOfWork unitOfWork,
            IAdaylarBE adaylarBE,
            IMulakatOlusturBE mulakatOlusturBE,
            ILogger<AdayTanimlamaController> logger,
            IBranslarBE branslarBE,
            IUlkeTercihleriBE ulkeTercihleriBE,
            IDerecelerBE derecelerBE,
            IKomisyonlarBE komisyonlarBE,
            IProgressService progressService)
        {
            _unitOfWork = unitOfWork;
            _adaylarBE = adaylarBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _logger = logger;
            _branslarBE = branslarBE;
            _derecelerBE = derecelerBE;
            _komisyonlarBE = komisyonlarBE;
            _progressService = progressService;
            _ulkeTercihleriBE = ulkeTercihleriBE;
        }
        #endregion

        #region Index
        [Route("AD10001", Name = "AdayTanimlamaIndexRoute")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetirSelectedBox().Data;
            return View();
        }
        #endregion

        #region GetProgress
        [HttpGet]
        public IActionResult GetProgress()
        {
            try
            {
                if (HttpContext?.Session == null)
                {
                    return Json(new
                    {
                        islemAsamasi = "Hata",
                        error = "Oturum bilgisi bulunamadı!",
                        mesaj = "Oturum bilgisi bulunamadı!"
                    });
                }

                var sessionId = HttpContext.Session.Id;
                if (string.IsNullOrEmpty(sessionId))
                {
                    return Json(new
                    {
                        islemAsamasi = "Hata",
                        error = "Geçersiz oturum ID!",
                        mesaj = "Geçersiz oturum ID!"
                    });
                }

                var progress = _progressService.GetProgress(sessionId);
                if (progress == null)
                {
                    return Json(new
                    {
                        islemAsamasi = "Hata",
                        error = "İşlem bilgisi bulunamadı!",
                        mesaj = "İşlem bilgisi bulunamadı!"
                    });
                }

                return Json(new
                {
                    islemAsamasi = progress.IslemAsamasi,
                    toplamKayit = progress.ToplamKayit,
                    islemYapilan = progress.IslemYapilan,
                    yuzde = progress.Yuzde,
                    basariliEklenen = progress.BasariliEklenen,
                    success = progress.Success,
                    error = progress.Error,
                    mesaj = progress.Mesaj
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Progress bilgisi alınırken hata oluştu: {Message}", ex.Message);
                return Json(new
                {
                    islemAsamasi = "Hata",
                    error = "İşlem sırasında bir hata oluştu!",
                    mesaj = "İşlem sırasında bir hata oluştu!"
                });
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
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Lütfen bir dosya seçin!";
                        p.Mesaj = "Lütfen bir dosya seçin!";
                    });
                    return Json(new { success = false });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                if (user == null)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Oturum bilgisi bulunamadı!";
                        p.Mesaj = "Oturum bilgisi bulunamadı!";
                    });
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
                            _progressService.UpdateProgress(sessionId, p =>
                            {
                                p.IslemAsamasi = "Hata";
                                p.Error = "Excel dosyası boş veya geçersiz!";
                                p.Mesaj = "Excel dosyası boş veya geçersiz!";
                            });
                            return Json(new { success = false });
                        }

                        var toplamKayit = rowCount - 1;
                        var islemYapilan = 0;

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Baslatiliyor";
                            p.ToplamKayit = toplamKayit;
                            p.IslemYapilan = 0;
                            p.BasariliEklenen = 0;
                            p.Mesaj = "Dosya yükleme işlemi başlatılıyor...";
                        });

                        await Task.Delay(100); // UI güncellemesi için kısa bekleme

                        // AŞAMA 1: TC Kontrol
                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "TCKontrol";
                            p.Mesaj = "TC kontrolleri yapılıyor...";
                        });

                        var tcListesi = new List<string>();
                        var gecerliTcler = new List<string>();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var tc = worksheet.Cells[row, 1].Value?.ToString();
                            if (!string.IsNullOrEmpty(tc))
                            {
                                if (!_adaylarBE.TCKontrol(tc))
                                {
                                    gecerliTcler.Add(tc);
                                }
                                islemYapilan++;
                                _progressService.UpdateProgress(sessionId, p =>
                                {
                                    p.IslemYapilan = islemYapilan;
                                    p.Mesaj = $"TC kontrolleri yapılıyor... ({islemYapilan}/{toplamKayit})";
                                });
                                await Task.Delay(10);
                            }
                        }

                        var tekrarEdenTCSayisi = toplamKayit - gecerliTcler.Count;

                        // Kayıt aşamasına geçiş
                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "KayitBasliyor";
                            p.IslemYapilan = 0;
                            p.ToplamKayit = gecerliTcler.Count;
                            p.Mesaj = "Kayıt işlemi başlatılıyor...";
                        });

                        await Task.Delay(500);

                        // AŞAMA 2: Kayıt İşlemi
                        islemYapilan = 0;
                        var basariliEklenen = 0;

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Kayit";
                            p.IslemYapilan = 0;
                            p.Mesaj = "Kayıtlar işleniyor...";
                        });

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
                                MulakatYil=Convert.ToInt32(worksheet.Cells[row, 9].Value),
                            };

                            var result = _adaylarBE.AdayEkle(aday, user);
                            if (result.IsSuccess)
                            {
                                basariliEklenen++;
                            }

                            islemYapilan++;
                            _progressService.UpdateProgress(sessionId, p =>
                            {
                                p.IslemYapilan = islemYapilan;
                                p.BasariliEklenen = basariliEklenen;
                                p.Mesaj = $"Kayıtlar işleniyor... ({islemYapilan}/{gecerliTcler.Count})";
                            });
                            await Task.Delay(10);
                        }

                        // İşlem Tamamlandı
                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Tamamlandi";
                            p.IslemYapilan = gecerliTcler.Count;
                            if (tekrarEdenTCSayisi > 0)
                            {
                                p.Error = $"{tekrarEdenTCSayisi} adet TC numarası sistemde mevcuttur!";
                            }
                            if (basariliEklenen > 0)
                            {
                                p.Success = $"{basariliEklenen} adet kayıt başarıyla eklenmiştir.";
                            }
                            p.Mesaj = "İşlem tamamlandı";
                        });

                        await Task.Delay(500);

                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel'den aday yüklenirken hata oluştu: {Message}", ex.Message);
                _progressService.UpdateProgress(sessionId, p =>
                {
                    p.IslemAsamasi = "Hata";
                    p.Error = "İşlem sırasında hata oluştu: " + ex.Message;
                    p.Mesaj = "Hata oluştu!";
                });
                return Json(new { success = false, error = ex.Message });
            }
            finally
            {
                _progressService.ResetProgress(sessionId);
            }
        }
        #endregion

        #region AdayBasvuruBilgileriYukle
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> AdayBasvuruBilgileriYukle(IFormFile file)
        {
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetirSelectedBox().Data;          

            var sessionId = HttpContext.Session.Id;

            try
            {
                if (file == null || file.Length == 0)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Lütfen bir dosya seçin!";
                        p.Mesaj = "Lütfen bir dosya seçin!";
                    });
                    return Json(new { success = false });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                if (user == null)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Oturum bilgisi bulunamadı!";
                        p.Mesaj = "Oturum bilgisi bulunamadı!";
                    });
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
                            _progressService.UpdateProgress(sessionId, p =>
                            {
                                p.IslemAsamasi = "Hata";
                                p.Error = "Excel dosyası boş veya geçersiz!";
                                p.Mesaj = "Excel dosyası boş veya geçersiz!";
                            });
                            return Json(new { success = false });
                        }

                        var toplamKayit = rowCount - 1;
                        var islemYapilan = 0;

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Baslatiliyor";
                            p.ToplamKayit = toplamKayit;
                            p.IslemYapilan = 0;
                            p.BasariliEklenen = 0;
                            p.Mesaj = "Dosya yükleme işlemi başlatılıyor...";
                        });

                        await Task.Delay(100); // UI güncellemesi için kısa bekleme

                        // AŞAMA 1: Kayıt İşlemi
                        islemYapilan = 0;
                        var kayitEdilecekToplam = toplamKayit;
                        var basariliEklenen = 0;

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Kayit";
                            p.IslemYapilan = 0;
                            p.ToplamKayit = kayitEdilecekToplam;
                            p.Mesaj = "Kayıtlar işleniyor...";
                        });

                        for (int row = 2; row <= rowCount; row++)
                        {
                            //BransAdi ile BransId yi eşleştirme
                            var brans = _branslarBE.BranslariGetir().Data
                                .FirstOrDefault(b => b.BransAdi == worksheet.Cells[row, 62].Value?.ToString());
                            var ulketercih = _ulkeTercihleriBE.UlkeTercihAdlariGetir().Data
                                .FirstOrDefault(u => u.UlkeTercihAdi == worksheet.Cells[row, 65].Value?.ToString() && u.YabancıDil== worksheet.Cells[row, 16].Value?.ToString());                                                        

                            var aday = new AdayBasvuruBilgileriVM
                            {
                                TC = worksheet.Cells[row, 1].Value?.ToString(),
                                Askerlik = worksheet.Cells[row, 2].Value?.ToString(),
                                KurumKod = worksheet.Cells[row, 3].Value?.ToString(),
                                KurumAdi = worksheet.Cells[row, 4].Value?.ToString(),
                                Ogrenim = worksheet.Cells[row, 5].Value?.ToString(),
                                MezunOkulKodu = worksheet.Cells[row, 6].Value?.ToString(),
                                Mezuniyet = worksheet.Cells[row, 7].Value?.ToString(),
                                HizmetYil = worksheet.Cells[row, 8].Value?.ToString(),
                                HizmetAy = worksheet.Cells[row, 9].Value?.ToString(),
                                HizmetGun = worksheet.Cells[row, 10].Value?.ToString(),
                                Derece = worksheet.Cells[row, 11].Value?.ToString(),
                                Kademe = worksheet.Cells[row, 12].Value?.ToString(),
                                Enaz5Yil = worksheet.Cells[row, 13].Value?.ToString(),
                                DahaOnceYDGorev = worksheet.Cells[row, 14].Value?.ToString(),
                                YIciGorevBasTar = worksheet.Cells[row, 15].Value?.ToString(),
                                YabanciDilBasvuru = worksheet.Cells[row, 16].Value?.ToString(),
                                YabanciDilAdi = worksheet.Cells[row, 17].Value?.ToString(),
                                YabanciDilTuru = worksheet.Cells[row, 18].Value?.ToString(),
                                YabanciDilTarihi = worksheet.Cells[row, 19].Value?.ToString(),
                                YabanciDilPuan = worksheet.Cells[row, 20].Value?.ToString(),
                                YabanciDilSeviye = worksheet.Cells[row, 21].Value?.ToString(),
                                IlTercihi1 = worksheet.Cells[row, 22].Value?.ToString(),
                                IlTercihi2 = worksheet.Cells[row, 23].Value?.ToString(),
                                IlTercihi3 = worksheet.Cells[row, 24].Value?.ToString(),
                                IlTercihi4 = worksheet.Cells[row, 25].Value?.ToString(),
                                IlTercihi5 = worksheet.Cells[row, 26].Value?.ToString(),
                                BasvuruTarihi = worksheet.Cells[row, 27].Value?.ToString(),
                                SonDegisiklikTarihi = worksheet.Cells[row, 28].Value?.ToString(),
                                OnayDurumu = worksheet.Cells[row, 29].Value?.ToString(),
                                OnayDurumuAck = worksheet.Cells[row, 30].Value?.ToString(),
                                MYYSTarihi = worksheet.Cells[row, 31].Value?.ToString(),
                                MYYSSinavTedbiri = worksheet.Cells[row, 32].Value?.ToString(),
                                MYYSTedbirAck = worksheet.Cells[row, 33].Value?.ToString(),
                                MYYSPuan = worksheet.Cells[row, 34].Value?.ToString(),
                                MYYSSonuc = worksheet.Cells[row, 35].Value?.ToString(),
                                MYSSDurum = worksheet.Cells[row, 36].Value?.ToString(),
                                MYSSDurumAck = worksheet.Cells[row, 37].Value?.ToString(),
                                IlMemGorus = worksheet.Cells[row, 38].Value?.ToString(),
                                Referans = worksheet.Cells[row, 39].Value?.ToString(),
                                ReferansAck = worksheet.Cells[row, 40].Value?.ToString(),
                                GorevIptalAck = worksheet.Cells[row, 41].Value?.ToString(),
                                GorevIptalBrans = worksheet.Cells[row, 42].Value?.ToString(),
                                GorevIptalYil = worksheet.Cells[row, 43].Value?.ToString(),
                                GorevIptalBAOK = worksheet.Cells[row, 44].Value?.ToString(),
                                IlkGorevKaydi = worksheet.Cells[row, 45].Value?.ToString(),
                                YabanciDilALM = worksheet.Cells[row, 46].Value?.ToString(),
                                YabanciDilING = worksheet.Cells[row, 47].Value?.ToString(),
                                YabanciDilFRS = worksheet.Cells[row, 48].Value?.ToString(),
                                YabanciDilDiger = worksheet.Cells[row, 49].Value?.ToString(),
                                GorevdenUzaklastirma = worksheet.Cells[row, 50].Value?.ToString(),
                                EDurum = worksheet.Cells[row, 51].Value?.ToString(),
                                MDurum = worksheet.Cells[row, 52].Value?.ToString(),
                                PDurum = worksheet.Cells[row, 53].Value?.ToString(),
                                // GenelDurum için IlMemGorus ve PDurum kontrolü
                                GenelDurum = (worksheet.Cells[row, 38].Value?.ToString()?.ToLower().Contains("olumlu") == true ||
                                            worksheet.Cells[row, 38].Value?.ToString()?.ToLower().Contains("uygun") == true ||
                                            worksheet.Cells[row, 38].Value?.ToString()?.ToLower().Contains("uygundur") == true ||
                                            worksheet.Cells[row, 53].Value?.ToString()?.ToLower().Contains("olumlu") == true ||
                                            worksheet.Cells[row, 53].Value?.ToString()?.ToLower().Contains("uygun") == true ||
                                            worksheet.Cells[row, 53].Value?.ToString()?.ToLower().Contains("uygundur") == true) ? "OLUMLU" :
                                           (worksheet.Cells[row, 38].Value?.ToString()?.ToLower().Contains("olumsuz") == true ||
                                            worksheet.Cells[row, 38].Value?.ToString()?.ToLower().Contains("uygun değildir") == true ||
                                            worksheet.Cells[row, 53].Value?.ToString()?.ToLower().Contains("olumsuz") == true ||
                                            worksheet.Cells[row, 53].Value?.ToString()?.ToLower().Contains("uygun değildir") == true) ? "OLUMSUZ" :
                                            worksheet.Cells[row, 54].Value?.ToString(),
                                //
                                YLisans = worksheet.Cells[row, 55].Value?.ToString(),
                                Doktora = worksheet.Cells[row, 56].Value?.ToString(),
                                Sendika = worksheet.Cells[row, 57].Value?.ToString(),
                                SendikaAck = worksheet.Cells[row, 58].Value?.ToString(),
                                MYYSSoruItiraz = worksheet.Cells[row, 59].Value?.ToString(),
                                MYYSSonucItiraz = worksheet.Cells[row, 60].Value?.ToString(),
                                BasvuruBrans = worksheet.Cells[row, 61].Value?.ToString(),                                
                                BransId = brans?.BransId,
                                BransAdi=brans?.BransAdi,
                                DereceId = _derecelerBE.DereceleriGetir().Data
                                    .FirstOrDefault(d => d.DereceAdi == worksheet.Cells[row, 63].Value?.ToString())?.DereceId,
                                DereceAdi = worksheet.Cells[row, 63].Value?.ToString(),
                                Unvan = worksheet.Cells[row, 64].Value?.ToString(),
                                DahaOnceSinav = worksheet.Cells[row, 66].Value?.ToString(),
                                UlkeTercihId = ulketercih?.UlkeTercihId,
                                UlkeTercihAdi = ulketercih?.UlkeTercihAdi,                                
                                MulakatId = ((List<MulakatlarVM>)ViewBag.Mulakatlar).FirstOrDefault()?.MulakatId,                               
                                AdayId = _adaylarBE.AdayTemelBilgileriGetir().Data
                                    .FirstOrDefault(a => a.TC == worksheet.Cells[row, 1].Value?.ToString())?.AdayId
                            };

                            var result = _adaylarBE.AdayBasvuruBilgileriEkle(aday, user);
                            if (result.IsSuccess)
                            {
                                basariliEklenen++;
                            }

                            islemYapilan++;
                            _progressService.UpdateProgress(sessionId, p =>
                            {
                                p.IslemYapilan = islemYapilan;
                                p.BasariliEklenen = basariliEklenen;
                                p.Mesaj = $"Kayıtlar işleniyor... ({islemYapilan}/{kayitEdilecekToplam})";
                            });
                            await Task.Delay(10);
                        }

                        // İşlem Tamamlandı
                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Tamamlandi";
                            p.IslemYapilan = kayitEdilecekToplam;
                            if (basariliEklenen > 0)
                            {
                                p.Success = $"{basariliEklenen} adet kayıt başarıyla eklenmiştir.";
                            }
                            p.Mesaj = "İşlem tamamlandı";
                        });

                        await Task.Delay(500);

                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel'den aday başvuru bilgileri yüklenirken hata oluştu: {Message}", ex.Message);
                _progressService.UpdateProgress(sessionId, p =>
                {
                    p.IslemAsamasi = "Hata";
                    p.Error = "İşlem sırasında hata oluştu: " + ex.Message;
                    p.Mesaj = "Hata oluştu!";
                });
                return Json(new { success = false });
            }
            finally
            {
                _progressService.ResetProgress(sessionId);
            }
        }
        #endregion

        #region AdayIletisimBilgileriYukle
        [HttpPost]
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> AdayIletisimBilgileriYukle(IFormFile file)
        {
            var sessionId = HttpContext.Session.Id;

            try
            {
                var mulakatResult = _mulakatOlusturBE.MulakatlariGetirSelectedBox();
                if (mulakatResult.IsSuccess)
                {
                    ViewBag.Mulakatlar = mulakatResult.Data;
                }

                if (file == null || file.Length == 0)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Lütfen bir dosya seçin!";
                        p.Mesaj = "Lütfen bir dosya seçin!";
                    });
                    return Json(new { success = false });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                if (user == null)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Oturum bilgisi bulunamadı!";
                        p.Mesaj = "Oturum bilgisi bulunamadı!";
                    });
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
                            _progressService.UpdateProgress(sessionId, p =>
                            {
                                p.IslemAsamasi = "Hata";
                                p.Error = "Excel dosyası boş veya geçersiz!";
                                p.Mesaj = "Excel dosyası boş veya geçersiz!";
                            });
                            return Json(new { success = false });
                        }

                        var toplamKayit = rowCount - 1;
                        var islemYapilan = 0;
                        var basariliEklenen = 0;

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Baslatiliyor";
                            p.ToplamKayit = toplamKayit;
                            p.IslemYapilan = 0;
                            p.BasariliEklenen = 0;
                            p.Mesaj = "İletişim bilgileri yükleme işlemi başlatılıyor...";
                        });

                        await Task.Delay(500);

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Kayit";
                            p.Mesaj = "İletişim bilgileri sisteme yükleniyor...";
                        });

                        for (int row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                var adayIletisim = new AdayIletisimBilgileriVM
                                {
                                    TC = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                                    CepTelNo = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                                    EPosta = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                                    NufusIl = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                                    NufusIlce = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                                    IkametAdres = worksheet.Cells[row, 6].Value?.ToString().Trim(),
                                    IkametIl = worksheet.Cells[row, 7].Value?.ToString().Trim(),
                                    IkametIlce = worksheet.Cells[row, 8].Value?.ToString().Trim(),
                                    MulakatId = ((List<MulakatlarVM>)ViewBag.Mulakatlar)?.FirstOrDefault()?.MulakatId
                                };

                                if (string.IsNullOrEmpty(adayIletisim.TC))
                                {
                                    continue;
                                }

                                var adayBilgisi = _adaylarBE.AdayTemelBilgileriGetir().Data
                                    .FirstOrDefault(a => a.TC == adayIletisim.TC);
                      
                                if (adayBilgisi != null)
                                {
                                    adayIletisim.AdayId = adayBilgisi.AdayId;                                 

                                    var result = _adaylarBE.AdayIletisimBilgileriEkle(adayIletisim, user);
                                    if (result.IsSuccess)
                                    {
                                        basariliEklenen++;
                                    }
                                }

                                islemYapilan++;
                                _progressService.UpdateProgress(sessionId, p =>
                                {
                                    p.IslemYapilan = islemYapilan;
                                    p.BasariliEklenen = basariliEklenen;
                                    p.Mesaj = $"İletişim bilgileri güncelleniyor... ({islemYapilan}/{toplamKayit})";
                                });

                                if (islemYapilan % 5 == 0)
                                {
                                    await Task.Delay(100);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, $"Kayıt işlenirken hata oluştu. Satır: {row}");
                            }
                        }

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Tamamlandi";
                            p.IslemYapilan = toplamKayit;
                            p.BasariliEklenen = basariliEklenen;
                            p.Success = $"İşlem tamamlandı! {basariliEklenen} adet iletişim bilgisi başarıyla yüklenmiştir.";
                            p.Mesaj = "İşlem başarıyla tamamlandı!";
                        });

                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İletişim bilgileri yükleme işlemi sırasında hata: {Message}", ex.Message);
                _progressService.UpdateProgress(sessionId, p =>
                {
                    p.IslemAsamasi = "Hata";
                    p.Error = "İşlem sırasında hata oluştu: " + ex.Message;
                    p.Mesaj = "Hata oluştu!";
                });
                return Json(new { success = false });
            }
            finally
            {
                _progressService.ResetProgress(sessionId);
            }
        }
        #endregion

        #region AdayIletisimBilgileriniGetir
        public IActionResult AdayIletisimBilgileriniGetir(string TC)
        {
            var result = _adaylarBE.AdayIletisimBilgileriniGetir(TC);
            var data = (result.IsSuccess && result.Data != null && result.Data.Any()) ? result.Data : new List<AdayIletisimBilgileriVM>();
            return ViewComponent("AdayIletisimBilgileri", data);
        }
        #endregion

        #region AdayBasvuruBilgileriniGetir
        [HttpGet]
        public IActionResult AdayBasvuruBilgileriniGetir(string TC)
        {
            var result = _adaylarBE.AdayBasvuruBilgileriniGetir(TC);
            var data = (result.IsSuccess && result.Data != null && result.Data.Any()) ? result.Data : new List<AdayBasvuruBilgileriVM>();
            return ViewComponent("AdayBasvuruBilgileri", data);
        }
        #endregion

        #region AdayMYSSBilgileriYukle
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> AdayMYSSBilgileriYukle(IFormFile file)
        {
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetirSelectedBox().Data;
            var sessionId = HttpContext.Session.Id;

            try
            {
                if (file == null || file.Length == 0)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Lütfen bir dosya seçin!";
                        p.Mesaj = "Lütfen bir dosya seçin!";
                    });
                    return Json(new { success = false });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                if (user == null)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Oturum bilgisi bulunamadı!";
                        p.Mesaj = "Oturum bilgisi bulunamadı!";
                    });
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
                            _progressService.UpdateProgress(sessionId, p =>
                            {
                                p.IslemAsamasi = "Hata";
                                p.Error = "Excel dosyası boş veya geçersiz!";
                                p.Mesaj = "Excel dosyası boş veya geçersiz!";
                            });
                            return Json(new { success = false });
                        }

                        var toplamKayit = rowCount - 1;
                        var islemYapilan = 0;
                        var basariliEklenen = 0;

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Baslatiliyor";
                            p.ToplamKayit = toplamKayit;
                            p.IslemYapilan = 0;
                            p.BasariliEklenen = 0;
                            p.Mesaj = "MYSS bilgileri yükleme işlemi başlatılıyor...";
                        });

                        await Task.Delay(100);

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Kayit";
                            p.IslemYapilan = 0;
                            p.Mesaj = "MYSS bilgileri işleniyor...";
                        });

                        for (int row = 2; row <= rowCount; row++)
                        {
                            //KomisyonSiraNo ile KomisyonId yi eşleştirme
                            var komisyon = _komisyonlarBE.KomisyonlariGetir().Data
                                .FirstOrDefault(d => d.KomisyonSiraNo == Convert.ToInt32(worksheet.Cells[row, 7].Value?.ToString()));                            
                            //BransAdi ile BransId yi eşleştirme
                            var brans = _branslarBE.BranslariGetir().Data
                                .FirstOrDefault(b => b.BransAdi == worksheet.Cells[row, 13].Value?.ToString());
                            var ulketercih = _ulkeTercihleriBE.UlkeTercihAdlariGetir().Data
                                .FirstOrDefault(u => u.UlkeTercihAdi == worksheet.Cells[row, 15].Value?.ToString() && u.YabancıDil == worksheet.Cells[row, 14].Value?.ToString());

                            var aday = new AdayMYSSVM
                            {
                                TC = worksheet.Cells[row, 1].Value?.ToString(),
                                MYSSTarih = worksheet.Cells[row, 2].Value?.ToString(),
                                MYSSSaat = worksheet.Cells[row, 3].Value?.ToString(),
                                MYSSMulakatYer = worksheet.Cells[row, 4].Value?.ToString(),
                                MYSSDurum = worksheet.Cells[row, 5].Value?.ToString(),
                                MYSSDurumAck = worksheet.Cells[row, 6].Value?.ToString(),
                                MYSSKomisyonSiraNo = worksheet.Cells[row, 7].Value != null ? 
                                    Convert.ToInt32(worksheet.Cells[row, 7].Value) : 0,
                                KomisyonSN = worksheet.Cells[row, 8].Value != null ? 
                                    Convert.ToInt32(worksheet.Cells[row, 8].Value) : 0,
                                KomisyonGunSN = worksheet.Cells[row, 9].Value != null ? 
                                    Convert.ToInt32(worksheet.Cells[row, 9].Value) : 0,
                                MYSPuan = worksheet.Cells[row, 10].Value?.ToString(),
                                MYSSonuc = worksheet.Cells[row, 11].Value?.ToString(),
                                MYSSonucAck = worksheet.Cells[row, 12].Value?.ToString(),

                                KomisyonId = komisyon?.KomisyonId,                                
                                MYSSKomisyonAdi = komisyon?.KomisyonAdi,

                                BransId = brans?.BransId,
                                BransAdi = brans?.BransAdi,

                                DereceId = _derecelerBE.DereceleriGetir().Data
                                    .FirstOrDefault(d => d.DereceAdi == worksheet.Cells[row, 16].Value?.ToString())?.DereceId,
                                DereceAdi = worksheet.Cells[row, 16].Value?.ToString(),
                               
                                UlkeTercihId = ulketercih?.UlkeTercihId,
                                UlkeTercihAdi = ulketercih?.UlkeTercihAdi,

                                MulakatId = ((List<MulakatlarVM>)ViewBag.Mulakatlar).FirstOrDefault()?.MulakatId,
                                AdayId = _adaylarBE.AdayTemelBilgileriGetir().Data
                                    .FirstOrDefault(a => a.TC == worksheet.Cells[row, 1].Value?.ToString())?.AdayId
                            };

                            var result = _adaylarBE.AdayMYSSBilgileriEkle(aday, user);
                            if (result.IsSuccess)
                            {
                                basariliEklenen++;
                            }

                            islemYapilan++;
                            _progressService.UpdateProgress(sessionId, p =>
                            {
                                p.IslemYapilan = islemYapilan;
                                p.BasariliEklenen = basariliEklenen;
                                p.Mesaj = $"MYSS bilgileri işleniyor... ({islemYapilan}/{toplamKayit})";
                            });
                            await Task.Delay(10);
                        }

                        // İşlem Tamamlandı
                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Tamamlandi";
                            p.IslemYapilan = toplamKayit;
                            if (basariliEklenen > 0)
                            {
                                p.Success = $"{basariliEklenen} adet MYSS bilgisi başarıyla eklenmiştir.";
                            }
                            p.Mesaj = "İşlem tamamlandı";
                        });

                        await Task.Delay(500);

                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MYSS bilgileri yükleme işlemi sırasında hata: {Message}", ex.Message);
                _progressService.UpdateProgress(sessionId, p =>
                {
                    p.IslemAsamasi = "Hata";
                    p.Error = "İşlem sırasında hata oluştu: " + ex.Message;
                    p.Mesaj = "Hata oluştu!";
                });
                return Json(new { success = false });
            }
            finally
            {
                _progressService.ResetProgress(sessionId);
            }
        }
        #endregion

        #region AdayMYSSBilgileriniGetir
        [HttpGet]
        public IActionResult AdayMYSSBilgileriniGetir(string TC)
        {
            var result = _adaylarBE.AdayMYSSBilgileriniGetir(TC);
            var data = (result.IsSuccess && result.Data != null && result.Data.Any()) ? result.Data : new List<AdayMYSSVM>();
            return ViewComponent("AdayMYSSBilgileri", data);
        }
        #endregion

        #region AdayTYSBilgileriYukle
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> AdayTYSBilgileriYukle(IFormFile file)
        {
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetirSelectedBox().Data;
            var sessionId = HttpContext.Session.Id;

            try
            {
                if (file == null || file.Length == 0)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Lütfen bir dosya seçin!";
                        p.Mesaj = "Lütfen bir dosya seçin!";
                    });
                    return Json(new { success = false });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                if (user == null)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Oturum bilgisi bulunamadı!";
                        p.Mesaj = "Oturum bilgisi bulunamadı!";
                    });                        
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
                            _progressService.UpdateProgress(sessionId, p =>
                            {
                                p.IslemAsamasi = "Hata";
                                p.Error = "Excel dosyası boş veya geçersiz!";
                                p.Mesaj = "Excel dosyası boş veya geçersiz!";
                            });
                            return Json(new { success = false });
                        }

                        var toplamKayit = rowCount - 1;
                        var islemYapilan = 0;
                        var basariliEklenen = 0;

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Baslatiliyor";
                            p.ToplamKayit = toplamKayit;
                            p.IslemYapilan = 0;
                            p.BasariliEklenen = 0;
                            p.Mesaj = "TYS bilgileri yükleme işlemi başlatılıyor...";
                        });

                        await Task.Delay(100);

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Kayit";
                            p.IslemYapilan = 0;
                            p.Mesaj = "TYS bilgileri işleniyor...";
                        });

                        for (int row = 2; row <= rowCount; row++)
                        {
                            //KomisyonSiraNo ile KomisyonId yi eşleştirme
                            var komisyon = _komisyonlarBE.KomisyonlariGetir().Data
                                .FirstOrDefault(d => d.KomisyonSiraNo == Convert.ToInt32(worksheet.Cells[row, 7].Value?.ToString()));
                            //BransAdi ile BransId yi eşleştirme
                            var brans = _branslarBE.BranslariGetir().Data
                                .FirstOrDefault(b => b.BransAdi == worksheet.Cells[row, 10].Value?.ToString());
                            var ulketercih = _ulkeTercihleriBE.UlkeTercihAdlariGetir().Data
                                .FirstOrDefault(u => u.UlkeTercihAdi == worksheet.Cells[row, 12].Value?.ToString() && u.YabancıDil == worksheet.Cells[row, 11].Value?.ToString()); 

                            var aday = new AdayTYSVM
                            {
                                TC = worksheet.Cells[row, 1].Value?.ToString(),
                                TYSTarih = worksheet.Cells[row, 2].Value?.ToString(),
                                TYSSaat = worksheet.Cells[row, 3].Value?.ToString(),
                                TYSMulakatYer = worksheet.Cells[row, 4].Value?.ToString(),
                                TYSDurumu = worksheet.Cells[row, 5].Value?.ToString(),
                                TYSDurumAck = worksheet.Cells[row, 6].Value?.ToString(),
                                TYSKomisyonSiraNo = worksheet.Cells[row, 7].Value != null ?
                                    Convert.ToInt32(worksheet.Cells[row, 7].Value) : 0,                            
                                KomisyonSN = worksheet.Cells[row, 8].Value != null ?
                                    Convert.ToInt32(worksheet.Cells[row, 8].Value) : 0,
                                KomisyonGunSN = worksheet.Cells[row, 9].Value != null ?
                                    Convert.ToInt32(worksheet.Cells[row, 9].Value) : 0,

                                KomisyonId = komisyon?.KomisyonId,
                                TYSKomisyonAdi = komisyon?.KomisyonAdi,

                                BransId = brans?.BransId,
                                BransAdi = brans?.BransAdi,

                                DereceId = _derecelerBE.DereceleriGetir().Data
                                    .FirstOrDefault(d => d.DereceAdi == worksheet.Cells[row, 13].Value?.ToString())?.DereceId,
                                DereceAdi = worksheet.Cells[row, 13].Value?.ToString(),

                                UlkeTercihId = ulketercih?.UlkeTercihId,
                                UlkeTercihAdi = ulketercih?.UlkeTercihAdi,


                                MulakatId = ((List<MulakatlarVM>)ViewBag.Mulakatlar).FirstOrDefault()?.MulakatId,
                                AdayId = _adaylarBE.AdaylariGetir().Data
                                    .FirstOrDefault(a => a.TC == worksheet.Cells[row, 1].Value?.ToString())?.AdayId
                            };

                            var result = _adaylarBE.AdayTYSBilgileriEkle(aday, user);
                            if (result.IsSuccess)
                            {
                                basariliEklenen++;
                            }

                            islemYapilan++;
                            _progressService.UpdateProgress(sessionId, p =>
                            {
                                p.IslemYapilan = islemYapilan;
                                p.BasariliEklenen = basariliEklenen;
                                p.Mesaj = $"TYS bilgileri işleniyor... ({islemYapilan}/{toplamKayit})";
                            });
                            await Task.Delay(10);
                        }

                        // İşlem Tamamlandı
                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Tamamlandi";
                            p.IslemYapilan = toplamKayit;
                            if (basariliEklenen > 0)
                            {
                                p.Success = $"{basariliEklenen} adet TYS bilgisi başarıyla eklenmiştir.";
                            }
                            p.Mesaj = "İşlem tamamlandı";
                        });

                        await Task.Delay(500);

                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TYS bilgileri yükleme işlemi sırasında hata: {Message}", ex.Message);
                _progressService.UpdateProgress(sessionId, p =>
                {
                    p.IslemAsamasi = "Hata";
                    p.Error = "İşlem sırasında hata oluştu: " + ex.Message;
                    p.Mesaj = "Hata oluştu!";
                });
                return Json(new { success = false });
            }
            finally
            {
                _progressService.ResetProgress(sessionId);
            }
        }
        #endregion

        #region AdayTYSBilgileriniGetir
        [HttpGet]
        public IActionResult AdayTYSBilgileriniGetir(string TC)
        {
            var result = _adaylarBE.AdayTYSBilgileriniGetir(TC);
            var data = (result.IsSuccess && result.Data != null && result.Data.Any()) ? result.Data : new List<AdayTYSVM>();
            return ViewComponent("AdayTYSBilgileri", data);
        }
        #endregion

        #region AdayBasvuruBilgileriTopluGuncelle
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> AdayBilgileriTopluGuncelle(IFormFile file)
        {
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetirSelectedBox().Data;
            var sessionId = HttpContext.Session.Id;

            try
            {
                if (file == null || file.Length == 0)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Lütfen bir dosya seçin!";
                        p.Mesaj = "Lütfen bir dosya seçin!";
                    });
                    return Json(new { success = false });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                if (user == null)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Oturum bilgisi bulunamadı!";
                        p.Mesaj = "Oturum bilgisi bulunamadı!";
                    });
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
                            _progressService.UpdateProgress(sessionId, p =>
                            {
                                p.IslemAsamasi = "Hata";
                                p.Error = "Excel dosyası boş veya geçersiz!";
                                p.Mesaj = "Excel dosyası boş veya geçersiz!";
                            });
                            return Json(new { success = false });
                        }

                        var toplamKayit = rowCount - 1;
                        var islemYapilan = 0;
                        var basariliEklenen = 0;

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Baslatiliyor";
                            p.ToplamKayit = toplamKayit;
                            p.IslemYapilan = 0;
                            p.BasariliEklenen = 0;
                            p.Mesaj = "Güncelleme bilgileri yükleme işlemi başlatılıyor...";
                        });

                        await Task.Delay(100);

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Kayit";
                            p.IslemYapilan = 0;
                            p.Mesaj = "Güncelleme bilgileri işleniyor...";
                        });

                        for (int row = 2; row <= rowCount; row++)
                        {
                            try 
                            {
                                var tc = worksheet.Cells[row, 1].Value?.ToString();
                                var tarih = worksheet.Cells[row, 2].Value?.ToString();
                                
                                if (string.IsNullOrEmpty(tc) || string.IsNullOrEmpty(tarih))
                                {
                                    _logger.LogWarning($"Satır {row}: TC veya tarih bilgisi boş");
                                    continue;
                                }

                                var data = _unitOfWork.adayBasvuruBilgileriRepository.GetFirstOrDefault(a => a.TC == tc && a.MYYSTarihi == tarih);
                                
                                if (data == null)
                                {
                                    _logger.LogWarning($"Satır {row}: TC:{tc} ve Tarih:{tarih} ile eşleşen kayıt bulunamadı");
                                    continue;
                                }

                                var mulakatId = ((List<MulakatlarVM>)ViewBag.Mulakatlar).FirstOrDefault()?.MulakatId;
                                if (mulakatId == null)
                                {
                                    _logger.LogWarning($"Satır {row}: MulakatId bulunamadı");
                                    continue;
                                }

                                var aday = new AdayBasvuruBilgileriVM
                                {
                                    Id = data.Id,
                                    TC = tc,
                                    MulakatId = mulakatId,
                                    AdayId = data.AdayId,
                                    // Diğer mevcut bilgileri de kopyalayalım ki güncelleme sırasında kaybolmasın
                                    MYYSTarihi = data.MYYSTarihi,
                                    // Diğer alanları da ekleyin...
                                };

                                var result = _adaylarBE.AdayTopluGuncelle(aday, user);
                                if (result.IsSuccess)
                                {
                                    basariliEklenen++;
                                    _logger.LogInformation($"Satır {row}: TC:{tc} başarıyla güncellendi");
                                }
                                else
                                {
                                    _logger.LogWarning($"Satır {row}: TC:{tc} güncellenemedi. Hata: {result.Message}");
                                }

                                islemYapilan++;
                                _progressService.UpdateProgress(sessionId, p =>
                                {
                                    p.IslemYapilan = islemYapilan;
                                    p.BasariliEklenen = basariliEklenen;
                                    p.Mesaj = $"Güncelleme bilgileri işleniyor... ({islemYapilan}/{toplamKayit})";
                                });
                                await Task.Delay(10);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, $"Satır {row} işlenirken hata oluştu");
                            }
                        }

                        // İşlem Tamamlandı
                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Tamamlandi";
                            p.IslemYapilan = toplamKayit;
                            if (basariliEklenen > 0)
                            {
                                p.Success = $"{basariliEklenen} adet güncelleme bilgisi başarıyla eklenmiştir.";
                            }
                            p.Mesaj = "İşlem tamamlandı";
                        });

                        await Task.Delay(500);

                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Güncelleme bilgileri yükleme işlemi sırasında hata: {Message}", ex.Message);
                _progressService.UpdateProgress(sessionId, p =>
                {
                    p.IslemAsamasi = "Hata";
                    p.Error = "İşlem sırasında hata oluştu: " + ex.Message;
                    p.Mesaj = "Hata oluştu!";
                });
                return Json(new { success = false });
            }
            finally
            {
                _progressService.ResetProgress(sessionId);
            }
        }
        #endregion

        #region AdayMYSSBilgileriTopluGuncelle
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> AdayMYSSBilgileriTopluGuncelle(IFormFile file)
        {
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetirSelectedBox().Data;
            var sessionId = HttpContext.Session.Id;

            try
            {
                if (file == null || file.Length == 0)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Lütfen bir dosya seçin!";
                        p.Mesaj = "Lütfen bir dosya seçin!";
                    });
                    return Json(new { success = false });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                if (user == null)
                {
                    _progressService.UpdateProgress(sessionId, p =>
                    {
                        p.IslemAsamasi = "Hata";
                        p.Error = "Oturum bilgisi bulunamadı!";
                        p.Mesaj = "Oturum bilgisi bulunamadı!";
                    });
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
                            _progressService.UpdateProgress(sessionId, p =>
                            {
                                p.IslemAsamasi = "Hata";
                                p.Error = "Excel dosyası boş veya geçersiz!";
                                p.Mesaj = "Excel dosyası boş veya geçersiz!";
                            });
                            return Json(new { success = false });
                        }

                        var toplamKayit = rowCount - 1;
                        var islemYapilan = 0;
                        var basariliEklenen = 0;

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Baslatiliyor";
                            p.ToplamKayit = toplamKayit;
                            p.IslemYapilan = 0;
                            p.BasariliEklenen = 0;
                            p.Mesaj = "Güncelleme bilgileri yükleme işlemi başlatılıyor...";
                        });

                        await Task.Delay(100);

                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Kayit";
                            p.IslemYapilan = 0;
                            p.Mesaj = "Güncelleme bilgileri işleniyor...";
                        });

                        for (int row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                var tc = worksheet.Cells[row, 1].Value?.ToString();
                                var tarih = worksheet.Cells[row, 2].Value?.ToString();

                                if (string.IsNullOrEmpty(tc) || string.IsNullOrEmpty(tarih))
                                {
                                    _logger.LogWarning($"Satır {row}: TC veya tarih bilgisi boş");
                                    continue;
                                }

                                var data = _unitOfWork.adayMYSSRepository.GetFirstOrDefault(a => a.TC == tc && a.MYSSTarih == tarih);

                                if (data == null)
                                {
                                    _logger.LogWarning($"Satır {row}: TC:{tc} ve Tarih:{tarih} ile eşleşen kayıt bulunamadı");
                                    continue;
                                }

                                var mulakatId = ((List<MulakatlarVM>)ViewBag.Mulakatlar).FirstOrDefault()?.MulakatId;
                                if (mulakatId == null)
                                {
                                    _logger.LogWarning($"Satır {row}: MulakatId bulunamadı");
                                    continue;
                                }

                                var aday = new AdayMYSSVM
                                {
                                    Id = data.Id,
                                    TC = tc,
                                    MulakatId = mulakatId,
                                    AdayId = data.AdayId,
                                    UlkeTercihAdi = worksheet.Cells[row, 3].Value?.ToString(),
                                    UlkeTercihId = Guid.Parse(worksheet.Cells[row, 4].Value?.ToString()),
                                    BransAdi = worksheet.Cells[row, 5].Value?.ToString(),
                                    BransId = Guid.Parse(worksheet.Cells[row, 6].Value?.ToString()),
                                };

                                var result = _adaylarBE.AdayMYSSTopluGuncelle(aday, user);
                                if (result.IsSuccess)
                                {
                                    basariliEklenen++;
                                    _logger.LogInformation($"Satır {row}: TC:{tc} başarıyla güncellendi");
                                }
                                else
                                {
                                    _logger.LogWarning($"Satır {row}: TC:{tc} güncellenemedi. Hata: {result.Message}");
                                }

                                islemYapilan++;
                                _progressService.UpdateProgress(sessionId, p =>
                                {
                                    p.IslemYapilan = islemYapilan;
                                    p.BasariliEklenen = basariliEklenen;
                                    p.Mesaj = $"Güncelleme bilgileri işleniyor... ({islemYapilan}/{toplamKayit})";
                                });
                                await Task.Delay(10);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, $"Satır {row} işlenirken hata oluştu");
                            }
                        }

                        // İşlem Tamamlandı
                        _progressService.UpdateProgress(sessionId, p =>
                        {
                            p.IslemAsamasi = "Tamamlandi";
                            p.IslemYapilan = toplamKayit;
                            if (basariliEklenen > 0)
                            {
                                p.Success = $"{basariliEklenen} adet güncelleme bilgisi başarıyla eklenmiştir.";
                            }
                            p.Mesaj = "İşlem tamamlandı";
                        });

                        await Task.Delay(500);

                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Güncelleme bilgileri yükleme işlemi sırasında hata: {Message}", ex.Message);
                _progressService.UpdateProgress(sessionId, p =>
                {
                    p.IslemAsamasi = "Hata";
                    p.Error = "İşlem sırasında hata oluştu: " + ex.Message;
                    p.Mesaj = "Hata oluştu!";
                });
                return Json(new { success = false });
            }
            finally
            {
                _progressService.ResetProgress(sessionId);
            }
        }
        #endregion
    }
}
