using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.ViewComponents
{
    public class KomisyonListesiViewComponent : ViewComponent
    {
        private readonly IKomisyonlarBE _komisyonlarBE;

        public KomisyonListesiViewComponent(IKomisyonlarBE komisyonlarBE)
        {
            _komisyonlarBE = komisyonlarBE;
        }

        public IViewComponentResult Invoke(Guid? mulakatId = null)
        {
            if (!mulakatId.HasValue)
            {
                return View(null);
            }

            var requestmodel = _komisyonlarBE.KomisyonlariGetir(mulakatId.Value.ToString());
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View(null);
        }
    }
}
