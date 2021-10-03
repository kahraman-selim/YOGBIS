﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Implementaion
{
    public class UlkelerBE : IUlkelerBE
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UlkelerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result<List<UlkelerVM>> UlkeleriGetir()
        {
            var data = _unitOfWork.ulkelerRepository.GetAll().OrderBy(u=>u.UlkeAdi).ToList();
            var ulkeler = _mapper.Map<List<Ulkeler>, List<UlkelerVM>>(data);
            return new Result<List<UlkelerVM>>(true, ResultConstant.RecordFound, ulkeler);
        }

        public Result<UlkelerVM> UlkeEkle(UlkelerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var ulkeler = _mapper.Map<UlkelerVM, Ulkeler>(model);
                    //ulkeler.KitaId = 1;
                    ulkeler.KullaniciId = user.LoginId;
                    _unitOfWork.ulkelerRepository.Add(ulkeler);                    
                    _unitOfWork.Save();
                    return new Result<UlkelerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<UlkelerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<UlkelerVM>(false, "Boş veri olamaz");
            }
        }

        public Result<UlkelerVM> UlkeGetir(int id)
        {
            var data = _unitOfWork.ulkelerRepository.Get(id);
            if (data != null)
            {
                var ulkeler = _mapper.Map<Ulkeler, UlkelerVM>(data);
                return new Result<UlkelerVM>(true, ResultConstant.RecordFound, ulkeler);
            }
            else
            {
                return new Result<UlkelerVM>(false, ResultConstant.RecordNotFound);
            }
        }

        public Result<UlkelerVM> UlkeGuncelle(UlkelerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var ulkeler = _mapper.Map<UlkelerVM, Ulkeler>(model);
                    ulkeler.KullaniciId = user.LoginId;
                    _unitOfWork.ulkelerRepository.Update(ulkeler);
                    _unitOfWork.Save();
                    return new Result<UlkelerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<UlkelerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<UlkelerVM>(false, "Boş veri olamaz");
            }
        }

        public Result<bool> UlkeSil(int id)
        {
            throw new NotImplementedException();
        }

        public Result<List<UlkelerVM>> UlkeGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.ulkelerRepository.GetAll(u=>u.KullaniciId==userId, includeProperties:"Kullanici,Kitalar").OrderBy(u => u.UlkeAdi).ToList();

            if (data!=null)
            {
                List<UlkelerVM> returnData = new List<UlkelerVM>();
                foreach (var item in data)
                {
                    returnData.Add(new UlkelerVM()
                    {
                        UlkeId=item.UlkeId,
                        UlkeAdi=item.UlkeAdi,
                        UlkeBayrak=item.UlkeBayrak,
                        UlkeAciklama=item.UlkeAciklama,
                        KayitTarihi=item.KayitTarihi,
                        KitaId=item.KitaId,                        
                        KullaniciId=item.KullaniciId,
                        KullaniciAdi=item.Kullanici.Ad + " " + item.Kullanici.Soyad,
                        KitaAdi=item.Kitalar.KitaAdi                        
                    });
                }
                return new Result<List<UlkelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<UlkelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
    }
}