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
        #region Değişkenler
        private readonly IDerecelerBE _derecelerBE;
        #endregion

        #region Dönüştürücüler
        public DerecelerController(IDerecelerBE derecelerBE)
        {
            _derecelerBE = derecelerBE;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region DereceEkle(Get)
        [HttpGet]
        public IActionResult DereceEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }
        #endregion

        #region DereceEkle(Post)
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

                return RedirectToAction("Index");

            }
        }
        #endregion

        #region Guncelle
        public ActionResult Guncelle(Guid? id)
        {

            if (id != Guid.Empty)
            {
                var data = _derecelerBE.DereceGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region DereceSil
        [HttpDelete]
        public IActionResult DereceSil(Guid id)
        {
            if (id == Guid.Empty)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _derecelerBE.DereceSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        } 
        #endregion
    }
}
