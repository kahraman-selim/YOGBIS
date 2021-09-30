﻿using AutoMapper;
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
    public class SoruKategorileriBE : ISoruKategorileriBE
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SoruKategorileriBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Result<List<SoruKategorilerVM>> GetAllSoruKategoriler()
        {
            var data = _unitOfWork.sorukategorilerRepository.GetAll().ToList();
            var soruKategoriler = _mapper.Map<List<SoruKategoriler>, List<SoruKategorilerVM>>(data);
            return new Result<List<SoruKategorilerVM>>(true, ResultConstant.RecordFound, soruKategoriler);
        }
        public Result<List<SoruKategorilerVM>> GetAllByKullaniciId(string userId)
        {
            var data = _unitOfWork.sorukategorilerRepository.GetAll(u => u.KullaniciId == userId,
                includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<SoruKategorilerVM> returnData = new List<SoruKategorilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruKategorilerVM()
                    {
                        SoruKategorilerId = item.SoruKategorilerId,
                        SoruKategorilerAdi=item.SoruKategorilerAdi,
                        SoruKategorilerDerece=item.SoruKategorilerDerece,
                        SoruKategorilerKullanimi=item.SoruKategorilerKullanimi,
                        SoruKategorilerPuan=item.SoruKategorilerPuan,
                        KayitTarihi = DateTime.Today,
                        KullaniciId=item.KullaniciId
                    });
                }
                return new Result<List<SoruKategorilerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SoruKategorilerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<SoruKategorilerVM> GetAllSoruKategoriler(int id)
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
        public Result<SoruKategorilerVM> SoruKategoriEkle(SoruKategorilerVM model)
        {
            if (model != null)
            {
                try
                {
                    var soruKategori = _mapper.Map<SoruKategorilerVM, SoruKategoriler>(model);
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
        public Result<SoruKategorilerVM> SoruKategoriGuncelle(SoruKategorilerVM model)
        {
            if (model != null)
            {
                try
                {
                    var soruKategori = _mapper.Map<SoruKategorilerVM, SoruKategoriler>(model);
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
        public Result<bool> SoruKategoriSil(int id)
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
    }
}
