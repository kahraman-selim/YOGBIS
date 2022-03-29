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
    public class OkullarController : Controller
    {

        #region Değişkenler
        private readonly IOkullarBE _okullarBE;
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly IEyaletlerBE _eyaletlerBE;
        private readonly IFotoGaleriBE _fotoGaleriBE;
        private readonly ISehirlerBE _sehirlerBE;
        private readonly IOkulBinaBolumBE _okulBinaBolumBE;
        private readonly ISubelerBE _subelerBE;
        private readonly ISiniflarBE _siniflarBE;
        private readonly IOgrencilerBE _ogrencilerBE;
        private readonly IUnitOfWork _unitOfWork;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Dönüştürücüler
        [Obsolete]
        public OkullarController(IOkullarBE okullarBE, IUlkelerBE ulkelerBE, IKullaniciBE kullaniciBE, IHostingEnvironment hostingEnvironment, 
            IEyaletlerBE eyaletlerBE, ISehirlerBE sehirlerBE, IFotoGaleriBE fotoGaleriBE, IOkulBinaBolumBE okulBinaBolumBE, ISubelerBE subelerBE,
            ISiniflarBE siniflarBE, IOgrencilerBE ogrencilerBE, IUnitOfWork unitOfWork)
        {
            _okullarBE = okullarBE;
            _ulkelerBE = ulkelerBE;
            _kullaniciBE = kullaniciBE;
            _eyaletlerBE = eyaletlerBE;
            _sehirlerBE = sehirlerBE;
            _fotoGaleriBE = fotoGaleriBE;
            _okulBinaBolumBE = okulBinaBolumBE;
            _subelerBE = subelerBE;
            _siniflarBE = siniflarBE;
            _ogrencilerBE = ogrencilerBE;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Index
        [Authorize(Roles = "Administrator,Manager,Representative,SubManager,Follower")]
        public IActionResult Index(Guid? ulkeId, Guid? okulId, int ay)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.OkulAdi = string.Empty;

            if (ulkeId == null)
            {
                if (User.IsInRole(EnumsKullaniciRolleri.Administrator.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Manager.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Follower.ToString()))
                {
                    var requestmodel = _okullarBE.OkullariGetir();

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
            else
            {
                if (okulId == null)
                {
                    if (User.IsInRole(EnumsKullaniciRolleri.Administrator.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Manager.ToString()) || User.IsInRole(EnumsKullaniciRolleri.Follower.ToString()))
                    {

                        var requestmodel = _okullarBE.OkulGetirUlkeId((Guid)ulkeId);

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
                else
                {
                    if (ay > 0)
                    {
                        var requestmodel = _okullarBE.OkulGetirOkulIdSecilenAy((Guid)okulId, ay);

                        if (requestmodel.IsSuccess)
                        {
                            return View(requestmodel.Data);
                        }
                        return View(user);
                    }
                    else
                    {
                        var requestmodel = _okullarBE.OkulGetirOkulId((Guid)okulId);

                        if (requestmodel.IsSuccess)
                        {
                            return View(requestmodel.Data);
                        }
                        return View(user);
                    }

                }
            }

        }
        #endregion

        #region OkulEkleGet
        [Authorize(Roles = "Administrator,Manager")]
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
        [Authorize(Roles = "Administrator,Manager")]
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
        [Authorize(Roles = "Administrator,Manager")]
        [Route("Okullar/OC10003", Name = "OkulGuncelle")]
        public async Task<ActionResult> Guncelle(Guid? id, Guid UlkeId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.UlkeAdi = _ulkelerBE.UlkeleriGetir().Data;
            ViewBag.Eyaletler = _eyaletlerBE.EyaletleriGetirUlkeId((Guid)UlkeId).Data;
            ViewBag.Sehirler = _sehirlerBE.SehirleriGetirUlkeId((Guid)UlkeId).Data;
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
        [Authorize(Roles = "Administrator,Manager")]
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
        public IActionResult OkulDetayGuncelle(Guid? id, Guid UlkeId)
        {
            ViewBag.Eyaletler = _eyaletlerBE.EyaletleriGetirUlkeId((Guid) UlkeId).Data;
            
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
        public async Task<IActionResult> OkulDetayGuncelle(OkullarVM model, Guid? OkulId)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (model.FotoGaleris != null)
            {
                string fotoklasorler = "img/Okullar/";
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

            if (OkulId != null)
            {
                var okullogoAdi = "/img/OkulLogo/noimages.jpg";
                //önceki yüklenen fotoyu dosyadan sil
                var okullogurl = _okullarBE.OkulLogoURLGetir((Guid)OkulId).Data;
                if (okullogurl != null)
                {
                    string[] parcalar = okullogurl.ToString().Split("/img/OkulLogo/");
                    okullogoAdi = parcalar[1].ToString();
                }


                if (model.OkulLogo != null)
                {

                    if (okullogoAdi != "/img/OkulLogo/noimages.jpg")
                    {
                        System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/OkulLogo/" + okullogoAdi);
                    }

                    string klasorler = "img/OkulLogo/";
                    model.OkulLogoURL = await FotoYukle(klasorler, model.OkulLogo);
                    //string[] parcala = model.OkulLogoURL.ToString().Split("/img/OkulLogo/");                    

                }
                else
                {
                    model.OkulLogoURL = okullogurl;
                }

                var data = _okullarBE.OkulDetayGuncelle(model, user);

                if (data.IsSuccess)
                {
                    return RedirectToAction("OkulDetayGuncelle",new { id=(Guid)OkulId });
                }
            }
            return View();
        }
        #endregion

        #region EyaletEkleJson
        [Authorize(Roles = "SubManager")]
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

        #region SehirEkleJson
        [Authorize(Roles = "SubManager")]
        [HttpPost]
        public JsonResult SehirEkleJson(SehirlerVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _sehirlerBE.SehirEkle(model, user);
            if (data.IsSuccess)
            {
                return Json("200");
            }
            return Json("Eklenecek veri bulunamadı !");
        }
        #endregion

        #region OkulBolumEkleJson
        [Authorize(Roles = "SubManager")]
        [HttpPost]
        public JsonResult OkulBolumEkleJson(OkulBinaBolumVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _okulBinaBolumBE.OkulBinaBolumEkle(model, user);
            if (data.IsSuccess)
            {
                return Json("200");
            }
            return Json("Eklenecek veri bulunamadı !");
        }
        #endregion

        #region OkulEyaletGetir
        [Authorize(Roles = "Administrator,Manager,SubManager")]
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

        #region OkulSehirGetir
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        public IActionResult OkulSehirGetir(string SehirAdi)
        {
            if (SehirAdi != null)
            {
                var data = _unitOfWork.sehirlerRepository.GetAll(x => x.SehirAdi == SehirAdi);
                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region OkulFotoSil
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        [Obsolete]
        public IActionResult OkulFotoSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var okulfotourl = _fotoGaleriBE.FotoURLGetir((Guid)id);
            string[] parcalar = okulfotourl.Data.ToString().Split("/img/Okullar/");
            var okulFotoAdi = parcalar[1].ToString();

            if (okulFotoAdi != null)
            {
                System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/img/Okullar/" + okulFotoAdi);
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

            using FileStream fs = new FileStream(dosyaKlasor, FileMode.Create);
            await dosya.CopyToAsync(fs);
            return "/" + dosyaAdi;
        }
        #endregion

        #region OkulBinaBolumSil 
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        [HttpDelete]
        public IActionResult OkulBinaBolumSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _okulBinaBolumBE.OkulBinaBolumSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

        #region SehirAdGetir
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        public IActionResult SehirAdGetir(Guid eyaletId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            if (eyaletId != null)
            {
                var data = _unitOfWork.sehirlerRepository.GetAll(x => x.EyaletId == eyaletId);

                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region SubeSinifOgrenciGet
        [Authorize(Roles = "SubManager")]
        [HttpGet]
        [Route("Okullar/OC10006", Name = "OkulSubeSinifOgrenci")]
        public IActionResult SubeSinifOgrenci(Guid? id)
        {
            if (id != null)
            {
                var data = _okullarBE.OkulGetir((Guid)id);
                ViewBag.SinifAdi = _siniflarBE.SiniflariGetirOkulId((Guid)id).Data;
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region SubeEkleJson
        [Authorize(Roles = "SubManager")]
        [HttpPost]
        public JsonResult SubeEkleJson(SubelerVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _subelerBE.SubeEkle(model, user);
            if (data.IsSuccess)
            {
                return Json("200");
            }
            return Json("Eklenecek veri bulunamadı !");

        }
        #endregion

        #region SubeGuncelleJson
        [Authorize(Roles = "SubManager")]
        [HttpPost]
        public JsonResult SubeGuncelleJson(SubelerVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _subelerBE.SubeGuncelle(model, user);
            if (data.IsSuccess)
            {
                return Json("200");
            }
            return Json("Eklenecek veri bulunamadı !");

        }
        #endregion

        #region OkulSubeGetir
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        public IActionResult OkulSubeGetir(Guid SubeId)
        {

            if (SubeId != null)
            {
                var data = _unitOfWork.subelerRepository.GetAll(x => x.SubeId == SubeId);
                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region OkulSubeleriGetir
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        public IActionResult OkulSubeleriGetir(Guid SubeId)
        {

            if (SubeId != null)
            {
                var data = _unitOfWork.subelerRepository.GetAll(x => x.SubeId == SubeId);
                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region OkulSubeSil
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        [HttpDelete]
        public IActionResult OkulSubeSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _subelerBE.SubeSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

        #region SinifEkleJson
        [Authorize(Roles = "SubManager")]
        [HttpPost]
        public JsonResult SinifEkleJson(SiniflarVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _siniflarBE.SinifEkle(model, user);
            if (data.IsSuccess)
            {
                return Json("200");
            }
            return Json("Eklenecek veri bulunamadı !");

        }
        #endregion

        #region SinifGuncelleJson
        [Authorize(Roles = "SubManager")]
        [HttpPost]
        public JsonResult SinifGuncelleJson(SiniflarVM model)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            var data = _siniflarBE.SinifGuncelle(model, user);
            if (data.IsSuccess)
            {
                return Json("200");
            }
            return Json("Eklenecek veri bulunamadı !");

        }
        #endregion

        #region OkulSinifGetir
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        public IActionResult OkulSinifGetir(Guid SinifId)
        {

            if (SinifId != null)
            {
                var data = _unitOfWork.siniflarRepository.GetAll(x => x.SinifId == SinifId);
                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region OkulSiniflariGetir
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        public IActionResult OkulSiniflariGetir(Guid OkulId)
        {

            if (OkulId != null)
            {
                var data = _unitOfWork.siniflarRepository.GetAll(x => x.OkulId == OkulId);
                return Json(data);
            }

            return NotFound();
        }
        #endregion

        #region OkulSinifSil
        [Authorize(Roles = "Administrator,Manager,SubManager")]
        [HttpDelete]
        public IActionResult OkulSinifSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _siniflarBE.SinifSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
        #endregion

    }
}
