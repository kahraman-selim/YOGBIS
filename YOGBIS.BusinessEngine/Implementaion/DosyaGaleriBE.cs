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
    public class DosyaGaleriBE : IDosyaGaleriBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Donusturuculer
        public DosyaGaleriBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region DosyaGaleriGetir
        public Result<List<DosyaGaleriVM>> DosyaGaleriGetir()
        {
            var data = _unitOfWork.dosyaGaleriRepository.GetAll(includeProperties: "Kullanici").ToList();
            var Dosyalar = _mapper.Map<List<DosyaGaleri>, List<DosyaGaleriVM>>(data);

            if (data != null)
            {
                List<DosyaGaleriVM> returnData = new List<DosyaGaleriVM>();

                foreach (var item in data)
                {
                    returnData.Add(new DosyaGaleriVM()
                    {
                        DosyaGaleriId=item.DosyaGaleriId,
                        DosyaAdi=item.DosyaAdi,
                        DosyaURL=item.DosyaURL,
                        KayitTarihi=item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<DosyaGaleriVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<DosyaGaleriVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DosyaGetirKullaniciId
        public Result<List<DosyaGaleriVM>> DosyaGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.dosyaGaleriRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<DosyaGaleriVM> returnData = new List<DosyaGaleriVM>();

                foreach (var item in data)
                {
                    returnData.Add(new DosyaGaleriVM()
                    {
                        DosyaGaleriId=item.DosyaGaleriId,
                        DosyaAdi=item.DosyaAdi,
                        DosyaURL=item.DosyaURL,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<DosyaGaleriVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<DosyaGaleriVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DosyaGetir(Guid id)
        public Result<DosyaGaleriVM> DosyaGetir(Guid id)
        {
            var data = _unitOfWork.dosyaGaleriRepository.Get(id);
            if (data != null)
            {
                var dereceler = _mapper.Map<DosyaGaleri, DosyaGaleriVM>(data);
                return new Result<DosyaGaleriVM>(true, ResultConstant.RecordFound, dereceler);
            }
            else
            {
                return new Result<DosyaGaleriVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DosyaAdGetir(Guid id)
        public Result<string> DosyaAdGetir(Guid id)
        {
            var data = _unitOfWork.dosyaGaleriRepository.Get(id);
            if (data != null)
            {
                var Dosyaadi = data.DosyaAdi;
                return new Result<string>(true, ResultConstant.RecordFound, Dosyaadi);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DosyaURLGetir(Guid id)
        public Result<string> DosyaURLGetir(Guid id)
        {
            var data = _unitOfWork.dosyaGaleriRepository.Get(id);
            if (data != null)
            {
                var Dosyaurl = data.DosyaURL;

                return new Result<string>(true, ResultConstant.RecordFound, Dosyaurl);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DosyaURLGetirEtkinlikId(Guid id)
        public Result<List<string>> DosyaURLGetirEtkinlikId(Guid etkinlikId)
        {
            
            var data = _unitOfWork.etkinliklerRepository.GetFirstOrDefault(u => u.EtkinlikId == etkinlikId, includeProperties: "DosyaGaleri");
            if (data != null)
            {
                List<string> dosyarurls = new List<string>();

                if (data.DosyaGaleri != null)
                {
                    foreach (var item in data.DosyaGaleri.ToList())
                    {
                        var dosya = _unitOfWork.dosyaGaleriRepository.GetFirstOrDefault(u => u.DosyaGaleriId == item.DosyaGaleriId);
                        if (dosya != null)
                        {
                            dosyarurls.Add(dosya.DosyaURL.ToString());
                        }
                    }

                    return new Result<List<string>>(true, ResultConstant.RecordFound, dosyarurls);
                }
                else
                {
                    return new Result<List<string>>(false, ResultConstant.RecordNotFound);
                }
                
            }
            else
            {
                return new Result<List<string>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DosyaEkle
        public Result<DosyaGaleriVM> DosyaEkle(DosyaGaleriVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var Dosyagaleri = _mapper.Map<DosyaGaleriVM, DosyaGaleri>(model);
                    Dosyagaleri.KaydedenId = user.LoginId;
                    _unitOfWork.dosyaGaleriRepository.Add(Dosyagaleri);
                    _unitOfWork.Save();
                    return new Result<DosyaGaleriVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<DosyaGaleriVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<DosyaGaleriVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region DosyaGuncelle
        public Result<DosyaGaleriVM> DosyaGuncelle(DosyaGaleriVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var Dosyagaleri = _mapper.Map<DosyaGaleriVM, DosyaGaleri>(model);
                    Dosyagaleri.KaydedenId = user.LoginId;
                    _unitOfWork.dosyaGaleriRepository.Update(Dosyagaleri);
                    _unitOfWork.Save();
                    return new Result<DosyaGaleriVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<DosyaGaleriVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<DosyaGaleriVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region DosyaSil
        public Result<bool> DosyaSil(Guid id)
        {
            var data = _unitOfWork.dosyaGaleriRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.dosyaGaleriRepository.Remove(data);
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
