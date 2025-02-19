using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.Implementaion;

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
        private readonly ILogger<UlkeTercihleriController> _logger;
        #endregion

        #region Dönüştürücüler
        public UlkeTercihleriController(IUlkeTercihleriBE ulkeTercihleriBE, IDerecelerBE derecelerBE, 
            IMulakatOlusturBE mulakatOlusturBE, IUnitOfWork unitOfWork, IBranslarBE branslarBE, ILogger<UlkeTercihleriController> logger)
        {
            _ulkeTercihleriBE = ulkeTercihleriBE;
            _derecelerBE = derecelerBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _unitOfWork = unitOfWork;
            _branslarBE = branslarBE;
            _logger = logger;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;
            
            return View();
        }
        #endregion

        #region UlkeTercihEkle(Get)
        [HttpGet]
        public IActionResult UlkeTercihEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;
            ViewBag.Branslar = _branslarBE.BranslariGetir().Data;

            return View();
        }
        #endregion

        #region UlkeTercihEkle(Post)
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UlkeTercihEkle(UlkeTercihVM model, Guid? UlkeTercihId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;
            ViewBag.Branslar = _branslarBE.BranslariGetir().Data;

            if (UlkeTercihId != null)
            {
                var data = _ulkeTercihleriBE.UlkeTercihGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _ulkeTercihleriBE.UlkeTercihEkle(model, user);

                return RedirectToAction("Index");

            }
        }
        #endregion

        #region Guncelle
        public IActionResult Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;
            ViewBag.Branslar = _branslarBE.BranslariGetir().Data;

            if (id != Guid.Empty)
            {
                var data = _ulkeTercihleriBE.UlkeTercihGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region UlkeTercihSil
        public IActionResult UlkeTercihSil(Guid id)
        {
            if (id == Guid.Empty)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _ulkeTercihleriBE.UlkeTercihSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });
        }
        #endregion

        #region MulakatAdGetir(Guid dereceId) 
        public IActionResult MulakatAdGetir(Guid dereceId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (dereceId != null)
            {
                var data = _unitOfWork.mulakatlarRepository.GetAll(x => x.DereceId == dereceId);

                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region BransYükleme
        [HttpPost]
        public async Task<IActionResult> ExceldenBransEkle(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    TempData["Error"] = "Lütfen bir Excel dosyası seçin.";
                    return RedirectToAction("Index");
                }

                if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    TempData["Error"] = "Lütfen .xlsx formatında bir dosya yükleyin.";
                    return RedirectToAction("Index");
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
                        var rowCount = worksheet.Dimension?.Rows ?? 0;

                        if (rowCount <= 1)
                        {
                            TempData["Error"] = "Excel dosyası boş veya geçersiz.";
                            return RedirectToAction("Index");
                        }

                        // Sadece gerekli hücreleri oku
                        var basariliEklenen = 0;
                        for (int row = 2; row <= rowCount; row++) // İlk satır başlık olduğu için 2'den başlıyoruz
                        {
                            var brans = new BranslarVM
                            {
                                BransAdi = worksheet.Cells[row, 1].Value?.ToString(),
                                UlkeTercihId = Guid.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
                            };

                            // Soruyu işle

                            var result = _branslarBE.BransEkle(brans, user);
                            if (!result.IsSuccess)
                            {
                                // Hata durumunda loglama
                                _logger.LogError($"Soru eklenirken hata: {result.Message}");
                            }

                            basariliEklenen++;
                        }


                        TempData["Success"] = $"{basariliEklenen} soru başarıyla eklendi.";
                        TempData.Keep("Success"); // Veriyi bir sonraki istek için koru
                        return RedirectToAction("Index");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel'den soru yüklenirken hata oluştu: {Message}", ex.Message);
                TempData["Error"] = $"Dosya işlenirken bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}
