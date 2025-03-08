using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayMYSSBilgileriViewComponent : ViewComponent
    {
        private readonly IAdaylarBE _adaylarBE;

        public AdayMYSSBilgileriViewComponent(IAdaylarBE adaylarBE)
        {
            _adaylarBE = adaylarBE;
        }

        public IViewComponentResult Invoke(List<AdayMYSSVM> model)
        {
            return View(model);
        }
    }
}