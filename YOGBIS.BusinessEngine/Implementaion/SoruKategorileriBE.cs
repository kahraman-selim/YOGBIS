﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Implementaion
{
    public class SoruKategorileriBE : ISoruKategorileriBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDerecelerBE _derecelerBE;
        #endregion

        #region Dönüştürücüler
        public SoruKategorileriBE(IUnitOfWork unitOfWork, IMapper mapper, IDerecelerBE derecelerBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _derecelerBE = derecelerBE;
        }
        #endregion

        #region SoruKategorileriGetir
        public Result<List<SoruKategorilerVM>> SoruKategorileriGetir()
        {
            //var data = _unitOfWork.sorukategorilerRepository.GetAll().ToList();
            //var soruKategoriler = _mapper.Map<List<SoruKategoriler>, List<SoruKategorilerVM>>(data);
            //return new Result<List<SoruKategorilerVM>>(true, ResultConstant.RecordFound, soruKategoriler);

            var data = _unitOfWork.sorukategorilerRepository.GetAll(includeProperties: "Kullanici").ToList();
            var kategoriler = _mapper.Map<List<SoruKategoriler>, List<SoruKategorilerVM>>(data);

            if (data != null)
            {
                List<SoruKategorilerVM> returnData = new List<SoruKategorilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruKategorilerVM()
                    {
                        SoruKategorilerId = item.SoruKategorilerId,
                        SoruKategorilerAdi = item.SoruKategorilerAdi,
                        SoruKategorilerKullanimi = item.SoruKategorilerKullanimi,
                        SoruKategorilerPuan = item.SoruKategorilerPuan,
                        DereceId = item.DereceId,
                        DereceAdi = _derecelerBE.DereceAdGetir(item.DereceId).Data,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SoruKategorilerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SoruKategorilerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SoruKategorileriGetirDereceId
        public Result<List<SoruKategorilerVM>> SoruKategorileriGetirDereceId(Guid dereceId)
        {
            var data = _unitOfWork.sorukategorilerRepository.GetAll(u => u.DereceId == dereceId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<SoruKategorilerVM> returnData = new List<SoruKategorilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruKategorilerVM()
                    {
                        SoruKategorilerId = item.SoruKategorilerId,
                        SoruKategorilerAdi = item.SoruKategorilerAdi,
                        SoruKategorilerKullanimi = item.SoruKategorilerKullanimi,
                        SoruKategorilerPuan = item.SoruKategorilerPuan,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        DereceId = item.DereceId,
                    });
                }
                return new Result<List<SoruKategorilerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SoruKategorilerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SoruKategoriKullaniciId
        public Result<List<SoruKategorilerVM>> SoruKategoriKullaniciId(string userId)
        {
            var data = _unitOfWork.sorukategorilerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<SoruKategorilerVM> returnData = new List<SoruKategorilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruKategorilerVM()
                    {
                        SoruKategorilerId = item.SoruKategorilerId,
                        SoruKategorilerAdi = item.SoruKategorilerAdi,
                        SoruKategorilerKullanimi = item.SoruKategorilerKullanimi,
                        SoruKategorilerPuan = item.SoruKategorilerPuan,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        DereceId = item.DereceId,
                    });
                }
                return new Result<List<SoruKategorilerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SoruKategorilerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SoruKategoriGetir(int id)
        public Result<SoruKategorilerVM> SoruKategoriGetir(Guid id)
        {
            var data = _unitOfWork.sorukategorilerRepository.Get(id);
            if (data != null)
            {
                var soruKategoriler = _mapper.Map<SoruKategoriler, SoruKategorilerVM>(data);
                return new Result<SoruKategorilerVM>(true, ResultConstant.RecordFound, soruKategoriler);
            }
            else
            {
                return new Result<SoruKategorilerVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SoruKategoriEkle
        public Result<SoruKategorilerVM> SoruKategoriEkle(SoruKategorilerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var soruKategori = _mapper.Map<SoruKategorilerVM, SoruKategoriler>(model);
                    soruKategori.KaydedenId = user.LoginId;
                    _unitOfWork.sorukategorilerRepository.Add(soruKategori);
                    _unitOfWork.Save();
                    return new Result<SoruKategorilerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SoruKategorilerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SoruKategorilerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SoruKategoriGuncelle
        public Result<SoruKategorilerVM> SoruKategoriGuncelle(SoruKategorilerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var soruKategori = _mapper.Map<SoruKategorilerVM, SoruKategoriler>(model);
                    soruKategori.KaydedenId = user.LoginId;
                    _unitOfWork.sorukategorilerRepository.Update(soruKategori);
                    _unitOfWork.Save();
                    return new Result<SoruKategorilerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SoruKategorilerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SoruKategorilerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SoruKategoriSil
        public Result<bool> SoruKategoriSil(Guid id)
        {
            var data = _unitOfWork.sorukategorilerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.sorukategorilerRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        } 
        #endregion
    }
}
