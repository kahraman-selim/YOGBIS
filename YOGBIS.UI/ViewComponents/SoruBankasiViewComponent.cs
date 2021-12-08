using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.ViewComponents
{
    public class SoruBankasiViewComponent : ViewComponent
    {
        private readonly ISoruBankasiBE _soruBankasiBE;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Kullanici> _userManager;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager;

        public SoruBankasiViewComponent(ISoruBankasiBE soruBankasiBE, Microsoft.AspNetCore.Identity.UserManager<Kullanici> userManager, Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager)
        {
            _soruBankasiBE = soruBankasiBE;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var Loginuser = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            //var user = _userManager.FindByIdAsync(Loginuser.LoginId);
            Kullanici user = _userManager.Users.SingleOrDefault(x => x.UserName == HttpContext.User.Identity.Name);
            //var userRole = _roleManager.FindByIdAsync(user.Id);

            if (_userManager.IsInRoleAsync(user, "Manager").Result)
            {
                var requestmodel = _soruBankasiBE.SoruGetirOnaylayanId(user.Id);
                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }
                return View();
            }
            else if (_userManager.IsInRoleAsync(user, "Follower").Result)
            {
                var requestmodel = _soruBankasiBE.SoruGetirKullaniciId(user.Id);
                if (requestmodel.IsSuccess)
                {
                    return View(requestmodel.Data);
                }
                return View();
            }
            else
            {
                return View();
            }
        }
    }
}
