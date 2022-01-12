using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.ConstantsModels
{
    public static class SeedData
    {
        #region Seed
        public static void Seed(UserManager<Kullanici> userManager, RoleManager<IdentityRole> roleManager)
        {
            _ = SeedRolesAsync(roleManager);
            _ = SeedSuperAdminAsync(userManager);
        }
        #endregion

        #region SeedSuperAdminAsync
        private static async Task SeedSuperAdminAsync(UserManager<Kullanici> userManager)
        {
            //Seed Default User
            var defaultUser = new Kullanici
            {
                UserName = "Administrator",
                Email = "yogbis@yogbis.com.tr",
                Ad = "YOGBİS",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Aktif = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Yogbis@2021.");
                    await userManager.AddToRoleAsync(defaultUser, ConstantsModels.EnumsKullaniciRolleri.Administrator.ToString());
                    //await userManager.AddToRoleAsync(defaultUser, EnumExtension<EnumsKullaniciRolleri>.GetDisplayValue(ConstantsModels.EnumsKullaniciRolleri.Manager).ToString());
                    //await userManager.AddToRoleAsync(defaultUser, EnumExtension<EnumsKullaniciRolleri>.GetDisplayValue(ConstantsModels.EnumsKullaniciRolleri.SubManager).ToString());
                    //await userManager.AddToRoleAsync(defaultUser, EnumExtension<EnumsKullaniciRolleri>.GetDisplayValue(ConstantsModels.EnumsKullaniciRolleri.Follower).ToString());
                    //await userManager.AddToRoleAsync(defaultUser, EnumExtension<EnumsKullaniciRolleri>.GetDisplayValue(ConstantsModels.EnumsKullaniciRolleri.Lecturer).ToString());
                    //await userManager.AddToRoleAsync(defaultUser, EnumExtension<EnumsKullaniciRolleri>.GetDisplayValue(ConstantsModels.EnumsKullaniciRolleri.Teacher).ToString());
                }

            }
        }
        #endregion

        #region SeedRolesAsync
        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(ConstantsModels.EnumsKullaniciRolleri.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ConstantsModels.EnumsKullaniciRolleri.Manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ConstantsModels.EnumsKullaniciRolleri.Representative.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ConstantsModels.EnumsKullaniciRolleri.SubManager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ConstantsModels.EnumsKullaniciRolleri.Follower.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ConstantsModels.EnumsKullaniciRolleri.Lecturer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ConstantsModels.EnumsKullaniciRolleri.Teacher.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ConstantsModels.EnumsKullaniciRolleri.Commissioner.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ConstantsModels.EnumsKullaniciRolleri.CommissionerHead.ToString()));
        } 
        #endregion

        #region EskiYöntem
        //private static void SeedUsers(UserManager<Kullanici> userManager)
        //{
        //    if (userManager.FindByNameAsync(ResultConstant.Admin_Email).Result==null)
        //    {
        //        var user = new Kullanici
        //        {
        //            UserName = ResultConstant.Admin_Email,
        //            Email = ResultConstant.Admin_Email,
        //            Aktif=true
        //        };
        //        var result = userManager.CreateAsync(user, ResultConstant.Admin_Password).Result;
        //        if (result.Succeeded)
        //        {
        //            userManager.AddToRoleAsync(user, ResultConstant.Admin_Role);
        //        }
        //    }
        //}

        //private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        //{
        //    if (!roleManager.RoleExistsAsync(ResultConstant.Admin_Role).Result)
        //    {
        //        var role = new IdentityRole
        //        {
        //            Name = ResultConstant.Admin_Role
        //        };
        //        var result = roleManager.CreateAsync(role).Result;
        //    }
        //    if (!roleManager.RoleExistsAsync(ResultConstant.Kullanici_Role).Result)
        //    {
        //        var role = new IdentityRole
        //        {
        //            Name = ResultConstant.Kullanici_Role
        //        };
        //        var result = roleManager.CreateAsync(role).Result;
        //    }
        //}        
        #endregion
    }
}
