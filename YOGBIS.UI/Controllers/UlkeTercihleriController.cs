using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.Implementaion;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UlkeTercihleriController : Controller
    {
        #region Değişkenler
        private readonly IUlkeTercihleriBE _ulkeTercihleriBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBranslarBE _branslarBE;
        private readonly IUlkeTercihBranslarBE _ulkeTercihBranslarBE;
        private readonly ILogger<UlkeTercihleriController> _logger;
        #endregion

        #region Dönüştürücüler
        public UlkeTercihleriController(IUlkeTercihleriBE ulkeTercihleriBE, IDerecelerBE derecelerBE, 
            IMulakatOlusturBE mulakatOlusturBE, IUnitOfWork unitOfWork, IBranslarBE branslarBE, IUlkeTercihBranslarBE ulkeTercihBranslarBE, ILogger<UlkeTercihleriController> logger)
        {
            _ulkeTercihleriBE = ulkeTercihleriBE;
            _derecelerBE = derecelerBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _ulkeTercihBranslarBE = ulkeTercihBranslarBE;
            _branslarBE = branslarBE;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion

        #region Index
        [Route("UT10001", Name = "UlkeTercihleriIndexRoute")]
        public IActionResult Index(int? year)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            
            if (year == null)
            {
                year = DateTime.Now.Year;
            }

            ViewBag.CurrentYear = year;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return ViewComponent("UlkeTercihleri", new { year = year });
            }

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetirKontejanSelectedBox().Data;
            return View();
        }
        #endregion

        #region UlkeTercihEkle(Get)
        [HttpGet]
        public IActionResult UlkeTercihEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetirKontejanSelectedBox().Data;
            ViewBag.Branslar = _branslarBE.BranslariGetir().Data;

            return View();
        }
        #endregion

        #region UlkeTercihEkle(Post)
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UlkeTercihEkle(UlkeTercihVM model, int? year)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _ulkeTercihleriBE.UlkeTercihEkle(model, user);
            if (data.IsSuccess)
            {
                // Seçilen mülakatın yılını al
                var mulakat = _mulakatOlusturBE.MulakatGetir(model.MulakatId);
                if (mulakat.IsSuccess && mulakat.Data != null)
                {
                    year = mulakat.Data.BaslamaTarihi.Year;
                }
                else
                {
                    year = DateTime.Now.Year;
                }

                TempData["success"] = "Ülke tercihi başarıyla eklendi.";
                return RedirectToAction(nameof(Index), new { year = year });
            }

            TempData["error"] = data.Message;
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetirKontejanSelectedBox().Data;
            ViewBag.Branslar = _branslarBE.BranslariGetir().Data;
            ViewBag.CurrentYear = year ?? DateTime.Now.Year;
            return View(model);
        }
        #endregion

        #region Guncelle
        [Route("UT10002", Name = "UlkeTercihleriGuncelleRoute")]
        public IActionResult Guncelle(Guid? id, int? year)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetirKontejanSelectedBox().Data;
            ViewBag.Branslar = _branslarBE.BranslariGetir().Data;
            ViewBag.CurrentYear = year ?? DateTime.Now.Year;

            if (year == null)
            {
                year = DateTime.Now.Year;
            }

            if (id == null || id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index), new { year = year });
            }

            var data = _ulkeTercihleriBE.UlkeTercihGetir((Guid)id);
            if (!data.IsSuccess || data.Data == null)
            {
                return RedirectToAction(nameof(Index), new { year = year });
            }

            ViewBag.CurrentYear = year;
            return View(data.Data);
        }

        [HttpPost]
        public IActionResult Guncelle(UlkeTercihVM model, int? year)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _ulkeTercihleriBE.UlkeTercihGuncelle(model, user);
            if (data.IsSuccess)
            {
                // Seçilen mülakatın yılını al
                var mulakat = _mulakatOlusturBE.MulakatGetir(model.MulakatId);
                if (mulakat.IsSuccess && mulakat.Data != null)
                {
                    year = mulakat.Data.BaslamaTarihi.Year;
                }
                else
                {
                    year = DateTime.Now.Year;
                }

                TempData["success"] = "Ülke tercihi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index), new { year = year });
            }

            TempData["error"] = data.Message;
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetirKontejanSelectedBox().Data;
            ViewBag.Branslar = _branslarBE.BranslariGetir().Data;
            ViewBag.CurrentYear = year ?? DateTime.Now.Year;
            return View(model);
        }
        #endregion

        #region BransGuncelle
        public IActionResult BransGuncelle(Guid? id, int? year)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (year == null)
            {
                year = DateTime.Now.Year;
            }

            if (id == null || id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index), new { year = year });
            }

            var data = _ulkeTercihBranslarBE.UlkeTercihBransGetir((Guid)id);
            if (!data.IsSuccess || data.Data == null)
            {
                return RedirectToAction(nameof(Index), new { year = year });
            }

            // Ülke tercihini al
            var ulkeTercih = _ulkeTercihleriBE.UlkeTercihGetir(data.Data.UlkeTercihId);
            if (ulkeTercih.IsSuccess && ulkeTercih.Data != null)
            {
                data.Data.UlkeTercihAdi = ulkeTercih.Data.UlkeTercihAdi;
            }

            ViewBag.CurrentYear = year;
            return View(data.Data);
        }

        [HttpPost]
        public IActionResult BransGuncelle(UlkeTercihBranslarVM model, int? year)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (year == null)
            {
                year = DateTime.Now.Year;
            }

            if (ModelState.IsValid)
            {
                var data = _ulkeTercihBranslarBE.UlkeTercihBransGuncelle(model, user);
                if (data.IsSuccess)
                {
                    TempData["SuccessMessage"] = "Branş başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index), new { year = year });
                }
                TempData["ErrorMessage"] = data.Message;
            }

            // Hata durumunda ülke adını tekrar al
            var ulkeTercihHata = _ulkeTercihleriBE.UlkeTercihGetir(model.UlkeTercihId);
            if (ulkeTercihHata.IsSuccess && ulkeTercihHata.Data != null)
            {
                model.UlkeTercihAdi = ulkeTercihHata.Data.UlkeTercihAdi;
            }

            ViewBag.CurrentYear = year;
            return View(model);
        }

        [HttpPost]
        public IActionResult BransSil(Guid id, int? year)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (year == null)
            {
                year = DateTime.Now.Year;
            }

            var data = _ulkeTercihBranslarBE.UlkeTercihBransSil(id,user);
            if (data.IsSuccess)
            {
                TempData["SuccessMessage"] = "Branş başarıyla silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = data.Message;
            }

            return RedirectToAction(nameof(Index), new { year = year });
        }
        #endregion

        #region UlkeTercihSil
        [HttpDelete]
        public IActionResult UlkeTercihSil(Guid id, int? year)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
            }

            var data = _ulkeTercihleriBE.UlkeTercihSil(id);
            if (data.IsSuccess)
            {
                TempData["success"] = "Ülke tercihi başarıyla silindi.";
                return Json(new { success = true, redirectUrl = Url.Action("Index", new { year = year }) });
            }

            TempData["error"] = data.Message;
            return Json(new { success = false, message = data.Message });
        }
        #endregion

        #region MulakatAdGetir(Guid dereceId) 
        public IActionResult MulakatAdGetir(Guid dereceId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (dereceId != null)
            {
               // var data = _unitOfWork.mulakatlarRepository.GetAll(x => x.DereceId == dereceId);

               // return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region BransEkle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BransEkle(Guid ulkeTercihId, Guid tercihBransId, string yabancıDil, int bransKontSayi, string bransCinsiyet, bool esitBrans, int? year)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
            }

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            var data = _ulkeTercihleriBE.BransEkle(ulkeTercihId, tercihBransId, yabancıDil, bransKontSayi, bransCinsiyet, esitBrans, user);

            if (data.IsSuccess)
            {
                TempData["success"] = "Branş başarıyla eklendi.";
                return RedirectToAction(nameof(Guncelle), new { id = ulkeTercihId, year = year });
            }

            TempData["error"] = data.Message;
            return RedirectToAction(nameof(Guncelle), new { id = ulkeTercihId, year = year });
        }
        #endregion

        #region ExceldenBransYukle
        [HttpPost]
        public async Task<IActionResult> ExceldenBransYukle(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    TempData["ErrorMessage"] = "Lütfen bir Excel dosyası seçin.";
                    return RedirectToAction(nameof(Index));
                }

                if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    TempData["ErrorMessage"] = "Lütfen geçerli bir Excel dosyası (.xlsx) seçin.";
                    return RedirectToAction(nameof(Index));
                }

                var branslar = new List<BranslarVM>();
                var hatalar = new List<string>();
                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        if (rowCount <= 1)
                        {
                            TempData["Error"] = "Excel dosyası boş veya geçersiz.";
                            return RedirectToAction("Index");
                        }

                        var basariliEklenen = 0;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var brans = new BranslarVM
                            {
                                BransAdi = worksheet.Cells[row, 1].Value?.ToString(),
                            };

                            var result = _branslarBE.BransEkle(brans, user);
                            if (!result.IsSuccess)
                            {
                                // Hata durumunda loglama
                                _logger.LogError($"Branş eklenirken hata: {result.Message}");
                            }

                            basariliEklenen++;
                        }

                        TempData["Success"] = $"{basariliEklenen} branş başarıyla eklendi.";
                        TempData.Keep("Success"); // Veriyi bir sonraki istek için koru
                        return RedirectToAction("Index");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Excel'den branş yüklenirken hata oluştu: {ex.Message}");
                TempData["ErrorMessage"] = "Excel'den branş yüklenirken bir hata oluştu.";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
