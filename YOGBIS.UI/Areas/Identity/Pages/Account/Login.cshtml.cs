using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using YOGBIS.Data.Contracts;
using YOGBIS.Common.SessionOperations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using YOGBIS.Data.DbModels;
using YOGBIS.Common.ConstantsModels;

namespace YOGBIS.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        public LoginModel(SignInManager<Kullanici> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<Kullanici> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "E-Posta alanı boş geçilemez !")]
            [Display(Name = "E-Posta")]
            [EmailAddress]            
            public string Email { get; set; }

            [Required(ErrorMessage = "Şifrenizi yazmalısınız !")]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string Password { get; set; }

            [Display(Name = "Beni hatırla?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = _unitOfWork.kullaniciRepository.GetFirstOrDefault(u => u.Email == Input.Email.ToLower() && u.Aktif==true);

                    var userInfo = new SessionContext()
                    {
                        Email = user.Email,
                        FirstName = user.Ad,
                        //TODO:Admın Bilgisini dinamic olarak getir
                        IsAdmin = false,
                        LastName = user.Soyad,
                        LoginId = user.Id,
                        Aktif=user.Aktif
                    };

                    //Set To User ınfo Session
                    HttpContext.Session.SetString(ResultConstant.LoginUserInfo, JsonConvert.SerializeObject(userInfo));

                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı veya şifre yanlış !");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
