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
        private readonly IUlkelerBE _ulkelerBE;
        #endregion

        #region Dönüştürücüler
        public OkullarBE(IUnitOfWork unitOfWork, IMapper mapper, IUlkelerBE ulkelerBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ulkelerBE = ulkelerBE;
        }
        #endregion

        #region OkullarıGetir
        public Result<List<OkullarVM>> OkullariGetir()
        {
            var data = _unitOfWork.okullarRepository.GetAll(includeProperties: "Kullanici,Sehirler,Eyaletler").OrderBy(o => o.OkulAdi).ToList();

            if (data != null)
            {
                List<OkullarVM> returnData = new List<OkullarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkullarVM()
                    {
                        OkulId = (Guid)item.OkulId,
                        OkulKodu = item.OkulKodu,
                        OkulAdi = item.OkulAdi,
                        OkulUlkeId = item.OkulUlkeId,
                        OkulDurumu=item.OkulDurumu,
                        OkulUlkeAdi = _ulkelerBE.UlkeAdGetir((Guid)item.OkulUlkeId).Data,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
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

        #region OkullarıGetirAZ
        public Result<List<OkullarVM>> OkullariGetirAZ()
        {
            var data = _unitOfWork.okullarRepository.GetAll(includeProperties: "Kullanici,Sehirler,Eyaletler").OrderBy(o => o.OkulAdi).ToList();

            if (data != null)
            {
                List<OkullarVM> returnData = new List<OkullarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkullarVM()
                    {
                        OkulId = (Guid)item.OkulId,
                        OkulKodu = item.OkulKodu,
                        OkulAdi = item.OkulAdi,
                        OkulUlkeId = item.OkulUlkeId,
                        OkulDurumu=item.OkulDurumu,
                        OkulUlkeAdi = _ulkelerBE.UlkeAdGetir((Guid)item.OkulUlkeId).Data,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
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

        #region OkulGetir(Guid id)
        public Result<OkullarVM> OkulGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.okullarRepository.GetFirstOrDefault(u => u.OkulId == id); //includeProperties: "Ulkeler,Kullanici"
                if (data != null)
                {
                    OkullarVM okul = new OkullarVM();
                    okul.OkulId = (Guid)data.OkulId;
                    okul.OkulKodu = data.OkulKodu;
                    okul.OkulAdi = data.OkulAdi;
                    okul.OkulDurumu = data.OkulDurumu;
                    okul.OkulUlkeAdi = _ulkelerBE.UlkeAdGetir((Guid)data.OkulUlkeId).Data;
                    okul.OkulUlkeId = data.OkulUlkeId;
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
                    var okullar = new Okullar()
                    {

                        OkulUlkeId = (Guid)model.OkulUlkeId,
                        OkulKodu = model.OkulKodu,
                        OkulAdi = model.OkulAdi,
                        OkulDurumu = model.OkulDurumu,
                        KayitTarihi = model.KayitTarihi,
                        KaydedenId = user.LoginId,                                         
                        EyaletId= null,
                        SehirId= null,
                        
                    };

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
            if (model.OkulId != null)
            {
                try
                {
                    var data = _unitOfWork.okullarRepository.Get((Guid)model.OkulId);
                    if (data != null)
                    {
                        data.OkulKodu = model.OkulKodu;
                        data.OkulAdi = model.OkulAdi;
                        data.OkulUlkeId = (Guid)model.OkulUlkeId;
                        data.OkulDurumu = model.OkulDurumu;
                        data.KayitTarihi = model.KayitTarihi;
                        data.OkulMudurId = model.OkulMudurId != null ? model.OkulMudurId : null;
                        data.KaydedenId = user.LoginId;
                        data.EyaletId = null;
                        data.SehirId = null;

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

        #region OkulDetayGuncelle
        public Result<OkullarVM> OkulDetayGuncelle(OkullarVM model, SessionContext user)
        {
            if (model.OkulId != null)
            {
                try
                {
                    var data = _unitOfWork.okullarRepository.Get(model.OkulId);
                    if (data != null)
                    {
                        data.OkulKodu = model.OkulKodu;
                        data.OkulAdi = model.OkulAdi;
                        data.OkulUlkeId = (Guid)model.OkulUlkeId;
                        data.KayitTarihi = model.KayitTarihi;
                        data.OkulMudurId = model.OkulMudurId;
                        data.KaydedenId = user.LoginId;
                        ///okulmüdürünün ekleyeceği alanlar////////
                        data.OkulLogoURL = model.OkulLogoURL;
                        data.OkulBilgi = model.OkulBilgi;
                        data.OkulAcilisTarihi = model.OkulAcilisTarihi;
                        data.OkulHizmetGecisDonem = model.OkulHizmetGecisDonem;
                        data.OkulKapaliAlan = model.OkulKapaliAlan;
                        data.OkulAcikAlan = model.OkulAcikAlan;
                        data.OkulMulkiDurum = model.OkulMulkiDurum;
                        data.OkulInternetAdresi = model.OkulInternetAdresi;
                        data.OkulEPostaAdresi = model.OkulEPostaAdresi;
                        data.OkulTelefon = model.OkulTelefon;
                        data.EyaletId = (Guid)model.EyaletId;
                        data.SehirId = (Guid)model.SehirId;

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
        public Result<bool> OkulSil(Guid id)
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
