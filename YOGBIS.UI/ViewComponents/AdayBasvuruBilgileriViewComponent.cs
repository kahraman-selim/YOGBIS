using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayBasvuruBilgileriViewComponent : ViewComponent
    {
        private readonly IAdaylarBE _adaylarBE;

        public AdayBasvuruBilgileriViewComponent(IAdaylarBE adaylarBE)
        {
            _adaylarBE = adaylarBE;
        }

        public IViewComponentResult Invoke(List<AdayBasvuruBilgileriVM> model)
        {
            return View(model);
        }
    }
}
