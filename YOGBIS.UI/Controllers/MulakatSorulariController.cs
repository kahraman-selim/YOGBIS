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
    [Authorize(Roles = "Administrator")]
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
        public IActionResult Index(Guid? id, Guid? DereceId, Guid? SoruKategorilerId, Guid? MulakatId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = string.Empty;
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
                var data = _mulakatSorulariBE.MulakatSoruGuncelle(model, user);

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
                var data = _unitOfWork.mulakatlarRepository.GetAll(x => x.DereceId == dereceId);  // _mulakatOlusturBE.MulakatDonemAdiGetir(dereceId).Data; //
                //string donem = JsonConvert.SerializeObject(data);

                return Json(data);
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

        /*#region SoruYükleme
        [HttpPost]
        public async Task<IActionResult> ExceldenMulakatSoruEkle(IFormFile file)
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


                        if (rowCount == 0)
                        {
                            TempData["Error"] = "Excel dosyası boş veya geçersiz.";
                            return RedirectToAction("Index");
                        }

                        // Parça boyutu (100-200 arasında)
                        int chunkSize = 150; // Bu değeri ihtiyacınıza göre ayarlayabilirsiniz
                        int totalChunks = (int)Math.Ceiling((double)rowCount / chunkSize);

                        for (int chunkIndex = 0; chunkIndex < totalChunks; chunkIndex++)
                        {
                            var chunkSorular = new List<MulakatSorulariVM>();
                            int startRow = 2 + (chunkIndex * chunkSize); // İlk satır başlık olduğu için 2'den başlıyoruz
                            int endRow = Math.Min(startRow + chunkSize, rowCount + 1);

                            // İlk satır başlık olduğu için 2'den başlıyoruz
                            for (int row = startRow; row <= endRow; row++)
                            {
                                try
                                {
                                    var soru = new MulakatSorulariVM
                                    {
                                        SoruSiraNo = int.Parse(worksheet.Cells[row, 1].Value?.ToString() ?? "0"),
                                        SoruNo = int.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
                                        DereceId = Guid.Parse(worksheet.Cells[row, 3].Value?.ToString() ?? ""),
                                        SoruKategorilerId = Guid.Parse(worksheet.Cells[row, 4].Value?.ToString() ?? ""),
                                        SoruKategoriSiraNo = int.Parse(worksheet.Cells[row, 5].Value?.ToString() ?? "0"),
                                        Soru = worksheet.Cells[row, 6].Value?.ToString(),
                                        Cevap = worksheet.Cells[row, 7].Value?.ToString(),
                                        SinavKateogoriTurId = int.Parse(worksheet.Cells[row, 8].Value?.ToString() ?? "0"),
                                        SinavKategoriTurAdi = worksheet.Cells[row, 9].Value?.ToString(),
                                        MulakatId = Guid.Parse(worksheet.Cells[row, 10].Value?.ToString() ?? ""),
                                    };

                                    // Temel validasyonlar
                                    if (string.IsNullOrEmpty(soru.Soru))
                                    {
                                        hatalar.Add($"Satır {row}: Soru boş olamaz.");
                                        continue;
                                    }

                                    chunkSorular.Add(soru);

                                }
                                catch (Exception ex)
                                {
                                    hatalar.Add($"Satır {row}: {ex.Message}");
                                }
                            }


                        }

                    }
                }

                // Hata varsa göster
                if (hatalar.Any())
                {
                    TempData["Error"] = string.Join("<br/>", hatalar);
                    return RedirectToAction("Index");
                }

                TempData["Success"] = $"{sorular.Count} soru başarıyla okundu.";
               // return RedirectToAction("Index");

                //Soruları veritabanına ekle
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
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel'den soru yüklenirken hata oluştu: {Message}", ex.Message);
                TempData["Error"] = $"Dosya işlenirken bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index");
            }
        } 
        #endregion*/

        /*[HttpPost]
        public async Task<IActionResult> ExceldenMulakatSoruEkle(IFormFile file)
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

                        if (rowCount == 0)
                        {
                            TempData["Error"] = "Excel dosyası boş veya geçersiz.";
                            return RedirectToAction("Index");
                        }

                        // Parça boyutu (100-200 arasında)
                        int chunkSize = 100; // Bu değeri ihtiyacınıza göre ayarlayabilirsiniz
                        int totalChunks = (int)Math.Ceiling((double)rowCount / chunkSize);

                        for (int chunkIndex = 0; chunkIndex < totalChunks; chunkIndex++)
                        {
                            var chunkSorular = new List<MulakatSorulariVM>();
                            int startRow = 2 + (chunkIndex * chunkSize); // İlk satır başlık olduğu için 2'den başlıyoruz
                            int endRow = Math.Min(startRow + chunkSize, rowCount + 1);

                            for (int row = startRow; row <= endRow; row++)
                            {
                                try
                                {
                                    var soru = new MulakatSorulariVM
                                    {
                                        SoruSiraNo = int.Parse(worksheet.Cells[row, 1].Value?.ToString() ?? "0"),
                                        SoruNo = int.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
                                        DereceId = Guid.Parse(worksheet.Cells[row, 3].Value?.ToString() ?? ""),
                                        SoruKategorilerId = Guid.Parse(worksheet.Cells[row, 4].Value?.ToString() ?? ""),
                                        SoruKategoriSiraNo = int.Parse(worksheet.Cells[row, 5].Value?.ToString() ?? "0"),
                                        Soru = worksheet.Cells[row, 6].Value?.ToString(),
                                        Cevap = worksheet.Cells[row, 7].Value?.ToString(),
                                        SinavKateogoriTurId = int.Parse(worksheet.Cells[row, 8].Value?.ToString() ?? "0"),
                                        SinavKategoriTurAdi = worksheet.Cells[row, 9].Value?.ToString(),
                                        MulakatId = Guid.Parse(worksheet.Cells[row, 10].Value?.ToString() ?? ""),
                                    };

                                    // Temel validasyonlar
                                    if (string.IsNullOrEmpty(soru.Soru))
                                    {
                                        hatalar.Add($"Satır {row}: Soru boş olamaz.");
                                        continue;
                                    }

                                    chunkSorular.Add(soru);
                                }
                                catch (Exception ex)
                                {
                                    hatalar.Add($"Satır {row}: {ex.Message}");
                                }
                            }

                            // Parçayı veritabanına ekle
                            var basariliEklenen = 0;
                            foreach (var soru in chunkSorular)
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
                            }
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel'den soru yüklenirken hata oluştu: {Message}", ex.Message);
                TempData["Error"] = $"Dosya işlenirken bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index");
            }
        }*/

        /*[HttpPost]
        public async Task<IActionResult> ExceldenMulakatSoruEkle(IFormFile file)
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

                var hatalar = new List<string>();
                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension?.Rows ?? 0;

                        if (rowCount == 0)
                        {
                            TempData["Error"] = "Excel dosyası boş veya geçersiz.";
                            return RedirectToAction("Index");
                        }

                        // Parça boyutu (örneğin 100 kayıt)
                        int chunkSize = 100;
                        int totalChunks = (int)Math.Ceiling((double)(rowCount - 1) / chunkSize); // İlk satır başlık olduğu için -1

                        for (int chunkIndex = 0; chunkIndex < totalChunks; chunkIndex++)
                        {
                            var chunkSorular = new List<MulakatSorulariVM>();
                            int startRow = 2 + (chunkIndex * chunkSize); // İlk satır başlık olduğu için 2'den başlıyoruz
                            int endRow = Math.Min(startRow + chunkSize, rowCount + 1);

                            for (int row = startRow; row <= endRow; row++)
                            {
                                try
                                {
                                    var soru = new MulakatSorulariVM
                                    {
                                        SoruSiraNo = int.Parse(worksheet.Cells[row, 1].Value?.ToString() ?? "0"),
                                        SoruNo = int.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
                                        DereceId = Guid.Parse(worksheet.Cells[row, 3].Value?.ToString() ?? ""),
                                        SoruKategorilerId = Guid.Parse(worksheet.Cells[row, 4].Value?.ToString() ?? ""),
                                        SoruKategoriSiraNo = int.Parse(worksheet.Cells[row, 5].Value?.ToString() ?? "0"),
                                        Soru = worksheet.Cells[row, 6].Value?.ToString(),
                                        Cevap = worksheet.Cells[row, 7].Value?.ToString(),
                                        SinavKateogoriTurId = int.Parse(worksheet.Cells[row, 8].Value?.ToString() ?? "0"),
                                        SinavKategoriTurAdi = worksheet.Cells[row, 9].Value?.ToString(),
                                        MulakatId = Guid.Parse(worksheet.Cells[row, 10].Value?.ToString() ?? ""),
                                    };

                                    // Temel validasyonlar
                                    if (string.IsNullOrEmpty(soru.Soru))
                                    {
                                        hatalar.Add($"Satır {row}: Soru boş olamaz.");
                                        continue;
                                    }

                                    chunkSorular.Add(soru);
                                }
                                catch (Exception ex)
                                {
                                    hatalar.Add($"Satır {row}: {ex.Message}");
                                }
                            }

                            // Toplu ekleme işlemi
                            if (chunkSorular.Any())
                            {
                                try
                                {
                                    var result = _mulakatSorulariBE.TopluMulakatSoruEkle(chunkSorular, user);
                                    if (result.IsSuccess)
                                    {
                                        TempData["Success"] = $"{chunkSorular.Count} soru başarıyla eklendi.";
                                    }
                                    else
                                    {
                                        hatalar.Add($"Parça {chunkIndex + 1} eklenirken hata: {result.Message}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    hatalar.Add($"Parça {chunkIndex + 1} eklenirken hata: {ex.Message}");
                                }
                            }
                        }
                    }
                }

                // Hata varsa göster
                if (hatalar.Any())
                {
                    TempData["Warning"] = $"Bazı hatalar oluştu:<br/>{string.Join("<br/>", hatalar)}";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel'den soru yüklenirken hata oluştu: {Message}", ex.Message);
                TempData["Error"] = $"Dosya işlenirken bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index");
            }
        }*/
    }
}
