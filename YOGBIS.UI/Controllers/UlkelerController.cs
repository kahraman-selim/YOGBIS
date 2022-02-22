using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class UlkelerController : Controller
    {
        #region Değişkenler
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IKitalarBE _kitalarBE;
        private readonly IFotoGaleriBE _fotoGaleriBE;
        private readonly IUnitOfWork _unitOfWork;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
               
        #endregion

        #region Donüştürücüler
        [Obsolete]
        public UlkelerController(IUlkelerBE ulkelerBE, IKitalarBE kitalarBE, IHostingEnvironment hostingEnvironment, IFotoGaleriBE fotoGaleriBE, IUnitOfWork unitOfWork)
        {
            _ulkelerBE = ulkelerBE;
            _kitalarBE = kitalarBE;
            _fotoGaleriBE = fotoGaleriBE;
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
        } 
        #endregion

        #region Index
        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var requestmodel = _ulkelerBE.UlkeleriGetir();  
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View(user);
        }
        #endregion

        #region UlkeEkleGet
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        
        [Route("Ulkeler/UC10002", Name = "UlkeEkleRoute")]
        public IActionResult UlkeEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;
            return View();
        } 
        #endregion

        #region UlkeEklePost
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Obsolete]
        [Route("Ulkeler/UC10002", Name = "UlkeEkleRoute")]
        public async Task<IActionResult> UlkeEkle(UlkelerVM model, Guid? UlkeId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;

            if (model.FotoGaleris != null)
            {
                string fotoklasorler = "img/Ulkeler/";                
                model.FotoGaleri = new List<FotoGaleriVM>();

                foreach (var file in model.FotoGaleris)
                {
                    var galeri = new FotoGaleriVM(){
                        
                        FotoURL = await FotoYukle(fotoklasorler, file),
                        FotoAdi = file.Name,
                        KaydedenId = user.LoginId,
                        KayitTarihi = model.KayitTarihi
                    };
                    model.FotoGaleri.Add(galeri);
                }

            }

            if (UlkeId != null)
            {

                //önceki yüklenen fotoyu dosyadan sil
                var ulkebayrakurl = _ulkelerBE.UlkeBayrakURLGetir((Guid)UlkeId);
                string[] parcalar = ulkebayrakurl.Data.ToString().Split("/img/Bayraklar/");
                var ulkeBayrakAdi = parcalar[1].ToString();

                if (model.UlkeBayrak != null)
                {

                    if (ulkeBayrakAdi != null)
                    {
                       System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/Bayraklar/" + ulkeBayrakAdi);
                    }

                    string klasorler = "img/Bayraklar/";
                    model.UlkeBayrakURL = await FotoYukle(klasorler, model.UlkeBayrak);
                    string[] parcala = model.UlkeBayrakURL.ToString().Split("/img/Bayraklar/");
                    model.UlkeBayrakAdi = parcala[1].ToString();

                }
                else
                {
                    model.UlkeBayrakURL = ulkebayrakurl.Data.ToString();
                    model.UlkeBayrakAdi = ulkeBayrakAdi.ToString();
                }

                var data = _ulkelerBE.UlkeGuncelle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                string klasorler = "img/Bayraklar/";
                model.UlkeBayrakURL = await FotoYukle(klasorler, model.UlkeBayrak);
                string[] parcalar = model.UlkeBayrakURL.ToString().Split("img/Bayraklar/");
                model.UlkeBayrakAdi = parcalar[1].ToString();

                var data = _ulkelerBE.UlkeEkle(model, user);
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
        [Route("Ulkeler/UC10003", Name = "UlkeGuncelle")]
        public ActionResult Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.KitaAdi = _kitalarBE.KitalariGetir().Data;

            if (id != null)
            {
                var data = _ulkelerBE.UlkeGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        } 
        #endregion

        #region UlkeSil
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [Obsolete]
        public IActionResult UlkeSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var ulkebayrakurl = _ulkelerBE.UlkeBayrakURLGetir((Guid)id);
            string[] parcala = ulkebayrakurl.Data.ToString().Split("/img/Bayraklar/");
            var bayrakadi = parcala[1].ToString();

            if (bayrakadi != null)
            {
                System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/Bayraklar/" + bayrakadi);                
            }

            //1. Yöntem
            //var ulkeid = _unitOfWork.ulkelerRepository.GetFirstOrDefault(u => u.UlkeId == id, includeProperties: "FotoGaleri");
            //if (ulkeid != null)
            //{
            //    foreach (var item in ulkeid.FotoGaleri.ToList())
            //    {
            //        var foto = _unitOfWork.fotoGaleriRepository.GetFirstOrDefault(u => u.FotoGaleriId == item.FotoGaleriId);
            //        if (foto != null)
            //        {
            //            string fotourls = foto.FotoURL.ToString();
            //            string[] parcalar = fotourls.ToString().Split("/img/Ulkeler/");
            //            var fotoadi = parcalar[1].ToString();
            //            if (fotoadi != null)
            //            {
            //                System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/Ulkeler/" + fotoadi);
            //            }
            //        }
            //    }                
            //}

            //2. Yöntem
            var fotourls = _fotoGaleriBE.FotoURLGetirUlkeId((Guid)id).Data;

            if (fotourls != null)
            {
                foreach (var item in fotourls)
                {
                    string[] parcalar = item.ToString().Split("/img/Ulkeler/");
                    var fotoadi = parcalar[1].ToString();

                    if (fotoadi != null)
                    {
                        System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/Ulkeler/" + fotoadi);
                    }
                }
            }

            var data = _ulkelerBE.UlkeSil(id);

            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        } 
        #endregion

        #region UlkeDetayByUlkeKodu
        [Authorize(Roles = "Administrator,Manager")]
        [Route("Ulkeler/UC10004", Name = "UlkeDetayById")]
        public IActionResult UlkeDetay(string ulkeKodu)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _unitOfWork.ulkelerRepository.GetFirstOrDefault(x => x.UlkeKodu == ulkeKodu);

            if (data != null)
            {
                var ulkeid = _ulkelerBE.UlkeIdGetir(ulkeKodu).Data;

                return RedirectToAction("UlkeDetayById", new {id=(Guid)ulkeid });

            }
            
            var id = Guid.NewGuid();
            return RedirectToAction("UlkeDetayById", new { id = (Guid)id });

        }
        #endregion

        #region UlkeDetayById
        [Authorize(Roles = "Administrator,Manager")]
        [Route("Ulkeler/UC10005", Name = "UlkeDetayByUlkeKodu")]
        public IActionResult UlkeDetayById(Guid id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            
            var data = _unitOfWork.ulkelerRepository.GetFirstOrDefault(x => x.UlkeId == id);

            if (data != null)
            {
                var requestmodel = _ulkelerBE.UlkeGetir(id);                

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }

                return View(user);
            }

            return View();
        }
        #endregion

        #region UlkeFotoSil
        [Obsolete]
        public IActionResult UlkeFotoSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var ulkefotourl = _fotoGaleriBE.FotoURLGetir((Guid)id);
            string[] parcalar = ulkefotourl.Data.ToString().Split("/img/Ulkeler/");
            var ulkeFotoAdi = parcalar[1].ToString();

            if (ulkeFotoAdi != null)
            {
                System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/Ulkeler/" + ulkeFotoAdi);
            }

            var data = _fotoGaleriBE.FotoSil(id);          
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

        #region FotoYukle
        [Obsolete]
        private async Task<string> FotoYukle(string dosyaAdi, IFormFile dosya)
        {

            dosyaAdi += Guid.NewGuid().ToString() + "_" + dosya.FileName;

            string dosyaKlasor = Path.Combine(_hostingEnvironment.WebRootPath, dosyaAdi);

            await dosya.CopyToAsync(new FileStream(dosyaKlasor, FileMode.Create));

            return "/" + dosyaAdi;
        }
        #endregion
    }
}
