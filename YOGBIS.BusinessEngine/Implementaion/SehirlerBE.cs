using AutoMapper;
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
    public class SehirlerBE : ISehirlerBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Donusturuculer
        public SehirlerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region SehirleriGetir
        public Result<List<SehirlerVM>> SehirleriGetir()
        {

            var data = _unitOfWork.sehirlerRepository.GetAll(includeProperties: "Kullanici,Eyaletler,Ulkeler").ToList();
            var sehirler = _mapper.Map<List<Sehirler>, List<SehirlerVM>>(data);

            if (data != null)
            {
                List<SehirlerVM> returnData = new List<SehirlerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SehirlerVM()
                    {
                        SehirId=item.SehirId,
                        SehirAdi=item.SehirAdi,
                        Baskent=item.Baskent,
                        EyaletId=item.EyaletId,
                        EyaletAdi=item.Eyaletler.EyaletAdi,
                        SehirVatandas=item.SehirVatandas,
                        SehirAciklama=item.SehirAciklama,                        
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SehirlerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SehirlerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SehirGetirKullaniciId
        public Result<List<SehirlerVM>> SehirGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.sehirlerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,Eyaletler,Ulkeler").ToList();
            if (data != null)
            {
                List<SehirlerVM> returnData = new List<SehirlerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SehirlerVM()
                    {
                        SehirId = item.SehirId,
                        SehirAdi = item.SehirAdi,
                        Baskent = item.Baskent,
                        EyaletId = item.EyaletId,
                        EyaletAdi = item.Eyaletler.EyaletAdi,
                        SehirVatandas = item.SehirVatandas,
                        SehirAciklama = item.SehirAciklama,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SehirlerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SehirlerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SehirGetir(int id)
        public Result<SehirlerVM> SehirGetir(int id)
        {
            var data = _unitOfWork.sehirlerRepository.Get(id);
            if (data != null)
            {
                var sehirler = _mapper.Map<Sehirler, SehirlerVM>(data);
                return new Result<SehirlerVM>(true, ResultConstant.RecordFound, sehirler);
            }
            else
            {
                return new Result<SehirlerVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SehirEkle
        public Result<SehirlerVM> SehirEkle(SehirlerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var sehir = _mapper.Map<SehirlerVM, Sehirler>(model);
                    sehir.KaydedenId = user.LoginId;
                    _unitOfWork.sehirlerRepository.Add(sehir);
                    _unitOfWork.Save();
                    return new Result<SehirlerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SehirlerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SehirlerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SehirGuncelle
        public Result<SehirlerVM> SehirGuncelle(SehirlerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var sehir = _mapper.Map<SehirlerVM, Sehirler>(model);
                    sehir.KaydedenId = user.LoginId;
                    _unitOfWork.sehirlerRepository.Update(sehir);
                    _unitOfWork.Save();
                    return new Result<SehirlerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SehirlerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SehirlerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SehirSil
        public Result<bool> SehirSil(int id)
        {
            var data = _unitOfWork.sehirlerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.sehirlerRepository.Remove(data);
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
