using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = ResultConstant.Admin_Role)]
    public class SoruBankasiController : Controller
    {
       
        private readonly ISoruBankasiBE _soruBankasiBE;
        public SoruBankasiController(ISoruBankasiBE soruBankasiBE)
        {
            _soruBankasiBE = soruBankasiBE;
        }        
        
        public IActionResult Index()
        {
            var data = _soruBankasiBE.GetAllSorular();
            if (data.IsSuccess)
            {
                var result = data.Data;
                return View(result);
            }
            return View();
        }
        public IActionResult SoruEkle()
        {
            return View();
        }        
        [HttpPost]
        public IActionResult SoruEkle(SoruBankasiVM model)
        {
            #region Ekleme ve Güncelleme Örneği
            //if (model.MulakatSorulariId>0)
            //{
            //    var data = _mulakatSorulariBE.MulakatSorusuGuncelle(model);
            //}
            //else
            //{
            //    if (ModelState.IsValid)
            //    {
            //        var data = _mulakatSorulariBE.MulakatSorusuEkle(model);
            //        if (data.IsSuccess)
            //        {
            //            return RedirectToAction("Index");
            //        }
            //        return View(model);
            //    }
            //    else
            //    {
            //        return View(model);
            //    }
            //} 
            #endregion

            if (ModelState.IsValid)
            {
                var data = _soruBankasiBE.SoruEkle(model);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            else
            {
                return View(model);
            }
        }
        
        [HttpGet]
        public IActionResult SoruGuncelle(int id)
        {
            if (id < 0)
                return View();
            var data = _soruBankasiBE.GetAllSorular(id);
            if (data.IsSuccess)
                return View(data.Data);
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SoruGuncelle(SoruBankasiVM model)
        {
            if (ModelState.IsValid)
            {
                var data = _soruBankasiBE.SoruGuncelle(model);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        
        [HttpDelete]
        public IActionResult SoruSil(int id)
        {
            if (id < 0)
                return Json(new {success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _soruBankasiBE.SoruSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
  
    }
}
