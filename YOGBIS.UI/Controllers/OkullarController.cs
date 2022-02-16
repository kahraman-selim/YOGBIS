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

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class OkullarController : Controller
    {
        private readonly IOkullarBE _okullarBE;
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IKullaniciBE _kullaniciBE;        
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public OkullarController(IOkullarBE okullarBE, IUlkelerBE ulkelerBE, IKullaniciBE kullaniciBE, IHostingEnvironment hostingEnvironment)
        {
            _okullarBE = okullarBE;
            _ulkelerBE = ulkelerBE;
            _kullaniciBE = kullaniciBE;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var requestmodel = _okullarBE.OkullariGetir();
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View(user);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult OkulEkle() 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            //var okulmudur = await _kullaniciBE.OkulMuduruGetir();
            //ViewBag.OkulMuduru = okulmudur.Data;
       
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Obsolete]
        public IActionResult OkulEkle(OkullarVM model, int? OkulId) 
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            //var okulmudur = await _kullaniciBE.OkulMuduruGetir();
            //ViewBag.OkulMuduru = okulmudur.Data;

            //if (okulmudur.Data == null)
            //{
            //    model.OkulMudurId = null;
            //}
            //if (model.OkulLogo != null)
            //{
            //    string klasorler = "img/Okullar/";
            //    model.OkulLogoURL = await FotoYukle(klasorler, model.OkulLogo);
            //}

            if (OkulId > 0)
            {
                //if (model.OkulMudurId==user.LoginId)
                //{
                //    var datamd = _okullarBE.OkulGuncelle(model, user);

                //    if (datamd.IsSuccess)
                //    {
                //        return RedirectToAction("OC10001","Okullar");
                //    }
                //}

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

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Guncelle(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            var okulmudur = await _kullaniciBE.OkulMuduruGetir();
            ViewBag.OkulMuduru = okulmudur.Data;

            if (id > 0)
            {
                var data = _okullarBE.OkulGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public IActionResult OkulSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _okullarBE.OkulSil(id); 
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }

        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult OkulDetay()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var requestmodel = _okullarBE.OkullariGetir();
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View(user);
        }

        [Obsolete]
        private async Task<string> FotoYukle(string dosyaYolu, IFormFile dosya)
        {

            //fotoadi = dosya.FileName;

            dosyaYolu += Guid.NewGuid().ToString() + "_" + dosya.FileName;

            string dosyaKlasor = Path.Combine(_hostingEnvironment.WebRootPath, dosyaYolu);

            await dosya.CopyToAsync(new FileStream(dosyaKlasor, FileMode.Create));

            return "/" + dosyaYolu;
        }

    }
}
