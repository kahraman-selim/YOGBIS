using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class DuyurularController : Controller
    {
        #region Değişkenler
        public DuyurularController()
        {

        } 
        #endregion

        #region Index
        public IActionResult Index()
        {
            return View();
        } 
        #endregion

        #region DuyuruEkleGet
        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet]
        [Route("Duyurular/DC10002", Name = "DuyuruEkleRoute")]
        public IActionResult DuyuruEkle()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));

            return View();
        }
        #endregion
    }
}
