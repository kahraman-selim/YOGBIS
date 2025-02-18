using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;

namespace YOGBIS.UI.ViewComponents
{
    public class UlkeTercihleriViewComponent : ViewComponent
    {
        private readonly IUlkeTercihleriBE _ulkeTercihleriBE;
        
        public UlkeTercihleriViewComponent(IUlkeTercihleriBE ulkeTercihleriBE)
        {
            _ulkeTercihleriBE = ulkeTercihleriBE;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var requestmodel = _ulkeTercihleriBE.UlkeTercihleriGetir();

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

                TempData["ErrorMessage"] = "Bilgiler getirilirken bir hata oluştu.";
                return View();
            }
        }
    }
}
