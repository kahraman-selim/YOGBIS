﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.Extentsion;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class OkullarController : Controller
    {
        private readonly IOkullarBE _okullarBE;
        private readonly IUlkelerBE _ulkelerBE;

        public OkullarController(IOkullarBE okullarBE, IUlkelerBE ulkelerBE)
        {
            _okullarBE = okullarBE;
            _ulkelerBE = ulkelerBE;
        }
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var requestmodel = _okullarBE.OkullariGetir();
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult OkulEkle() 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            return View();
        }

        
        [HttpPost]
        public IActionResult OkulEkle(OkullarVM model, int? OkulId) 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;

            //if (OkulId > 0)
            //{
            //    var data = _okullarBE.OkulGuncelle(model, user);

            //    return RedirectToAction("Index");
            //}
            //else
            //{
                var data = _okullarBE.OkulEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            //}
        }


        public ActionResult Guncelle(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;

            if (id > 0)
            {
                var data = _okullarBE.OkulGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }

        [ValidateAntiForgeryToken]
        public ActionResult Guncelle(OkullarVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;

            var data = _okullarBE.OkulGuncelle(model, user);
            if (data.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        [HttpDelete]
        public IActionResult OkulSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _okullarBE.OkulSil(id); 
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
    }
}
