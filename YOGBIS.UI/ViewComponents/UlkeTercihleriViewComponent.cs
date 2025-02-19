using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.VModels;

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
                System.Diagnostics.Debug.WriteLine("UlkeTercihleriViewComponent - InvokeAsync başladı");
                var requestmodel = _ulkeTercihleriBE.UlkeTercihleriGetir();

                System.Diagnostics.Debug.WriteLine($"UlkeTercihleriViewComponent - IsSuccess: {requestmodel.IsSuccess}");
                if (requestmodel.IsSuccess)
                {
                    System.Diagnostics.Debug.WriteLine($"UlkeTercihleriViewComponent - Veri sayısı: {requestmodel.Data?.Count ?? 0}");
                    foreach (var item in requestmodel.Data)
                    {
                        System.Diagnostics.Debug.WriteLine($"Ülke: {item.UlkeTercihAdi}, Branş sayısı: {item.TercihBranslar?.Count ?? 0}");
                    }
                    return View(requestmodel.Data);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"UlkeTercihleriViewComponent - Hata mesajı: {requestmodel.Message}");
                    TempData["ErrorMessage"] = requestmodel.Message;
                    return View(new List<UlkeTercihVM>());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UlkeTercihleriViewComponent - Hata: {ex.Message}");
                TempData["ErrorMessage"] = "Bilgiler getirilirken bir hata oluştu.";
                return View(new List<UlkeTercihVM>());
            }
        }
    }
}
