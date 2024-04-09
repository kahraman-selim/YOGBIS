using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class KomisyonListesiViewComponent : ViewComponent
    {
        private readonly IKomisyonlarBE _komisyonlarBE;

        public KomisyonListesiViewComponent(IKomisyonlarBE komisyonlarBE)
        {
            _komisyonlarBE = komisyonlarBE;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var requestmodel = _komisyonlarBE.KomisyonlariGetir();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
