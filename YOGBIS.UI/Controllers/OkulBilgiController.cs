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

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class OkulBilgiController : Controller
    {
        
        #region Değişkenler
        private readonly IOkulBilgiBE _okulBilgiBE;
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IOkullarBE _okullarBE;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Dönüştürücüler
        public OkulBilgiController(IOkulBilgiBE okulBilgiBE, IUlkelerBE ulkelerBE, IOkullarBE okullarBE, IUnitOfWork unitOfWork)
        {
            _okulBilgiBE = okulBilgiBE;
            _ulkelerBE = ulkelerBE;
            _okullarBE = okullarBE;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Index
        [Authorize(Roles = "Administrator,Manager,SubManager,Follower")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;
            

            var requestmodel = _okulBilgiBE.OkulBilgiGetirKullaniciId(user.LoginId);

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View(user);
        }
        #endregion

        #region OkulBilgiEkleGet
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        [HttpGet]
        [Route("OkulBilgi/OBC10002", Name = "OkulBilgiEkleRoute")]
        public IActionResult OkulBilgiEkle(Guid UlkeId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;
            ViewBag.OkulAdi = string.Empty;
            return View();
        }
        #endregion

        #region OkulBilgiEklePost
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        [HttpPost]
        [Route("OkulBilgi/OBC10002", Name = "OkulBilgiEkleRoute")]
        public IActionResult OkulBilgiEkle(OkulBilgiVM model)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;
            ViewBag.OkulAdi = string.Empty; //_okullarBE.OkullariGetirAZ().Data;

            var data = _okulBilgiBE.OkulBilgiEkle(model, user);
            if (data.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(model);
            
        }
        #endregion

        #region GuncelleGet
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        [Route("OkulBilgi/OBC10003", Name = "OkulBilgiGuncelleRoute")]
        public ActionResult Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;
            ViewBag.OkulAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;

            if (id != null)
            {
                var data = _okulBilgiBE.OkulBilgiGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region GuncellePost
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("OkulBilgi/OBC10003", Name = "OkulBilgiGuncelleRoute")]
        public ActionResult Guncelle(OkulBilgiVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;
            ViewBag.OkulAdi = string.Empty;

            var data = _okulBilgiBE.OkulBilgiGuncelle(model, user);
            if (data.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region OkulBilgiSil
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        [HttpDelete]
        public IActionResult OkulBilgiSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _okulBilgiBE.OkulBilgiSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

        #region OkulBilgileriGetir
        [Authorize(Roles = "Administrator,Manager")]
        [Route("OkulBilgi/OBC10004", Name = "OkulBilgiGenelRoute")]
        public IActionResult OkulBilgileriGetir(Guid? ulkeId)
        {

            if (ulkeId != null)
            {
                var data = _okulBilgiBE.OkulBilgiGetirUlkeId((Guid)ulkeId);
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = string.Empty;

                if (data.IsSuccess)
                {
                    return View(data.Data);
                }

                return View();
            }
            else
            {
                var requestmodel = _okulBilgiBE.OkulBilgileriGetir();
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = string.Empty; //_okullarBE.OkullariGetirAZ().Data;

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }

                return View();
            }
        }
        #endregion

        #region OkulBilgileriGetirOkulId
        [Authorize(Roles = "Administrator,Manager")]
        [Route("OkulBilgi/OBC10005", Name = "OkulBilgiOkulIdRoute")]
        public IActionResult OkulBilgileriGetirOkulId(Guid? okulId)
        {

            if (okulId != null)
            {
                var data = _okulBilgiBE.OkulBilgiGetirOkulId((Guid)okulId);
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

                if (data.IsSuccess)
                {
                    return View(data.Data);
                }

                return View();
            }

            return View();
        }
        #endregion

        #region OkulAdGetir
        public IActionResult OkulAdGetir(Guid ulkeId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (ulkeId != null)
            {


                var data = _unitOfWork.okullarRepository.GetAll(x => x.UlkeId == ulkeId && x.OkulMudurId == user.LoginId); //_okullarBE.OkulGetirUlkeId((Guid)ulkeId).Data;  //_unitOfWork.okullarRepository.GetAll(x=>x.OkulUlkeId==ulkeId);   

                return Json(data);


            }

            return NotFound();
        } 
        #endregion

    }
}
