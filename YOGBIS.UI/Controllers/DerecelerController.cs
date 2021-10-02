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
            return View();
        }

        public IActionResult Ekle() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(DerecelerVM model) 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            //if (model.Id > 0)
            //{
            //    //var data = _derecelerBE.DereceGuncelle(model);
            //    return View(model);
            //}
            //else
            //{

            //    var data = _derecelerBE.DereceEkle(model, user);
            //    if (data.IsSuccess)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    return View(model);
            //}
            if (ModelState.IsValid)
            {
                var data = _derecelerBE.DereceEkle(model); 
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
    }
}
