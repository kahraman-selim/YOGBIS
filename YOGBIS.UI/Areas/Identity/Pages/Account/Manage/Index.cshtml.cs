using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Areas.Identity.Pages.Account.Manage
{
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

        [Display(Name = "Kullanıcı")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Telefon Numarası")]
            public string PhoneNumber { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public bool? Aktif { get; set; }
        }

        private async Task LoadAsync(Kullanici user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            
            Username = userName;
            
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Ad=user.Ad,
                Soyad=user.Soyad,
                Aktif=user.Aktif
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
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
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if (Input.Ad != user.Ad)
            {
                user.Ad = Input.Ad;
            }
            if (Input.Soyad != user.Soyad)
            {
                user.Soyad = Input.Soyad;
            }
            if (Input.Aktif != user.Aktif)
            {
                user.Aktif = Input.Aktif;
            }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Kullanıcı bilgileriniz güncellendi !";
            return RedirectToPage();
        }
    }
}
