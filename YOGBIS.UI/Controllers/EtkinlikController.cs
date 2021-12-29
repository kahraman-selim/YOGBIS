using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.UI.Controllers
{
    [Authorize]
    public class EtkinlikController : Controller
    {
        private readonly IAktivitelerBE _aktivitelerBE;
        private readonly IOkullarBE _okullarBE;
        private readonly IUlkelerBE _ulkelerBE;

        public EtkinlikController(IAktivitelerBE aktivitelerBE, IOkullarBE okullarBE, IUlkelerBE ulkelerBE)
        {
            _aktivitelerBE = aktivitelerBE;
            _okullarBE = okullarBE;
            _ulkelerBE = ulkelerBE;
        }

        [Authorize(Roles = "Administrator,Manager,Teacher,Follower")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
