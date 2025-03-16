using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
