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

        #region BransEkle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BransEkle(Guid UlkeTercihId, Guid TercihBransId, string BransCinsiyet, int BransKontSayi, bool EsitBrans)
        {
            try
            {
                if (UlkeTercihId == Guid.Empty || TercihBransId == Guid.Empty)
                {
                    TempData["error"] = "Ülke tercihi ve branş seçimi zorunludur.";
                    return RedirectToAction(nameof(Guncelle), new { id = UlkeTercihId });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                
                // Seçilen branşı getir
                var secilenBrans = _branslarBE.BransGetir(TercihBransId).Data;
                if (secilenBrans == null)
                {
                    TempData["error"] = "Seçilen branş bulunamadı.";
                    return RedirectToAction(nameof(Guncelle), new { id = UlkeTercihId });
                }

                // Yeni branş nesnesi oluştur
                var yeniBrans = new UlkeTercihBranslarVM
                {
                    BransAdi = secilenBrans.BransAdi,
                    BransCinsiyet = BransCinsiyet,
                    BransKontSayi = BransKontSayi,
                    EsitBrans = EsitBrans,
                    UlkeTercihId = UlkeTercihId
                };

                // Branşı ekle
                var result = _ulkeTercihBranslarBE.UlkeTercihBransEkle(yeniBrans, user);
                if (result.IsSuccess)
                {
                    TempData["success"] = result.Message;
                }
                else
                {
                    TempData["error"] = result.Message;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"BransEkle - Hata: {ex.Message}");
                TempData["error"] = "Branş eklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Guncelle), new { id = UlkeTercihId });
            }
        }
        #endregion

        #region BransSil
        [HttpGet]
        public IActionResult BransSil(Guid id)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                
                // Silinecek branşı getir
                var brans = _ulkeTercihBranslarBE.UlkeTercihBransGetir(id).Data;
                if (brans == null)
                {
                    TempData["ErrorMessage"] = "Silinecek branş bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                // Branşı sil
                var result = _ulkeTercihBranslarBE.UlkeTercihBransSil(id);
                if (result.IsSuccess)
                {
                    TempData["Success"] = "Branş başarıyla silindi.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Branş silinirken bir hata oluştu: " + result.Message;
                }

                return RedirectToAction(nameof(Guncelle), new { id = brans.UlkeTercihId });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Branş silinirken hata oluştu: {ex.Message}");
                TempData["ErrorMessage"] = "Branş silinirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
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
