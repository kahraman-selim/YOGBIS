using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;
        
        public IndexModel(
            UserManager<Kullanici> userManager,
            SignInManager<Kullanici> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string UserNameChangeLimitMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Ad")]
            public string Ad { get; set; }

            [Display(Name = "Soyad")]
            public string Soyad { get; set; }

            [Display(Name = "Kullanıcı Adı")]
            public string Username { get; set; }

            [Phone(ErrorMessage ="Telefon numarası bilgisi geçersiz...")]
            [Display(Name = "Telefon Numarası")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Profil Fotoğrafı")]
            public byte[] KullaniciResim { get; set; }


        }

        private async Task LoadAsync(Kullanici user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var ad = user.Ad;
            var soyad = user.Soyad;
            var kullaniciResim = user.KullaniciResim;
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Ad=user.Ad,
                Soyad=user.Soyad,
                Username = userName,
                KullaniciResim =kullaniciResim
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kimliğe sahip kullanıcı yüklenemiyor. Kullanıcı= '{_userManager.GetUserId(User)}'.");
            }

            UserNameChangeLimitMessage = $"Kullanıcı adınızı en fazla {user.KulaniciAdDegLimiti} kez değiştirebilirsiniz.";
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kimliğe sahip kullanıcı yüklenemiyor. Kullanıcı= '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Telefon numarasını ayarlamaya çalışırken beklenmeyen bir hata oluştu.";
                    return RedirectToPage();
                }
            }
            var ad = user.Ad;
            var soyad = user.Soyad;

            if (Input.Ad != user.Ad)
            {
                user.Ad = Input.Ad;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Soyad != user.Soyad)
            {
                user.Soyad = Input.Soyad;
                await _userManager.UpdateAsync(user);
            }
            if (user.KulaniciAdDegLimiti > 0)
            {
                if (Input.Ad != user.Ad)
                {
                    var userNameExists = await _userManager.FindByNameAsync(Input.Ad);
                    if (userNameExists != null)
                    {
                        StatusMessage = "Kullanıcı adı önceden alınmış. Farklı bir kullanıcı adı seçin.";
                        return RedirectToPage();
                    }

                    var setUserName = await _userManager.SetUserNameAsync(user, Input.Ad);
                    if (!setUserName.Succeeded)
                    {
                        StatusMessage = "Kullanıcı adını ayarlamaya çalışırken beklenmeyen bir hata oluştu.";
                        return RedirectToPage();
                    }
                    else
                    {
                        user.KulaniciAdDegLimiti -= 1;
                        await _userManager.UpdateAsync(user);
                    }
                }
            }
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.KullaniciResim = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Kullanıcı bilgileriniz güncellendi !";
            return RedirectToPage();
        }
    }
}
