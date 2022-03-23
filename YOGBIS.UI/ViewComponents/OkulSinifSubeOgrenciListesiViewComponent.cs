using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{   

    public class OkulSinifSubeOgrenciListesiViewComponent : ViewComponent
    {
        private readonly ISubelerBE _subelerBE;
        public OkulSinifSubeOgrenciListesiViewComponent(ISubelerBE subelerBE)
        {
            _subelerBE = subelerBE;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid OkulId)
        {
            var requestmodel = _subelerBE.SubeleriGetirOkulId(OkulId);
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
