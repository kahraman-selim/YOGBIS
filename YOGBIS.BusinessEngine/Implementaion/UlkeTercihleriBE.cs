using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.Exceptions;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Implementaion
{
    public class UlkeTercihleriBE:IUlkeTercihleriBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDerecelerBE _derecelerBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        #endregion

        #region Donusturuculer
        public UlkeTercihleriBE(IUnitOfWork unitOfWork, IMapper mapper, IDerecelerBE derecelerBE,IMulakatOlusturBE mulakatOlusturBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _derecelerBE = derecelerBE;
            _mulakatOlusturBE = mulakatOlusturBE;
        }
        #endregion

        #region UlkeTercihleriGetir
        public Result<List<UlkeTercihVM>> UlkeTercihleriGetir()
        {

            try
            {
                var data = _unitOfWork.ulkeTercihRepository.GetAll(includeProperties: "Kullanici,SoruDereceler").OrderBy(t => t.UlkeTercihSiraNo).ToList();

                // Veri yoksa hata fırlat
                if (data == null || !data.Any())
                {
                    return new Result<List<UlkeTercihVM>>(false, ResultConstant.RecordNotFound);
                }

                // Mapping işlemi
                var tercihler = _mapper.Map<List<UlkeTercih>, List<UlkeTercihVM>>(data);

                // Mapping sonrası kontrol
                if (tercihler == null || !tercihler.Any())
                {
                    return new Result<List<UlkeTercihVM>>(false, ResultConstant.RecordNotFound);
                }

                // Verileri işle ve döndür
                var returnData = data.Select(item => new UlkeTercihVM
                {
                    UlkeTercihId = item.UlkeTercihId,
                    UlkeTercihAdi = item.UlkeTercihAdi,
                    UlkeTercihSiraNo = item.UlkeTercihSiraNo,
                    YabancıDil = item.YabancıDil,
                    DereceId = item.DereceId !=null ? item.DereceId : Guid.Empty,
                    DereceAdi= item.SoruDereceler.DereceAdi, 
                    MulakatId = item.Mulakatlar != null ? item.MulakatId : Guid.Empty,
                    MulakatYil = item.Mulakatlar != null ? item.Mulakatlar.BaslamaTarihi.Year : 0,
                    KayitTarihi = item.KayitTarihi,
                    KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                    KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,

                }).ToList();

                return new Result<List<UlkeTercihVM>>(true, ResultConstant.RecordFound, returnData);
            }
            catch (Exception ex)
            {
                return new Result<List<UlkeTercihVM>>(false, $"Dereceler getirilirken bir hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region UlkeTercihGetir(Guid id)
        public Result<UlkeTercihVM> UlkeTercihGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.ulkeTercihRepository.GetFirstOrDefault(x => x.UlkeTercihId == id, includeProperties: "Kullanici,SoruDereceler");

                if (data != null)
                {
                    UlkeTercihVM tercihulke = new UlkeTercihVM();

                    tercihulke.UlkeTercihId = data.UlkeTercihId;
                    tercihulke.UlkeTercihAdi = data.UlkeTercihAdi;
                    tercihulke.UlkeTercihSiraNo = data.UlkeTercihSiraNo;
                    tercihulke.YabancıDil = data.YabancıDil;
                    tercihulke.DereceId = data.DereceId != null ? data.DereceId : Guid.Empty;
                    tercihulke.DereceAdi = data.SoruDereceler.DereceAdi;
                    tercihulke.MulakatId = data.MulakatId != null ? data.MulakatId : Guid.Empty;
                    tercihulke.MulakatYil = data.Mulakatlar != null ? data.Mulakatlar.BaslamaTarihi.Year : 0;
                    tercihulke.KayitTarihi = data.KayitTarihi;
                    tercihulke.KaydedenId = data.Kullanici != null ? data.KaydedenId : string.Empty;
                    tercihulke.KaydedenAdi = data.Kullanici !=null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;


                    return new Result<UlkeTercihVM>(true, ResultConstant.RecordFound, tercihulke);
                }
                else
                {
                    return new Result<UlkeTercihVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<UlkeTercihVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region UlkeTercihEkle
        public Result<UlkeTercihVM> UlkeTercihEkle(UlkeTercihVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var ulketercih = _mapper.Map<UlkeTercihVM, UlkeTercih>(model);
                    ulketercih.KaydedenId = user.LoginId;

                    _unitOfWork.ulkeTercihRepository.Add(ulketercih);
                    _unitOfWork.Save();
                    return new Result<UlkeTercihVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<UlkeTercihVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<UlkeTercihVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region UlkeTercihGuncelle
        public Result<UlkeTercihVM> UlkeTercihGuncelle(UlkeTercihVM model, SessionContext user)
        {
            if (model.UlkeTercihId != null)
            {
                try
                {
                    var data = _unitOfWork.ulkeTercihRepository.Get(model.UlkeTercihId);
                    if (data != null)
                    {
                        data.UlkeTercihAdi = model.UlkeTercihAdi;
                        data.UlkeTercihSiraNo = model.UlkeTercihSiraNo;
                        data.YabancıDil = model.YabancıDil;
                        data.DereceId = model.DereceId;
                        data.MulakatId = model.MulakatId;
                        data.KaydedenId = model.KaydedenId;
                        data.KayitTarihi = model.KayitTarihi;

                        _unitOfWork.ulkeTercihRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<UlkeTercihVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<UlkeTercihVM>(false, "Lütfen kayıt seçiniz");
                    }
                }
                catch (Exception ex)
                {

                    return new Result<UlkeTercihVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<UlkeTercihVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region UlkeTercihSil
        public Result<bool> UlkeTercihSil(Guid id)
        {
            var data = _unitOfWork.ulkeTercihRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.ulkeTercihRepository.Remove(data);
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
