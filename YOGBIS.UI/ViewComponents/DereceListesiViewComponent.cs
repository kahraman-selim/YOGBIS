using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
           
            var requestmodel = _derecelerBE.DereceleriGetir();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
