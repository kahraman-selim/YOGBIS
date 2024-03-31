using Microsoft.AspNetCore.Authorization;
#pragma warning disable IDE0005 // Using directive is unnecessary.
using Microsoft.AspNetCore.Http;
#pragma warning restore IDE0005 // Using directive is unnecessary.
using Microsoft.AspNetCore.Mvc;
#pragma warning disable IDE0005 // Using directive is unnecessary.
using Newtonsoft.Json;
using System;
#pragma warning restore IDE0005 // Using directive is unnecessary.
using YOGBIS.BusinessEngine.Contracts;
#pragma warning disable IDE0005 // Using directive is unnecessary.
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
#pragma warning restore IDE0005 // Using directive is unnecessary.

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator,CommissionerHead")]
    public class MulakatController : Controller
    {

        private readonly IMulakatSorulariBE _mulakatSorulariBE;
        
        public MulakatController(IMulakatSorulariBE mulakatSorulariBE)
        {
            _mulakatSorulariBE = mulakatSorulariBE;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult Index(int id, string derece)
        //{
        //    var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
        //    var sorugetirmodel = _mulakatBE.GetAllMulakatSorulariById(id, derece);
        //    if (sorugetirmodel.IsSuccess)
        //    {
        //        return View(sorugetirmodel);
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult MulakatSoruGetir(int id,string derece)
        //{
        //    #region MyRegion
        //    var sorugetirmodel = _mulakatSorulariBE.GetAllMulakatSorulariById(id, derece);
        //    if (sorugetirmodel.IsSuccess)
        //    {
        //        return View(sorugetirmodel);
        //    }
        //    return View();

        //    #endregion

        //}

    }
}
