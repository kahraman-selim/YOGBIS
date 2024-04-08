using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MulakatSorulariController : Controller
    {

        #region Değişkenler
        private readonly IMulakatSorulariBE _mulakatSorulariBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Dönüştürücüler
        public MulakatSorulariController(IMulakatSorulariBE mulakatSorulariBE, IDerecelerBE derecelerBE, ISoruKategorileriBE soruKategorileriBE, IMulakatOlusturBE mulakatOlusturBE, IUnitOfWork unitOfWork)
        {
            _mulakatSorulariBE = mulakatSorulariBE;
            _derecelerBE = derecelerBE;
            _soruKategorileriBE = soruKategorileriBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _unitOfWork = unitOfWork;
        } 
        #endregion

        #region Index
        public IActionResult Index(Guid? id, Guid? DereceId, Guid? SoruKategorilerId, Guid? MulakatId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.SoruKategorileri = _soruKategorileriBE.SoruKategorileriGetir().Data;
            ViewBag.Mulakatlar=_mulakatOlusturBE.MulakatlariGetir().Data;

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
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            return View();
        }
        #endregion

        #region MulakatSoruEkle(Post)
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult MulakatSoruEkle(MulakatSorulariVM model, Guid? MulakatSorulariId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;

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
        public ActionResult Guncelle(Guid? id, Guid? DereceId, Guid? SoruKategorilerId, Guid? MulakatId)
        {

            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.SoruKategorileri = string.Empty;
             // ViewBag.SoruKategorileri = _soruKategorileriBE.SoruKategoriGetir((Guid)SoruKategorilerId).Data;
             ViewBag.Mulakatlar = string.Empty;

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

        #region MulakatAdGetir(Guid dereceId) 
        public IActionResult MulakatAdGetir(Guid dereceId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (dereceId != null)
            {
                var data = _unitOfWork.mulakatlarRepository.GetAll(x => x.DereceId == dereceId);

                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region KategoriAdGetir(Guid dereceId) 
        public IActionResult KategoriAdGetir(Guid dereceId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (dereceId != null)
            {
                var data = _unitOfWork.sorukategorilerRepository.GetAll(x => x.DereceId == dereceId).OrderBy(x=>x.SoruKategorilerSiraNo);

                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region KategoriDetayGetir(Guid soruKategorilerId) 
        public IActionResult KategoriDetayGetir(Guid soruKategorilerId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (soruKategorilerId != null)
            {
                var data = _unitOfWork.sorukategorilerRepository.GetAll(x => x.SoruKategorilerId == soruKategorilerId).OrderBy(x => x.SoruKategorilerSiraNo);

                return Json(data);
            }

            return NotFound();
        } 
        #endregion
    }
}
