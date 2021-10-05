using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    //[Authorize(Roles = ResultConstant.Admin_Role)]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly UserManager<Kullanici> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<Kullanici> userManager,
            SignInManager<Kullanici> signInManager,
            ILogger<RegisterModel> logger)    //IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required(ErrorMessage ="Kullanıcı Adı zorunlu bir alandır...")]
            public string Ad { get; set; }

            [Required(ErrorMessage = "Kullanıcı Soyadı zorunlu bir alandır...")]
            public string Soyad { get; set; }

            [Required(ErrorMessage = "E-Posta adresi zorunlu bir alandır...")]
            [EmailAddress(ErrorMessage = "Girilen adres bir E-Posta adresi değil...")]
            [Display(Name = "E-Posta")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Şifrenizi yazmalısınız...")]
            [StringLength(100, ErrorMessage = " Şifreniz en az {2}, en fazla {1} karakter uzunluğunda olmalıdır. En az bir küçük harf, büyük harf ve alfanümerik karakter içermelidir.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Şifreyi Onayla")]
            [Compare("Password", ErrorMessage = "Girilen şifreler eşleşmiyor !")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Kullanici 
                { 
                    Ad=Input.Ad,
                    Soyad=Input.Soyad.ToUpper(),
                    UserName = Input.Email, 
                    Email = Input.Email, 
                    Aktif=true
                };
                
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, ResultConstant.Kullanici_Role).Wait(); 
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    return LocalRedirect(returnUrl);
                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
