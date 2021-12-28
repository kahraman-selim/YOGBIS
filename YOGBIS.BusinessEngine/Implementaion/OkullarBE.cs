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
    public class OkullarBE : IOkullarBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Dönüştürücüler
        public OkullarBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region OkullarıGetir
        public Result<List<OkullarVM>> OkullariGetir()
        {
            var data = _unitOfWork.okullarRepository.GetAll().OrderBy(u => u.OkulAdi).ToList();
            var okullar = _mapper.Map<List<Okullar>, List<OkullarVM>>(data);
            return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, okullar);

            //var data = _unitOfWork.okullarRepository.GetAll(includeProperties: "Ulkeler,Kullanici").OrderBy(o => o.OkulAdi).OrderBy(u => u.Sehirler.Eyaletler.Ulkeler.UlkeAdi).ToList();
            //var okullar = _mapper.Map<List<Okullar>, List<OkullarVM>>(data);

            //if (data != null)
            //{
            //    List<OkullarVM> returnData = new List<OkullarVM>();

            //    foreach (var item in data)
            //    {
            //        returnData.Add(new OkullarVM()
            //        {
            //            OkulId = item.OkulId,
            //            OkulKodu = item.OkulKodu,
            //            OkulAdi = item.OkulAdi,
            //            //UlkeId = item.UlkeId,
            //            //UlkeAdi = item.Ulkeler.UlkeAdi,
            //            KaydedenId = item.KaydedenId,
            //            //KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
            //        });
            //    }
            //    return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, returnData);
            //}
            //else
            //{
            //    return new Result<List<OkullarVM>>(false, ResultConstant.RecordNotFound);
            //}
        }
        #endregion

        #region OkullarıGetirAZ
        public Result<List<OkullarVM>> OkullariGetirAZ()
        {
            var data = _unitOfWork.okullarRepository.GetAll().OrderBy(o => o.OkulAdi).ToList();     //GetAll(includeProperties: "Ulkeler,Kullanici")       

            if (data != null)
            {
                List<OkullarVM> returnData = new List<OkullarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkullarVM()
                    {
                        OkulId = item.OkulId,
                        OkulKodu = item.OkulKodu,
                        OkulAdi = item.OkulAdi,
                        //UlkeId = item.UlkeId,
                        //UlkeAdi = item.Ulkeler.UlkeAdi,
                        KaydedenId = item.KaydedenId,
                        //KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
                    });
                }
                return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkullarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulGetirKullanıcıId
        public Result<List<OkullarVM>> OkulGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.okullarRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Ulkeler,Kullanici").OrderBy(u => u.OkulAdi).ToList();
            if (data != null)
            {
                List<OkullarVM> returnData = new List<OkullarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkullarVM()
                    {
                        OkulId = item.OkulId,
                        OkulKodu = item.OkulKodu,
                        OkulAdi = item.OkulAdi,
                        //UlkeId = item.UlkeId,
                        //UlkeAdi = item.Ulkeler.UlkeAdi,
                        KaydedenId = item.KaydedenId,
                        //KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
                    });
                }
                return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkullarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulGetir
        public Result<OkullarVM> OkulGetir(int id)
        {
            if (id > 0)
            {
                var data = _unitOfWork.okullarRepository.GetFirstOrDefault(u => u.OkulId == id, includeProperties: "Ulkeler,Kullanici");
                if (data != null)
                {
                    OkullarVM okul = new OkullarVM();
                    okul.OkulId = data.OkulId;
                    okul.OkulKodu = data.OkulKodu;
                    okul.OkulAdi = data.OkulAdi;
                    //okul.UlkeId = data.UlkeId;
                    //okul.UlkeAdi = data.Ulkeler.UlkeAdi;
                    okul.KaydedenId = data.KaydedenId;
                    //okul.KullaniciAdi = data.KullaniciId;
                    //okul.KullaniciAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    return new Result<OkullarVM>(true, ResultConstant.RecordFound, okul);
                }
                else
                {
                    return new Result<OkullarVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<OkullarVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulEkle
        public Result<OkullarVM> OkulEkle(OkullarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    Okullar okullar = new Okullar();
                    okullar.OkulKodu = model.OkulKodu;
                    okullar.OkulAdi = model.OkulAdi;
                    //okullar.UlkeId = model.UlkeId;
                    okullar.KaydedenId = user.LoginId;
                    _unitOfWork.okullarRepository.Add(okullar);
                    _unitOfWork.Save();
                    return new Result<OkullarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<OkullarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<OkullarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region OkulGuncelle
        public Result<OkullarVM> OkulGuncelle(OkullarVM model, SessionContext user)
        {
            if (model.OkulId > 0)
            {
                try
                {
                    var data = _unitOfWork.okullarRepository.Get(model.OkulId);
                    if (data != null)
                    {
                        data.OkulKodu = model.OkulKodu;
                        data.OkulAdi = model.OkulAdi;
                        //data.UlkeId = model.UlkeId;
                        data.KaydedenId = user.LoginId;

                        _unitOfWork.okullarRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<OkullarVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<OkullarVM>(false, "Lütfen kayıt seçiniz");
                    }

                }
                catch (Exception ex)
                {

                    return new Result<OkullarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<OkullarVM>(false, "Lütfen kayıt seçiniz");
            }
        }
        #endregion

        #region Okulsil
        public Result<bool> OkulSil(int id)
        {
            var data = _unitOfWork.okullarRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.okullarRepository.Remove(data);
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
