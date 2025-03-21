using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using System.Security.Claims;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayMulakatListeViewComponent : ViewComponent
    {
        private readonly IAdaylarBE _adaylarBE;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdayMulakatListeViewComponent(IAdaylarBE adaylarBE, IHttpContextAccessor httpContextAccessor)
        {
            _adaylarBE = adaylarBE;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            var mulakatTarihi = "15.04.2024";
            
            if (!string.IsNullOrEmpty(userName) && userName == "Komisyon1")
            {
                var result = _adaylarBE.GetirKomisyonMulakatListesi("Komisyon-1", mulakatTarihi);
                if (result.IsSuccess)
                {
                    return View(result.Data);
                }
            }
            
            return View();
        }
    }
}
