using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayBilgiViewComponent : ViewComponent
    {
        private readonly IAdaylarBE _adaylarBE;

        public AdayBilgiViewComponent(IAdaylarBE adaylarBE)
        {
            _adaylarBE = adaylarBE;
        }

        public IViewComponentResult Invoke(AdayBasvuruBilgileriVM model)
        {
            return View(model);
        }
    }
}
