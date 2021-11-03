using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class SoruKategoriListesiViewComponent : ViewComponent
    {
        private readonly ISoruKategorileriBE _soruKategorileriBE;

        public SoruKategoriListesiViewComponent(ISoruKategorileriBE soruKategorileriBE)
        {
            _soruKategorileriBE = soruKategorileriBE;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var requestmodel = _soruKategorileriBE.SoruKategorileriGetir();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
