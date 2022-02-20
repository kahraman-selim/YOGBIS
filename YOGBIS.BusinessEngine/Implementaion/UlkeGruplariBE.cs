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
    public class UlkeGruplariBE : IUlkeGruplariBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Donusturuculer
        public UlkeGruplariBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region UlkeGruplariGetir
        public Result<List<UlkeGruplariVM>> UlkeGruplariGetir()
        {
            var data = _unitOfWork.ulkeGruplariRepository.GetAll().ToList();
            var ulkegruplari = _mapper.Map<List<UlkeGruplari>, List<UlkeGruplariVM>>(data);
            return new Result<List<UlkeGruplariVM>>(true, ResultConstant.RecordFound, ulkegruplari);

        }
        #endregion

        #region UlkeGrupGetir(Guid id)
        public Result<UlkeGruplariVM> UlkeGrupGetir(Guid id)
        {
            var data = _unitOfWork.ulkeGruplariRepository.Get(id);
            if (data != null)
            {
                var ulkegrup = _mapper.Map<UlkeGruplari, UlkeGruplariVM>(data);
                return new Result<UlkeGruplariVM>(true, ResultConstant.RecordFound, ulkegrup);
            }
            else
            {
                return new Result<UlkeGruplariVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion        

        #region UlkeGrupEkle
        public Result<UlkeGruplariVM> UlkeGrupEkle(UlkeGruplariVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var ulkegrup = _mapper.Map<UlkeGruplariVM, UlkeGruplari>(model);
                    ulkegrup.KaydedenId = user.LoginId;
                    _unitOfWork.ulkeGruplariRepository.Add(ulkegrup);
                    _unitOfWork.Save();
                    return new Result<UlkeGruplariVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<UlkeGruplariVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<UlkeGruplariVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region UlkeGrupGuncelle
        public Result<UlkeGruplariVM> UlkeGrupGuncelle(UlkeGruplariVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var ulkegrup = _mapper.Map<UlkeGruplariVM, UlkeGruplari>(model);
                    ulkegrup.KaydedenId = user.LoginId;
                    _unitOfWork.ulkeGruplariRepository.Update(ulkegrup);
                    _unitOfWork.Save();
                    return new Result<UlkeGruplariVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<UlkeGruplariVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<UlkeGruplariVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region UlkeGrupSil
        public Result<bool> UlkeGrupSil(Guid id)
        {
            var data = _unitOfWork.ulkeGruplariRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.ulkeGruplariRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region UlkeGrupGetirKullaniciId
        public Result<List<UlkeGruplariVM>> UlkeGrupGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.ulkeGruplariRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<UlkeGruplariVM> returnData = new List<UlkeGruplariVM>();

                foreach (var item in data)
                {
                    returnData.Add(new UlkeGruplariVM()
                    {
                        UlkeGrupAdi = item.UlkeGrupAdi,
                        UlkeGrupAciklama = item.UlkeGrupAciklama,
                        KayitTarihi = item.KayitTarihi,
                        KullaniciAdi = item.Kullanici.Ad + " " + item.Kullanici.Soyad,
                        KaydedenId = item.KaydedenId
                    });
                }
                return new Result<List<UlkeGruplariVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<UlkeGruplariVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion
    }
}
