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
    public class SehirlerController : Controller
    {
        #region Değişkenler
        private readonly ISehirlerBE _sehirlerBE;
        #endregion

        #region Dönüştürücüler
        public SehirlerController(ISehirlerBE sehirlerBE)
        {
            _sehirlerBE = sehirlerBE;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }
        #endregion

        #region SehirEkleGet
        [HttpGet]
        public IActionResult SehirEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }
        #endregion

        #region SehirEklePost
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SehirEkle(SehirlerVM model, Guid? SehirId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (SehirId != null)
            {
                var data = _sehirlerBE.SehirGuncelle(model, user);

                return View(model);
            }
            else
            {
                var data = _sehirlerBE.SehirEkle(model, user);
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
                var data = _sehirlerBE.SehirGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region SehirSil
        [HttpDelete]
        public IActionResult SehirSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _sehirlerBE.SehirSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });
        }
        #endregion
    }
}
