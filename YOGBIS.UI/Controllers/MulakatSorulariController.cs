﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Administrator")]
    public class MulakatSorulariController : Controller
    {

        #region Değişkenler
        private readonly IMulakatSorulariBE _mulakatSorulariBE;
        #endregion

        #region Dönüştürücüler
        public MulakatSorulariController(IMulakatSorulariBE mulakatSorulariBE)
        {
            _mulakatSorulariBE = mulakatSorulariBE;
        } 
        #endregion

        #region Index
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _mulakatSorulariBE.MulakatSorulariGetir();
            if (data.IsSuccess)
            {
                var result = data.Data;
                return View(result);
            }
            return View();
        }
        #endregion

        #region MulakatSoruEkle
        public IActionResult MulakatSoruEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }
        #endregion

        #region MulakatSoruEklePost
        [HttpPost]
        public IActionResult MulakatSoruEkle(MulakatSorulariVM model)
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
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (ModelState.IsValid)
            {
                var data = _mulakatSorulariBE.MulakatSorusuEkle(model, user);
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
        #endregion

        /*[HttpGet]
        public IActionResult MulakatSoruGuncelle(int SoruSiraNo)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (id == null)
                return View();
            var data = _mulakatSorulariBE.MulakatSorulariGetir(int SoruSiraNo);
            if (data.IsSuccess)
                return View(data.Data);
            return View();
        }*/

        #region MulakatSoruGuncelle

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult MulakatSoruGuncelle(MulakatSorulariVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (ModelState.IsValid)
            {
                var data = _mulakatSorulariBE.MulakatSorusuGuncelle(model, user);
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
        #endregion


        #region MulakatSoruSil
        /*[HttpDelete]
        public IActionResult MulakatSoruSil(int SoruSiraNo)
        {
            if (id == null)
                return Json(new {success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _mulakatSorulariBE.MulakatSorusuSil(SoruSiraNo);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }*/
        #endregion

    }
}
