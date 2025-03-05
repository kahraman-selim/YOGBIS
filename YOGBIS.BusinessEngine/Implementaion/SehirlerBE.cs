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

            var data = _unitOfWork.sehirlerRepository.GetAll(includeProperties: "Kullanici,Ulkeler").ToList();
            var sehirler = _mapper.Map<List<Sehirler>, List<SehirlerVM>>(data);

            if (data != null)
            {
                List<SehirlerVM> returnData = new List<SehirlerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SehirlerVM()
                    {
                        SehirId=item.SehirId,
                        SehirAdi= CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.SehirAdi.ToString()),
                        Baskent=item.Baskent,
                        EyaletId=item.EyaletId,
                        //EyaletAdi=item.Eyaletler.EyaletAdi,
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

        #region SehirleriGetirUlkeId
        public Result<List<SehirlerVM>> SehirleriGetirUlkeId(Guid UlkeId)
        {
            var data = _unitOfWork.sehirlerRepository.GetAll(u => u.UlkeId == UlkeId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<SehirlerVM> returnData = new List<SehirlerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SehirlerVM()
                    {
                        SehirId=item.SehirId,
                        SehirAdi= CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.SehirAdi.ToString()),
                        Baskent=item.Baskent,
                        SehirAciklama=item.SehirAciklama,
                        //TemsilciId=item.TemsilciId,
                        EyaletId = item.EyaletId,
                        UlkeId = item.UlkeId,
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
            var data = _unitOfWork.sehirlerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,Ulkeler").ToList();
            if (data != null)
            {
                List<SehirlerVM> returnData = new List<SehirlerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SehirlerVM()
                    {
                        SehirId = item.SehirId,
                        SehirAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.SehirAdi.ToString()),
                        Baskent = item.Baskent,
                        EyaletId = item.EyaletId,
                        //EyaletAdi = item.Eyaletler.EyaletAdi,
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

        #region SehirGetir(Guid id)
        public Result<SehirlerVM> SehirGetir(Guid id)
        {

            if (id != null)
            {
                var data = _unitOfWork.sehirlerRepository.GetFirstOrDefault(s => s.SehirId == id, includeProperties: "Kullanici,Ulkeler");
                if (data != null)
                {
                    SehirlerVM sehir = new SehirlerVM();
                    sehir.KayitTarihi = data.KayitTarihi;
                    sehir.SehirAciklama = data.SehirAciklama;
                    sehir.SehirAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.SehirAdi.ToString());
                    sehir.KaydedenId = data.KaydedenId;
                    sehir.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    return new Result<SehirlerVM>(true, ResultConstant.RecordFound, sehir);
                }
                else
                {
                    return new Result<SehirlerVM>(false, ResultConstant.RecordNotFound);
                }
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
                    var sehirler = new Sehirler()
                    {
                        SehirAdi = model.SehirAdi.ToLower(),
                        EyaletId = (model.EyaletId == null || model.EyaletId.ToString() == "00000000-0000-0000-0000-000000000000") ? null : model.EyaletId,
                        KayitTarihi = model.KayitTarihi,
                        KaydedenId=user.LoginId,
                        UlkeId=model.UlkeId,
                        //TemsilciId = model.TemsilciId != null ? model.TemsilciId : string.Empty,
                    };

                    _unitOfWork.sehirlerRepository.Add(sehirler);
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
            if (model.SehirId != null)
            {
                var data = _unitOfWork.sehirlerRepository.Get(model.SehirId);
                if (data != null)
                {
                    data.SehirAdi = model.SehirAdi.ToLower();
                    data.SehirAciklama = model.SehirAciklama;
                    data.EyaletId= (model.EyaletId == null || model.EyaletId.ToString() == "00000000-0000-0000-0000-000000000000") ? null : model.EyaletId;
                    data.KayitTarihi = model.KayitTarihi;
                    data.KaydedenId = user.LoginId;
                    //data.TemsilciId= model.TemsilciId != null ? model.TemsilciId : string.Empty;
                    data.UlkeId = model.UlkeId;

                    _unitOfWork.sehirlerRepository.Update(data);
                    _unitOfWork.Save();
                    return new Result<SehirlerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                else
                {
                    return new Result<SehirlerVM>(false, "Lütfen kayıt seçiniz");
                }

            }
            else
            {
                return new Result<SehirlerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SehirSil
        public Result<bool> SehirSil(Guid id)
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

        #region SehirAdGetir(Guid id)
        public Result<string> SehirAdGetir(Guid id)
        {
            if (id==null)
            {
                var sehiradi = "";
                return new Result<string>(true, ResultConstant.RecordFound, sehiradi);
            }
            else
            {
                var data = _unitOfWork.sehirlerRepository.Get(id);
                if (data != null)
                {
                    var sehiradi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.SehirAdi.ToString());
                    return new Result<string>(true, ResultConstant.RecordFound, sehiradi);
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
