using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    public class KitalarController : Controller
    {
        private readonly IKitalarBE _kitalarBE;

        public KitalarController(IKitalarBE kitalarBE)
        {
            _kitalarBE = kitalarBE;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(KitalarVM model)
        {
            var data = _kitalarBE.KitaEkle(model);
            if (data.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(model);        
        }
    }
}
