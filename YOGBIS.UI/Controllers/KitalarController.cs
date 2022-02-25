using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KitalarController : Controller
    {
        #region Değişkenler
        private readonly IKitalarBE _kitalarBE;
        #endregion

        #region Dönüştürücüler
        public KitalarController(IKitalarBE kitalarBE)
        {
            _kitalarBE = kitalarBE;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region IndexPost
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
        #endregion
    }
}
