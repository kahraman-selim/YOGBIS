using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using System.Security.Claims;
using Newtonsoft.Json;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.Const;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using YOGBIS.Data.DbModels;
using System.Collections.Generic;

namespace YOGBIS.UI.ViewComponents
{
    public class AdayMulakatListeViewComponent : ViewComponent
    {
        private readonly IAdaylarBE _adaylarBE;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Kullanici> _userManager;

        public AdayMulakatListeViewComponent(
            IAdaylarBE adaylarBE, 
            IHttpContextAccessor httpContextAccessor,
            UserManager<Kullanici> userManager)
        {
            _adaylarBE = adaylarBE;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            var mulakatTarihi = "15.04.2024";
            var currentUser = await _userManager.FindByNameAsync(userName);
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var viewModel = new AdayMulakatListeViewModel();

            if (currentUser.UserName == "Administrator")
            {
                // Administrator için tüm CommissionerHead rolündeki kullanıcıları getir
                var commissionHeads = await _userManager.GetUsersInRoleAsync("CommissionerHead");
                viewModel.KomisyonBaskanları = commissionHeads.Select(u => new KomisyonBaskanViewModel 
                { 
                    Id = u.Id, 
                    AdSoyad = $"{u.Ad}",
                    UserName = u.Ad // Komisyon adını UserName olarak kullan
                }).ToList();
                
                // Administrator ilk açılışta boş liste görecek
                //viewModel.AdayListesi = new List<YOGBIS.Common.VModels.AdayMYSSVM>();
                var result = _adaylarBE.GetirKomisyonMulakatListesi("Komisyon-10", mulakatTarihi);
                if (result.IsSuccess)
                {
                    viewModel.AdayListesi = result.Data;
                }
            }
            else
            {
                // Diğer kullanıcılar kendi listelerini görecek
                var result = _adaylarBE.GetirKomisyonMulakatListesi(currentUser.Ad, mulakatTarihi);
                if (result.IsSuccess)
                {
                    viewModel.AdayListesi = result.Data;
                }
            }
            
            return View(viewModel);
        }
    }

    public class AdayMulakatListeViewModel
    {
        public IEnumerable<KomisyonBaskanViewModel> KomisyonBaskanları { get; set; }
        public object AdayListesi { get; set; }
    }

    public class KomisyonBaskanViewModel
    {
        public string Id { get; set; }
        public string AdSoyad { get; set; }
        public string UserName { get; set; }
    }
}
