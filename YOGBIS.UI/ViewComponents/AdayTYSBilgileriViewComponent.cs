using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayTYSBilgileriViewComponent : ViewComponent
    {
        private readonly IAdaylarBE _adaylarBE;

        public AdayTYSBilgileriViewComponent(IAdaylarBE adaylarBE)
        {
            _adaylarBE = adaylarBE;
        }

        public IViewComponentResult Invoke(List<AdayTYSVM> model)
        {
            return View(model);
        }
    }
}
