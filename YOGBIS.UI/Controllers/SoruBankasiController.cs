using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator,Manager,Follower")]
    public class SoruBankasiController : Controller
    {

        #region Değişkenler
        private readonly ISoruBankasiBE _soruBankasiBE;
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IDerecelerBE _derecelerBE;
        private readonly IKullaniciBE _kullaniciBE;
        #endregion

        #region Dönüştürücüler
        public SoruBankasiController(ISoruBankasiBE soruBankasiBE, ISoruKategorileriBE soruKategorileriBE, IDerecelerBE derecelerBE, IKullaniciBE kullaniciBE)
        {
            _soruBankasiBE = soruBankasiBE;
            _soruKategorileriBE = soruKategorileriBE;
            _derecelerBE = derecelerBE;
            _kullaniciBE = kullaniciBE;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            var kullanici = await _kullaniciBE.OnayKullaniciGetir();
            ViewBag.Onaylayan = kullanici.Data;

            if (id != null)
            {
                var data = _soruBankasiBE.SoruGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region SoruEkleGet
        [HttpGet]
        public IActionResult SoruEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            return View();
        }
        #endregion

        #region SoruEklePost
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SoruEkle(SoruBankasiVM model, Guid? SoruBankasiId, string[] OnaylayanId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            if (SoruBankasiId != null)
            {
                var data = _soruBankasiBE.SoruGuncelle(model, user);
                return RedirectToAction("Index");
            }
            else
            {
                ////List<SoruOnayVM> soruOnayVMs = new List<SoruOnayVM>();
                ////foreach (var item in OnaylayanId)
                ////{
                //    var soruonay = new SoruOnayVM()
                //    {
                //        KayitTarihi = model.KayitTarihi,
                //        OnayAciklama = model.OnayDurumuAciklama,
                //        OnayDurumu = (int)EnumsSoruOnay.Onaya_Gonderildi,
                //        OnaylayanId = item
                //    };
                //    soruOnayVMs.Add(soruonay);
                ////}

                var data = _soruBankasiBE.SoruEkle(model, user, OnaylayanId);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        #endregion

        #region Güncelle
        public IActionResult Guncelle(Guid? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            if (id != null)
            {
                var data = _soruBankasiBE.SoruGetir((Guid)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region Sil
        [HttpDelete]
        public IActionResult SoruSil(Guid id)
        {
            if (id == null)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _soruBankasiBE.SoruSil(id);
            if (data.IsSuccess)
                return Json(new { success = data.IsSuccess, message = data.Message });
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        } 
        #endregion

    }
}
