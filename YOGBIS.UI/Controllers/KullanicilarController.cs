using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KullanicilarController : Controller
    {

        #region Değişkenler
        private readonly IKullaniciBE _kullaniciBE;
        private readonly UserManager<Kullanici> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Dönüştürücüler
        public KullanicilarController(IKullaniciBE kullaniciBE, UserManager<Kullanici> userManager, RoleManager<IdentityRole> roleManager)
        {
            _kullaniciBE = kullaniciBE;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var data = _kullaniciBE.KullaniciGetir();
            if (data.IsSuccess)
            {
                var result = data.Data;
                return View(result);
            }
            return View();
        }
        #endregion

        #region KullaniciGuncelleGet
        //[HttpGet]
        //public ActionResult KullaniciGuncelle(string id)
        //{
        //    if (id != null)
        //        return View();
        //    var data = _kullaniciBE.KullaniciGetir(id);
        //    if (data.IsSuccess)
        //        return View(data.Data);
        //    return View();
        //}
        #endregion

        #region KullaniciGuncellePost
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult KullaniciGuncelle(KullaniciVM model)
        {
            if (ModelState.IsValid)
            {
                var data = _kullaniciBE.KullaniciGuncelle(model);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region KullaniciDurum
        public IActionResult Durum(string id)
        {

            var user = _userManager.FindByIdAsync(id);

            if (user.Result.Aktif == true)
                user.Result.Aktif = false;
            else
                user.Result.Aktif = true;

            _userManager.UpdateAsync(user.Result);

            return RedirectToAction("Index");

        } 
        #endregion

    }
}
