using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        #region Tanımlamalar
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<Kullanici> _userManager;

        public LogoutModel(SignInManager<Kullanici> signInManager, ILogger<LogoutModel> logger, UserManager<Kullanici> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        } 
        #endregion

        #region OnGet
        public void OnGet()
        {
        }
        #endregion

        /*#region Onpost
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out.");

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                //return RedirectToPage();
                return RedirectToPage("./Login");
            }
        } 
        #endregion*/

        #region OnPostLogoutAsync
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            try
            {
                // Mevcut kullanıcının kimliğini al
                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser != null)
                {
                    // Oturum durumunu false yap
                    currentUser.OturumDurumu = false;

                    // Değişiklikleri kaydet
                    var result = await _userManager.UpdateAsync(currentUser);

                    // Güncelleme başarısız olursa logla
                    if (!result.Succeeded)
                    {
                        _logger.LogError("Kullanıcı oturum durumu güncellenemedi.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Olası hataları yakala ve logla
                _logger.LogError(ex, "Çıkış sırasında hata oluştu");
            }

            // Oturumu sonlandır
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();

            return RedirectToPage("/Login");
        }
        #endregion
    }
}
