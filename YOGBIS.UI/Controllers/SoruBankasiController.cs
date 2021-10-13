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
    [Authorize(Roles = "Administrator")]
    public class SoruBankasiController : Controller
    {
       
        private readonly ISoruBankasiBE _soruBankasiBE;
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IDerecelerBE _derecelerBE;
        public SoruBankasiController(ISoruBankasiBE soruBankasiBE, ISoruKategorileriBE soruKategorileriBE, IDerecelerBE derecelerBE)
        {
            _soruBankasiBE = soruBankasiBE;
            _soruKategorileriBE = soruKategorileriBE;
            _derecelerBE = derecelerBE;
        }
        
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            //var requestModel = _soruBankasiBE.SoruGetirKullaniciId(user.LoginId);

            var requestModel = _soruBankasiBE.SorulariGetir(); 
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
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;
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
        public IActionResult SoruEkle(SoruBankasiVM model, int? SoruBankasiId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            if (SoruBankasiId>0)
            {
                var data = _soruBankasiBE.SoruGuncelle(model, user);
                return RedirectToAction("Index");
            }
            else
            {
                var data = _soruBankasiBE.SoruEkle(model,user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        
        public IActionResult Guncelle(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            if (id>0)
            {
                var data = _soruBankasiBE.SoruGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
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
