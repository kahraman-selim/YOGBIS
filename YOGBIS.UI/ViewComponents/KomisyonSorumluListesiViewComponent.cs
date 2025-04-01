using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class KomisyonSorumluListesiViewComponent : ViewComponent
    {
        private readonly IKomisyonlarBE _komisyonlarBE;

        public KomisyonSorumluListesiViewComponent(IKomisyonlarBE komisyonlarBE)
        {
            _komisyonlarBE = komisyonlarBE;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var komisyonPersonellerResult = _komisyonlarBE.KomisyonPersonelleriGetir();
                if (!komisyonPersonellerResult.IsSuccess)
                {
                    ViewBag.ErrorMessage = komisyonPersonellerResult.Message;
                    return View();
                }

                return View(komisyonPersonellerResult.Data);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Komisyon personelleri listesi yüklenirken bir hata oluştu: {ex.Message}";
                return View();
            }
        }
    }
}
