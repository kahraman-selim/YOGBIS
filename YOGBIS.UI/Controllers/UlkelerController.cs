using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class UlkelerController : Controller
    {
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IKitalarBE _kitalarBE;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public UlkelerController(IUlkelerBE ulkelerBE, IKitalarBE kitalarBE, IHostingEnvironment hostingEnvironment)
        {
            _ulkelerBE = ulkelerBE;
            _kitalarBE = kitalarBE;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var requestmodel = _ulkelerBE.UlkeleriGetir();  //UlkeGetirKullaniciId(user.LoginId);
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View(user);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult UlkeEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> UlkeEkle(UlkelerVM model, int? UlkeId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;            

            if (model.UlkeBayrak != null)
            {
                string klasorler = "img/Bayraklar/";
                model.UlkeBayrakURL = await FotoYukle(klasorler, model.UlkeBayrak);
                string[] parca = model.UlkeBayrakURL.ToString().Split('_');
                model.UlkeBayrakAdi = parca[1].ToString();
            }
            else
            {
                var ulkebayrakurl = _ulkelerBE.UlkeBayrakURLGetir((int)UlkeId);
                model.UlkeBayrakURL = ulkebayrakurl.ToString();
                //string[] parca = ulkebayrakurl.ToString().Split('_');
                //model.UlkeBayrakAdi = parca[1].ToString();
            }

            if (model.FotoGaleris != null)
            {
                string klasorler = "img/Ulkeler/";

                model.FotoGaleri = new List<FotoGaleriVM>();

                foreach (var file in model.FotoGaleris)
                {
                    var galeri = new FotoGaleriVM()
                    {
                        FotoAdi = file.FileName,
                        FotoURL = await FotoYukle(klasorler, file),
                        KaydedenId = user.LoginId,
                        KayitTarihi = model.KayitTarihi
                    };
                    model.FotoGaleri.Add(galeri);
                }

            }

            if (UlkeId > 0)
            {
                var data = _ulkelerBE.UlkeGuncelle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var data = _ulkelerBE.UlkeEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        [Route("Ulkeler/{id:int:min(1)}", Name = "UlkeDetayRoute")]
        public ActionResult Guncelle(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;


            if (id > 0)
            {
                var data = _ulkelerBE.UlkeGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }

        //[Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //[Obsolete]
        //public async Task<ActionResult> Guncelle(UlkelerVM model)
        //{
        //    var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
        //    ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;

        //    if (model.UlkeBayrak != null)
        //    {
        //        string klasorler = "img/Bayraklar/";
        //        model.UlkeBayrakURL = await FotoYukle(klasorler, model.UlkeBayrak);
        //    }

        //    var data = _ulkelerBE.UlkeGuncelle(model, user);
        //    if (data.IsSuccess)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public IActionResult UlkeSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _ulkelerBE.UlkeSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }

        [Authorize(Roles = "Administrator,Manager")]
        //[Route("Ulkeler/{id:int:min(1)}", Name = "UlkeGenelDetayRoute")]
        public IActionResult UlkeDetay(string ulkeKodu)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var requestmodel = _ulkelerBE.UlkeGetirUlkeKodu(ulkeKodu);
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View(user);
        }

        [Authorize(Roles = "Administrator,Manager")]
        //[Route("Ulkeler/{id:int:min(1)}", Name = "UlkeGenelDetayRoute")]
        public IActionResult UC10001(int id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var requestmodel = _ulkelerBE.UlkeGetir(id);
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View(user);
        }

        public IActionResult UlkeFotoSil(int id)
        {
            if (id < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _ulkelerBE.UlkeFotoSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        [Obsolete]
        private async Task<string> FotoYukle(string dosyaYolu, IFormFile dosya)
        {
            
            dosyaYolu += Guid.NewGuid().ToString() + "_" + dosya.FileName;

            string dosyaKlasor = Path.Combine(_hostingEnvironment.WebRootPath, dosyaYolu);

            await dosya.CopyToAsync(new FileStream(dosyaKlasor, FileMode.Create));

            return "/" + dosyaYolu;
        }
    }
}
