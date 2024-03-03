using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator,CommissionerHead")]
    public class MulakatController : Controller
    {

        private readonly IMulakatSorulariBE _mulakatSorulariBE;
        public MulakatController(IMulakatSorulariBE mulakatSorulariBE)
        {
            _mulakatSorulariBE = mulakatSorulariBE;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult Index(int id, string derece)
        //{
        //    var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
        //    var sorugetirmodel = _mulakatBE.GetAllMulakatSorulariById(id, derece);
        //    if (sorugetirmodel.IsSuccess)
        //    {
        //        return View(sorugetirmodel);
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult MulakatSoruGetir(int id,string derece)
        //{
        //    #region MyRegion
        //    var sorugetirmodel = _mulakatSorulariBE.GetAllMulakatSorulariById(id, derece);
        //    if (sorugetirmodel.IsSuccess)
        //    {
        //        return View(sorugetirmodel);
        //    }
        //    return View();

        //    #endregion

        //}

    }
}
