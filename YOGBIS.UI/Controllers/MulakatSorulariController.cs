using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
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
        public IActionResult Index(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (id != null)
            {
                var data = _mulakatSorulariBE.MulakatSoruGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
            
        }
        #endregion

        #region MulakatSoruEkle(Get)
        public IActionResult MulakatSoruEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            return View();
        }
        #endregion

        #region MulakatSoruEkle(Post)
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult MulakatSoruEkle(MulakatSorulariVM model, Guid? MulakatSorulariId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (MulakatSorulariId != null)
            {
                var data = _mulakatSorulariBE.MulakatSoruGuncelle(model, user);

                return RedirectToAction("Index");
            }
            else
            {
                var data = _mulakatSorulariBE.MulakatSoruEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        #endregion

        #region Guncelle
        public ActionResult Guncelle(Guid? id)
        {

            if (id != null)
            {
                var data = _mulakatSorulariBE.MulakatSoruGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region MulakatSoruSil
        [HttpDelete]
        public IActionResult MulakatSoruSil(Guid id)
        {
            if (id == null)
                return Json(new {success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _mulakatSorulariBE.MulakatSorusuSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

    }
}
