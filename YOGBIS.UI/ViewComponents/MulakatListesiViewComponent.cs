using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class MulakatListesiViewComponent : ViewComponent
    {
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        public MulakatListesiViewComponent(IMulakatOlusturBE mulakatOlusturBE)
        {
            _mulakatOlusturBE = mulakatOlusturBE;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var requestmodel = _mulakatOlusturBE.MulakatlariGetir();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
