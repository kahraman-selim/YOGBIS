using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Kullanici> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public KullaniciBE(IUnitOfWork unitOfWork, IMapper mapper, UserManager<Kullanici> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Result<List<KullaniciVM>> KullaniciGetir()
        {
            var data = _unitOfWork.kullaniciRepository.GetAll().ToList();
            var kullanicilar = _mapper.Map<List<Kullanici>, List<KullaniciVM>>(data);
            return new Result<List<KullaniciVM>>(true, ResultConstant.RecordFound, kullanicilar);
        }

        public Result<KullaniciVM> KullaniciGetir(int Id)
        {
            var data = _unitOfWork.kullaniciRepository.Get(Id);
            if (data != null)
            {
                var kullanici = _mapper.Map<Kullanici, KullaniciVM>(data);
                return new Result<KullaniciVM>(true, ResultConstant.RecordFound, kullanici);
            }
            else
            {
                return new Result<KullaniciVM>(false, ResultConstant.RecordNotFound);
            }
        }

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

        public Result<List<KullaniciVM>> OnayKullaniciGetir()
        {

            //var data = _unitOfWork.kullaniciRepository.GetAll().ToList();
            var data = _roleManager.Roles.Where(x => x.Name == "Manager").ToList();
            //var kullanicilar = _mapper.Map<List<Kullanici>, List<KullaniciVM>>(data);

            if (data != null)
            {
                List<KullaniciVM> returnData = new List<KullaniciVM>();

                foreach (var item in returnData)
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
    }
}
