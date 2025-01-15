using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayBilgiViewComponent : ViewComponent
    {

        private readonly IAdaylarBE _adaylarBE;

        public AdayBilgiViewComponent(IAdaylarBE adaylarBE)
        {
            _adaylarBE = adaylarBE;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var requestmodel = _adaylarBE.AdaylariGetir();
           // if (requestmodel.IsSuccess)
           // {
           //     return View(requestmodel.Data);
          //  }
            return View();
        }
    }
}
