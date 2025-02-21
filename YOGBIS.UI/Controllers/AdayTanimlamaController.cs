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
using System.IO;
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

        #region ExceldenAdayEkle
        [HttpPost]
        public async Task<IActionResult> ExceldenAdayEkle(IFormFile file)
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

                var adaylar = new List<AdaylarVM>();
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
                            var aday = new AdaylarVM
                            {
                                TC = worksheet.Cells[row, 1].Value?.ToString(),
                                Ad = worksheet.Cells[row, 2].Value?.ToString(),
                                Soyad = worksheet.Cells[row, 3].Value?.ToString(),
                                BabaAd = worksheet.Cells[row, 4].Value?.ToString(),
                                AnaAd = worksheet.Cells[row, 5].Value?.ToString(),
                                DogumTarihi = worksheet.Cells[row, 6].Value?.ToString(),
                                DogumTarihi2 = worksheet.Cells[row, 6].Value.ToString().Replace("/","."),
                                DogumYeri = worksheet.Cells[row, 7].Value?.ToString(),
                                Cinsiyet = worksheet.Cells[row, 8].Value?.ToString(),
                            };

                            var result = _adaylarBE.AdayEkle(aday, user);
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
