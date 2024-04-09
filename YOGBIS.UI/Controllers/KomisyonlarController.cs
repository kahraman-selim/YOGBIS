using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.BusinessEngine.Implementaion;
using System.Threading.Tasks;
using YOGBIS.Data.Contracts;


namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KomisyonlarController : Controller
    {
        #region Değişkenler
        private readonly IKomisyonlarBE _komisyonlarBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Dönüştücüler
        public KomisyonlarController(IKomisyonlarBE komisyonlarBE, IKullaniciBE kullaniciBE, IUnitOfWork unitOfWork)
        {

            _komisyonlarBE = komisyonlarBE;
            _kullaniciBE = kullaniciBE;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;
            
            if (id != null) 
            {
                var data = _komisyonlarBE.KomisyonGetir((Guid)id);
                return View(data);
            }
            else 
            {
                return View();
            }
            
        }
        #endregion

        #region KomisyonEkle(Get)
        public async Task<IActionResult> KomisyonEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;

            return View();
        }
        #endregion

        #region KomisyonEkle(Post)
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> KomisyonEkle(KomisyonlarVM model, Guid? KomisyonlarId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;

            if (KomisyonlarId != null)
            {
                var data = _komisyonlarBE.KomisyonGuncelle(model, user);

                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            else
            {
                var data = _komisyonlarBE.KomisyonEkle(model, user);

                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        #endregion

        #region Guncelle
        public async Task<ActionResult> Guncelle(Guid? id)
        {

            var komisyon = await _kullaniciBE.KomisyonGetir();
            ViewBag.Komisyonlar = komisyon.Data;

            if (id != null)
            {
                var data = _komisyonlarBE.KomisyonGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region KomisyonSil
        [HttpDelete]
        public IActionResult KomisyonSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _komisyonlarBE.KomisyonSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion
    }
}
