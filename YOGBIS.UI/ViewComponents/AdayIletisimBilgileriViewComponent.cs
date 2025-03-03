using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayIletisimBilgileriViewComponent:ViewComponent
    {
        private readonly IAdaylarBE _adaylarBE;

        public AdayIletisimBilgileriViewComponent(IAdaylarBE adaylarBE)
        {
            _adaylarBE = adaylarBE;
        }

        public IViewComponentResult Invoke(List<AdayIletisimBilgileriVM> model)
        {
            return View(model);
        }
    }
}
