using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Data.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayBasvuruBilgileriViewComponent:ViewComponent
    {
        private readonly IAdaylarBE _adaylarBE;

        public AdayBasvuruBilgileriViewComponent(IAdaylarBE adaylarBE)
        {
            adaylarBE = _adaylarBE;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            return View();
        }
    }
}
