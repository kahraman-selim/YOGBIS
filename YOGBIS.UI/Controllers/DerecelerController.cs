using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class DerecelerController : Controller
    {
        private readonly IDerecelerBE _derecelerBE;

        public DerecelerController(IDerecelerBE derecelerBE)
        {
            _derecelerBE = derecelerBE;
        }
        public IActionResult Index(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            //var requestmodel= _derecelerBE.DereceleriGetir(); 
            //if (requestmodel.IsSuccess)
            //{
            //    return View(requestmodel.Data);
            //}
            if (id != null)
            {
                var data = _derecelerBE.DereceGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult DereceEkle() 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DereceEkle(SoruDerecelerVM model, Guid? DereceId) 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (DereceId != null)
            {
                var data = _derecelerBE.DereceGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _derecelerBE.DereceEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }

        public ActionResult Guncelle(Guid? id)
        {

            if (id != null)
            {
                var data = _derecelerBE.DereceGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }

        [HttpDelete]
        public IActionResult DereceSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _derecelerBE.DereceSil(id); 
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
    }
}
