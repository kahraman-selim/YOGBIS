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
using YOGBIS.Data.Contracts;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class DuyurularController : Controller
    {
        #region Değişkenler
        private readonly IDuyurularBE _duyurularBE;
        private readonly IFotoGaleriBE _fotoGaleriBE;
        private readonly IDosyaGaleriBE _dosyaGaleriBE;
        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;
        #endregion

        #region Dönüştürücüler
        [Obsolete]
        public DuyurularController(IDuyurularBE duyurularBE, IFotoGaleriBE fotoGaleriBE, IDosyaGaleriBE dosyaGaleriBE, IWebHostEnvironment hostingEnvironment)
        {
            _duyurularBE = duyurularBE;
            _fotoGaleriBE = fotoGaleriBE;
            _dosyaGaleriBE = dosyaGaleriBE;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Index
        [Authorize]
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            var requestmodel = _duyurularBE.DuyurulariGetir();

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View(user);
        } 
        #endregion

        #region DuyuruEkleGet
        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet]
        [Route("Duyurular/DC10002", Name = "DuyuruEkleRoute")]
        public IActionResult DuyuruEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            return View();
        }
        #endregion

        #region DuyuruEklePost
        [Authorize(Roles = "Administrator,Manager")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Duyurular/DC10002", Name = "DuyuruEkleRoute")]
        [Obsolete]
        public async Task<IActionResult> DuyuruEkle(DuyurularVM model, Guid? DuyurularId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (model.FotoGaleris != null)
            {
                string fotoklasorler = "img/FotoGaleri/";
                model.FotoGaleri = new List<FotoGaleriVM>();

                foreach (var file in model.FotoGaleris)
                {
                    var galeri = new FotoGaleriVM()
                    {
                        FotoURL = await FotoYukle(fotoklasorler, file),
                        FotoAdi = file.FileName,
                        KaydedenId = user.LoginId,
                        KayitTarihi = model.KayitTarihi
                    };
                    model.FotoGaleri.Add(galeri);
                }

            }

            if (DuyurularId != null)
            {
                if (model.DuyuruKapakResim != null)
                {

                    //önceki yüklenen fotoyu dosyadan sil
                    var duyurukapakurl = _duyurularBE.DuyuruKapakURLGetir((Guid)DuyurularId).Data;
                    if (duyurukapakurl != null)
                    {
                        string[] parcalar = duyurukapakurl.ToString().Split("/img/FotoGaleri/");
                        string duyuruKapakAdi = parcalar[1].ToString();
                        if (duyuruKapakAdi != null || duyuruKapakAdi != "")
                        {
                            System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/FotoGaleri/" + duyuruKapakAdi);
                        }
                    }

                    string klasorler = "img/FotoGaleri/";
                    model.DuyuruKapakResimUrl = await FotoYukle(klasorler, model.DuyuruKapakResim);
                    string[] parcala = model.DuyuruKapakResimUrl.ToString().Split("/img/FotoGaleri/");
                    model.DuyuruKapakAdi = parcala[1].ToString();

                }
                else
                {
                    var duyurukapakurl = _duyurularBE.DuyuruKapakURLGetir((Guid)DuyurularId).Data;
                    if (duyurukapakurl != null)
                    {
                        string[] parcalar = duyurukapakurl.ToString().Split("/img/FotoGaleri/");
                        string etkinlikKapakAdi = parcalar[1].ToString();

                        model.DuyuruKapakResimUrl = duyurukapakurl.ToString();
                        model.DuyuruKapakAdi = etkinlikKapakAdi.ToString();
                    }
                }

                var data = _duyurularBE.DuyuruGuncelle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (model.DuyuruKapakResim != null)
                {
                    string klasorler = "img/FotoGaleri/";
                    model.DuyuruKapakResimUrl = await FotoYukle(klasorler, model.DuyuruKapakResim);
                    string[] parcalar = model.DuyuruKapakResimUrl.ToString().Split("img/FotoGaleri/");
                    model.DuyuruKapakAdi = parcalar[1].ToString();
                }

                var data = _duyurularBE.DuyuruEkle(model, user);
                if (data.IsSuccess)
                {
                    return View();
                }
                return View();
            }

            return View();
        }
        #endregion

        #region Guncelle
        [Authorize(Roles = "Administrator,Manager")]
        [Route("Duyurular/DC10003", Name = "DuyuruGuncelleRoute")]
        public ActionResult Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (id != null)
            {
                var data = _duyurularBE.DuyuruGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region DuyuruSil
        [Authorize(Roles = "Administrator,Manager")]
        [HttpDelete]
        [Obsolete]
        public IActionResult DuyuruSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });
            
            var duyurukapakurl = _duyurularBE.DuyuruKapakURLGetir((Guid)id).Data;
            if (duyurukapakurl != null)
            {
                string[] parcala = duyurukapakurl.ToString().Split("/img/FotoGaleri/");
                string kapakadi = parcala[1].ToString();

                if (kapakadi != null || kapakadi != "")
                {
                    System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/FotoGaleri/" + kapakadi);
                }
            }


            var fotourls = _fotoGaleriBE.FotoURLGetirDuyuruId((Guid)id).Data;

            if (fotourls != null)
            {
                foreach (var item in fotourls)
                {
                    string[] parcalar = item.ToString().Split("/img/FotoGaleri/");
                    var fotoadi = parcalar[1].ToString();

                    if (fotoadi != null)
                    {
                        System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/FotoGaleri/" + fotoadi);
                    }
                }
            }

            var dosyaurls = _dosyaGaleriBE.DosyaURLGetirDuyuruId((Guid)id).Data;

            if (dosyaurls != null)
            {
                foreach (var item in dosyaurls)
                {
                    string[] parcalar = item.ToString().Split("/img/FotoGaleri/");
                    var dosyaadi = parcalar[1].ToString();

                    if (dosyaadi != null)
                    {
                        System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/FotoGaleri/" + dosyaadi);
                    }
                }
            }

            var data = _duyurularBE.DuyuruSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

        #region DuyuruDetay
        [Authorize]
        [HttpGet]
        [Route("Duyurular/DC10004", Name = "DuyuruDetayRoute")]
        public IActionResult DuyuruDetay(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (id != null)
            {
                var data = _duyurularBE.DuyuruGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region FotoYukle
        [Obsolete]
        private async Task<string> FotoYukle(string fotoAdi, IFormFile foto)
        {

            fotoAdi += Guid.NewGuid().ToString() + "_" + foto.FileName;

            string fotoKlasor = Path.Combine(_hostingEnvironment.WebRootPath, fotoAdi);

            using FileStream fs = new FileStream(fotoKlasor, FileMode.Create);
            await foto.CopyToAsync(fs);
            return "/" + fotoAdi;
        }
        #endregion
    }
}
