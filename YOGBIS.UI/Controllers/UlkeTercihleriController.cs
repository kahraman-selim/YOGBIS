using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.Implementaion;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UlkeTercihleriController : Controller
    {
        #region Değişkenler
        private readonly IUlkeTercihleriBE _ulkeTercihleriBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Dönüştürücüler
        public UlkeTercihleriController(IUlkeTercihleriBE ulkeTercihleriBE, IDerecelerBE derecelerBE, IMulakatOlusturBE mulakatOlusturBE, IUnitOfWork unitOfWork)
        {
            _ulkeTercihleriBE = ulkeTercihleriBE;
            _derecelerBE = derecelerBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;

            return View();
        }
        #endregion

        #region UlkeTercihEkle(Get)
        [HttpGet]
        public IActionResult UlkeTercihEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = string.Empty;

            return View();
        }
        #endregion

        #region UlkeTercihEkle(Post)
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UlkeTercihEkle(UlkeTercihVM model, Guid? UlkeTercihId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = string.Empty;

            if (UlkeTercihId != null)
            {
                var data = _ulkeTercihleriBE.UlkeTercihGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _ulkeTercihleriBE.UlkeTercihEkle(model, user);

                return RedirectToAction("Index");

            }
        }
        #endregion

        #region Guncelle
        public IActionResult Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Mulakatlar = _mulakatOlusturBE.MulakatlariGetir().Data;

            if (id != Guid.Empty)
            {
                var data = _ulkeTercihleriBE.UlkeTercihGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region UlkeTercihSil
        public IActionResult UlkeTercihSil(Guid id)
        {
            if (id == Guid.Empty)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _ulkeTercihleriBE.UlkeTercihSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });
        }
        #endregion

        #region MulakatAdGetir(Guid dereceId) 
        public IActionResult MulakatAdGetir(Guid dereceId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (dereceId != null)
            {
                var data = _unitOfWork.mulakatlarRepository.GetAll(x => x.DereceId == dereceId);
                //string donem = JsonConvert.SerializeObject(data);

                return Json(data);
            }

            return NotFound();
        }
        #endregion
    }
}
