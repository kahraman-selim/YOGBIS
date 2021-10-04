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
    [Authorize(Roles = ResultConstant.Admin_Role)]
    public class DerecelerController : Controller
    {
        private readonly IDerecelerBE _derecelerBE;

        public DerecelerController(IDerecelerBE derecelerBE)
        {
            _derecelerBE = derecelerBE;
        }
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            var data = _derecelerBE.DereceGetir(); 
            if (data.IsSuccess)
            {
                var result = data.Data;
                return View(result);
            }
            return View();
        }

        public IActionResult DereceEkle() 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }
        [HttpPost]
        public IActionResult DereceEkle(DerecelerVM model, int? Id) 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (Id > 0)
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

        public ActionResult Guncelle(int? id)
        {
            
            if (id > 0)
            {
                var data = _derecelerBE.DereceGetir((int)id); 
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }

        [HttpDelete]
        public IActionResult DereceSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _derecelerBE.DereceSil(id); 
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
    }
}
