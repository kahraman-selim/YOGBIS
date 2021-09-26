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
    public class SoruKategorileriController : Controller
    {
       
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        public SoruKategorileriController(ISoruKategorileriBE soruKategorileriBE)
        {
            _soruKategorileriBE = soruKategorileriBE;
        }        
        
        public IActionResult Index()
        {
            var data = _soruKategorileriBE.GetAllSoruKategoriler();
            if (data.IsSuccess)
            {
                var result = data.Data;
                return View(result);
            }
            return View();
        }
        public IActionResult SoruKategoriEkle()
        {
            return View();
        }        
        [HttpPost]
        public IActionResult SoruKategoriEkle(SoruKategorilerVM model)
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
                var data = _soruKategorileriBE.SoruKategoriEkle(model);
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
        public IActionResult SoruKategoriGuncelle(int id)
        {
            if (id < 0)
                return View();
            var data = _soruKategorileriBE.GetAllSoruKategoriler(id);
            if (data.IsSuccess)
                return View(data.Data);
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SoruKategoriGuncelle(SoruKategorilerVM model)
        {
            if (ModelState.IsValid)
            {
                var data = _soruKategorileriBE.SoruKategoriGuncelle(model);
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
        public IActionResult SoruKategoriSil(int id)
        {
            if (id < 0)
                return Json(new {success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _soruKategorileriBE.SoruKategoriSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
  
    }
}
