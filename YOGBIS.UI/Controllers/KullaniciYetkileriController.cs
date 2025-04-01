﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KullaniciYetkileriController : Controller
    {
        
        #region Değişkenler
        private readonly UserManager<Kullanici> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Dönüştürücüler
        public KullaniciYetkileriController(UserManager<Kullanici> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        #endregion

        #region Index
        [Route("KY10001", Name = "KulanaciYetkileriIndexRoute")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<KullaniciYetkilerVM>();
            foreach (Kullanici user in users)
            {
                var thisViewModel = new KullaniciYetkilerVM();
                thisViewModel.Id = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.Ad = user.Ad;
                thisViewModel.Soyad = user.Soyad;
                thisViewModel.UserName = user.UserName;
                thisViewModel.EnumsKullaniciRolleri = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }

            return View(userRolesViewModel);
        }
        #endregion

        #region YonetimGet
        public async Task<IActionResult> Yonetim(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Kullanıcı bulunamadı Id = {userId}";
                return View("NotFound");
            }

            ViewBag.UserName = user.UserName;

            var model = new List<KullaniciYekiYonetimVM>();
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new KullaniciYekiYonetimVM()
                {
                    RoleId = role.Id,                    
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }

                model.Add(userRolesViewModel);
            }
            return View(model);
        }
        #endregion

        #region YonetimPost
        [HttpPost]
        public async Task<IActionResult> Yonetim(List<KullaniciYekiYonetimVM> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Kullanıcının mevcut rolleri silinemiyor");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Kullanıcıya seçili roller eklenemiyor");
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region GetUserRoles
        private async Task<IEnumerable<string>> GetUserRoles(Kullanici user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        } 
        #endregion
    }
}
