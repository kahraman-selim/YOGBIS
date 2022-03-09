using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class SubelerBE : ISubelerBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Donusturuculer
        public SubelerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region SubeleriGetir
        public Result<List<SubelerVM>> SubeleriGetir()
        {

            var data = _unitOfWork.subelerRepository.GetAll(includeProperties: "Kullanici,Okullar").ToList();
            //var subeler = _mapper.Map<List<Subeler>, List<SubelerVM>>(data);

            if (data != null)
            {
                List<SubelerVM> returnData = new List<SubelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SubelerVM()
                    {
                        SubeId=item.SubeId,
                        SubeAdi=CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.SubeAdi.ToString()),
                        KayitTarihi=item.KayitTarihi,
                        SubeAcilisTarihi=item.SubeAcilisTarihi,
                        OkulId=item.OkulId,                        
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SubelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SubelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SubeleriGetirOkulId
        public Result<List<SubelerVM>> SubeleriGetirOkulId(Guid OkulId)
        {
            var data = _unitOfWork.subelerRepository.GetAll(u => u.OkulId == OkulId, includeProperties: "Kullanici,Okullar").ToList();
            if (data != null)
            {
                List<SubelerVM> returnData = new List<SubelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SubelerVM()
                    {
                        SubeId = item.SubeId,
                        SubeAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.SubeAdi.ToString()),
                        KayitTarihi = item.KayitTarihi,
                        SubeAcilisTarihi = item.SubeAcilisTarihi,
                        OkulId = item.OkulId,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SubelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SubelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SubeGetirKullaniciId
        public Result<List<SubelerVM>> SubeGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.subelerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,Okullar").ToList();
            if (data != null)
            {
                List<SubelerVM> returnData = new List<SubelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SubelerVM()
                    {
                        SubeId = item.SubeId,
                        SubeAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.SubeAdi.ToString()),
                        KayitTarihi = item.KayitTarihi,
                        SubeAcilisTarihi = item.SubeAcilisTarihi,
                        OkulId = item.OkulId,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SubelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SubelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SubeGetir(Guid id)
        public Result<SubelerVM> SubeGetir(Guid id)
        {

            if (id != null)
            {
                var data = _unitOfWork.subelerRepository.GetFirstOrDefault(s => s.SubeId == id, includeProperties: "Kullanici,Okullar");
                if (data != null)
                {
                    SubelerVM sube = new SubelerVM();
                    sube.SubeId = data.SubeId;
                    sube.SubeAdi= CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.SubeAdi.ToString());
                    sube.KayitTarihi = data.KayitTarihi;
                    sube.SubeAcilisTarihi = data.SubeAcilisTarihi;
                    sube.OkulId = data.OkulId; 
                    sube.KaydedenId = data.KaydedenId;
                    sube.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    return new Result<SubelerVM>(true, ResultConstant.RecordFound, sube);
                }
                else
                {
                    return new Result<SubelerVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<SubelerVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SubeEkle
        public Result<SubelerVM> SubeEkle(SubelerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var subeler = new Subeler()
                    {
                        SubeAdi=model.SubeAdi.ToLower(),
                        SubeAcilisTarihi=model.SubeAcilisTarihi,
                        OkulId=model.OkulId,
                        KayitTarihi = model.KayitTarihi,
                        KaydedenId=user.LoginId                       
                    };

                    _unitOfWork.subelerRepository.Add(subeler);
                    _unitOfWork.Save();
                    return new Result<SubelerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SubelerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SubelerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SubeGuncelle
        public Result<SubelerVM> SubeGuncelle(SubelerVM model, SessionContext user)
        {
            if (model.SubeId != null)
            {
                var data = _unitOfWork.subelerRepository.Get(model.SubeId);
                if (data != null)
                {
                    data.SubeAdi = model.SubeAdi.ToLower();
                    data.SubeAcilisTarihi = model.SubeAcilisTarihi;
                    data.OkulId = model.OkulId;
                    data.KayitTarihi = model.KayitTarihi;
                    data.KaydedenId = user.LoginId;

                    _unitOfWork.subelerRepository.Update(data);
                    _unitOfWork.Save();
                    return new Result<SubelerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                else
                {
                    return new Result<SubelerVM>(false, "Lütfen kayıt seçiniz");
                }

            }
            else
            {
                return new Result<SubelerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SubeSil
        public Result<bool> SubeSil(Guid id)
        {
            var data = _unitOfWork.subelerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.subelerRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region SubeAdGetir(Guid id)
        public Result<string> SubeAdGetir(Guid id)
        {
            if (id==null)
            {
                var subeadi = "";
                return new Result<string>(true, ResultConstant.RecordFound, subeadi);
            }
            else
            {
                var data = _unitOfWork.subelerRepository.Get(id);
                if (data != null)
                {
                    var subeadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.SubeAdi.ToString());
                    return new Result<string>(true, ResultConstant.RecordFound, subeadi);
                }
                else
                {
                    return new Result<string>(false, ResultConstant.RecordNotFound);
                }
            }
        }
        #endregion
    }
}
