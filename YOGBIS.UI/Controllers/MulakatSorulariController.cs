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
    public class MulakatSorulariController : Controller
    {
       
        private readonly IMulakatSorulariBE _mulakatSorulariBE;
        public MulakatSorulariController(IMulakatSorulariBE mulakatSorulariBE)
        {
            _mulakatSorulariBE = mulakatSorulariBE;
        }        
        
        public IActionResult Index()
        {
            var data = _mulakatSorulariBE.GetAllMulakatSorulari();
            if (data.IsSuccess)
            {
                var result = data.Data;
                return View(result);
            }
            return View();
        }
        public IActionResult MulakatSoruEkle()
        {
            return View();
        }        
        [HttpPost]
        public IActionResult MulakatSoruEkle(MulakatSorulariVM model)
        {
            #region Ekleme ve Güncelleme Örneği
            //if (model.MulakatSorulariId>0)
            //{
            //    var data = _mulakatSorulariBE.MulakatSorusuGuncelle(model);
            //}
            //else
            //{
            //    if (ModelState.IsValid)
            //    {
            //        var data = _mulakatSorulariBE.MulakatSorusuEkle(model);
            //        if (data.IsSuccess)
            //        {
            //            return RedirectToAction("Index");
            //        }
            //        return View(model);
            //    }
            //    else
            //    {
            //        return View(model);
            //    }
            //} 
            #endregion

            if (ModelState.IsValid)
            {
                var data = _mulakatSorulariBE.MulakatSorusuEkle(model);
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
        
        [HttpGet]
        public IActionResult MulakatSoruGuncelle(int id)
        {
            if (id < 0)
                return View();
            var data = _mulakatSorulariBE.GetAllMulakatSorulari(id);
            if (data.IsSuccess)
                return View(data.Data);
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult MulakatSoruGuncelle(MulakatSorulariVM model)
        {
            if (ModelState.IsValid)
            {
                var data = _mulakatSorulariBE.MulakatSorusuGuncelle(model);
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

        
        [HttpDelete]
        public IActionResult MulakatSoruSil(int id, MulakatSorulariVM model)
        {
            if (id < 0)
                return RedirectToAction("Index", "MulakatSorulari");
            var data = _mulakatSorulariBE.GetAllMulakatSorulari(id);
            if (data.IsSuccess)
            {
                if (ModelState.IsValid)
                {
                    var datasil = _mulakatSorulariBE.MulakatSorusuSil(model);
                    if (datasil.IsSuccess)
                    {
                        return RedirectToAction("Index","MulakatSorulari");
                    }
                    return View(model);
                }
                else
                {
                    return View(model);
                }
            }

            return RedirectToAction("Index", "MulakatSorulari");
        }

        //[HttpDelete]
        //public IActionResult MulakatSoruSil(MulakatSorulariVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var data = _mulakatSorulariBE.MulakatSorusuSil(model);
        //        if (data.IsSuccess)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        return View(model);
        //    }
        //    else
        //    {
        //        return View(model); 
        //    }
        //}
    }
}
