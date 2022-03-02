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
    public class OkulBinaBolumBE : IOkulBinaBolumBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Donusturuculer
        public OkulBinaBolumBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region OkulBinaBolumleriGetir
        public Result<List<OkulBinaBolumVM>> OkulBinaBolumleriGetir()
        {

            var data = _unitOfWork.okulBinaBolumRepository.GetAll(includeProperties: "Kullanici,Okullar,Sehirler,Eyaletler,Ulkeler").ToList();
            var bolumler = _mapper.Map<List<OkulBinaBolum>, List<OkulBinaBolumVM>>(data);

            if (data != null)
            {
                List<OkulBinaBolumVM> returnData = new List<OkulBinaBolumVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkulBinaBolumVM()
                    {
                        OkulBinaBolumId=item.OkulBinaBolumId,
                        BolumAdedi=item.BolumAdedi,
                        BolumAdi=item.BolumAdi,
                        KayitTarihi=item.KayitTarihi,
                        OkulId=item.OkulId,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<OkulBinaBolumVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkulBinaBolumVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulBinaBolumGetirKullaniciId
        public Result<List<OkulBinaBolumVM>> OkulBinaBolumGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.okulBinaBolumRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,Okullar,Sehirler,Eyaletler,Ulkeler").ToList();
            if (data != null)
            {
                List<OkulBinaBolumVM> returnData = new List<OkulBinaBolumVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkulBinaBolumVM()
                    {
                        OkulBinaBolumId = item.OkulBinaBolumId,
                        BolumAdedi = item.BolumAdedi,
                        BolumAdi = item.BolumAdi,
                        KayitTarihi = item.KayitTarihi,
                        OkulId = item.OkulId,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<OkulBinaBolumVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkulBinaBolumVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulBinaBolumGetir(Guid id)
        public Result<OkulBinaBolumVM> OkulBinaBolumGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.okulBinaBolumRepository.GetFirstOrDefault(e => e.OkulBinaBolumId == id, includeProperties: "Kullanici,Okullar,Sehirler,Eyaletler,Ulkeler");
                if (data != null)
                {
                    OkulBinaBolumVM binabolum = new OkulBinaBolumVM();
                    binabolum.OkulBinaBolumId = data.OkulBinaBolumId;
                    binabolum.BolumAdedi = data.BolumAdedi;
                    binabolum.BolumAdi = data.BolumAdi;
                    binabolum.KayitTarihi = data.KayitTarihi;
                    binabolum.OkulId = data.OkulId;
                    binabolum.KaydedenId = data.KaydedenId;
                    binabolum.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    return new Result<OkulBinaBolumVM>(true, ResultConstant.RecordFound, binabolum);
                }
                else
                {
                    return new Result<OkulBinaBolumVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<OkulBinaBolumVM>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion

        #region OkulBinaBolumEkle
        public Result<OkulBinaBolumVM> OkulBinaBolumEkle(OkulBinaBolumVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var bolumler = new OkulBinaBolum()
                    {
                        BolumAdedi=model.BolumAdedi,
                        BolumAdi=model.BolumAdi,
                        OkulId=model.OkulId,
                        KaydedenId = user.LoginId,
                        KayitTarihi = model.KayitTarihi
                    };

                    _unitOfWork.okulBinaBolumRepository.Add(bolumler);
                    _unitOfWork.Save();
                    return new Result<OkulBinaBolumVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<OkulBinaBolumVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<OkulBinaBolumVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region OkulBinaBolumGuncelle
        public Result<OkulBinaBolumVM> OkulBinaBolumGuncelle(OkulBinaBolumVM model, SessionContext user)
        {
            if (model.OkulBinaBolumId != null)
            {
                var data = _unitOfWork.okulBinaBolumRepository.Get(model.OkulBinaBolumId);
                if (data != null)
                {
                    data.KaydedenId = user.LoginId;
                    data.KayitTarihi = model.KayitTarihi;
                    data.BolumAdedi = model.BolumAdedi;
                    data.BolumAdi = model.BolumAdi;

                    _unitOfWork.okulBinaBolumRepository.Update(data);
                    _unitOfWork.Save();
                    return new Result<OkulBinaBolumVM>(true, ResultConstant.RecordCreateSuccess);
                }
                else
                {
                    return new Result<OkulBinaBolumVM>(false, "Lütfen kayıt seçiniz");
                }
            }
            else
            {
                return new Result<OkulBinaBolumVM>(false, "Lütfen kayıt seçiniz");
            }
        }
        #endregion

        #region OkulBinaBolumSil
        public Result<bool> OkulBinaBolumSil(Guid id)
        {
            var data = _unitOfWork.okulBinaBolumRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.okulBinaBolumRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region OkulBinaBolumIdGetir
        public Result<Guid> OkulBinaBolumIdGetir(string okulbinabolumAdi)
        {
            if (okulbinabolumAdi == null)
            {
                return new Result<Guid>(false, ResultConstant.RecordFound);
            }
            else
            {
                var data = _unitOfWork.okulBinaBolumRepository.GetFirstOrDefault(x => x.BolumAdi == okulbinabolumAdi);

                if (data != null)
                {

                    var bolumId = data.OkulBinaBolumId;


                    return new Result<Guid>(true, ResultConstant.RecordFound, bolumId);
                }
                else
                {
                    return new Result<Guid>(false, ResultConstant.RecordNotFound);
                }
            }
        }
        #endregion

        #region OkulBinaBolumGetirBolumAdi(string okulbinabolumAdi)
        public Result<OkulBinaBolumVM> OkulBinaBolumGetirBolumAdi(string okulbinabolumAdi)
        {
            if (okulbinabolumAdi != null)
            {
                var bolumId = OkulBinaBolumIdGetir(okulbinabolumAdi).Data;

                var data = _unitOfWork.okulBinaBolumRepository.GetFirstOrDefault(e => e.OkulBinaBolumId == bolumId, includeProperties: "Kullanici,Okullar,Sehirler,Eyaletler,Ulkeler");
                if (data != null)
                {
                    OkulBinaBolumVM binabolum = new OkulBinaBolumVM();

                    binabolum.OkulBinaBolumId = data.OkulBinaBolumId;
                    binabolum.BolumAdedi = data.BolumAdedi;
                    binabolum.BolumAdi = data.BolumAdi;
                    binabolum.KayitTarihi = data.KayitTarihi;
                    binabolum.OkulId = data.OkulId;
                    binabolum.KaydedenId = data.KaydedenId;
                    binabolum.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;


                    return new Result<OkulBinaBolumVM>(true, ResultConstant.RecordFound, binabolum);
                }
                else
                {
                    return new Result<OkulBinaBolumVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<OkulBinaBolumVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulBinaBolumAdGetir(Guid id)
        public Result<string> OkulBinaBolumAdGetir(Guid id)
        {
            var data = _unitOfWork.okulBinaBolumRepository.Get(id);
            if (data != null)
            {
                var bolumadi = data.BolumAdi;
                return new Result<string>(true, ResultConstant.RecordFound, bolumadi);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion
    }
}
