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

            var ulkeTercihleri = _ulkeTercihleriBE.UlkeTercihleriGetir();
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;
            
            return View(ulkeTercihleri.Data);
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
        public IActionResult BransEkle(Guid UlkeTercihId, Guid BransId, string BransCinsiyet, int BransKontSayi, bool EsitBrans)
        {
            try
            {
                if (UlkeTercihId == Guid.Empty || BransId == Guid.Empty)
                {
                    TempData["error"] = "Ülke tercihi ve branş seçimi zorunludur.";
                    return RedirectToAction(nameof(Guncelle), new { id = UlkeTercihId });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                
                // Seçilen branşı getir
                var secilenBrans = _branslarBE.BransGetir(BransId).Data;
                if (secilenBrans == null)
                {
                    TempData["error"] = "Seçilen branş bulunamadı.";
                    return RedirectToAction(nameof(Guncelle), new { id = UlkeTercihId });
                }

                // Yeni branş nesnesi oluştur
                var yeniBrans = new BranslarVM
                {
                    BransId = Guid.NewGuid(),
                    BransAdi = secilenBrans.BransAdi,
                    BransCinsiyet = BransCinsiyet,
                    BransKontSayi = BransKontSayi,
                    EsitBrans = EsitBrans,
                    UlkeTercihId = UlkeTercihId,
                    KayitTarihi = DateTime.Now,
                    KaydedenId = user.LoginId,
                    KaydedenAdi = user.FirstName + " " + user.LastName
                };

                // Branşı ekle
                var result = _branslarBE.BransEkle(yeniBrans, user);
                if (result.IsSuccess)
                {
                    TempData["success"] = result.Message;
                }
                else
                {
                    TempData["error"] = result.Message;
                }

                return RedirectToAction(nameof(Guncelle), new { id = UlkeTercihId });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Branş eklenirken hata oluştu: {ex.Message}");
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
                var brans = _branslarBE.BransGetir(id).Data;
                if (brans == null)
                {
                    TempData["ErrorMessage"] = "Silinecek branş bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                // Branşı sil
                var result = _branslarBE.BransSil(id);
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

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++) // İlk satır başlık olduğu için 2'den başlıyoruz
                        {
                            var brans = new BranslarVM
                            {
                                //BransId = Guid.NewGuid(),
                                BransAdi = worksheet.Cells[row, 1].Value?.ToString(),
                                //BransCinsiyet = worksheet.Cells[row, 2].Value?.ToString(),
                                //BransKontSayi = Convert.ToInt32(worksheet.Cells[row, 3].Value),
                                //EsitBrans = Convert.ToBoolean(worksheet.Cells[row, 4].Value),
                                UlkeTercihId = Guid.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
                                //KayitTarihi = DateTime.Now,
                                //KaydedenId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value)
                            };

                            var result = _branslarBE.BransEkle(brans, user);
                            if (!result.IsSuccess)
                            {
                                TempData["ErrorMessage"] = $"Satır {row}: {result.Message}";
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }

                TempData["Success"] = "Branşlar başarıyla yüklendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Excel'den branş yüklenirken hata oluştu: {ex.Message}");
                TempData["ErrorMessage"] = "Excel'den branş yüklenirken bir hata oluştu.";
            }

            return RedirectToAction(nameof(Index));
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
