using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.VModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;
using System.Runtime.InteropServices;
using YOGBIS.Common.ResultModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KomisyonTanimlamaController : Controller
    {
        #region Değişkenler
        private readonly UserManager<Kullanici> _userManager;
        private readonly ILogger<KomisyonTanimlamaController> _logger;
        private readonly IKomisyonlarBE _komisyonlarBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Dönüştücüler
        public KomisyonTanimlamaController(
            UserManager<Kullanici> userManager,
            ILogger<KomisyonTanimlamaController> logger,
            IKomisyonlarBE komisyonlarBE,
            IKullaniciBE kullaniciBE,
            IMulakatOlusturBE mulakatOlusturBE,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _logger = logger;
            _komisyonlarBE = komisyonlarBE;
            _kullaniciBE = kullaniciBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Index
        [Route("KT10001", Name = "KomisyonPersonelTanimlamaIndexRoute")]
        public async Task<IActionResult> Index(Guid? id)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
                
                var personel = await _kullaniciBE.KomisyonSorumluGetir();
                ViewBag.KomisyonPersoneller = personel.Data;

                var komisyon = await _kullaniciBE.KomisyonGetir();
                ViewBag.Komisyonlar = komisyon.Data;  

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Index sayfası görüntülenirken hata oluştu: {ex.Message}");
                TempData["ErrorMessage"] = "Bir hata oluştu. Lütfen tekrar deneyiniz.";
                return View();
            }
        }
        #endregion
       
    }
}
