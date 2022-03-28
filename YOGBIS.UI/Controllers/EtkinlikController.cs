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
    public class EtkinlikController : Controller
    {
        
        #region Değişkenler
        private readonly IEtkinliklerBE _etkinliklerBE;
        private readonly IOkullarBE _okullarBE;
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IFotoGaleriBE _fotoGaleriBE;
        private readonly IDosyaGaleriBE _dosyaGaleriBE;
        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;
        #endregion

        #region Dönüştürücüler
        [Obsolete]
        public EtkinlikController(IEtkinliklerBE etkinliklerBE, IOkullarBE okullarBE, IUlkelerBE ulkelerBE, IFotoGaleriBE fotoGaleriBE, IDosyaGaleriBE dosyaGaleriBE, IWebHostEnvironment hostingEnvironment)
        {
            _etkinliklerBE = etkinliklerBE;
            _okullarBE = okullarBE;
            _ulkelerBE = ulkelerBE;
            _fotoGaleriBE = fotoGaleriBE;
            _dosyaGaleriBE = dosyaGaleriBE;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Index
        [Authorize(Roles = "Administrator,Manager,Representative,SubManager,Follower")]
        public IActionResult Index(Guid? etkinlikId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (etkinlikId == null)
            {
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = string.Empty;

                if (User.IsInRole(EnumsKullaniciRolleri.Administrator.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Manager.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Follower.ToString()))
                {
                    var requestmodel = _etkinliklerBE.EtkinlikleriGetir();

                    if (requestmodel.IsSuccess)
                    {
                        return View(requestmodel.Data);
                    }

                    return View(user);
                }
                else
                {
                    ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                    ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

                    var requestmodel = _etkinliklerBE.EtkinlikGetirKullaniciId(user.LoginId);

                    if (requestmodel.IsSuccess)
                    {
                        return View(requestmodel.Data);
                    }

                    return View(user);
                }
            }
            else
            {
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

                var requestmodel = _etkinliklerBE.EtkinlikGetir((Guid)etkinlikId);

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }

                return View(user);
            }
        }
        #endregion

        #region EtkinlikEkleGet
        [Authorize(Roles = "Administrator,Manager,Representative,SubManager,Follower")]
        [HttpGet]
        [Route("Etkinlik/EC10002", Name = "EtkinlikEkleRoute")]
        public IActionResult EtkinlikEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;
            ViewBag.OkulAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;
            ViewData["ulkeid"] = _okullarBE.UlkeIdGetirYoneticiId(user.LoginId).Data;
            return View();
        }
        #endregion

        #region EtkinlikEklePost
        [Authorize(Roles = "Administrator,Manager,Representative,SubManager,Follower")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Etkinlik/EC10002", Name = "EtkinlikEkleRoute")]
        [Obsolete]
        public async Task<IActionResult> EtkinlikEkle(EtkinliklerVM model, Guid? EtkinlikId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;
            ViewBag.OkulAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;
            ViewData["ulkeid"] = _okullarBE.UlkeIdGetirYoneticiId(user.LoginId).Data;
            model.UlkeId = (Guid)ViewData["ulkeid"];

            if (model.FotoGaleris != null)
            {
                string fotoklasorler = "img/EtkinlikFoto/";
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

            if (model.DosyaGaleris != null)
            {
                string dosyaklasorler = "img/EtkinlikDosyalar/";
                model.DosyaGaleri = new List<DosyaGaleriVM>();

                foreach (var file in model.DosyaGaleris)
                {
                    var dosyagaleri = new DosyaGaleriVM()
                    {

                        DosyaURL = await DosyaYukle(dosyaklasorler, file),
                        DosyaAdi=file.FileName,
                        KaydedenId = user.LoginId,
                        KayitTarihi = model.KayitTarihi
                    };
                    model.DosyaGaleri.Add(dosyagaleri);
                }

            }

            if (EtkinlikId != null)
            {

                if (model.EtkinlikKapakResim != null)
                {

                    //önceki yüklenen fotoyu dosyadan sil
                    var etkinlikkapakurl = _etkinliklerBE.EtkinlikKapakURLGetir((Guid)EtkinlikId).Data;
                    if (etkinlikkapakurl != null)
                    {
                        string[] parcalar = etkinlikkapakurl.ToString().Split("/img/EtkinlikKapakFoto/");
                        string etkinlikKapakAdi = parcalar[1].ToString();
                        if (etkinlikKapakAdi != null || etkinlikKapakAdi != "")
                        {
                            System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/EtkinlikKapakFoto/" + etkinlikKapakAdi);
                        }
                    }

                    string klasorler = "img/EtkinlikKapakFoto/";
                    model.EtkinlikKapakResimUrl = await FotoYukle(klasorler, model.EtkinlikKapakResim);
                    string[] parcala = model.EtkinlikKapakResimUrl.ToString().Split("/img/EtkinlikKapakFoto/");
                    model.EtkinlikKapakAdi = parcala[1].ToString();

                }
                else
                {
                    var etkinlikkapakurl = _etkinliklerBE.EtkinlikKapakURLGetir((Guid)EtkinlikId).Data;
                    if (etkinlikkapakurl != null)
                    {
                        string[] parcalar = etkinlikkapakurl.ToString().Split("/img/EtkinlikKapakFoto/");
                        string etkinlikKapakAdi = parcalar[1].ToString();

                        model.EtkinlikKapakResimUrl = etkinlikkapakurl.ToString();
                        model.EtkinlikKapakAdi = etkinlikKapakAdi.ToString();
                    }
                }

                var data = _etkinliklerBE.EtkinlikGuncelle(model, user);
                if (data.IsSuccess)
                {
                    //return RedirectToAction("Index");
                    return RedirectToAction("Guncelle", new { id = (Guid)EtkinlikId });
                }
            }
            else
            {
                if (model.EtkinlikKapakResim != null)
                {
                    string klasorler = "img/EtkinlikKapakFoto/";
                    model.EtkinlikKapakResimUrl = await FotoYukle(klasorler, model.EtkinlikKapakResim);
                    string[] parcalar = model.EtkinlikKapakResimUrl.ToString().Split("img/EtkinlikKapakFoto/");
                    model.EtkinlikKapakAdi = parcalar[1].ToString();
                }

                var data = _etkinliklerBE.EtkinlikEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                    //return RedirectToAction("Guncelle", new { id = (Guid)EtkinlikId });
                }
                return View(model);
            }

            return View();
        }
        #endregion

        #region Guncelle
        [Authorize(Roles = "Administrator,Manager,Representative,SubManager,Follower")]
        [Route("Etkinlik/EC10003", Name = "EtkinlikGuncelleRoute")]
        public ActionResult Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;
            ViewBag.OkulAdi = _okullarBE.OkulGetirYoneticiId(user.LoginId).Data;

            if (id != null)
            {
                var data = _etkinliklerBE.EtkinlikGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region EtkinlikSil
        [Authorize(Roles = "Administrator,Manager,Representative,SubManager,Follower")]
        [HttpDelete]
        [Obsolete]
        public IActionResult EtkinlikSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });
            var etkinlikkapakurl = _etkinliklerBE.EtkinlikKapakURLGetir((Guid)id).Data;
            if (etkinlikkapakurl != null)
            {
                string[] parcala = etkinlikkapakurl.ToString().Split("/img/EtkinlikKapakFoto/");
                string kapakadi = parcala[1].ToString();

                if (kapakadi != null || kapakadi != "")
                {
                    System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/EtkinlikKapakFoto/" + kapakadi);
                }
            }


            var fotourls = _fotoGaleriBE.FotoURLGetirEtkinlikId((Guid)id).Data;

            if (fotourls != null)
            {
                foreach (var item in fotourls)
                {
                    string[] parcalar = item.ToString().Split("/img/EtkinlikFoto/");
                    var fotoadi = parcalar[1].ToString();

                    if (fotoadi != null)
                    {
                        System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/EtkinlikFoto/" + fotoadi);
                    }
                }
            }

            var dosyaurls = _dosyaGaleriBE.DosyaURLGetirEtkinlikId((Guid)id).Data;

            if (dosyaurls != null)
            {
                foreach (var item in dosyaurls)
                {
                    string[] parcalar = item.ToString().Split("/img/EtkinlikDosyalar/");
                    var dosyaadi = parcalar[1].ToString();

                    if (dosyaadi != null)
                    {
                        System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/EtkinlikDosyalar/" + dosyaadi);
                    }
                }
            }

            var data = _etkinliklerBE.EtkinlikSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

        #region Etkinlikler
        [Authorize(Roles = "Administrator,Manager,Representative,SubManager,Follower")]
        [Route("Etkinlik/EC10004", Name = "EtkinliklerRoute")]
        public IActionResult Etkinlikler(Guid OkulId)
        {

            if (OkulId != null)
            {
                var data = _etkinliklerBE.EtkinlikGetirOkulId(OkulId);
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

                if (data.IsSuccess)
                {
                    return View(data.Data);
                }

                return View();
            }
            else
            {
                var requestmodel = _etkinliklerBE.EtkinlikleriGetir();
                ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
                ViewBag.OkulAdi = _okullarBE.OkullariGetir().Data;

                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }

                return View();
            }

        }
        #endregion

        #region EtkinlikGetirOkulId
        [Authorize(Roles = "Administrator,Manager,Follower")]
        [Route("Etkinlik/EC10005", Name = "EtkinliklerOkulIdRoute")]
        public ActionResult EtkinlikGetirOkulId(Guid okulId)
        {
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = string.Empty;

            var requestmodel = _etkinliklerBE.EtkinlikGetirOkulId(okulId);

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View();
        }
        #endregion

        #region EtkinlikGetirUlkeId
        [Authorize(Roles = "Administrator,Manager,Follower")]
        [Route("Etkinlik/EC10006", Name = "EtkinliklerUlkeIdRoute")]
        public ActionResult EtkinlikGetirUlkeId(Guid ulkeId)
        {
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = string.Empty;

            var requestmodel = _etkinliklerBE.EtkinlikGetirUlkeId(ulkeId);

            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View();
        }
        #endregion

        #region EtkinlikDetay
        [Authorize(Roles = "Administrator,Manager,Representative,SubManager,Follower")]
        [Route("Etkinlik/EC10007", Name = "EtkinlikDetayRoute")]
        public ActionResult Detay(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = _okullarBE.OkullariGetirAZ().Data;

            if (id != null)
            {
                var data = _etkinliklerBE.EtkinlikGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region EtkinlikFotoSil
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        [Obsolete]
        public IActionResult EtkinlikFotoSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var etkinlikfotourl = _fotoGaleriBE.FotoURLGetir((Guid)id);
            string[] parcalar = etkinlikfotourl.Data.ToString().Split("/img/EtkinlikFoto/");
            var etkinlikFotoAdi = parcalar[1].ToString();

            if (etkinlikFotoAdi != null)
            {
                System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/EtkinlikFoto/" + etkinlikFotoAdi);
            }

            var data = _fotoGaleriBE.FotoSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });
        }
        #endregion

        #region EtkinlikDosyaSil
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        [Obsolete]
        public IActionResult EtkinlikDosyaSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var etkinlikdosyaurl = _dosyaGaleriBE.DosyaURLGetir((Guid)id);
            string[] parcalar = etkinlikdosyaurl.Data.ToString().Split("/img/EtkinlikDosyalar/");
            var etkinlikDosyaAdi = parcalar[1].ToString();

            if (etkinlikDosyaAdi != null)
            {
                System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/EtkinlikDosyalar/" + etkinlikDosyaAdi);
            }

            var data = _dosyaGaleriBE.DosyaSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });
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

        #region DosyaYukle
        [Obsolete]
        private async Task<string> DosyaYukle(string dosyaAdi, IFormFile dosya)
        {

            dosyaAdi += Guid.NewGuid().ToString() + "_" + dosya.FileName;

            string dosyaKlasor = Path.Combine(_hostingEnvironment.WebRootPath, dosyaAdi);

            using FileStream ds = new FileStream(dosyaKlasor, FileMode.Create);
            await dosya.CopyToAsync(ds);
            return "/" + dosyaAdi;
        }
        #endregion

        #region DosyaIndir
        [Obsolete]
        [HttpGet("DosyaIndir")]
        public IActionResult DosyaIndir(Guid dosyaGaleriId)
        {

            var data = _dosyaGaleriBE.DosyaGetir(dosyaGaleriId);

            var dosyaurl = data.Data.DosyaURL.ToString();
            if (dosyaurl != null)
            {
                string[] parcala = dosyaurl.ToString().Split("/img/EtkinlikDosyalar/");
                string dosyaadi = parcala[1].ToString();
                string[] dosya = dosyaadi.ToString().Split("_");
                string ds = dosya[1].ToString();

                byte[] dosyaBytes = System.IO.File.ReadAllBytes(_hostingEnvironment.WebRootPath + dosyaurl);

                System.IO.File.WriteAllBytes(_hostingEnvironment.WebRootPath + dosyaurl, dosyaBytes);
                MemoryStream ms = new MemoryStream(dosyaBytes);
                return File(dosyaBytes, System.Net.Mime.MediaTypeNames.Application.Octet, ds);
            }
            return NotFound();
        } 
        #endregion
    }
}
