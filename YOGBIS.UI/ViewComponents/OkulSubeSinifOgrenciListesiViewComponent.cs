using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{   

    public class OkulSubeSinifOgrenciListesiViewComponent : ViewComponent
    {
        private readonly IOkullarBE _okullarBE;
        public OkulSubeSinifOgrenciListesiViewComponent(IOkullarBE okullarBE)
        {
            _okullarBE = okullarBE;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid OkulId)
        {
            var requestmodel = _okullarBE.SubeSinifOgrenciGetirOkulId(OkulId);
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
