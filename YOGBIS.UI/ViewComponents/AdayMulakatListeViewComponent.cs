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

        public async Task<IViewComponentResult> InvokeAsync(string selectedKomisyon = null)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            var mulakatTarihi = "15.04.2024"; // bu alan datetime olarak değiştirilecek
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
                
                // Eğer seçili komisyon varsa onun listesini getir
                if (!string.IsNullOrEmpty(selectedKomisyon))
                {
                    var result = _adaylarBE.GetirKomisyonMulakatListesi(selectedKomisyon, mulakatTarihi);
                    if (result.IsSuccess)
                    {
                        viewModel.AdayListesi = result.Data;
                    }
                    else
                    {
                        viewModel.AdayListesi = Enumerable.Empty<YOGBIS.Common.VModels.AdayMYSSVM>();
                    }
                }
                else
                {
                    // İlk açılışta boş liste göster
                    viewModel.AdayListesi = Enumerable.Empty<YOGBIS.Common.VModels.AdayMYSSVM>();
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
                else
                {
                    viewModel.AdayListesi = Enumerable.Empty<YOGBIS.Common.VModels.AdayMYSSVM>();
                }
            }
            
            return View(viewModel);
        }
    }

    public class AdayMulakatListeViewModel
    {
        public IEnumerable<KomisyonBaskanViewModel> KomisyonBaskanları { get; set; }
        public IEnumerable<YOGBIS.Common.VModels.AdayMYSSVM> AdayListesi { get; set; }
    }

    public class KomisyonBaskanViewModel
    {
        public string Id { get; set; }
        public string AdSoyad { get; set; }
        public string UserName { get; set; }
    }
}
