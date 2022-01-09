using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Index(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            var kullanici = await _kullaniciBE.OnayKullaniciGetir();
            ViewBag.Onaylayan = kullanici.Data;

            if (id > 0)
            {
                var data = _soruBankasiBE.SoruGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }

        }
        #endregion

        [HttpGet]
        #region Ekle
        public IActionResult SoruEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            return View();
        } 
        #endregion

        [ValidateAntiForgeryToken]
        [HttpPost]
        #region Ekle
        public IActionResult SoruEkle(SoruBankasiVM model, int? SoruBankasiId)
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            if (SoruBankasiId > 0)
            {
                var data = _soruBankasiBE.SoruGuncelle(model, user);
                return RedirectToAction("Index");
            }
            else
            {
                var data = _soruBankasiBE.SoruEkle(model, user);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        #endregion

        #region Güncelle
        public async Task<IActionResult> Guncelle(int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            if (id > 0)
            {
                var data = _soruBankasiBE.SoruGetir((int)id);
                return View(data.Data);
            }
            else
            {
                return View();
            }
        } 
        #endregion

        [HttpDelete]
        #region Sil
        public IActionResult SoruSil(int id)
        {
            if (id < 0)
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
