using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using YOGBIS.BusinessEngine.Contracts;

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
            if (!mulakatId.HasValue || !dereceId.HasValue || sorusirano <= 0)
            {
                return View(null);
            }
            var requestmodel = _mulakatSorulariBE.MulakatAdaySoruGetir(sorusirano, mulakatId, dereceId);
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View(null);
        }
    }   
}
