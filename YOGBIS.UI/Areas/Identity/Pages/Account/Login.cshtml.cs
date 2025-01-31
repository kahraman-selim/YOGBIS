using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        #region Tanımlamalar
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        public LoginModel(SignInManager<Kullanici> signInManager,
            ILogger<LoginModel> logger,
            UserManager<Kullanici> userManager, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        } 
        #endregion

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        #region InputModel
        public class InputModel
        {
            [Required(ErrorMessage = "E-Posta alanı boş geçilemez !")]
            [Display(Name = "E-Posta")]
            [EmailAddress(ErrorMessage = "Geçersiz E-posta adresi !")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Şifrenizi yazmalısınız !")]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string Password { get; set; }

            [Display(Name = "Beni hatırla?")]
            public bool RememberMe { get; set; }
        }
        #endregion

        #region OnGetAsync
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
        #endregion

        #region OnPostAsyncOturumKontrol
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var userName = Input.Email;
                if (IsValidEmail(Input.Email))
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {
                        userName = user.UserName;

                        if (user.Aktif == true)
                        {
                            // Kullanıcının zaten bir oturumu varsa
                            var existingSession = HttpContext.Session.GetString(ResultConstant.LoginUserInfo);
                            if (!string.IsNullOrEmpty(existingSession))
                            {
                                ModelState.AddModelError(string.Empty, "Kullanıcı oturum açmış durumda. Bir sorun olduğunu düşünüyorsanız Sistem Yöneticisine başvurunuz.");
                                return Page();
                            }

                            var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                            if (result.Succeeded)
                            {
                                user.OturumDurumu = true;
                                await _userManager.UpdateAsync(user);

                                var userInfo = new SessionContext()
                                {
                                    Email = user.Email,
                                    FirstName = user.Ad,
                                    //TODO:Admın Bilgisini dinamic olarak getir
                                    IsAdmin = false,
                                    LastName = user.Soyad,
                                    LoginId = user.Id,
                                    Aktif = user.Aktif
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
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı veya şifre yanlış !");
                            return Page();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı veya şifre yanlış !");
                        return Page();
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        } 
        #endregion

        /*#region OnPostAsync
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {

                //var user = _unitOfWork.kullaniciRepository.GetFirstOrDefault(u => u.Email == Input.Email.ToLower() && u.Aktif == true);
                var userName = Input.Email;
                if (IsValidEmail(Input.Email))
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {
                        userName = user.UserName;

                        if (user.Aktif == true)
                        {
                            var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                            if (result.Succeeded)
                            {
                                var userInfo = new SessionContext()
                                {
                                    Email = user.Email,
                                    FirstName = user.Ad,
                                    //TODO:Admın Bilgisini dinamic olarak getir
                                    IsAdmin = false,
                                    LastName = user.Soyad,
                                    LoginId = user.Id,
                                    Aktif = user.Aktif
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
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı veya şifre yanlış !");
                            return Page();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı veya şifre yanlış !");
                        return Page();
                    }
                    //var currentuser = await _unitOfWork.kullaniciRepository.GetFirstOrDefault(u => u.Email == Input.Email.ToLower() && u.Aktif == true);

                }


                //if (user == null)
                //{
                //    ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı veya şifre yanlış !");
                //    return Page();
                //}
                //else
                //{
                //    var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                //    if (result.Succeeded)
                //    {
                //        var userInfo = new SessionContext()
                //        {
                //            Email = user.Email,
                //            FirstName = user.Ad,
                //            //TODO:Admın Bilgisini dinamic olarak getir
                //            IsAdmin = false,
                //            LastName = user.Soyad,
                //            LoginId = user.Id,
                //            Aktif = user.Aktif
                //        };

                //        //Set To User ınfo Session
                //        HttpContext.Session.SetString(ResultConstant.LoginUserInfo, JsonConvert.SerializeObject(userInfo));

                //        _logger.LogInformation("User logged in.");
                //        return LocalRedirect(returnUrl);

                //    }
                //    // This doesn't count login failures towards account lockout
                //    // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                //    if (result.RequiresTwoFactor)
                //    {
                //        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                //    }
                //    if (result.IsLockedOut)
                //    {
                //        _logger.LogWarning("User account locked out.");
                //        return RedirectToPage("./Lockout");
                //    }
                //    else
                //    {
                //        ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı veya şifre yanlış !");
                //        return Page();
                //    }

                //}

            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        #endregion*/

        #region IsValidEmail
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        } 
        #endregion
    }
}
