using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TemsilciliklerController : Controller
    {
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        private readonly IDerecelerBE _derecelerBE;

        public TemsilciliklerController(ISoruKategorileriBE soruKategorileriBE, IDerecelerBE derecelerBE)
        {
            _soruKategorileriBE = soruKategorileriBE;
            _derecelerBE = derecelerBE;
        }
        public IActionResult Index()
        {
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = _soruKategorileriBE.SoruKategorileriGetir().Data;

            return View();
        }
    }
}
