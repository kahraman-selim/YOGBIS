using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class UlkeListesiViewComponent : ViewComponent
    {
        private readonly IUlkelerBE _ulkelerBE;
        public UlkeListesiViewComponent(IUlkelerBE ulkelerBE)
        {
            _ulkelerBE = ulkelerBE;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var requestmodel = _ulkelerBE.UlkeleriGetirViewComponent();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
