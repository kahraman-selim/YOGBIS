using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KullanicilarController : Controller
    {

        #region Değişkenler
        private readonly IKullaniciBE _kullaniciBE;
        private readonly UserManager<Kullanici> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<KullanicilarController> _logger;
        #endregion

        #region Dönüştürücüler
        public KullanicilarController(IKullaniciBE kullaniciBE, UserManager<Kullanici> userManager, RoleManager<IdentityRole> roleManager, ILogger<KullanicilarController> logger)
        {
            _kullaniciBE = kullaniciBE;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        #endregion

        #region Index
        [Route("KU10001", Name = "KullanıcılarIndexRoute")]
        public IActionResult Index()
        {
            var data = _kullaniciBE.KullaniciGetir();
            if (data.IsSuccess)
            {
                var result = data.Data;
                return View(result);
            }
            return View();
        }
        #endregion

        #region KullaniciGuncelleGet
        //[HttpGet]
        //public ActionResult KullaniciGuncelle(string id)
        //{
        //    if (id != null)
        //        return View();
        //    var data = _kullaniciBE.KullaniciGetir(id);
        //    if (data.IsSuccess)
        //        return View(data.Data);
        //    return View();
        //}
        #endregion

        #region KullaniciGuncellePost
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult KullaniciGuncelle(KullaniciVM model)
        {
            if (ModelState.IsValid)
            {
                var data = _kullaniciBE.KullaniciGuncelle(model);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region KullaniciDurum
        public IActionResult Durum(string id)
        {

            var user = _userManager.FindByIdAsync(id);

            if (user.Result.Aktif == true)
                user.Result.Aktif = false;
            else
                user.Result.Aktif = true;

            _userManager.UpdateAsync(user.Result);

            return RedirectToAction("Index");

        }
        #endregion

        #region OturumYenile
        public IActionResult OturumYenile(string id)
        {

            var user = _userManager.FindByIdAsync(id);

            if (user.Result.OturumDurumu == true) // Oturum Açılmışsa 
                user.Result.OturumDurumu = false; // Oturumu kapalı olarak değiştir
            else
                user.Result.OturumDurumu = true;

            _userManager.UpdateAsync(user.Result);

            return RedirectToAction("Index");

        }
        #endregion

        #region DeactivateNonAdminUsers
        public async Task<IActionResult> DeactivateNonAdminUsers()
        {
            try
            {
                // Admin rolündeki kullanıcıları bul
                var adminRoleUsers = await _userManager.GetUsersInRoleAsync("Administrator");
                var adminUserIds = adminRoleUsers.Select(u => u.Id).ToList();

                // Tüm kullanıcıları getir
                var allUsers = await _userManager.Users
                    .Where(u => u.Aktif == true && !adminUserIds.Contains(u.Id))
                    .ToListAsync();

                int deactivatedCount = 0;
                var errors = new List<string>();

                // Her bir admin olmayan kullanıcıyı pasif yap
                foreach (var user in allUsers)
                {
                    user.Aktif = false;
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        deactivatedCount++;
                    }
                    else
                    {
                        errors.Add($"Kullanıcı {user.UserName} güncellenirken hata: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }

                // İşlem sonucunu TempData'ya kaydet
                TempData["Message"] = $"{deactivatedCount} kullanıcı pasif hale getirildi.";
                if (errors.Any())
                {
                    TempData["Errors"] = string.Join("\n", errors);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Hata logla
                _logger.LogError(ex, "Kullanıcıları pasif yaparken hata oluştu");
                TempData["Error"] = "İşlem sırasında bir hata oluştu.";
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region ActivateNonAdminUsers
        public async Task<IActionResult> ActivateNonAdminUsers()
        {
            try
            {
                // Admin rolündeki kullanıcıları bul
                var adminRoleUsers = await _userManager.GetUsersInRoleAsync("Administrator");
                var adminUserIds = adminRoleUsers.Select(u => u.Id).ToList();

                // Tüm kullanıcıları getir
                var allUsers = await _userManager.Users
                    .Where(u => u.Aktif == false && !adminUserIds.Contains(u.Id))
                    .ToListAsync();

                int activatedCount = 0;
                var errors = new List<string>();

                // Her bir admin olmayan kullanıcıyı aktif yap
                foreach (var user in allUsers)
                {
                    user.Aktif = true;
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        activatedCount++;
                    }
                    else
                    {
                        errors.Add($"Kullanıcı {user.UserName} güncellenirken hata: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }

                // İşlem sonucunu TempData'ya kaydet
                TempData["Message"] = $"{activatedCount} kullanıcı aktif hale getirildi.";
                if (errors.Any())
                {
                    TempData["Errors"] = string.Join("\n", errors);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Hata logla
                _logger.LogError(ex, "Kullanıcıları aktif yaparken hata oluştu");
                TempData["Error"] = "İşlem sırasında bir hata oluştu.";
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}
