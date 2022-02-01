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
        public IActionResult Test()
        {
            ViewBag.Dereceler = _derecelerBE.DereceleriGetir().Data;
            ViewBag.Kategoriler = string.Empty;// _soruKategorileriBE.SoruKategorileriGetir().Data;
            return View();
        }

        public JsonResult SoruKategoriGetir(int dereceId)
        {
            if (dereceId < 0)
                return Json(new { success = false, message = "Silmek için Kayıt Seçiniz" });

            var data = _soruKategorileriBE.SoruKategorileriGetirDereceId(dereceId);
            if (data.IsSuccess)
            {
                return Json(new { success = data.IsSuccess, message = data.Message });
                ViewBag.Kategoriler = data;
            }
            else
                return Json(new { success = data.IsSuccess, message = data.Message });

        }
    }
}
