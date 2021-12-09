using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KullanicilarController : Controller
    {

        private readonly IKullaniciBE _kullaniciBE;
        private readonly UserManager<Kullanici> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public KullanicilarController(IKullaniciBE kullaniciBE, UserManager<Kullanici> userManager, RoleManager<IdentityRole> roleManager)
        {
            _kullaniciBE = kullaniciBE;
            _userManager = userManager;
            _roleManager = roleManager;
        }


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

        [HttpGet]
        public ActionResult KullaniciGuncelle(int id)
        {
            if (id < 0)
                return View();
            var data = _kullaniciBE.KullaniciGetir(id);
            if (data.IsSuccess)
                return View(data.Data);
            return View();
        }

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

    }
}
