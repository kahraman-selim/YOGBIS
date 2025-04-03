﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator,Manager")]
    public class AdayBasvuruController : Controller
    {
        #region Değişkenler
        private readonly IAdaylarBE _adaylarBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly IUlkeTercihleriBE _ulkeTercihleriBE;
        private readonly IKomisyonlarBE _komisyonlarBE;
        #endregion

        #region Dönüştürücüler
        public AdayBasvuruController(IAdaylarBE adaylarBE, IKullaniciBE kullaniciBE, IUlkeTercihleriBE ulkeTercihleriBE, IKomisyonlarBE komisyonlarBE)
        {
            _adaylarBE = adaylarBE;
            _kullaniciBE = kullaniciBE;
            _ulkeTercihleriBE = ulkeTercihleriBE;
            _komisyonlarBE = komisyonlarBE;
        }
        #endregion

        #region Index
        [Route("AD10006", Name = "AdayBasvuruIndexRoute")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            return View();
        }
        #endregion

        #region AdayEkle(Get)        
        [HttpGet]
        public IActionResult AdayEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            return View();
        }
        #endregion

        #region AdayEkle(Post)        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AdayEkle(AdayBasvuruBilgileriVM model, Guid? Id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            //var komisyon = await _kullaniciBE.KomisyonGetir();
            var komisyon = _komisyonlarBE.KomisyonAdlariGetir();
            ViewBag.Komisyonlar = komisyon.Data;

            if (Id != null)
            {
                var data = _adaylarBE.AdayBasvuruGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                //var data = _adaylarBE.AdayMYSSGuncelle(model, user);
                //if (data.IsSuccess)
                //{
                //    return RedirectToAction("Index");
                //}
                return View(model);
            }
        }
        #endregion

        #region Guncelle
        [Route("AD10008", Name = "AdayBasvuruGuncelleRoute")]
        public ActionResult Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            //var komisyon = await _kullaniciBE.KomisyonGetir();
            //var komisyon = _komisyonlarBE.KomisyonAdlariGetir();
            //ViewBag.Komisyonlar = komisyon.Data;

            if (id != null)
            {
                var data = _adaylarBE.AdayBasvuruGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion
    }
}
