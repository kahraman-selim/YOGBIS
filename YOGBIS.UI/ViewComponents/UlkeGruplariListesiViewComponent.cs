using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class UlkeGruplariListesiViewComponent:ViewComponent
    {
        private readonly IUlkeGruplariBE _ulkeGruplariBE;
        public UlkeGruplariListesiViewComponent(IUlkeGruplariBE ulkeGruplariBE)
        {
            _ulkeGruplariBE = ulkeGruplariBE;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var requestmodel = _ulkeGruplariBE.UlkeGruplariGetir();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
