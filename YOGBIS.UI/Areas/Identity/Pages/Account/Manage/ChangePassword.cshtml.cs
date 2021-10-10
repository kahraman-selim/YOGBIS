using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<Kullanici> userManager,
            SignInManager<Kullanici> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage ="Geçerli Şifre zorunludur")]
            [DataType(DataType.Password)]
            [Display(Name = "Geçerli Şifre")]
            public string OldPassword { get; set; }

            [StringLength(100, ErrorMessage = "Şifreniz en az {2}, en fazla {1} karakter uzunluğunda olmalıdır. En az bir küçük harf, büyük harf, sayı ve alfanümerik karakter içermelidir.(Örnek:Sifre123?)", MinimumLength = 6)]
            [Required(ErrorMessage = "Yeni Şifre zorunludur")]
            [DataType(DataType.Password)]
            [Display(Name = "Yeni Şifre")]
            public string NewPassword { get; set; }

            [Required(ErrorMessage = "Şifreyi doğrulamak zorunludur")]
            [DataType(DataType.Password)]
            [Display(Name = "Şifreyi Doğrula")]
            [Compare("NewPassword", ErrorMessage = "Yeni şifre ve doğrulam şifresi eşleşmiyor")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Şifreniz değiştirildi.";

            return RedirectToPage();
        }
    }
}
