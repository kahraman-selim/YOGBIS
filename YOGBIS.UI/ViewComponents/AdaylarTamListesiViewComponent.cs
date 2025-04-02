using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class AdaylarTamListesiViewComponent : ViewComponent
    {

        private readonly IAdaylarBE _adaylarBE;

        public AdaylarTamListesiViewComponent(IAdaylarBE adaylarBE)
        {
            _adaylarBE = adaylarBE;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var requestmodel = _adaylarBE.MYSSAdaylariGetir();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }  
    }
}
