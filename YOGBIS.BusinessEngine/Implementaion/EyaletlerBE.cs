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
    public class EyaletlerBE : IEyaletlerBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Donusturuculer
        public EyaletlerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region EyaletleriGetir
        public Result<List<EyaletlerVM>> EyaletleriGetir()
        {

            var data = _unitOfWork.eyaletlerRepository.GetAll(includeProperties: "Kullanici,Ulkeler").ToList();
            var eyaletler = _mapper.Map<List<Eyaletler>, List<EyaletlerVM>>(data);

            if (data != null)
            {
                List<EyaletlerVM> returnData = new List<EyaletlerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EyaletlerVM()
                    {
                        EyaletId=item.EyaletId,
                        EyaletAdi=item.EyaletAdi,
                        EyaletAciklama=item.EyaletAciklama,
                        EyaletVatandas=item.EyaletVatandas,
                        UlkeId=item.UlkeId,
                        UlkeAdi=item.Ulkeler.UlkeAdi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<EyaletlerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<EyaletlerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region EyaletGetirKullaniciId
        public Result<List<EyaletlerVM>> EyaletGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.eyaletlerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,Ulkeler").ToList();
            if (data != null)
            {
                List<EyaletlerVM> returnData = new List<EyaletlerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EyaletlerVM()
                    {
                        EyaletId = item.EyaletId,
                        EyaletAdi = item.EyaletAdi,
                        EyaletAciklama = item.EyaletAciklama,
                        EyaletVatandas = item.EyaletVatandas,
                        UlkeId = item.UlkeId,
                        UlkeAdi = item.Ulkeler.UlkeAdi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<EyaletlerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<EyaletlerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region EyaletGetir(int id)
        public Result<EyaletlerVM> EyaletGetir(int id)
        {
            var data = _unitOfWork.eyaletlerRepository.Get(id);
            if (data != null)
            {
                var eyaletler = _mapper.Map<Eyaletler, EyaletlerVM>(data);
                return new Result<EyaletlerVM>(true, ResultConstant.RecordFound, eyaletler);
            }
            else
            {
                return new Result<EyaletlerVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region EyaletEkle
        public Result<EyaletlerVM> EyaletEkle(EyaletlerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var eyalet = _mapper.Map<EyaletlerVM, Eyaletler>(model);
                    eyalet.KaydedenId = user.LoginId;
                    _unitOfWork.eyaletlerRepository.Add(eyalet);
                    _unitOfWork.Save();
                    return new Result<EyaletlerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<EyaletlerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<EyaletlerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region EyaletGuncelle
        public Result<EyaletlerVM> EyaletGuncelle(EyaletlerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var eyalet = _mapper.Map<EyaletlerVM, Eyaletler>(model);
                    eyalet.KaydedenId = user.LoginId;
                    _unitOfWork.eyaletlerRepository.Update(eyalet);
                    _unitOfWork.Save();
                    return new Result<EyaletlerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<EyaletlerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<EyaletlerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region EyaletSil
        public Result<bool> EyaletSil(int id)
        {
            var data = _unitOfWork.eyaletlerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.eyaletlerRepository.Remove(data);
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
