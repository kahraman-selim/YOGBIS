using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class UlkeListesiViewComponent : ViewComponent
    {
        private readonly IUlkelerBE _ulkelerBE;
        public UlkeListesiViewComponent(IUlkelerBE ulkelerBE)
        {
            _ulkelerBE = ulkelerBE;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var requestmodel = _ulkelerBE.UlkeleriGetir();
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
