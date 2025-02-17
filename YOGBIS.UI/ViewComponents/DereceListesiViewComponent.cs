using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class DereceListesiViewComponent : ViewComponent
    {
        private readonly IDerecelerBE _derecelerBE;

        public DereceListesiViewComponent(IDerecelerBE derecelerBE)
        {
            _derecelerBE = derecelerBE;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            try
            {
                var requestmodel = _derecelerBE.DereceleriGetir();
                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }
                else
                {
                    TempData["ErrorMessage"] = requestmodel.Message;
                    return View();
                }
                    
            }
            catch (System.Exception)
            {

                TempData["ErrorMessage"] = "Dereceler getirilirken bir hata oluştu.";
                return View();
            }

        }
    }
}
