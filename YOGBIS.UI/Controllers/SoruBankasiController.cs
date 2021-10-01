using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = ResultConstant.Admin_Role)]
    public class SoruBankasiController : Controller
    {
       
        private readonly ISoruBankasiBE _soruBankasiBE;
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        public SoruBankasiController(ISoruBankasiBE soruBankasiBE, ISoruKategorileriBE soruKategorileriBE)
        {
            _soruBankasiBE = soruBankasiBE;
            _soruKategorileriBE = soruKategorileriBE;
        }
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            var requestModel = _soruBankasiBE.SoruGetirKullaniciId(user.LoginId);
            //ViewBag.SoruKategorileri=_soruKategorileriBE.GetAllSoruKategoriler();
            if (requestModel.IsSuccess)
                return View(requestModel.Data);  
            
                return View(user);
                     

            //var data = _soruBankasiBE.GetAllSorular();
            //if (data.IsSuccess)
            //{
            //    var result = data.Data;
            //    return View(result);
            //}
            //return View(user);
        }
        public IActionResult SoruEkle()
        {
            //ViewBag.SoruKategorileri = _soruKategorileriBE.GetAllSoruKategoriler().Data;
            //var data = _soruKategorileriBE.GetAllSoruKategoriler();
            //ViewBag.SoruKategorileri = data.Data.Select(q => new SelectListItem
            //{
            //    Text = q.SoruKategorilerAdi,
            //    Value = q.SoruKategorilerId.ToString()
            //});

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
            var data = _soruBankasiBE.SoruGetir(id);
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
