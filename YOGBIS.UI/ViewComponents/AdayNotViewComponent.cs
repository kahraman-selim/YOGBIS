using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.Const;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Common.ResultModels;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayNotViewComponent : ViewComponent
    {
        private readonly IAdaylarBE _adaylarBE;
        private readonly IKomisyonlarBE _komisyonlarBE;
        private readonly ILogger<AdayNotViewComponent> _logger;

        public AdayNotViewComponent(IAdaylarBE adaylarBE, IKomisyonlarBE komisyonlarBE, ILogger<AdayNotViewComponent> logger)
        {
            _adaylarBE = adaylarBE;
            _komisyonlarBE = komisyonlarBE;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync(string komisyonKullaniciId, string secilenadaytcno = null, string mulakatId = null)
        {
            //var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            var user = HttpContext.Session.GetString(ResultConstant.LoginUserInfo);
            var komisyon = await _komisyonlarBE.KomisyonGetirByUserId(komisyonKullaniciId);

            var returndata = new AdaySinavNotlarVM();
            var komisyonbilgi = new List<KomisyonUyelerVM>();

            foreach (var item in komisyon)
            {
                komisyonbilgi.Add(
                     new KomisyonUyelerVM
                     {
                         KomisyonId = item.KomisyonId,
                         KomisyonUyeAdSoyad = item.KomisyonUyeAdiSoyadi,
                         KomisyonUyeSiraId = item.KomisyonUyeSiraNo, 
                         MulakatId= mulakatId,
                         SecilenAdayTCNo= secilenadaytcno
                     });
            }
            returndata.KomisyonKullaniciId = komisyonKullaniciId;
            returndata.komisyonUyelerVm = komisyonbilgi;

            // MYYS puanını al
            if (!string.IsNullOrEmpty(secilenadaytcno) && !string.IsNullOrEmpty(mulakatId))
            {
                _logger.LogInformation($"AdayNot - TC: {secilenadaytcno}, MulakatId: {mulakatId}");
               
                var result = _adaylarBE.GetirAdayBasvuruBilgileriByTcAndMulakatId(secilenadaytcno, Guid.Parse(mulakatId));
                _logger.LogInformation($"AdayNot - Result: {result?.IsSuccess}, Data: {result?.Data != null}");
                
                if (result?.Data != null)
                {
                    _logger.LogInformation($"AdayNot - MYYSPuan: {result.Data.MYYSPuan}");
                    var trCulture = CultureInfo.GetCultureInfo("tr-TR");
                    if (decimal.TryParse(result.Data.MYYSPuan, out decimal puan))
                    {
                        returndata.AdayMYYSPuan = puan > 0 ? 
                            puan.ToString("N2", trCulture) : 
                            string.Empty;
                    }
                    else
                    {
                        _logger.LogWarning($"AdayNot - MYYSPuan parse edilemedi: {result.Data.MYYSPuan}");
                    }
                }
            }
            
            return View(returndata);
        }
    }
}
