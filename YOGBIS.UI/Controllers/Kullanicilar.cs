using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = ResultConstant.Admin_Role)]
    public class KullanicilarController : Controller
    {

        private readonly IKullaniciBE _kullaniciBE;
        public KullanicilarController(IKullaniciBE kullaniciBE)
        {
            _kullaniciBE = kullaniciBE;
        }


        public IActionResult Index()
        {
            var data = _kullaniciBE.GetAllKullanici();
            if (data.IsSuccess)
            {
                var result = data.Data;
                return View(result);
            }
            return View();
            //if (id < 0)
            //    return View();
            //var data = _mulakatBE.GetAllMulakatSorulari(id);
            //if (data.IsSuccess)
            //    return View(data.Data);
            //return View();

            //return View();
        }

        public IActionResult Durum(string id)
        {

            var data = _kullaniciBE.GetAllKullanici(id);

            if (data.Data.Aktif == true)
                data.Data.Aktif = false;
            else
                data.Data.Aktif = true;


            _kullaniciBE.KullaniciGuncelle(data.Data);
            if (data.IsSuccess)
            {
                return Content("ok");
                //return RedirectToAction(nameof(Index));
            }
            else
            {
                return Content("false");
            }

            //return View(data.Data);
        }

        [HttpGet]
        public ActionResult KullaniciGuncelle(int id)
        {
            if (id < 0)
                return View();
            var data = _kullaniciBE.GetAllKullanici(id);
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
    }
}
