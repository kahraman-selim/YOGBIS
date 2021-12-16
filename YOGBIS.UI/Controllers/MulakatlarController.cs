using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator,Manager")]
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
        
        public IActionResult Index(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (id > 0)
            {
                var data = _mulakatOlusturBE.MulakatGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult MulakatEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult MulakatEkle(MulakatlarVM model, int? MulakatId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (MulakatId > 0)
            {
                var data = _mulakatOlusturBE.MulakatGuncelle(model, user);
                return RedirectToAction("Index");
            }
            else
            {
                var data = _mulakatOlusturBE.MulakatEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }

        public IActionResult Guncelle(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (id > 0)
            {
                var data = _mulakatOlusturBE.MulakatGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }

        [HttpDelete]
        public IActionResult MulakatSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _mulakatOlusturBE.MulakatSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
    }
}
