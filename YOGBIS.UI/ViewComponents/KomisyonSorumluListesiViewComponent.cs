using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.VModels;
using YOGBIS.Common.ResultModels;

namespace YOGBIS.UI.ViewComponents
{
    public class KomisyonSorumluListesiViewComponent : ViewComponent
    {
        private readonly IKomisyonlarBE _komisyonlarBE;
        private readonly IKullaniciBE _kullaniciBE;

        public KomisyonSorumluListesiViewComponent(
            IKomisyonlarBE komisyonlarBE,
            IKullaniciBE kullaniciBE)
        {
            _komisyonlarBE = komisyonlarBE;
            _kullaniciBE = kullaniciBE;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var komisyonlarResult = _komisyonlarBE.KomisyonlariGetir();
                if (!komisyonlarResult.Success)
                {
                    return Content($"Hata: {komisyonlarResult.Message}");
                }

                var personellerResult = await _kullaniciBE.KomisyonSorumluGetir();
                if (!personellerResult.Success)
                {
                    return Content($"Hata: {personellerResult.Message}");
                }

                var viewModel = personellerResult.Data.Select(p => new KomisyonSorumluViewModel
                {
                    PersonelId = Guid.Parse(p.Id),
                    PersonelAdSoyad = p.AdSoyad,
                    KomisyonListesi = komisyonlarResult.Data
                        .Where(k => k.IlgiliPersonelId == Guid.Parse(p.Id))
                        .Select(k => k.KomisyonAdi)
                        .ToList()
                }).ToList();

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return Content($"Komisyon listesi yüklenirken bir hata oluştu: {ex.Message}");
            }
        }
    }

    public class KomisyonSorumluViewModel
    {
        public Guid PersonelId { get; set; }
        public string PersonelAdSoyad { get; set; }
        public List<string> KomisyonListesi { get; set; }
    }
}
