using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class OkulBinaBolumListeViewComponent : ViewComponent
    {
        private readonly IOkulBinaBolumBE _okulbinabolumBE;
        public OkulBinaBolumListeViewComponent(IOkulBinaBolumBE okulbinabolumBE)
        {
            _okulbinabolumBE = okulbinabolumBE;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid OkulId)
        {
            var requestmodel = _okulbinabolumBE.OkulBinaBolumGetirOkulId(OkulId);
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
