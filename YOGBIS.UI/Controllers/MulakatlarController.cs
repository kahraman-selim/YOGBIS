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
    [Authorize(Roles = "Administrator")]
    public class MulakatlarController : Controller
    {
        #region Değişkenler
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly IKullaniciBE _kullaniciBE;
        #endregion

        #region Dönüştücüler
        public MulakatlarController(IMulakatOlusturBE mulakatOlusturBE, IDerecelerBE derecelerBE, IKullaniciBE kullaniciBE)
        {
            _mulakatOlusturBE = mulakatOlusturBE;
            _derecelerBE = derecelerBE;
            _kullaniciBE = kullaniciBE;
        }
        #endregion

        #region Index
        [Route("ML10001", Name = "MulakatlarIndexRoute")]
        public IActionResult Index(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (id != null)
            {
                var data = _mulakatOlusturBE.MulakatGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region MulakatEkleGet
        [HttpGet]
        public IActionResult MulakatEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            return View();
        }
        #endregion

        #region MulakatEklePost
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult MulakatEkle(MulakatlarVM model, Guid? MulakatId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (MulakatId != null)
            {
                var data = _mulakatOlusturBE.MulakatGuncelle(model, user);
                return RedirectToAction("Index");
            }
            else
            {
                var data = _mulakatOlusturBE.MulakatEkle(model, user);
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Guncelle
        [Route("ML10002", Name = "MulakatlarGuncelleRoute")]
        public IActionResult Guncelle(Guid? id)
        {
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (id != null)
            {
                var data = _mulakatOlusturBE.MulakatGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region MulakatSil
        [HttpDelete]
        public IActionResult MulakatSil(Guid id)
        {
            var data = _mulakatOlusturBE.MulakatSil(id);
            return RedirectToAction("Index");
        } 
        #endregion
    }
}
