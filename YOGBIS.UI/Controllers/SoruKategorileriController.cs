using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class SoruKategorileriController : Controller
    {
       
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IDerecelerBE _derecelerBE;
        public SoruKategorileriController(ISoruKategorileriBE soruKategorileriBE, IDerecelerBE derecelerBE)
        {
            _soruKategorileriBE = soruKategorileriBE;
            _derecelerBE = derecelerBE;
        }        
        
        public IActionResult Index()
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            var requestModel = _soruKategorileriBE.SoruKategoriKullaniciId(user.LoginId); //SoruKategoriGetir();
            if (requestModel.IsSuccess)
                return View(requestModel.Data);

            return View(user);

        }
        
        [HttpGet]
        public IActionResult SoruKategoriEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SoruKategoriEkle(SoruKategorilerVM model, int? SoruKategorilerId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (SoruKategorilerId>0)
            {
                var data = _soruKategorileriBE.SoruKategoriGuncelle(model, user);
                return RedirectToAction("Index");
            }
            else
            {
                var data = _soruKategorileriBE.SoruKategoriEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        
        public IActionResult Guncelle(int? id)
        {
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (id>0)
            {
                var data = _soruKategorileriBE.SoruKategoriGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
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
