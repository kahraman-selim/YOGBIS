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
                        var basariliEklenen = 0;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                var kategoriModel = new SoruKategorilerVM
                                {
                                    SoruKategorilerSiraNo = Convert.ToInt32(worksheet.Cells[row, 1].Value?.ToString().Trim()),
                                    SoruKategorilerAdi = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                                    SoruKategorilerKullanimi = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                                    SoruKategorilerPuan = Convert.ToInt32(worksheet.Cells[row, 4].Value?.ToString().Trim()),
                                    SoruKategorilerTakmaAdi = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                                    SoruKategorilerTamAdi = worksheet.Cells[row, 6].Value?.ToString().Trim(),
                                    SinavKateogoriTurId = Convert.ToInt32(worksheet.Cells[row, 7].Value?.ToString().Trim()),
                                    SinavKategoriTurAdi = worksheet.Cells[row, 8].Value?.ToString().Trim(),
                                    DereceId = dereceId
                                    //DereceId = _derecelerBE.DereceleriGetir().Data
                                    //.FirstOrDefault(d => d.DereceAdi == worksheet.Cells[row, 9].Value?.ToString())?.DereceId                                    
                                };
                                                                
                                var result = _soruKategorileriBE.SoruKategoriEkle(kategoriModel, user);
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
                            var message = $"{basariliEklenen} adet kategori kaydı başarıyla eklendi.";
                            if (hatalar.Any())
                            {
                                message += $" Ancak {hatalar.Count} adet hata oluştu: {string.Join(", ", hatalar)}";
                            }
                            return Json(new { success = true, message = message });
                        }
                        else
                        {
                            var errorMessage = "Kategori kaydı eklenemedi. ";
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
    }
}
