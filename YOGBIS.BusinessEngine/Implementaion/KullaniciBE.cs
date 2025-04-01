using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;
using Microsoft.Extensions.Logging;
using YOGBIS.Data.DataContext;

namespace YOGBIS.BusinessEngine.Implementaion
{
    public class KullaniciBE : IKullaniciBE
    {

        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Kullanici> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        private readonly YOGBISContext _context;
        #endregion

        #region Dönüştürücüler
        public KullaniciBE(IUnitOfWork unitOfWork, IMapper mapper, UserManager<Kullanici> userManager, RoleManager<IdentityRole> roleManager, ILogger<KullaniciBE> logger, YOGBISContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
        }
        #endregion

        #region KullaniciGetir
        public Result<List<KullaniciVM>> KullaniciGetir()
        {
            var data = _unitOfWork.kullaniciRepository.GetAll().OrderBy(t=>t.TcKimlikNo.PadLeft(11,'0')).ToList();
            var kullanicilar = _mapper.Map<List<Kullanici>, List<KullaniciVM>>(data);
            return new Result<List<KullaniciVM>>(true, ResultConstant.RecordFound, kullanicilar);
        }
        #endregion        

        #region KullaniciGuncelle
        public Result<KullaniciVM> KullaniciGuncelle(KullaniciVM model)
        {
            if (model != null)
            {
                try
                {
                    var kullanici = _mapper.Map<KullaniciVM, Kullanici>(model);
                    _unitOfWork.kullaniciRepository.Update(kullanici);
                    _unitOfWork.Save();
                    return new Result<KullaniciVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<KullaniciVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<KullaniciVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region OnayKullaniciGetir
        public async Task<Result<List<KullaniciVM>>> OnayKullaniciGetir()
        {
            var data = _unitOfWork.kullaniciRepository.GetAll().ToList();
            var newdata = await _userManager.GetUsersInRoleAsync("Manager");
            //var role = _roleManager.Roles.Select(x => x.Name == "Manager");
            var kullanicilar = _mapper.Map<List<Kullanici>, List<KullaniciVM>>(data);

            if (newdata != null)
            {
                List<KullaniciVM> returnData = new List<KullaniciVM>();

                foreach (var item in newdata)
                {
                    returnData.Add(new KullaniciVM()
                    {
                        Id = item.Id,
                        Ad = item.Ad,
                        Soyad = item.Soyad,                        
                        AdSoyad = item.Ad + " " + item.Soyad
                    });
                }
                return new Result<List<KullaniciVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<KullaniciVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulMuduruGetir
        public async Task<Result<List<KullaniciVM>>> OkulMuduruGetir()
        {
            var data = _unitOfWork.kullaniciRepository.GetAll().ToList();
            var newdata = await _userManager.GetUsersInRoleAsync("SubManager");
            //var role = _roleManager.Roles.Select(x => x.Name == "Manager");
            var kullanicilar = _mapper.Map<List<Kullanici>, List<KullaniciVM>>(data);

            if (newdata != null)
            {
                List<KullaniciVM> returnData = new List<KullaniciVM>();

                foreach (var item in newdata)
                {
                    returnData.Add(new KullaniciVM()
                    {
                        Id = item.Id,
                        Ad = item.Ad,
                        Soyad = item.Soyad,
                        AdSoyad = item.Ad + " " + item.Soyad
                    });
                }
                return new Result<List<KullaniciVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<KullaniciVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region KomisyonGetir
        public async Task<Result<List<KullaniciVM>>> KomisyonGetir()
        {
            var data = _unitOfWork.kullaniciRepository.GetAll().OrderBy(t=>t.TcKimlikNo).ToList();
            var newdata = await _userManager.GetUsersInRoleAsync("CommissionerHead");
            var kullanicilar = _mapper.Map<List<Kullanici>, List<KullaniciVM>>(data);

            if (newdata != null)
            {
                List<KullaniciVM> returnData = new List<KullaniciVM>();

                foreach (var item in newdata)
                {
                    returnData.Add(new KullaniciVM()
                    {
                        Id = item.Id,
                        TcKimlikNo = item.TcKimlikNo,
                        Ad=item.Ad,
                        Soyad=item.Soyad,
                        AdSoyad= item.Ad + " " + item.Soyad,
                    });
                }
                returnData = returnData.OrderBy(x => x.TcKimlikNo.PadLeft(11, '0')).ToList();
                return new Result<List<KullaniciVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<KullaniciVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region KullaniciAdSoyadGetir(string UserId)
        public Result<string> KullaniciAdSoyadGetir(string UserId)
        {
            var data = _unitOfWork.kullaniciRepository.GetFirstOrDefault(u => u.Id == UserId);

            if (data != null)
            {
                var kullaniciAdSoyad = data.Ad + " " + data.Soyad;

                return new Result<string>(true, ResultConstant.RecordFound, kullaniciAdSoyad);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region KomisyonKullaniciIDGetir(string KomisyonUserName)
        public Result<string> KomisyonKullaniciIDGetir(string KomisyonUserName)
        {
            try
            {
                _logger.LogInformation($"Aranan KomisyonUserName: {KomisyonUserName}");
                
                // Komisyon-1 formatındaki ismi Komisyon1 formatına çevir
                var userName = KomisyonUserName.Replace("-", "");
                _logger.LogInformation($"Dönüştürülen UserName: {userName}");
                
                var data = _unitOfWork.kullaniciRepository.GetFirstOrDefault(u => u.UserName == userName);
                _logger.LogInformation($"Kullanıcı bulundu mu: {data != null}");
                
                if (data != null)
                {
                    _logger.LogInformation($"UserName: {data.UserName}, Ad: {data.Ad}");
                    return new Result<string>(true, ResultConstant.RecordFound, data.UserName);
                }

                _logger.LogWarning("Kullanıcı bulunamadı");
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError($"KomisyonKullaniciIDGetir hatası: {ex.Message}");
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region KomisyonSorumluGetir
        public async Task<Result<List<KullaniciVM>>> KomisyonSorumluGetir()
        {
            try
            {
                _logger.LogInformation("Komisyon sorumlu personelleri getiriliyor...");

                // Commissioner rolünü bul
                var commissionerRole = await _roleManager.FindByNameAsync("Commissioner");
                if (commissionerRole == null)
                {
                    _logger.LogWarning("Commissioner rolü bulunamadı");
                    return new Result<List<KullaniciVM>>(false, "Commissioner rolü bulunamadı");
                }

                // Commissioner rolüne sahip kullanıcıları al
                var userIdsWithCommissionerRole = await _userManager.GetUsersInRoleAsync("Commissioner");
                
                // Kullanıcı listesini oluştur
                var returnData = userIdsWithCommissionerRole
                    .Join(_context.UserRoles,
                        user => user.Id,
                        userRole => userRole.UserId,
                        (user, userRole) => new { User = user, UserRole = userRole })
                    .Where(x => x.UserRole.RoleId == commissionerRole.Id)
                    .OrderBy(x => x.User.Ad)
                    .ThenBy(x => x.User.Soyad)
                    .Select(joined => new KullaniciVM
                    {
                        Id = joined.User.Id,
                        TcKimlikNo = joined.User.TcKimlikNo,
                        Ad = joined.User.Ad,
                        Soyad = joined.User.Soyad,
                        AdSoyad = $"{joined.User.Ad} {joined.User.Soyad}",
                        RolId = joined.UserRole.RoleId
                    })
                    .ToList();

                _logger.LogInformation($"Toplam {returnData.Count()} komisyon sorumlusu getirildi");
                return new Result<List<KullaniciVM>>(true, ResultConstant.RecordFound, returnData);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Komisyon sorumluları getirilirken hata oluştu: {ex.Message}");
                return new Result<List<KullaniciVM>>(false, $"Hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region KomisyonYardimciGetir
        public async Task<Result<List<KullaniciVM>>> KomisyonYardimciGetir()
        {
            try
            {
                _logger.LogInformation("Komisyon yardımcı personelleri getiriliyor...");

                // Follower rolünü bul
                var commissionerRole = await _roleManager.FindByNameAsync("Follower");
                if (commissionerRole == null)
                {
                    _logger.LogWarning("Follower rolü bulunamadı");
                    return new Result<List<KullaniciVM>>(false, "Follower rolü bulunamadı");
                }

                // Follower rolüne sahip kullanıcıları al
                var userIdsWithCommissionerRole = await _userManager.GetUsersInRoleAsync("Follower");

                // Kullanıcı listesini oluştur
                var returnData = userIdsWithCommissionerRole
                    .Join(_context.UserRoles,
                        user => user.Id,
                        userRole => userRole.UserId,
                        (user, userRole) => new { User = user, UserRole = userRole })
                    .Where(x => x.UserRole.RoleId == commissionerRole.Id)
                    .OrderBy(x => x.User.Ad)
                    .ThenBy(x => x.User.Soyad)
                    .Select(joined => new KullaniciVM
                    {
                        Id = joined.User.Id,
                        TcKimlikNo = joined.User.TcKimlikNo,
                        Ad = joined.User.Ad,
                        Soyad = joined.User.Soyad,
                        AdSoyad = $"{joined.User.Ad} {joined.User.Soyad}",
                        RolId = joined.UserRole.RoleId
                    })
                    .ToList();

                _logger.LogInformation($"Toplam {returnData.Count()} komisyon yardımcısı getirildi");
                return new Result<List<KullaniciVM>>(true, ResultConstant.RecordFound, returnData);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Komisyon yardımcıları getirilirken hata oluştu: {ex.Message}");
                return new Result<List<KullaniciVM>>(false, $"Hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region KomisyonBaskanlariniGetir
        public Result<List<KullaniciVM>> KomisyonBaskanlariniGetir()
        {
            try
            {
                _logger.LogInformation("Komisyon başkanları getiriliyor...");

                var komisyonBaskanRolId = _roleManager.Roles
                    .Where(r => r.Name == "KomisyonHeader")
                    .Select(r => r.Id)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(komisyonBaskanRolId))
                {
                    _logger.LogWarning("KomisyonHeader rolü bulunamadı");
                    return new Result<List<KullaniciVM>>(false, "Komisyon başkanı rolü bulunamadı");
                }

                var userRoles = _userManager.GetUsersInRoleAsync("KomisyonHeader").Result;
                var komisyonBaskanKullanicilari = userRoles.Select(user => new KullaniciVM
                {
                    Id = user.Id,
                    AdSoyad = user.Ad + " " + user.Soyad,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    EMail = user.Email
                })
                .OrderBy(x => x.AdSoyad)
                .ToList();

                if (!komisyonBaskanKullanicilari.Any())
                {
                    _logger.LogWarning("Komisyon başkanı rolüne sahip kullanıcı bulunamadı");
                    return new Result<List<KullaniciVM>>(false, "Komisyon başkanı bulunamadı");
                }

                _logger.LogInformation($"Komisyon başkanları başarıyla getirildi. Toplam: {komisyonBaskanKullanicilari.Count()}");
                return new Result<List<KullaniciVM>>(true, "Komisyon başkanları başarıyla getirildi", komisyonBaskanKullanicilari);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Komisyon başkanları getirilirken hata oluştu: {ex.Message}", ex);
                return new Result<List<KullaniciVM>>(false, $"Hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region KomisyonSorumluGetirBirleştirmeYöntemi
        //public async Task<Result<List<KullaniciVM>>> KomisyonSorumluGetir()
        //{
        //    var data = _unitOfWork.kullaniciRepository.GetAll().OrderBy(t => t.Ad).ToList();

        //    // Her iki roldeki kullanıcıları al
        //    var commissionerUsers = await _userManager.GetUsersInRoleAsync("Commissioner");
        //    var followerUsers = await _userManager.GetUsersInRoleAsync("Follower");

        //    // İki rolden gelen kullanıcıları birleştir ve tekrar eden kayıtları önle
        //    var combinedUsers = commissionerUsers.Union(followerUsers).ToList().OrderBy(x=>x.Ad).ThenBy(y=>y.Soyad);

        //    if (combinedUsers.Any())
        //    {
        //        List<KullaniciVM> returnData = new List<KullaniciVM>();

        //        foreach (var item in combinedUsers)
        //        {
        //            returnData.Add(new KullaniciVM()
        //            {
        //                Id = item.Id,
        //                TcKimlikNo = item.TcKimlikNo,
        //                Ad = item.Ad,
        //                Soyad = item.Soyad,
        //                AdSoyad = item.Ad + " " + item.Soyad
        //            });
        //        }
        //        return new Result<List<KullaniciVM>>(true, ResultConstant.RecordFound, returnData);
        //    }
        //    else
        //    {
        //        return new Result<List<KullaniciVM>>(false, ResultConstant.RecordNotFound);
        //    }
        //}
        #endregion
    }
}
