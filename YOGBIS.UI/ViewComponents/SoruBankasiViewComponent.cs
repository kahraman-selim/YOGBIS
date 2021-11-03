using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class SoruBankasiViewComponent : ViewComponent
    {
        private readonly ISoruBankasiBE _soruBankasiBE;

        public SoruBankasiViewComponent(ISoruBankasiBE soruBankasiBE)
        {
            _soruBankasiBE = soruBankasiBE;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var requestmodel = _soruBankasiBE.SorulariGetir();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
