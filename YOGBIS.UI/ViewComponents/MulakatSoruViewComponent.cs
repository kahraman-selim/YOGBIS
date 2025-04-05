using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.ViewComponents
{
    public class MulakatSoruViewComponent : ViewComponent
    {
        private readonly IMulakatSorulariBE _mulakatSorulariBE;

        public MulakatSoruViewComponent(IMulakatSorulariBE mulakatSorulariBE)
        {
            _mulakatSorulariBE = mulakatSorulariBE;
        }
        
        public IViewComponentResult Invoke(int sorusirano, Guid? mulakatId, Guid? dereceId)
        {
            if (!mulakatId.HasValue || !dereceId.HasValue)
            {
                return View(null);
            }

            var requestmodel = _mulakatSorulariBE.MulakatAdaySoruGetir(sorusirano, mulakatId, dereceId);
            if (!requestmodel.IsSuccess || requestmodel.Data == null || !requestmodel.Data.Any())
            {
                return View(null);
            }
            
            return View(requestmodel.Data);
        }
    }   
}
