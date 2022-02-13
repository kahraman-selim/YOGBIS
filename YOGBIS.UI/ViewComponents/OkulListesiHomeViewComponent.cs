using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class OkulListesiHomeViewComponent : ViewComponent
    {
        private readonly IOkullarBE _okullarBE;
        public OkulListesiHomeViewComponent(IOkullarBE okullarBE)
        {
            _okullarBE = okullarBE;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var requestmodel = _okullarBE.OkullariGetirAZ();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
