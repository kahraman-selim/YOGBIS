using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator,Manager")]
    public class AdayBasvuruController : Controller
    {
        #region Değişkenler
        private readonly IAdaylarBE _adaylarBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly IUlkeTercihleriBE _ulkeTercihleriBE;
        private readonly IKomisyonlarBE _komisyonlarBE;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Dönüştürücüler
        public AdayBasvuruController(IAdaylarBE adaylarBE, IKullaniciBE kullaniciBE, IUlkeTercihleriBE ulkeTercihleriBE, IKomisyonlarBE komisyonlarBE, IUnitOfWork unitOfWork)
        {
            _adaylarBE = adaylarBE;
            _kullaniciBE = kullaniciBE;
            _ulkeTercihleriBE = ulkeTercihleriBE;
            _komisyonlarBE = komisyonlarBE;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region BilgiFormuGuncelle
        [HttpPost]
        public async Task<IActionResult> BilgiFormuGuncelle(Guid Id, IFormFile file)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            try
            {
                if (file != null && file.Length > 0)
                {
                    // Maksimum 4MB dosya boyutu kontrolü
                    if (file.Length > 4 * 1024 * 1024)
                    {
                        TempData["ErrorMessage"] = "Dosya boyutu 4MB'dan büyük olamaz. Lütfen dosyayı sıkıştırıp tekrar deneyin.";
                        return RedirectToAction(nameof(Guncelle), new { id = Id });
                    }

                    if (file.ContentType != "application/pdf")
                    {
                        TempData["ErrorMessage"] = "Lütfen sadece PDF dosyası yükleyin.";
                        return RedirectToAction(nameof(Guncelle), new { id = Id });
                    }

                    using (var ms = new System.IO.MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();

                        var adayBasvuru = _unitOfWork.adayBasvuruBilgileriRepository.GetFirstOrDefault(x => x.Id == Id);

                        if (adayBasvuru == null)
                        {
                            TempData["ErrorMessage"] = "Aday başvuru bilgisi bulunamadı.";
                            return RedirectToAction(nameof(Guncelle), new { id = Id });
                        }

                        try
                        {
                            adayBasvuru.BilgiFormu = fileBytes;
                            adayBasvuru.KaydedenId = user.LoginId;

                            _unitOfWork.adayBasvuruBilgileriRepository.Update(adayBasvuru);
                            _unitOfWork.Save();

                            TempData["SuccessMessage"] = "Bilgi formu başarıyla güncellendi.";
                        }
                        catch (Exception ex)
                        {
                            var message = ex.InnerException?.Message ?? ex.Message;
                            TempData["ErrorMessage"] = $"Veritabanı güncelleme hatası: {message}";
                            return RedirectToAction(nameof(Guncelle), new { id = Id });
                        }
                        return RedirectToAction(nameof(Guncelle), new { id = Id });
                    }
                }

                TempData["WarningMessage"] = "Lütfen bir dosya seçin.";
                return RedirectToAction(nameof(Guncelle), new { id = Id });
            }
            catch (DbUpdateException ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                TempData["ErrorMessage"] = $"Veritabanı güncelleme hatası: {innerMessage}";
                return RedirectToAction(nameof(Guncelle), new { id = Id });
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                TempData["ErrorMessage"] = $"Dosya yükleme sırasında bir hata oluştu: {message}";
                return RedirectToAction(nameof(Guncelle), new { id = Id });
            }
        }
        #endregion

        #region GosterBilgiFormu
        public IActionResult GosterBilgiFormu(Guid id)
        {
            try
            {
                var adayBasvuru = _unitOfWork.adayBasvuruBilgileriRepository.GetFirstOrDefault(x => x.Id == id);
                if (adayBasvuru?.BilgiFormu == null)
                {
                    return NotFound("Dosya bulunamadı.");
                }

                return File(adayBasvuru.BilgiFormu, "application/pdf");
            }
            catch (Exception ex)
            {
                return NotFound("Dosya gösterilirken bir hata oluştu: " + ex.Message);
            }
        }
        #endregion

        #region Index
        [Route("AD10006", Name = "AdayBasvuruIndexRoute")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            return View();
        }
        #endregion

        #region AdayEkle(Get)        
        [HttpGet]
        public IActionResult AdayEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            return View();
        }
        #endregion

        #region AdayEkle(Post)        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AdayEkle(AdayBasvuruBilgileriVM model, Guid? Id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (Id != null)
            {
                var data = _adaylarBE.AdayBasvuruGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region Guncelle
        [Route("AD10008", Name = "AdayBilgiGuncelleRoute")]
        public ActionResult Guncelle(string TC)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));


            if (TC != null)
            {
                var data = _adaylarBE.AdayBasvuruGetir(TC);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion
    }
}
