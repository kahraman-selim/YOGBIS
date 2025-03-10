using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;


namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KomisyonlarController : Controller
    {
        #region Değişkenler
        private readonly IKomisyonlarBE _komisyonlarBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<KomisyonlarController> _logger;

        private static readonly object _lockObject = new object();
        #endregion

        #region Dönüştücüler
        public KomisyonlarController(IKomisyonlarBE komisyonlarBE, IKullaniciBE kullaniciBE, IMulakatOlusturBE mulakatOlusturBE,
            ILogger<KomisyonlarController> logger, IUnitOfWork unitOfWork)
        {

            _komisyonlarBE = komisyonlarBE;
            _kullaniciBE = kullaniciBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion

        #region Index
        [Route("KO10001", Name = "KomisyonlarIndexRoute")]
        public async Task<IActionResult> Index(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;
            
            
            if (id != null) 
            {
                var data = _komisyonlarBE.KomisyonGetir((Guid)id);
                return View(data);
            }
            else 
            {
                return View();
            }
            
        }
        #endregion

        #region KomisyonEkle(Get)
        public async Task<IActionResult> KomisyonEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;

            return View();
        }
        #endregion

        #region KomisyonEkle(Post)
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> KomisyonEkle(KomisyonlarVM model, Guid? KomisyonId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;

            if (KomisyonId != null)
            {
                var data = _komisyonlarBE.KomisyonGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _komisyonlarBE.KomisyonEkle(model, user);

                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Guncelle
        [Route("KO10002", Name = "KomisyonlarGuncelleRoute")]
        public async Task<ActionResult> Guncelle(Guid? id)
        {

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;

            if (id != null)
            {
                var data = _komisyonlarBE.KomisyonGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region KomisyonSil
        [HttpDelete]
        public IActionResult KomisyonSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _komisyonlarBE.KomisyonSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

        #region KomisyonBilgileriYukle
        [System.Web.Mvc.HttpPost]
        public async Task<IActionResult> KomisyonBilgileriYukle(IFormFile file)
        {

            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;

            var sessionId = HttpContext.Session.Id;
            try
            {
                if (file == null || file.Length == 0)
                {
                    TempData["Error"] = "Lütfen bir dosya seçin!";
                    return Json(new { success = false });
                }

                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                if (user == null)
                {
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
                            return Json(new { success = false });
                        }

                        var toplamKayit = rowCount - 1;
                        var islemYapilan = 0;

                        await Task.Delay(500);

                        // AŞAMA 1: Kayıt İşlemi
                        islemYapilan = 0;
                        var kayitEdilecekToplam = toplamKayit;

                        var basariliEklenen = 0;

                        for (int row = 2; row <= rowCount; row++)
                        {

                            //var komisyon = _kullaniciBE.KomisyonKullaniciIDGetir().Data.
                            //    FirstOrDefault(x=>x.Id == worksheet.Cells[row, 3].Value?.ToString().Trim());


                            var komisyon = new KomisyonlarVM
                            {

                                //KomisyonKullaniciId = komisyon?.Id,
                                KomisyonSiraNo = Convert.ToInt32(worksheet.Cells[row, 1].Value?.ToString().Trim()),
                                KomisyonAdi = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                                KomisyonUyeDurum = Convert.ToBoolean(worksheet.Cells[row, 4].Value?.ToString().Trim()),
                                KomisyonUyeSiraNo = Convert.ToInt32(worksheet.Cells[row, 5].Value?.ToString().Trim()),
                                KomisyonUyeGorevi = worksheet.Cells[row, 6].Value?.ToString().Trim(),
                                KomisyonUyeAdiSoyadi = worksheet.Cells[row, 7].Value?.ToString().Trim() + " " + worksheet.Cells[row, 8].Value?.ToString().Trim(),
                                KomisyonUyeGorevYeri = worksheet.Cells[row, 9].Value?.ToString().Trim(),
                                KomisyoUyeStatu = worksheet.Cells[row, 10].Value?.ToString().Trim(),
                                KomisyonUlkeGrubu = worksheet.Cells[row, 11].Value?.ToString().Trim(),
                                KomisyoUyeTel = "0",
                                KomisyonUyeEPosta = "y@y.com.tr",
                                KomisyonGorevBaslamaTarihi = DateTime.Parse(worksheet.Cells[row, 12].Value?.ToString().Trim()),
                                KomisyonGorevBitisTarihi = DateTime.Parse(worksheet.Cells[row, 13].Value?.ToString().Trim()),
                                
                                MulakatId = ((List<MulakatlarVM>)ViewBag.Mulakatlar).FirstOrDefault()?.MulakatId
                            };

                            var result = _komisyonlarBE.KomisyonEkle(komisyon, user);
                            if (result.IsSuccess)
                            {
                                basariliEklenen++;
                            }

                            islemYapilan++;

                            await Task.Delay(10);
                        }

                        await Task.Delay(500);

                        return Json(new { success = true });
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel'den aday yüklenirken hata oluştu: {Message}", ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }
        #endregion
    }
}
