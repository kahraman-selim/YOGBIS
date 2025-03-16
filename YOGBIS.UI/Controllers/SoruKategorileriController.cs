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
using YOGBIS.Data.Implementaion;
using YOGBIS.BusinessEngine.Implementaion;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SoruKategorileriController : Controller
    {
        #region Değişkenler
        private readonly UserManager<Kullanici> _userManager;
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly ILogger<SoruKategorileriController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private static readonly object _lockObject = new object();
        #endregion

        #region Dönüştürücüler
        public SoruKategorileriController(
            UserManager<Kullanici> userManager,
            ILogger<SoruKategorileriController> logger,

            ISoruKategorileriBE soruKategorileriBE, 
            IDerecelerBE derecelerBE,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _logger = logger;
            _soruKategorileriBE = soruKategorileriBE;
            _derecelerBE = derecelerBE;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Index
        public IActionResult Index(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            try
            {
                var result = _derecelerBE.DereceleriGetir();
                if (result.IsSuccess)
                {
                    ViewBag.Dereceler = result.Data;

                    if (id != null)
                    {
                        var data = _soruKategorileriBE.SoruKategoriGetir((Guid)id);
                        return View(data.Data);
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = result.Message;
                    return View();
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Dereceler getirilirken bir hata oluştu.";                
            }

            return View();
        }
        #endregion

        #region SoruKategoriEkleGet
        [HttpGet]
        public IActionResult SoruKategoriEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            return View();
        }
        #endregion

        #region SoruKategoriEklePost
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SoruKategoriEkle(SoruKategorilerVM model, Guid? SoruKategorilerId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (SoruKategorilerId != null)
            {
                var data = _soruKategorileriBE.SoruKategoriGuncelle(model, user);
                return RedirectToAction("Index");
            }
            else
            {
                var data = _soruKategorileriBE.SoruKategoriEkle(model, user);
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Guncelle
        public IActionResult Guncelle(Guid? id)
        {
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (id != null)
            {
                var data = _soruKategorileriBE.SoruKategoriGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region SoruKategoriSil
        [HttpDelete]
        public IActionResult SoruKategoriSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _soruKategorileriBE.SoruKategoriSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });
        }
        #endregion

        #region KategoriYukle
        [HttpPost]
        public async Task<IActionResult> KategoriYukle(IFormFile file, Guid dereceId)
        {
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            var sessionId = HttpContext.Session.Id;

            try
            {
                if (file == null || file.Length == 0)
                {
                    _logger.LogWarning("Excel dosyası boş veya seçilmedi");
                    return Json(new { success = false, error = "Lütfen bir Excel dosyası seçiniz!" });
                }

                if (dereceId == Guid.Empty)
                {
                    _logger.LogWarning("Derece seçilmedi");
                    return Json(new { success = false, error = "Lütfen bir Derece seçiniz!" });
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
                        var basariliKayitlar = 0;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                var kategori = new SoruKategorilerVM
                                {
                                    DereceId = dereceId,
                                    SoruKategorilerSiraNo = Convert.ToInt32(worksheet.Cells[row, 1].Value?.ToString()),
                                    SoruKategorilerAdi = worksheet.Cells[row, 2].Value?.ToString(),
                                    SoruKategorilerKullanimi = worksheet.Cells[row, 3].Value?.ToString(),
                                    SoruKategorilerPuan = Convert.ToInt32(worksheet.Cells[row, 4].Value?.ToString()),
                                    SoruKategorilerTakmaAdi = worksheet.Cells[row, 5].Value?.ToString(),
                                    SoruKategorilerTamAdi = worksheet.Cells[row, 6].Value?.ToString(),
                                    SinavKateogoriTurId = Convert.ToInt32(worksheet.Cells[row, 7].Value?.ToString()),
                                    SinavKategoriTurAdi = worksheet.Cells[row, 8].Value?.ToString()
                                };

                                if (string.IsNullOrWhiteSpace(kategori.SoruKategorilerAdi))
                                {
                                    hatalar.Add($"Satır {row}: Kategori adı boş olamaz.");
                                    continue;
                                }

                                if (kategori.SoruKategorilerSiraNo <= 0)
                                {
                                    hatalar.Add($"Satır {row}: Geçersiz sıra numarası.");
                                    continue;
                                }

                                var result = _soruKategorileriBE.SoruKategoriEkle(kategori, user);
                                if (result.IsSuccess)
                                {
                                    basariliKayitlar++;
                                }
                                else
                                {
                                    hatalar.Add($"Satır {row}: {result.Message}");
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, $"Excel yükleme hatası - Satır {row}");
                                hatalar.Add($"Satır {row}: İşlem sırasında bir hata oluştu.");
                            }
                        }

                        if (hatalar.Any())
                        {
                            var hataMessage = string.Join("\n", hatalar);
                            _logger.LogWarning($"Excel yükleme tamamlandı - {basariliKayitlar} başarılı, {hatalar.Count} hatalı kayıt. Hatalar: {hataMessage}");
                            return Json(new { success = false, error = hataMessage });
                        }

                        _logger.LogInformation($"Excel yükleme başarıyla tamamlandı - {basariliKayitlar} kayıt eklendi.");
                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel yükleme işlemi sırasında beklenmeyen bir hata oluştu");
                return Json(new { success = false, error = "Excel yükleme işlemi sırasında bir hata oluştu." });
            }
        }
        #endregion
    }
}
