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
    public class UlkelerController : Controller
    {
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IKitalarBE _kitalarBE;
        public UlkelerController(IUlkelerBE ulkelerBE, IKitalarBE kitalarBE)
        {
            _ulkelerBE = ulkelerBE;
            _kitalarBE = kitalarBE;
        }
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var requestmodel = _ulkelerBE.UlkeGetirKullaniciId(user.LoginId);
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir();

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View(user);
            //var data = _ulkelerBE.UlkeleriGetir();
            //if (data.IsSuccess)
            //{
            //    var result = data.Data;
            //    return View(result);
            //}
            //return View();
        }

        public IActionResult UlkeEkle() 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;
            return View();
            //ViewBag.KitaAdi = _kitalarBE.KitalariGetir(); 1.yöntem

            //var kitaadi = _kitalarBE.KitalariGetir(); 2. yöntem
            //ViewBag.KitaAdi = kitaadi.Data.Select(q => new SelectListItem
            //{
            //    Text = q.KitaAdi,
            //    Value = q.KitaId.ToString()
            //});
        }

        [HttpPost]
        public IActionResult UlkeEkle(UlkelerVM model, int? UlkeId)
        {            

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;

            if (UlkeId>0)
            {
                var data = _ulkelerBE.UlkeGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _ulkelerBE.UlkeEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }

            //if (ModelState.IsValid) //tekli kayıt yöntemi
            //{
            //    var data = _ulkelerBE.UlkeEkle(model,user);
            //    if (data.IsSuccess)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    return View(model);
            //}
            //else
            //{
            //    return View(model);
            //}
        }

        public ActionResult Guncelle(int? id) 
        {
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;

            if (id>0)
            {
                var data = _ulkelerBE.UlkeGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
            
        }

        [HttpDelete]
        public IActionResult UlkeSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _ulkelerBE.UlkeSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
    }
}
