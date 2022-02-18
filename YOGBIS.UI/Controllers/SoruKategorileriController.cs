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
    [Authorize(Roles = "Administrator")]
    public class SoruKategorileriController : Controller
    {

        #region Değişkenler
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IDerecelerBE _derecelerBE;
        #endregion

        #region Dönüştürücüler
        public SoruKategorileriController(ISoruKategorileriBE soruKategorileriBE, IDerecelerBE derecelerBE)
        {
            _soruKategorileriBE = soruKategorileriBE;
            _derecelerBE = derecelerBE;
        }
        #endregion

        #region Index
        public IActionResult Index(int? id)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            //var requestModel = _soruKategorileriBE.SoruKategoriKullaniciId(user.LoginId); //SoruKategoriGetir();
            //if (requestModel.IsSuccess)
            //    return View(requestModel.Data);

            //return View(user);
            if (id > 0)
            {
                var data = _soruKategorileriBE.SoruKategoriGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region SoruKategoriEkleGet
        [HttpGet]
        public IActionResult SoruKategoriEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            return View();
        }
        #endregion

        #region SoruKategoriEklePost
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SoruKategoriEkle(SoruKategorilerVM model, int? SoruKategorilerId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (SoruKategorilerId > 0)
            {
                var data = _soruKategorileriBE.SoruKategoriGuncelle(model, user);
                return RedirectToAction("Index");
            }
            else
            {
                var data = _soruKategorileriBE.SoruKategoriEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        #endregion

        #region Guncelle
        public IActionResult Guncelle(int? id)
        {
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

            if (id > 0)
            {
                var data = _soruKategorileriBE.SoruKategoriGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region SoruKategoriSil
        [HttpDelete]
        public IActionResult SoruKategoriSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _soruKategorileriBE.SoruKategoriSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        } 
        #endregion

    }
}
