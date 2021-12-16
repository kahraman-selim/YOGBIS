using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayListesiViewCopmonent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            //var requestmodel = _derecelerBE.DereceleriGetir();
            //if (requestmodel.IsSuccess)
            //{
            //    return View(requestmodel.Data);
            //}
            return View();
        }
    }
}
