using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class OkulOgrenciListesiViewComponent : ViewComponent
    {
        private readonly IOgrencilerBE _ogrencilerBE;
        public OkulOgrenciListesiViewComponent(IOgrencilerBE ogrencilerBE)
        {
            _ogrencilerBE = ogrencilerBE;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid OkulId)
        {
            var requestmodel = _ogrencilerBE.OgrenciGetirOkulId(OkulId);
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
