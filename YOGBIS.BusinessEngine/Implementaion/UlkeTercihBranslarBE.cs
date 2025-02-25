using AutoMapper;
using Renci.SshNet.Security.Cryptography.Ciphers.Modes;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class UlkeTercihBranslarBE:IUlkeTercihBranslarBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUlkeTercihleriBE _ulkeTercihleriBE;
        #endregion

        #region Dönüştürücüler
        public UlkeTercihBranslarBE(IUnitOfWork unitOfWork, IMapper mapper, IUlkeTercihleriBE ulkeTercihleriBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ulkeTercihleriBE = ulkeTercihleriBE;
        }
        #endregion

        #region BranslariGetir
        public Result<List<UlkeTercihBranslarVM>> UlkeTercihBranslariGetir()
        {

            try
            {
                var data = _unitOfWork.ulkeTercihBransRepository.GetAll(includeProperties: "Kullanici,UlkeTercih").OrderBy(t => t.BransAdi).ToList();

                // Veri yoksa hata fırlat
                if (data == null || !data.Any())
                {
                    return new Result<List<UlkeTercihBranslarVM>>(false, ResultConstant.RecordNotFound, default(List<UlkeTercihBranslarVM>));
                }

                // Mapping işlemi
                var branslar = _mapper.Map<List<UlkeTercihBranslar>, List<UlkeTercihBranslarVM>>(data);

                // Mapping sonrası kontrol
                if (branslar == null || !branslar.Any())
                {
                    return new Result<List<UlkeTercihBranslarVM>>(false, ResultConstant.RecordNotFound, default(List<UlkeTercihBranslarVM>));
                }

                // Verileri işle ve döndür
                var returnData = data.Select(item => new UlkeTercihBranslarVM
                {
                    TercihBransId=item.TercihBransId,
                    BransAdi=item.BransAdi,
                    BransId=item.BransId,
                    BransCinsiyet=item.BransCinsiyet,
                    BransKontSayi=item.BransKontSayi,
                    EsitBrans=item.EsitBrans,
                    UlkeTercihId=item.UlkeTercih != null ? item.UlkeTercihId : Guid.Empty,
                    KayitTarihi = item.KayitTarihi,
                    KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                    KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,

                }).ToList();

                return new Result<List<UlkeTercihBranslarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            catch (Exception ex)
            {
                return new Result<List<UlkeTercihBranslarVM>>(false, $"Dereceler getirilirken bir hata oluştu: {ex.Message}", default(List<UlkeTercihBranslarVM>));
            }
        }
        #endregion

        #region UlkeTercihBransGetir
        public Result<UlkeTercihBranslarVM> UlkeTercihBransGetir(Guid id)
        {
            try
            {
                var brans = _unitOfWork.ulkeTercihBransRepository.GetFirstOrDefault(
                    x => x.TercihBransId == id, 
                    includeProperties: "UlkeTercih,Brans"
                );

                if (brans == null)
                {
                    return new Result<UlkeTercihBranslarVM>(false, "Branş bulunamadı", default(UlkeTercihBranslarVM));
                }

                var tercihBrans = new UlkeTercihBranslarVM
                {
                    TercihBransId = brans.TercihBransId,
                    UlkeTercihId = brans.UlkeTercihId,
                    BransId = brans.BransId,
                    BransAdi = brans.BransAdi,
                    BransCinsiyet = brans.BransCinsiyet,
                    BransKontSayi = brans.BransKontSayi,
                    EsitBrans = brans.EsitBrans,
                    KaydedenId = brans.KaydedenId,
                    KayitTarihi = brans.KayitTarihi,
                    UlkeTercihAdi = brans.UlkeTercih?.UlkeTercihAdi
                };

                return new Result<UlkeTercihBranslarVM>(true, "Branş başarıyla getirildi", tercihBrans);
            }
            catch (Exception ex)
            {
                return new Result<UlkeTercihBranslarVM>(false, $"Branş getirilirken hata oluştu: {ex.Message}", default(UlkeTercihBranslarVM));
            }
        }
        #endregion

        #region BransEkle
        public Result<UlkeTercihBranslarVM> UlkeTercihBransEkle(UlkeTercihBranslarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var brans = _mapper.Map<UlkeTercihBranslarVM, UlkeTercihBranslar>(model);
                    brans.KaydedenId = user.LoginId;

                    _unitOfWork.ulkeTercihBransRepository.Add(brans);
                    _unitOfWork.Save();
                    return new Result<UlkeTercihBranslarVM>(true, ResultConstant.RecordCreateSuccess, default(UlkeTercihBranslarVM));
                }
                catch (Exception ex)
                {

                    return new Result<UlkeTercihBranslarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString(), default(UlkeTercihBranslarVM));
                }
            }
            else
            {
                return new Result<UlkeTercihBranslarVM>(false, "Boş veri olamaz", default(UlkeTercihBranslarVM));
            }
        }
        #endregion

        #region BransGuncelle
        public Result<UlkeTercihBranslarVM> UlkeTercihBransGuncelle(UlkeTercihBranslarVM model, SessionContext user)
        {
            if (model.TercihBransId != null)
            {
                try
                {
                    var data = _unitOfWork.ulkeTercihBransRepository.Get(model.TercihBransId);
                    if (data != null)
                    {
                        data.BransAdi = model.BransAdi;
                        data.BransId = model.BransId;
                        data.BransCinsiyet = model.BransCinsiyet;
                        data.BransKontSayi = model.BransKontSayi;
                        data.EsitBrans = model.EsitBrans;
                        data.UlkeTercihId = model.UlkeTercihId;
                        data.KaydedenId = model.KaydedenId;
                        data.KayitTarihi = model.KayitTarihi;

                        _unitOfWork.ulkeTercihBransRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<UlkeTercihBranslarVM>(true, ResultConstant.RecordCreateSuccess, default(UlkeTercihBranslarVM));
                    }
                    else
                    {
                        return new Result<UlkeTercihBranslarVM>(false, "Lütfen kayıt seçiniz", default(UlkeTercihBranslarVM));
                    }
                }
                catch (Exception ex)
                {

                    return new Result<UlkeTercihBranslarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString(), default(UlkeTercihBranslarVM));
                }
            }
            else
            {
                return new Result<UlkeTercihBranslarVM>(false, "Boş veri olamaz", default(UlkeTercihBranslarVM));
            }
        }
        #endregion

        #region BranshSil
        public Result<bool> UlkeTercihBransSil(Guid id, SessionContext user)
        {
            try
            {
                var data = _unitOfWork.ulkeTercihBransRepository.GetFirstOrDefault(
                    x => x.TercihBransId == id,
                    includeProperties: "UlkeTercih"
                );

                if (data == null)
                {
                    return new Result<bool>(false, "Silinecek branş bulunamadı", false);
                }

                var ulkeTercihId = data.UlkeTercihId;

                _unitOfWork.ulkeTercihBransRepository.Remove(data);
                _unitOfWork.Save();

                return new Result<bool>(true, "Branş başarıyla silindi", true);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, $"Branş silinirken hata oluştu: {ex.Message}", false);
            }
        }
        #endregion
    }
}
