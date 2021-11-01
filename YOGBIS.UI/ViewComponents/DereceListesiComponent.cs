using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class DereceListesiComponent : ViewComponent
    {
        private readonly IDerecelerBE _derecelerBE;

        public DereceListesiComponent(IDerecelerBE derecelerBE)
        {
            _derecelerBE = derecelerBE;
        }
        public IViewComponentResult Invoke()
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
