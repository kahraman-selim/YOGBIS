using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{   

    public class OkulSinifListesiViewComponent : ViewComponent
    {
        private readonly ISiniflarBE _siniflarBE;
        public OkulSinifListesiViewComponent(ISiniflarBE siniflarBE)
        {
            _siniflarBE = siniflarBE;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid OkulId)
        {
            var requestmodel = _siniflarBE.SiniflariGetirOkulId(OkulId);
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
