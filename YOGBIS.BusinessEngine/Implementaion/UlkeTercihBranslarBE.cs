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
                    return new Result<List<UlkeTercihBranslarVM>>(false, ResultConstant.RecordNotFound);
                }

                // Mapping işlemi
                var branslar = _mapper.Map<List<UlkeTercihBranslar>, List<UlkeTercihBranslarVM>>(data);

                // Mapping sonrası kontrol
                if (branslar == null || !branslar.Any())
                {
                    return new Result<List<UlkeTercihBranslarVM>>(false, ResultConstant.RecordNotFound);
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
                return new Result<List<UlkeTercihBranslarVM>>(false, $"Dereceler getirilirken bir hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region BransGetir(Guid id)
        public Result<UlkeTercihBranslarVM> UlkeTercihBransGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.ulkeTercihBransRepository.GetFirstOrDefault(x => x.TercihBransId == id, includeProperties: "Kullanici,UlkeTercih");

                if (data != null)
                {
                    UlkeTercihBranslarVM brans = new UlkeTercihBranslarVM();

                    brans.TercihBransId = data.TercihBransId;
                    brans.BransAdi = data.BransAdi;
                    brans.BransId = data.BransId;
                    brans.BransCinsiyet = data.BransCinsiyet;
                    brans.BransKontSayi = data.BransKontSayi;
                    brans.EsitBrans = data.EsitBrans;
                    brans.UlkeTercihId = data.UlkeTercih != null ? data.UlkeTercihId : Guid.Empty;
                    brans.KayitTarihi = data.KayitTarihi;
                    brans.KaydedenId = data.Kullanici != null ? data.KaydedenId : string.Empty;
                    brans.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;


                    return new Result<UlkeTercihBranslarVM>(true, ResultConstant.RecordFound, brans);
                }
                else
                {
                    return new Result<UlkeTercihBranslarVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<UlkeTercihBranslarVM>(false, ResultConstant.RecordNotFound);
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
                    return new Result<UlkeTercihBranslarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<UlkeTercihBranslarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<UlkeTercihBranslarVM>(false, "Boş veri olamaz");
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
                        return new Result<UlkeTercihBranslarVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<UlkeTercihBranslarVM>(false, "Lütfen kayıt seçiniz");
                    }
                }
                catch (Exception ex)
                {

                    return new Result<UlkeTercihBranslarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<UlkeTercihBranslarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region BranshSil
        public Result<bool> UlkeTercihBransSil(Guid id)
        {
            var data = _unitOfWork.ulkeTercihBransRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.ulkeTercihBransRepository.Remove(data);
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
