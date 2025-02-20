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
    public class BranslarBE:IBranslarBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUlkeTercihleriBE _ulkeTercihleriBE;
        #endregion

        #region Dönüştürücüler
        public BranslarBE(IUnitOfWork unitOfWork, IMapper mapper, IUlkeTercihleriBE ulkeTercihleriBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ulkeTercihleriBE = ulkeTercihleriBE;
        }
        #endregion

        #region BranslariGetir
        public Result<List<BranslarVM>> BranslariGetir()
        {

            try
            {
                var data = _unitOfWork.branslarRepository.GetAll(includeProperties: "Kullanici").OrderBy(t => t.BransAdi).ToList();

                // Veri yoksa hata fırlat
                if (data == null || !data.Any())
                {
                    return new Result<List<BranslarVM>>(false, ResultConstant.RecordNotFound);
                }

                // Mapping işlemi
                var branslar = _mapper.Map<List<Branslar>, List<BranslarVM>>(data);

                // Mapping sonrası kontrol
                if (branslar == null || !branslar.Any())
                {
                    return new Result<List<BranslarVM>>(false, ResultConstant.RecordNotFound);
                }

                // Verileri işle ve döndür
                var returnData = data.Select(item => new BranslarVM
                {
                    BransId=item.BransId,
                    BransAdi=item.BransAdi,
                    KayitTarihi = item.KayitTarihi,
                    KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                    KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,

                }).ToList();

                return new Result<List<BranslarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            catch (Exception ex)
            {
                return new Result<List<BranslarVM>>(false, $"Dereceler getirilirken bir hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region BransGetir(Guid id)
        public Result<BranslarVM> BransGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.branslarRepository.GetFirstOrDefault(x => x.BransId == id, includeProperties: "Kullanici");

                if (data != null)
                {
                    BranslarVM brans = new BranslarVM();

                    brans.BransId = data.BransId;
                    brans.BransAdi = data.BransAdi;
                    brans.KayitTarihi = data.KayitTarihi;
                    brans.KaydedenId = data.Kullanici != null ? data.KaydedenId : string.Empty;
                    brans.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;


                    return new Result<BranslarVM>(true, ResultConstant.RecordFound, brans);
                }
                else
                {
                    return new Result<BranslarVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<BranslarVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region BransEkle
        public Result<BranslarVM> BransEkle(BranslarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var brans = _mapper.Map<BranslarVM, Branslar>(model);
                    brans.KaydedenId = user.LoginId;

                    _unitOfWork.branslarRepository.Add(brans);
                    _unitOfWork.Save();
                    return new Result<BranslarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<BranslarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<BranslarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region BransGuncelle
        public Result<BranslarVM> BransGuncelle(BranslarVM model, SessionContext user)
        {
            if (model.BransId != null)
            {
                try
                {
                    var data = _unitOfWork.branslarRepository.Get(model.BransId);
                    if (data != null)
                    {
                        data.BransAdi = model.BransAdi;
                        data.KaydedenId = model.KaydedenId;
                        data.KayitTarihi = model.KayitTarihi;

                        _unitOfWork.branslarRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<BranslarVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<BranslarVM>(false, "Lütfen kayıt seçiniz");
                    }
                }
                catch (Exception ex)
                {

                    return new Result<BranslarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<BranslarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region BranshSil
        public Result<bool> BransSil(Guid id)
        {
            var data = _unitOfWork.branslarRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.branslarRepository.Remove(data);
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
