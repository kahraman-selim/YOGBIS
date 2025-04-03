using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator,Manager")]
    public class MulakatSorulariController : Controller
    {

        #region Değişkenler
        private readonly IMulakatSorulariBE _mulakatSorulariBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MulakatSorulariController> _logger;
        #endregion

        #region Dönüştürücüler
        public MulakatSorulariController(IMulakatSorulariBE mulakatSorulariBE, IDerecelerBE derecelerBE, ISoruKategorileriBE soruKategorileriBE, 
            IMulakatOlusturBE mulakatOlusturBE, IUnitOfWork unitOfWork, ILogger<MulakatSorulariController> logger)
        {
            _mulakatSorulariBE = mulakatSorulariBE;
            _derecelerBE = derecelerBE;
            _soruKategorileriBE = soruKategorileriBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion

        #region Index
        [Route("MS10001", Name = "MulakatSorularıIndexRoute")]
        public IActionResult Index(Guid? id, Guid? DereceId, Guid? SoruKategorilerId, Guid? MulakatId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;
            ViewBag.SoruKategorileri = string.Empty;

            //ViewBag.SoruKategorileri = _soruKategorileriBE.SoruKategorileriGetir().Data;
            //ViewBag.Mulakatlar=_mulakatOlusturBE.MulakatlariGetir().Data;

            if (id != null)
            {
                var data = _mulakatSorulariBE.MulakatSoruGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
            
        }
        #endregion

        #region MulakatSoruEkle(Get)
        public IActionResult MulakatSoruEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            //ViewBag.SoruKategorileri = string.Empty;
            ViewBag.SoruKategorileri = _soruKategorileriBE.SoruKategorileriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;
            return View();
        }
        #endregion

        #region MulakatSoruEkle(Post)
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult MulakatSoruEkle(MulakatSorulariVM model, Guid? MulakatSorulariId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            //ViewBag.SoruKategorileri = string.Empty;
            ViewBag.SoruKategorileri = _soruKategorileriBE.SoruKategorileriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;

            if (MulakatSorulariId != null)
            {
                var data = _mulakatSorulariBE.SoruNoIleTopluGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _mulakatSorulariBE.MulakatSoruEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        #endregion

        #region Guncelle
        [Route("MS10002", Name = "MulakatSorularıGuncelleRoute")]
        public ActionResult Guncelle(Guid? id, Guid? DereceId, Guid? SoruKategorilerId, Guid? MulakatId)
        {

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            //ViewBag.SoruKategorileri = string.Empty;
            ViewBag.SoruKategorileri = _soruKategorileriBE.SoruKategorileriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;

            if (id != null)
            {
                var data = _mulakatSorulariBE.MulakatSoruGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region SoruSiraNoIleTopluGuncelle
        [HttpPost]
        public JsonResult SoruSiraNoIleTopluGuncelle(MulakatSorulariVM model)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _mulakatSorulariBE.SoruSiraNoIleTopluGuncelle(model, user);

            return Json("200");

        }
        #endregion

        #region MulakatSoruSil
        [HttpDelete]
        public IActionResult MulakatSoruSil(Guid id)
        {
            if (id == null)
                return Json(new {success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _mulakatSorulariBE.MulakatSorusuSil(id);
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
               // var data = _unitOfWork.mulakatlarRepository.GetAll(x => x.DereceId == dereceId);  //  //_mulakatOlusturBE.MulakatDonemAdiGetir(dereceId).Data; 
                //string donem = JsonConvert.SerializeObject(data);

                //return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region KategoriAdGetir(Guid dereceId) 
        public IActionResult KategoriAdGetir(Guid dereceId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (dereceId != null)
            {
                var data = _unitOfWork.sorukategorilerRepository.GetAll(x => x.DereceId == dereceId).OrderBy(x=>x.SoruKategorilerSiraNo);

                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region KategoriDetayGetir(Guid soruKategorilerId) 
        public IActionResult KategoriDetayGetir(Guid soruKategorilerId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (soruKategorilerId != null)
            {
                var data = _unitOfWork.sorukategorilerRepository.GetAll(x => x.SoruKategorilerId == soruKategorilerId).OrderBy(x => x.SoruKategorilerSiraNo);

                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region SoruYükleme
        [HttpPost]
        public async Task<IActionResult> ExceldenMulakatSoruEkle(IFormFile file)
        {
            try
            {
                //çalışan metod

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

                var sorular = new List<MulakatSorulariVM>();
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
                        for (int row = 2; row <= rowCount; row++) 
                        {
                            var soru = new MulakatSorulariVM
                            {
                                SoruSiraNo = int.Parse(worksheet.Cells[row, 1].Value?.ToString() ?? "0"),
                                SoruNo = int.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
                                DereceId = Guid.Parse(worksheet.Cells[row, 3].Value?.ToString() ?? "0"),
                                SoruKategorilerId = Guid.Parse(worksheet.Cells[row, 4].Value?.ToString() ?? "0"),
                                SoruKategoriSiraNo = int.Parse(worksheet.Cells[row, 5].Value?.ToString() ?? "0"),
                                SoruKategoriAdi= worksheet.Cells[row, 6].Value?.ToString(),
                                Soru = worksheet.Cells[row, 7].Value?.ToString(),
                                Cevap = worksheet.Cells[row, 8].Value?.ToString(),
                                SinavKateogoriTurId = int.Parse(worksheet.Cells[row, 9].Value?.ToString() ?? "0"),
                                SinavKategoriTurAdi = worksheet.Cells[row, 10].Value?.ToString(),
                                Iptal=true,
                                MulakatId = Guid.Parse(worksheet.Cells[row, 11].Value?.ToString() ?? "0"),
                            };

                            // Soruyu işle
                            
                            var result = _mulakatSorulariBE.MulakatSoruEkle(soru, user);
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

                    /*using (var package = new ExcelPackage(stream))
                    {

                        // Parça boyutu (100-200 arasında)
                        int chunkSize = 200; // Bu değeri ihtiyacınıza göre ayarlayabilirsiniz
                        int totalChunks = (int)Math.Ceiling((double)rowCount / chunkSize);

                        for (int chunkIndex = 0; chunkIndex < totalChunks; chunkIndex++)
                        {
                            int startRow = 2 + (chunkIndex * chunkSize); // İlk satır başlık olduğu için 2'den başlıyoruz
                            int endRow = Math.Min(startRow + chunkSize - 1, rowCount); // endRow'u doğru hesapla
                            var chunkSorular = new List<MulakatSorulariVM>();
                            for (int row = startRow; row <= endRow; row++)
                            {
                                try
                                {
                                    // Guid değerlerini kontrol et
                                    if (!Guid.TryParse(worksheet.Cells[row, 3].Value?.ToString(), out Guid dereceId))
                                    {
                                        hatalar.Add($"Satır {row}: Geçersiz DereceId formatı.");
                                        continue;
                                    }

                                    if (!Guid.TryParse(worksheet.Cells[row, 4].Value?.ToString(), out Guid soruKategorilerId))
                                    {
                                        hatalar.Add($"Satır {row}: Geçersiz SoruKategorilerId formatı.");
                                        continue;
                                    }

                                    if (!Guid.TryParse(worksheet.Cells[row, 10].Value?.ToString(), out Guid mulakatId))
                                    {
                                        hatalar.Add($"Satır {row}: Geçersiz MulakatId formatı.");
                                        continue;
                                    }

                                    var soru = new MulakatSorulariVM
                                    {
                                        SoruSiraNo = int.Parse(worksheet.Cells[row, 1].Value?.ToString() ?? "0"),
                                        SoruNo = int.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
                                        DereceId = dereceId,
                                        SoruKategorilerId = soruKategorilerId,
                                        SoruKategoriSiraNo = int.Parse(worksheet.Cells[row, 5].Value?.ToString() ?? "0"),
                                        Soru = worksheet.Cells[row, 6].Value?.ToString(),
                                        Cevap = worksheet.Cells[row, 7].Value?.ToString(),
                                        SinavKateogoriTurId = int.Parse(worksheet.Cells[row, 8].Value?.ToString() ?? "0"),
                                        SinavKategoriTurAdi = worksheet.Cells[row, 9].Value?.ToString(),
                                        MulakatId = mulakatId,
                                    };

                                    // Temel validasyonlar
                                    if (string.IsNullOrEmpty(soru.Soru))
                                    {
                                        hatalar.Add($"Satır {row}: Soru boş olamaz.");
                                        continue;
                                    }

                                    sorular.Add(soru);
                                }
                                catch (Exception ex)
                                {
                                    hatalar.Add($"Satır {row}: {ex.Message}");
                                }
                            }
                        }
                    }*/

                }

                // Hata varsa göster
                //if (hatalar.Any())
                //{
                //    TempData["Error"] = string.Join("<br/>", hatalar);
                //    return RedirectToAction("Index");
                //}

                /*Soruları veritabanına ekle
                var basariliEklenen = 0;
                foreach (var soru in sorular)
                {
                    try
                    {
                        var result = _mulakatSorulariBE.MulakatSoruEkle(soru, user);
                        if (result.IsSuccess)
                        {
                            basariliEklenen++;
                        }
                        else
                        {
                            hatalar.Add($"Soru eklenirken hata: {result.Message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        hatalar.Add($"Soru eklenirken hata: {ex.Message}");
                    }
                }

                if (hatalar.Any())
                {
                    TempData["Warning"] = $"{basariliEklenen} soru başarıyla eklendi, ancak bazı hatalar oluştu:<br/>{string.Join("<br/>", hatalar)}";
                }
                else
                {
                    TempData["Success"] = $"{basariliEklenen} soru başarıyla eklendi.";
                }*/

                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel'den soru yüklenirken hata oluştu: {Message}", ex.Message);
                TempData["Error"] = $"Dosya işlenirken bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region SoruYüklemeUzunYöntem
        [HttpPost]
        public async Task<IActionResult> ExceldenMulakatSoruEkle2(IFormFile file)
        {
            try
            {
                // Dosya validasyonu
                if (!ValidateFile(file, out string errorMessage))
                {
                    TempData["Error"] = errorMessage;
                    return RedirectToAction("Index");
                }

                // Kullanıcı bilgilerini al
                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

                // Excel'den soruları oku
                var (sorular, hatalar) = await ReadQuestionsFromExcelAsync(file);

                // Soruları veritabanına ekle
                var basariliEklenen = await AddQuestionsToDatabaseAsync(sorular, user, hatalar);

                // Kullanıcıya geri bildirim
                SetFeedbackMessages(basariliEklenen, hatalar);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel'den soru yüklenirken hata oluştu: {Message}", ex.Message);
                TempData["Error"] = $"Dosya işlenirken bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        // Dosya validasyonu
        private bool ValidateFile(IFormFile file, out string errorMessage)
        {
            errorMessage = null;

            if (file == null || file.Length <= 0)
            {
                errorMessage = "Lütfen bir Excel dosyası seçin.";
                return false;
            }

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                errorMessage = "Lütfen .xlsx formatında bir dosya yükleyin.";
                return false;
            }

            return true;
        }

        // Excel'den soruları okuma
        private async Task<(List<MulakatSorulariVM> sorular, List<string> hatalar)> ReadQuestionsFromExcelAsync(IFormFile file)
        {
            var sorular = new List<MulakatSorulariVM>();
            var hatalar = new List<string>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension?.Rows ?? 0;

                    if (rowCount == 1)
                    {
                        hatalar.Add("Excel dosyası boş veya geçersiz.");
                        return (sorular, hatalar);
                    }

                    for (int row = 2; row <= rowCount; row++) // İlk satır başlık olduğu için 2'den başlıyoruz
                    {
                        try
                        {
                            var soru = new MulakatSorulariVM
                            {
                                SoruSiraNo = int.Parse(worksheet.Cells[row, 1].Value?.ToString() ?? "0"),
                                SoruNo = int.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
                                DereceId = Guid.Parse(worksheet.Cells[row, 3].Value?.ToString() ?? "0"),
                                SoruKategorilerId = Guid.Parse(worksheet.Cells[row, 4].Value?.ToString() ?? "0"),
                                SoruKategoriSiraNo = int.Parse(worksheet.Cells[row, 5].Value?.ToString() ?? "0"),
                                Soru = worksheet.Cells[row, 6].Value?.ToString(),
                                Cevap = worksheet.Cells[row, 7].Value?.ToString(),
                                SinavKateogoriTurId = int.Parse(worksheet.Cells[row, 8].Value?.ToString() ?? "0"),
                                SinavKategoriTurAdi = worksheet.Cells[row, 9].Value?.ToString(),
                                MulakatId = Guid.Parse(worksheet.Cells[row, 10].Value?.ToString() ?? "0"),
                            };

                            sorular.Add(soru);
                        }
                        catch (Exception ex)
                        {
                            hatalar.Add($"Satır {row}: {ex.Message}");
                        }
                    }
                }
            }

            return (sorular, hatalar);
        }

        // Soruları veritabanına ekleme
        private async Task<int> AddQuestionsToDatabaseAsync(List<MulakatSorulariVM> sorular, SessionContext user, List<string> hatalar)
        {
            var basariliEklenen = 0;

            foreach (var soru in sorular)
            {
                try
                {
                    var result = _mulakatSorulariBE.MulakatSoruEkle(soru, user);
                    if (result.IsSuccess)
                    {
                        basariliEklenen++;
                    }
                    else
                    {
                        hatalar.Add($"Soru eklenirken hata: {result.Message}");
                    }
                }
                catch (Exception ex)
                {
                    hatalar.Add($"Soru eklenirken hata: {ex.Message}");
                }
            }

            return basariliEklenen;
        }

        // Kullanıcıya geri bildirim
        private void SetFeedbackMessages(int basariliEklenen, List<string> hatalar)
        {
            if (hatalar.Any())
            {
                TempData["Warning"] = $"{basariliEklenen} soru başarıyla eklendi, ancak bazı hatalar oluştu:<br/>{string.Join("<br/>", hatalar)}";
            }
            else
            {
                TempData["Success"] = $"{basariliEklenen} soru başarıyla eklendi.";
            }
        }
        #endregion

        #region SoruYüklemeClaude
        [HttpPost]
        public async Task<IActionResult> ExceldenMulakatSoruEkleee(IFormFile file, MulakatSorulariVM model)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                var dereceler = _derecelerBE.DereceleriGetir().Data;
                var soruKategorileri = _soruKategorileriBE.SoruKategorileriGetir().Data;
                var mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;

                ViewBag.Dereceler = dereceler;
                ViewBag.SoruKategorileri = soruKategorileri;
                ViewBag.Mulakatlar = mulakatlar;

                // Seçilen değerleri kontrol et
                if (model.DereceId == Guid.Empty || model.SoruKategorilerId == Guid.Empty || model.MulakatId == Guid.Empty)
                {
                    TempData["Error"] = "Lütfen Derece, Soru Kategorisi ve Mülakat seçimlerini yapınız.";
                    return RedirectToAction("Index");
                }

                if (file == null || file.Length <= 0)
                {
                    TempData["Error"] = "Lütfen bir Excel dosyası seçin.";
                    return RedirectToAction("Index");
                }

                // ... Excel işlemleri ...
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension?.Rows ?? 0;

                        if (rowCount == 1)
                        {
                            TempData["Error"] = "Excel dosyası boş veya geçersiz.";
                            return RedirectToAction("Index");
                        }

                        var basariliEklenen = 0;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            var soru = new MulakatSorulariVM
                            {
                                SoruSiraNo = int.Parse(worksheet.Cells[row, 1].Value?.ToString() ?? "0"),
                                SoruNo = int.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
                                DereceId = model.DereceId,  // Form'dan gelen değer
                                SoruKategorilerId = model.SoruKategorilerId,  // Form'dan gelen değer
                                SoruKategoriSiraNo = int.Parse(worksheet.Cells[row, 3].Value?.ToString() ?? "0"),
                                Soru = worksheet.Cells[row, 4].Value?.ToString(),
                                Cevap = worksheet.Cells[row, 5].Value?.ToString(),
                                SinavKateogoriTurId = int.Parse(worksheet.Cells[row, 6].Value?.ToString() ?? "0"),
                                SinavKategoriTurAdi = worksheet.Cells[row, 7].Value?.ToString(),
                                MulakatId = model.MulakatId  // Form'dan gelen değer
                            };

                            var result = _mulakatSorulariBE.MulakatSoruEkle(soru, user);
                            if (!result.IsSuccess)
                            {
                                _logger.LogError($"Soru eklenirken hata: {result.Message}");
                            }

                            basariliEklenen++;
                        }

                        TempData["Success"] = $"{basariliEklenen} soru başarıyla eklendi.";
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
