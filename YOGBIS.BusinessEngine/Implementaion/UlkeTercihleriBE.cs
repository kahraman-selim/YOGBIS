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
        public Result<List<UlkeTercihVM>> UlkeTercihleriGetir(int year)
        {
            try
            {
                var data = _unitOfWork.ulkeTercihRepository
                    .GetAll(includeProperties: "Kullanici,SoruDereceler,Mulakatlar,UlkeTercihBranslar")
                    .Where(t => t.Mulakatlar.BaslamaTarihi.Year == year)
                    .OrderBy(t => t.SoruDereceler.DereceKodu) 
                    .ThenBy(t => t.UlkeTercihSiraNo)
                    .ThenBy(t => t.YabancıDil)
                    .ToList();

                if (data == null || !data.Any())
                {
                    return new Result<List<UlkeTercihVM>>(false, ResultConstant.RecordNotFound, default(List<UlkeTercihVM>));
                }

                var returnData = data.Select(item => new UlkeTercihVM
                {
                    UlkeTercihId = item.UlkeTercihId,
                    UlkeTercihAdi = item.UlkeTercihAdi,
                    UlkeTercihSiraNo = item.UlkeTercihSiraNo,
                    YabancıDil = item.YabancıDil,
                    DereceId = item.DereceId != null ? item.DereceId : Guid.Empty,
                    DereceAdi = item.SoruDereceler != null ? item.SoruDereceler.DereceAdi : string.Empty,
                    DereceKodu = item.SoruDereceler?.DereceKodu ?? 0,
                    MulakatId = item.MulakatId != null ? item.MulakatId : Guid.Empty,
                    MulakatYil = item.Mulakatlar != null ? item.Mulakatlar.BaslamaTarihi.Year : 0,
                    KayitTarihi = item.KayitTarihi,
                    KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                    KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    UlkeTercihBranslar = item.UlkeTercihBranslar?
                        .Select(b => new UlkeTercihBranslarVM
                        {
                            TercihBransId = b.TercihBransId,
                            BransAdi = b.BransAdi,
                            BransId=b.BransId,
                            BransCinsiyet = b.BransCinsiyet,
                            BransKontSayi = b.BransKontSayi,
                            EsitBrans = b.EsitBrans,
                            YabanciDil = b.YabanciDil,
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
                return new Result<List<UlkeTercihVM>>(false, ResultConstant.RecordNotFound, default(List<UlkeTercihVM>));
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
                        DereceKodu = data.SoruDereceler?.DereceKodu ?? 0,
                        MulakatId = data.MulakatId != null ? data.MulakatId : Guid.Empty,
                        MulakatYil = data.Mulakatlar != null ? data.Mulakatlar.BaslamaTarihi.Year : 0,
                        KayitTarihi = data.KayitTarihi,
                        KaydedenId = data.Kullanici != null ? data.KaydedenId : string.Empty,
                        KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty,
                        UlkeTercihBranslar = data.UlkeTercihBranslar?.Select(b => new UlkeTercihBranslarVM
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

            return new Result<UlkeTercihVM>(false, ResultConstant.RecordNotFound, default(UlkeTercihVM));
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
                    return new Result<UlkeTercihVM>(true, ResultConstant.RecordCreateSuccess, model);
                }
                catch (Exception ex)
                {

                    return new Result<UlkeTercihVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString(), default(UlkeTercihVM));
                }
            }
            else
            {
                return new Result<UlkeTercihVM>(false, "Boş veri olamaz", default(UlkeTercihVM));
            }
        }
        #endregion

        #region UlkeTercihGuncelle
        public Result<bool> UlkeTercihGuncelle(UlkeTercihVM model, SessionContext user)
        {
            try
            {
                var ulkeTercihi = _unitOfWork.ulkeTercihRepository.GetFirstOrDefault(u => u.UlkeTercihId == model.UlkeTercihId);
                if (ulkeTercihi != null)
                {
                    ulkeTercihi.UlkeTercihAdi = model.UlkeTercihAdi;
                    ulkeTercihi.YabancıDil = model.YabancıDil;
                    ulkeTercihi.DereceId = model.DereceId;
                    ulkeTercihi.MulakatId = model.MulakatId;
                    ulkeTercihi.UlkeTercihSiraNo = model.UlkeTercihSiraNo;
                    ulkeTercihi.KaydedenId = user.LoginId;

                    _unitOfWork.ulkeTercihRepository.Update(ulkeTercihi);
                    _unitOfWork.Save();

                    return new Result<bool>(true, "Ülke tercihi başarıyla güncellendi.", true);
                }
                
                return new Result<bool>(false, "Güncellenecek ülke tercihi bulunamadı.", false);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, $"Ülke tercihi güncellenirken bir hata oluştu: {ex.Message}", false);
            }
        }
        #endregion

        #region UlkeTercihSil
        public Result<bool> UlkeTercihSil(Guid id)
        {
            try
            {
                var ulkeTercihi = _unitOfWork.ulkeTercihRepository.GetFirstOrDefault(u => u.UlkeTercihId == id);
                if (ulkeTercihi != null)
                {
                    _unitOfWork.ulkeTercihRepository.Remove(ulkeTercihi);
                    _unitOfWork.Save();

                    return new Result<bool>(true, "Ülke tercihi başarıyla silindi.", true);
                }
                
                return new Result<bool>(false, "Silinecek ülke tercihi bulunamadı.", false);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, $"Ülke tercihi silinirken bir hata oluştu: {ex.Message}", false);
            }
        }
        #endregion

        #region BransEkle
        public Result<bool> BransEkle(Guid ulkeTercihId, Guid tercihBransId, string yabancıDil, int bransKontSayi, string bransCinsiyet, bool esitBrans, SessionContext user)
        {
            try
            {
                // Ülke tercihini kontrol et
                var ulkeTercihi = _unitOfWork.ulkeTercihRepository.GetFirstOrDefault(x => x.UlkeTercihId == ulkeTercihId);
                if (ulkeTercihi == null)
                {
                    return new Result<bool>(false, "Ülke tercihi bulunamadı.", false);
                }

                // Branşı kontrol et
                var brans = _unitOfWork.branslarRepository.GetFirstOrDefault(x => x.BransId == tercihBransId);
                if (brans == null)
                {
                    return new Result<bool>(false, "Seçilen branş bulunamadı.", false);
                }

                // Yeni branş nesnesi oluştur
                var yeniBrans = new UlkeTercihBranslar
                {
                    UlkeTercihId = ulkeTercihId,
                    BransId = tercihBransId,
                    BransAdi = brans.BransAdi,
                    YabanciDil = yabancıDil,
                    BransCinsiyet = bransCinsiyet,
                    BransKontSayi = bransKontSayi,
                    EsitBrans = esitBrans,
                    KayitTarihi = DateTime.Now,
                    KaydedenId = user.LoginId                    
                };

                _unitOfWork.ulkeTercihBransRepository.Add(yeniBrans);
                _unitOfWork.Save();

                return new Result<bool>(true, "Branş başarıyla eklendi.", true);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, $"Branş eklenirken bir hata oluştu: {ex.Message}", false);
            }
        }
        #endregion
    }
}
