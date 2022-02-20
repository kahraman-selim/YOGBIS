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
    public class DerecelerBE : IDerecelerBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Donusturuculer
        public DerecelerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region DereceleriGetir
        public Result<List<SoruDerecelerVM>> DereceleriGetir()
        {
            //var data = _unitOfWork.derecelerRepository.GetAll().ToList();
            //var dereceler = _mapper.Map<List<Dereceler>, List<DerecelerVM>>(data);
            //return new Result<List<DerecelerVM>>(true, ResultConstant.RecordFound, dereceler);

            var data = _unitOfWork.soruDerecelerRepository.GetAll(includeProperties: "Kullanici").ToList();
            var dereceler = _mapper.Map<List<SoruDereceler>, List<SoruDerecelerVM>>(data);

            if (data != null)
            {
                List<SoruDerecelerVM> returnData = new List<SoruDerecelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruDerecelerVM()
                    {
                        DereceId = item.DereceId,
                        DereceAdi = item.DereceAdi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SoruDerecelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SoruDerecelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DereceGetirKullaniciId
        public Result<List<SoruDerecelerVM>> DereceGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.soruDerecelerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<SoruDerecelerVM> returnData = new List<SoruDerecelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruDerecelerVM()
                    {
                        DereceId = item.DereceId,
                        DereceAdi = item.DereceAdi,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SoruDerecelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SoruDerecelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DereceGetir(Guid id)
        public Result<SoruDerecelerVM> DereceGetir(Guid id)
        {
            var data = _unitOfWork.soruDerecelerRepository.Get(id);
            if (data != null)
            {
                var dereceler = _mapper.Map<SoruDereceler, SoruDerecelerVM>(data);
                return new Result<SoruDerecelerVM>(true, ResultConstant.RecordFound, dereceler);
            }
            else
            {
                return new Result<SoruDerecelerVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DereceAdGetir(Guid id)
        public Result<string> DereceAdGetir(Guid id)
        {
            var data = _unitOfWork.soruDerecelerRepository.Get(id);
            if (data != null)
            {
                var dereceadi = data.DereceAdi;
                return new Result<string>(true, ResultConstant.RecordFound, dereceadi);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DereceEkle
        public Result<SoruDerecelerVM> DereceEkle(SoruDerecelerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var derece = _mapper.Map<SoruDerecelerVM, SoruDereceler>(model);
                    derece.KaydedenId = user.LoginId;
                    _unitOfWork.soruDerecelerRepository.Add(derece);
                    _unitOfWork.Save();
                    return new Result<SoruDerecelerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SoruDerecelerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SoruDerecelerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region DereceGuncelle
        public Result<SoruDerecelerVM> DereceGuncelle(SoruDerecelerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var derece = _mapper.Map<SoruDerecelerVM, SoruDereceler>(model);
                    derece.KaydedenId = user.LoginId;
                    _unitOfWork.soruDerecelerRepository.Update(derece);
                    _unitOfWork.Save();
                    return new Result<SoruDerecelerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SoruDerecelerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SoruDerecelerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region DereceSil
        public Result<bool> DereceSil(Guid id)
        {
            var data = _unitOfWork.soruDerecelerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.soruDerecelerRepository.Remove(data);
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
