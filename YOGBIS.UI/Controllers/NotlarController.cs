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
    public class NotlarController : Controller
    {
        #region Değişkenler
        private readonly INotlarBE _notlarBE;
        #endregion

        #region Dönüştürücüler
        public NotlarController(INotlarBE notlarBE)
        {
            _notlarBE = notlarBE;
        }
        #endregion

        #region Index
        public IActionResult Index(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (id > 0)
            {
                var data = _notlarBE.NotGetirKullaniciId(user.LoginId);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region NotEkleGet
        [HttpGet]
        public IActionResult NotEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }
        #endregion

        #region NotEklePost
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult NotEkle(NotlarVM model, int? NotId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (NotId > 0)
            {
                var data = _notlarBE.NotGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _notlarBE.NotEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        #endregion

        #region NotSil
        [HttpDelete]
        public IActionResult NotSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _notlarBE.NotSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        } 
        #endregion
    }
}
