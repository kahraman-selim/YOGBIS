using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class OkullarController : Controller
    {

        #region Değişkenler
        private readonly IOkullarBE _okullarBE;
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly IEyaletlerBE _eyaletlerBE;
        private readonly IUnitOfWork _unitOfWork;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Dönüştürücüler
        [Obsolete]
        public OkullarController(IOkullarBE okullarBE, IUlkelerBE ulkelerBE, IKullaniciBE kullaniciBE, IHostingEnvironment hostingEnvironment, IEyaletlerBE eyaletlerBE, IUnitOfWork unitOfWork)
        {
            _okullarBE = okullarBE;
            _ulkelerBE = ulkelerBE;
            _kullaniciBE = kullaniciBE;
            _eyaletlerBE = eyaletlerBE;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Index
        [Authorize(Roles = "Administrator,Manager,Representative,SubManager,Follower")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (User.IsInRole(EnumsKullaniciRolleri.Administrator.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Manager.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Follower.ToString()))
            {

                var requestmodel = _okullarBE.OkullariGetir();
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }
                return View(user);
            }
            else
            {
                var requestmodel = _okullarBE.OkulGetirYoneticiId(user.LoginId);

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }
                return View(user);
            }
        }
        #endregion

        #region OkulEkleGet
        [Authorize(Roles = "Administrator")]
        [HttpGet]

        [Route("Okullar/OC10002", Name = "OkulEkleRoute")]
        public async Task<IActionResult> OkulEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            var okulmudur = await _kullaniciBE.OkulMuduruGetir();
            ViewBag.OkulMuduru = okulmudur.Data;

            return View();
        }
        #endregion

        #region OkulEklePost
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Obsolete]
        [Route("Okullar/OC10002", Name = "OkulEkleRoute")]
        public async Task<IActionResult> OkulEkle(OkullarVM model, Guid? OkulId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            var okulmudur = await _kullaniciBE.OkulMuduruGetir();
            ViewBag.OkulMuduru = okulmudur.Data;

            if (okulmudur.Data == null)
            {
                model.OkulMudurId = null;
            }

            if (OkulId != null)
            {

                var data = _okullarBE.OkulGuncelle(model, user);

                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var data = _okullarBE.OkulEkle(model, user);

                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }

            return View();
        }
        #endregion

        #region Guncelle
        [Authorize(Roles = "Administrator")]
        [Route("Okullar/OC10003", Name = "OkulGuncelle")]
        public async Task<ActionResult> Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            var okulmudur = await _kullaniciBE.OkulMuduruGetir();
            ViewBag.OkulMuduru = okulmudur.Data;

            if (id != null)
            {
                var data = _okullarBE.OkulGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region OkulSil
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public IActionResult OkulSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _okullarBE.OkulSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

        #region OkulDetay
        [Authorize(Roles = "Administrator,Manager,Representative,SubManager")]
        [Route("Okullar/OC10004", Name = "OkulDetayById")]
        public IActionResult OkulDetay(Guid id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _unitOfWork.okullarRepository.GetFirstOrDefault(x => x.OkulId == id);

            if (data != null)
            {
                var requestmodel = _okullarBE.OkulGetir(id);

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }
                return View(user);
            }

            return View();
        }
        #endregion

        #region OkulDetayGuncelleGet
        [Authorize(Roles = "SubManager")]
        [HttpGet]
        [Route("Okullar/OC10005", Name = "OkulDetayGuncelle")]
        public IActionResult OkulDetayGuncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (id != null)
            {
                var data = _okullarBE.OkulGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region OkulDetayGuncellePost
        [Authorize(Roles = "SubManager")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Obsolete]
        [Route("Okullar/OC10005", Name = "OkulDetayGuncelle")]
        public IActionResult OkulDetayGuncelle(OkullarVM model, Guid? OkulId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (OkulId != null)
            {

                var data = _okullarBE.OkulDetayGuncelle(model, user);

                if (data.IsSuccess)
                {
                    return View(data.Data);
                }
            }
            return View();
        }
        #endregion

        #region EyaletEkleJson
        [HttpPost]
        public JsonResult EyaletEkleJson(EyaletlerVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _eyaletlerBE.EyaletEkle(model, user);
            if (data.IsSuccess)
            {
                return Json("200");
            }
            return Json("Eklenecek veri bulunamadı !");

        }
        #endregion

        #region OkulEyaletGetir
        public IActionResult OkulEyaletGetir(string EyaletAdi)
        {
                        
            if (EyaletAdi != null)
            {
                var data = _unitOfWork.eyaletlerRepository.GetAll(x=>x.EyaletAdi==EyaletAdi); //_eyaletlerBE.EyaletGetirEyaletAdi(EyaletAdi);   
                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region FotoYukle
        [Obsolete]
        private string FotoYukle(string dosyaAdi, IFormFile dosya)
        {

            dosyaAdi += Guid.NewGuid().ToString() + "_" + dosya.FileName;

            string dosyaKlasor = Path.Combine(_hostingEnvironment.WebRootPath, dosyaAdi);

            using (FileStream fs = new FileStream(dosyaKlasor, FileMode.Create))
            {
                dosya.CopyToAsync(fs);
                return "/" + dosyaAdi;
            }
        }
        #endregion

    }
}
