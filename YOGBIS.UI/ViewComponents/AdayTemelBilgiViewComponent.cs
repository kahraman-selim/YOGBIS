using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayTemelBilgiViewComponent : ViewComponent
    {
        private readonly IAdaylarBE _adaylarBE;

        public AdayTemelBilgiViewComponent(IAdaylarBE adaylarBE)
        {
            _adaylarBE = adaylarBE;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var requestmodel = _adaylarBE.AdayTemelBilgileriGetir();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }

            return View();
        }
    }
}
