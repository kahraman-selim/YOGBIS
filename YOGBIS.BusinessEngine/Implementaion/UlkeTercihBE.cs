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
    public class UlkeTercihBE:IUlkeTercihleriBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        #endregion

        #region Donusturuculer
        public UlkeTercihBE(IUnitOfWork unitOfWork, IMapper mapper, IMulakatOlusturBE mulakatOlusturBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mulakatOlusturBE = mulakatOlusturBE;
        }
        #endregion

        #region UlkeTercihleriGetir
        public Result<List<UlkeTercihVM>> UlkeTercihleriGetir()
        {
            try
            {
                // 1. Sorguyu IQueryable olarak oluştur (henüz execute etme)
                var query = _unitOfWork.ulkeTercihRepository.GetAll()
                    .AsNoTracking() // Change tracking'i devre dışı bırak
                    .Include(x => x.Mulakatlar)
                    .Include(x => x.Kullanici)
                    .OrderBy(x => x.UlkeTercihSiraNo);

                // 2. Sadece ihtiyaç duyulan alanları seç
                var data = query.Select(item => new UlkeTercihVM
                {
                    UlkeTecihId=item.UlkeTecihId,
                    UlkeTercihAdi=item.UlkeTercihAdi,
                    UlkeTercihSiraNo=item.UlkeTercihSiraNo,
                    YabancıDil=item.YabancıDil,
                    DereceId = item.Mulakatlar.DereceId,
                    MulakatId =item.Mulakatlar!=null?item.MulakatId:Guid.Empty,
                    MulakatYil = item.Mulakatlar.BaslamaTarihi.Date.Year,
                    KayitTarihi = item.KayitTarihi,
                    KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                    KaydedenAdi = item.Kullanici != null ?
                        item.Kullanici.Ad + " " + item.Kullanici.Soyad :
                        string.Empty
                }).ToList();
                if (data != null)
                {
                    return new Result<List<UlkeTercihVM>>(true, ResultConstant.RecordFound, data);
                }

                return new Result<List<UlkeTercihVM>>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                return new Result<List<UlkeTercihVM>>(false, ex.Message);
            }
        }
        #endregion

        #region UlkeTercihGetir(Guid id)
        public Result<UlkeTercihVM> UlkeTercihGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.ulkeTercihRepository.GetFirstOrDefault(x => x.UlkeTecihId == id, includeProperties: "Kullanici,Mulakatlar");

                if (data != null)
                {
                    UlkeTercihVM tercihulke = new UlkeTercihVM();

                    tercihulke.UlkeTecihId = data.UlkeTecihId;
                    tercihulke.UlkeTercihAdi = data.UlkeTercihAdi;
                    tercihulke.UlkeTercihSiraNo = data.UlkeTercihSiraNo;
                    tercihulke.YabancıDil = data.YabancıDil;
                    tercihulke.DereceId = data.Mulakatlar.DereceId;
                    tercihulke.MulakatId = data.MulakatId;
                    tercihulke.MulakatYil = data.Mulakatlar.BaslamaTarihi.Date.Year;
                    tercihulke.KayitTarihi = data.KayitTarihi;
                    tercihulke.KaydedenId = data.KaydedenId;
                    tercihulke.KaydedenAdi = data.Kullanici.Ad + " " + data.Kullanici.Soyad;


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
            if (model != null)
            {
                try
                {
                    var ulketercih = _mapper.Map<UlkeTercihVM, UlkeTercih>(model);
                    ulketercih.KaydedenId = user.LoginId;

                    _unitOfWork.ulkeTercihRepository.Update(ulketercih);
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
