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
    public class FotoGaleriBE : IFotoGaleriBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Donusturuculer
        public FotoGaleriBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region FotoGaleriGetir
        public Result<List<FotoGaleriVM>> FotoGaleriGetir()
        {
            var data = _unitOfWork.fotoGaleriRepository.GetAll(includeProperties: "Kullanici").ToList();
            var fotolar = _mapper.Map<List<FotoGaleri>, List<FotoGaleriVM>>(data);

            if (data != null)
            {
                List<FotoGaleriVM> returnData = new List<FotoGaleriVM>();

                foreach (var item in data)
                {
                    returnData.Add(new FotoGaleriVM()
                    {
                        FotoGaleriId=item.FotoGaleriId,
                        FotoAdi=item.FotoAdi,
                        FotoURL=item.FotoURL,
                        KayitTarihi=item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<FotoGaleriVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<FotoGaleriVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region FotoGetirKullaniciId
        public Result<List<FotoGaleriVM>> FotoGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.fotoGaleriRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<FotoGaleriVM> returnData = new List<FotoGaleriVM>();

                foreach (var item in data)
                {
                    returnData.Add(new FotoGaleriVM()
                    {
                        FotoGaleriId=item.FotoGaleriId,
                        FotoAdi=item.FotoAdi,
                        FotoURL=item.FotoURL,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<FotoGaleriVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<FotoGaleriVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region FotoGetir(Guid id)
        public Result<FotoGaleriVM> FotoGetir(Guid id)
        {
            var data = _unitOfWork.fotoGaleriRepository.Get(id);
            if (data != null)
            {
                var dereceler = _mapper.Map<FotoGaleri, FotoGaleriVM>(data);
                return new Result<FotoGaleriVM>(true, ResultConstant.RecordFound, dereceler);
            }
            else
            {
                return new Result<FotoGaleriVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region FotoAdGetir(Guid id)
        public Result<string> FotoAdGetir(Guid id)
        {
            var data = _unitOfWork.fotoGaleriRepository.Get(id);
            if (data != null)
            {
                var fotoadi = data.FotoAdi;
                return new Result<string>(true, ResultConstant.RecordFound, fotoadi);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region FotoURLGetir(Guid id)
        public Result<string> FotoURLGetir(Guid id)
        {
            var data = _unitOfWork.fotoGaleriRepository.Get(id);
            if (data != null)
            {
                var fotourl = data.FotoURL;

                return new Result<string>(true, ResultConstant.RecordFound, fotourl);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region FotoURLGetirUlkeId(Guid id)
        public Result<string[]> FotoURLGetirUlkeId(Guid ulkeId)
        {
            
            var data = _unitOfWork.ulkelerRepository.GetFirstOrDefault(u => u.UlkeId == ulkeId, includeProperties: "FotoGaleri");
            if (data != null)
            {

                foreach (var item in data.FotoGaleri.ToList())
                {
                    var foto = _unitOfWork.fotoGaleriRepository.GetFirstOrDefault(u => u.FotoGaleriId == item.FotoGaleriId);
                    if (foto != null)
                    {
                        string fotourl = foto.FotoURL.ToString();
                    }
                }
                return new Result<string[]>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<string[]>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region FotoEkle
        public Result<FotoGaleriVM> FotoEkle(FotoGaleriVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var fotogaleri = _mapper.Map<FotoGaleriVM, FotoGaleri>(model);
                    fotogaleri.KaydedenId = user.LoginId;
                    _unitOfWork.fotoGaleriRepository.Add(fotogaleri);
                    _unitOfWork.Save();
                    return new Result<FotoGaleriVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<FotoGaleriVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<FotoGaleriVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region FotoGuncelle
        public Result<FotoGaleriVM> FotoGuncelle(FotoGaleriVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var fotogaleri = _mapper.Map<FotoGaleriVM, FotoGaleri>(model);
                    fotogaleri.KaydedenId = user.LoginId;
                    _unitOfWork.fotoGaleriRepository.Update(fotogaleri);
                    _unitOfWork.Save();
                    return new Result<FotoGaleriVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<FotoGaleriVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<FotoGaleriVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region FotoSil
        public Result<bool> FotoSil(Guid id)
        {
            var data = _unitOfWork.fotoGaleriRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.fotoGaleriRepository.Remove(data);
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
