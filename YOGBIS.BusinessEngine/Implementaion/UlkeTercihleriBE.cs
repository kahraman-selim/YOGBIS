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
                var data = _unitOfWork.ulkeTercihRepository
                    .GetAll(includeProperties: "Kullanici,SoruDereceler,Mulakatlar,TercihBranslar,TercihBranslar.Kullanici")
                    .OrderBy(t => t.UlkeTercihSiraNo)
                    .ThenBy(t => t.YabancıDil)
                    .ToList();

                if (data == null || !data.Any())
                {
                    return new Result<List<UlkeTercihVM>>(false, ResultConstant.RecordNotFound);
                }

                var returnData = data.Select(item => new UlkeTercihVM
                {
                    UlkeTercihId = item.UlkeTercihId,
                    UlkeTercihAdi = item.UlkeTercihAdi,
                    UlkeTercihSiraNo = item.UlkeTercihSiraNo,
                    YabancıDil = item.YabancıDil,
                    DereceId = item.DereceId != null ? item.DereceId : Guid.Empty,
                    DereceAdi = item.SoruDereceler != null ? item.SoruDereceler.DereceAdi : string.Empty,
                    MulakatId = item.MulakatId != null ? item.MulakatId : Guid.Empty,
                    MulakatYil = item.Mulakatlar != null ? item.Mulakatlar.BaslamaTarihi.Year : 0,
                    KayitTarihi = item.KayitTarihi,
                    KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                    KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    TercihBranslar = item.TercihBranslar?
                        .Select(b => new UlkeTercihBranslarVM
                        {
                            TercihBransId = b.TercihBransId,
                            BransAdi = b.BransAdi,
                            BransId=b.BransId,
                            BransCinsiyet = b.BransCinsiyet,
                            BransKontSayi = b.BransKontSayi,
                            EsitBrans = b.EsitBrans,
                            UlkeTercihId = b.UlkeTercihId,
                            KayitTarihi = b.KayitTarihi,
                            KaydedenId = b.KaydedenId,
                            KaydedenAdi = b.Kullanici != null ? b.Kullanici.Ad + " " + b.Kullanici.Soyad : string.Empty
                        })
                        .OrderBy(b => !b.EsitBrans)
                        .ThenBy(b => b.BransCinsiyet)
                        .ThenBy(b => b.BransAdi)
                        .ToList() ?? new List<UlkeTercihBranslarVM>()
                }).ToList();

                return new Result<List<UlkeTercihVM>>(true, ResultConstant.RecordFound, returnData);
            }
            catch (Exception ex)
            {
                return new Result<List<UlkeTercihVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region UlkeTercihGetir
        public Result<UlkeTercihVM> UlkeTercihGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.ulkeTercihRepository.GetFirstOrDefault(
                    x => x.UlkeTercihId == id, 
                    includeProperties: "Kullanici,SoruDereceler,Mulakatlar");

                if (data != null)
                {
                    var tercihulke = new UlkeTercihVM()
                    {
                        UlkeTercihId = data.UlkeTercihId,
                        UlkeTercihAdi = data.UlkeTercihAdi,
                        UlkeTercihSiraNo = data.UlkeTercihSiraNo,
                        YabancıDil = data.YabancıDil,
                        DereceId = data.DereceId != null ? data.DereceId : Guid.Empty,
                        DereceAdi = data.SoruDereceler != null ? data.SoruDereceler.DereceAdi : string.Empty,
                        MulakatId = data.MulakatId != null ? data.MulakatId : Guid.Empty,
                        MulakatYil = data.Mulakatlar != null ? data.Mulakatlar.BaslamaTarihi.Year : 0,
                        KayitTarihi = data.KayitTarihi,
                        KaydedenId = data.Kullanici != null ? data.KaydedenId : string.Empty,
                        KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty,
                        TercihBranslar = data.TercihBranslar?.Select(b => new UlkeTercihBranslarVM
                        {
                            TercihBransId = b.TercihBransId,
                            BransAdi = b.BransAdi,
                            BransId=b.BransId,
                            BransCinsiyet = b.BransCinsiyet,
                            BransKontSayi = b.BransKontSayi,
                            EsitBrans = b.EsitBrans,
                            UlkeTercihId = b.UlkeTercihId,
                            KayitTarihi = b.KayitTarihi,
                            KaydedenId = b.KaydedenId,
                            KaydedenAdi = b.Kullanici != null ? b.Kullanici.Ad + " " + b.Kullanici.Soyad : string.Empty
                        }).ToList() ?? new List<UlkeTercihBranslarVM>()
                    };

                    return new Result<UlkeTercihVM>(true, ResultConstant.RecordFound, tercihulke);
                }
            }

            return new Result<UlkeTercihVM>(false, ResultConstant.RecordNotFound);
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
                        data.KayitTarihi = model.KayitTarihi;
                        data.KaydedenId = user.LoginId;

                        if (model.TercihBranslar != null)
                        {
                            data.TercihBranslar = new List<UlkeTercihBranslar>();
                            foreach (var item in model.TercihBranslar)
                            {
                                data.TercihBranslar.Add(new UlkeTercihBranslar()
                                {
                                    BransAdi=item.BransAdi,
                                    BransId=item.BransId,
                                    BransCinsiyet=item.BransCinsiyet,
                                    BransKontSayi=item.BransKontSayi,
                                    EsitBrans=item.EsitBrans,
                                    UlkeTercihId=item.UlkeTercihId,
                                    KayitTarihi=item.KayitTarihi                                    
                                });
                            }
                        }

                        _unitOfWork.ulkeTercihRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<UlkeTercihVM>(true, ResultConstant.RecordFound);
                    }
                    else
                    {
                        return new Result<UlkeTercihVM>(false, ResultConstant.RecordNotFound);
                    }
                }
                catch (Exception ex)
                {
                    return new Result<UlkeTercihVM>(false, ResultConstant.RecordNotFound + " " + ex.Message.ToString());
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
