using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.Const;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayNotViewComponent : ViewComponent
    {
        private readonly IAdaylarBE _adaylarBE;
        private readonly IKomisyonlarBE _komisyonlarBE;

        public AdayNotViewComponent(IAdaylarBE adaylarBE, IKomisyonlarBE komisyonlarBE)
        {
            _adaylarBE = adaylarBE;
            _komisyonlarBE = komisyonlarBE;
        }

        public async Task<IViewComponentResult> InvokeAsync(string komisyonKullaniciId,string? secilenadaytcno, string? mulakatId)
        {
            //var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            var user = HttpContext.Session.GetString(ResultConstant.LoginUserInfo);
            var komisyon = await _komisyonlarBE.KomisyonGetirByUserId(komisyonKullaniciId);

            var returndata = new AdaySinavNotlarVM();
            var komisyonbilgi= new List<KomisyonUyelerVM>();

            foreach (var item in komisyon)
            {
                komisyonbilgi.Add(

                     new KomisyonUyelerVM
                     {
                         KomisyonId = item.KomisyonId,
                         KomisyonUyeAdSoyad = item.KomisyonUyeAdiSoyadi,
                         KomisyonUyeSiraId = item.KomisyonUyeSiraNo
                     });
                  
            }
            
            returndata.komisyonUyelerVm = komisyonbilgi;
            
            return View(returndata);
        }
    }
}
