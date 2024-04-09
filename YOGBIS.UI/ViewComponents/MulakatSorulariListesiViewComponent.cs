using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class MulakatSorulariListesiViewComponent : ViewComponent
    {
        private readonly IMulakatSorulariBE _mulakarSorulariBE;

        public MulakatSorulariListesiViewComponent(IMulakatSorulariBE mulakarSorulariBE)
        {
            _mulakarSorulariBE = mulakarSorulariBE;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var requestmodel = _mulakarSorulariBE.MulakatSorulariGetir();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
