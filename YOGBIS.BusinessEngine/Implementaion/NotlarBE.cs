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
    public class NotlarBE : INotlarBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Dönüştürücüler
        public NotlarBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region NotlarıGetir
        public Result<List<NotlarVM>> NotlariGetir()
        {
            var data = _unitOfWork.notlarRepository.GetAll().ToList();
            var notlar = _mapper.Map<List<Notlar>, List<NotlarVM>>(data);
            return new Result<List<NotlarVM>>(true, ResultConstant.RecordFound, notlar);

            //var data = _unitOfWork.derecelerRepository.GetAll(includeProperties: "Kullanici").ToList();
            //var dereceler = _mapper.Map<List<Dereceler>, List<DerecelerVM>>(data);

            //if (data != null)
            //{
            //    List<DerecelerVM> returnData = new List<DerecelerVM>();

            //    foreach (var item in data)
            //    {
            //        returnData.Add(new DerecelerVM()
            //        {
            //            DereceId = item.DereceId,
            //            DereceAdi = item.DereceAdi,
            //            KullaniciAdi = item.Kullanici.Ad + " " + item.Kullanici.Soyad
            //        });
            //    }
            //    return new Result<List<DerecelerVM>>(true, ResultConstant.RecordFound, returnData);
            //}
            //else
            //{
            //    return new Result<List<DerecelerVM>>(false, ResultConstant.RecordNotFound);
            //}
        }
        #endregion

        #region NotGetirKullaniciId
        public Result<List<NotlarVM>> NotGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.notlarRepository.GetAll(u => u.KullaniciId == userId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<NotlarVM> returnData = new List<NotlarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new NotlarVM()
                    {
                        NotId=item.NotId,
                        NotAdi=item.NotAdi,
                        NotDetay=item.NotDetay,
                        NotRenk=item.NotRenk,
                        KayitTarihi = item.KayitTarihi,
                        KullaniciAdi = item.Kullanici.Ad + " " + item.Kullanici.Soyad,
                        KullaniciId = item.KullaniciId
                    });
                }
                return new Result<List<NotlarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<NotlarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region NotGetir(int id)
        public Result<NotlarVM> NotGetir(int id)
        {
            var data = _unitOfWork.notlarRepository.Get(id);
            if (data != null)
            {
                var notlar = _mapper.Map<Notlar, NotlarVM>(data);
                return new Result<NotlarVM>(true, ResultConstant.RecordFound, notlar);
            }
            else
            {
                return new Result<NotlarVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region NotEkle
        public Result<NotlarVM> NotEkle(NotlarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var not = _mapper.Map<NotlarVM, Notlar>(model);
                    not.KullaniciId = user.LoginId;
                    _unitOfWork.notlarRepository.Add(not);
                    _unitOfWork.Save();
                    return new Result<NotlarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<NotlarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<NotlarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region NotGuncelle
        public Result<NotlarVM> NotGuncelle(NotlarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var not = _mapper.Map<NotlarVM, Notlar>(model);
                    not.KullaniciId = user.LoginId;
                    _unitOfWork.notlarRepository.Update(not);
                    _unitOfWork.Save();
                    return new Result<NotlarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<NotlarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<NotlarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region NotSil
        public Result<bool> NotSil(int id)
        {
            var data = _unitOfWork.notlarRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.notlarRepository.Remove(data);
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
