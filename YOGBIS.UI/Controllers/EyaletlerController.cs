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
    public class EyaletlerController : Controller
    {
        #region Değişkenler
        private readonly IEyaletlerBE _eyaletlerBE;
        #endregion

        #region Dönüştürücüler
        public EyaletlerController(IEyaletlerBE eyaletlerBE)
        {
            _eyaletlerBE = eyaletlerBE;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }
        #endregion

        #region EyaletEkleGet
        [HttpGet]
        public IActionResult EyaletEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }
        #endregion

        #region EyaletEklePost
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EyaletEkle(EyaletlerVM model, Guid? EyaletId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (EyaletId != null)
            {
                var data = _eyaletlerBE.EyaletGuncelle(model, user);

                return View(model);
            }
            else
            {
                var data = _eyaletlerBE.EyaletEkle(model, user);
                if (data.IsSuccess)
                {
                    return View(model);
                }
                return View(model);
            }
        }
        #endregion

        #region Guncelle
        public IActionResult Guncelle(Guid? id)
        {
            if (id != null)
            {
                var data = _eyaletlerBE.EyaletGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region EyaletSil
        [HttpDelete]
        public IActionResult EyaletSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _eyaletlerBE.EyaletSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });
        }
        #endregion
    }
}
