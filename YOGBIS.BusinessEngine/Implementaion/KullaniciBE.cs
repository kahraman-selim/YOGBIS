using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

namespace YOGBIS.BusinessEngine.Implementaion
{
    public class KullaniciBE : IKullaniciBE
    {

        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Kullanici> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Dönüştürücüler
        public KullaniciBE(IUnitOfWork unitOfWork, IMapper mapper, UserManager<Kullanici> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion

        #region KullaniciGetir
        public Result<List<KullaniciVM>> KullaniciGetir()
        {
            var data = _unitOfWork.kullaniciRepository.GetAll().OrderBy(k=>k.UserName).ToList();
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
            var data = _unitOfWork.kullaniciRepository.GetAll().ToList();
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
                        Ad = item.Ad,
                        //Soyad = item.Soyad,
                        //AdSoyad = item.Ad + " " + item.Soyad
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
            var data = _unitOfWork.kullaniciRepository.GetFirstOrDefault(u => u.UserName == KomisyonUserName);

            if (data != null)
            {
                var id = data.Id;

                return new Result<string>(true, ResultConstant.RecordFound, id);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region KomisyonSorumluGetir
        public async Task<Result<List<KullaniciVM>>> KomisyonSorumluGetir()
        {
            var data = _unitOfWork.kullaniciRepository.GetAll().ToList();
            var newdata = await _userManager.GetUsersInRoleAsync("Commissioner");
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
    }
}
