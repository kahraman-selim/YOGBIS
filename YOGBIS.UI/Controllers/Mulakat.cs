using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize] //(Roles = ResultConstant.Admin_Role)]
    public class MulakatController : Controller
    {

        private readonly IMulakatBE _mulakatBE;
        public MulakatController(IMulakatBE mulakatBE)
        {
            _mulakatBE = mulakatBE;
        }


        public IActionResult Index()
        {
            var data = _mulakatBE.GetAllMulakatSorulari();
            if (data.IsSuccess)
            {
                var result = data.Data;
                return View(result);
            }
            return View();
            //if (id < 0)
            //    return View();
            //var data = _mulakatBE.GetAllMulakatSorulari(id);
            //if (data.IsSuccess)
            //    return View(data.Data);
            //return View();

            //return View();
        }
    }
}
