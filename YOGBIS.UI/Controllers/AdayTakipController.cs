using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles ="Administrator,Follower")]
    public class AdayTakipController : Controller
    {
        #region Değişkenler     
        private readonly IAdaylarBE _adaylarBE;
        #endregion

        #region Dönüştürücüler
        public AdayTakipController(IAdaylarBE adaylarBE)
        {
            _adaylarBE = adaylarBE;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var requestmodel = _adaylarBE.AdayTakipMulakatListesi();
            return View(requestmodel.Data ?? new List<AdayMYSSVM>());
        }
        #endregion

    }
}
