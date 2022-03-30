using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;

namespace YOGBIS.UI.ViewComponents
{
    public class DuyuruListesiViewComponent : ViewComponent
    {
        private readonly IDuyurularBE _duyurularBE;

        public DuyuruListesiViewComponent(IDuyurularBE duyurularBE)
        {
            _duyurularBE = duyurularBE;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultConstant.LoginUserInfo));
            var requestmodel = _duyurularBE.DuyuruGetirKullaniciId(user.LoginId);
            if (requestmodel.IsSuccess)
            {
                return View(requestmodel.Data);
            }
            return View();
        }
    }
}
