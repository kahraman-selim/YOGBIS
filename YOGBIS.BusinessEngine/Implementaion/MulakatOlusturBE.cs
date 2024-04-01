﻿using AutoMapper;
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
    public class MulakatOlusturBE : IMulakatOlusturBE
    {

        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Dönüştürücüler
        public MulakatOlusturBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region MulakatlariGetir
        public Result<List<MulakatlarVM>> MulakatlariGetir()
        {
            var data = _unitOfWork.mulakatlarRepository.GetAll(includeProperties: "Kullanici").OrderByDescending(s => s.MulakatId).ToList();
            if (data != null)
            {
                List<MulakatlarVM> returnData = new List<MulakatlarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new MulakatlarVM()
                    {
                        MulakatId=item.MulakatId,
                        OnaySayisi=item.OnaySayisi,
                        OnayTarihi=item.OnayTarihi,
                        KararSayisi=item.KararSayisi,
                        KararTarihi=item.KararTarihi,
                        YazılıSinavTarihi=item.YazılıSinavTarihi,
                        MulakatKategoriId=item.MulakatKategoriId,
                        MulakatAdi=item.MulakatAdi,
                        MulakatDonemi=item.MulakatDonemi,
                        DereceId = item.Dereceler.DereceId,
                        DereceAdi = item.Dereceler.DereceAdi,
                        BaslamaTarihi=item.BaslamaTarihi,
                        BitisTarihi=item.BitisTarihi,
                        AdaySayisi = item.AdaySayisi,
                        SorulanSoruSayisi = (int)item.SorulanSoruSayisi,
                        Durumu =item.Durumu,
                        MulakatAciklama=item.MulakatAciklama,
                        KaydedenId = item.KaydedenId != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,                                              
                        KayitTarihi = item.KayitTarihi
                    });
                }
                return new Result<List<MulakatlarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<MulakatlarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion                

        #region MulakatGetir
        public Result<MulakatlarVM> MulakatGetir(Guid id)
        {
            var data = _unitOfWork.mulakatlarRepository.Get(id);
            if (data != null)
            {
                var mulakatlar = _mapper.Map<Mulakatlar, MulakatlarVM>(data);
                return new Result<MulakatlarVM>(true, ResultConstant.RecordFound, mulakatlar);
            }
            else
            {
                return new Result<MulakatlarVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region MulakatEkle
        public Result<MulakatlarVM> MulakatEkle(MulakatlarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var mulakatlar = _mapper.Map<MulakatlarVM, Mulakatlar>(model);
                    mulakatlar.KaydedenId = user.LoginId;
                    mulakatlar.MulakatDonemi = model.BaslamaTarihi.Day.ToString()+"/"+model.BaslamaTarihi.Month.ToString()+"-"+model.BitisTarihi.Day.ToString()+"/"+model.BitisTarihi.Month.ToString()+"-"+model.BitisTarihi.Year.ToString()+" Dönemi";
                    _unitOfWork.mulakatlarRepository.Add(mulakatlar);
                    _unitOfWork.Save();
                    return new Result<MulakatlarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<MulakatlarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<MulakatlarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region MulakatGuncelle
        public Result<MulakatlarVM> MulakatGuncelle(MulakatlarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var mulakatlar = _mapper.Map<MulakatlarVM, Mulakatlar>(model);
                    mulakatlar.MulakatDonemi = model.BaslamaTarihi.Day.ToString() + "/" + model.BaslamaTarihi.Month.ToString() + "-" + model.BitisTarihi.Day.ToString() + "/" + model.BitisTarihi.Month.ToString() + "-" + model.BitisTarihi.Year.ToString() + " Dönemi";
                    mulakatlar.KaydedenId = user.LoginId;
                    _unitOfWork.mulakatlarRepository.Update(mulakatlar);
                    _unitOfWork.Save();
                    return new Result<MulakatlarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<MulakatlarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<MulakatlarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region MulakatSil
        public Result<bool> MulakatSil(Guid id)
        {
            var data = _unitOfWork.mulakatlarRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.mulakatlarRepository.Remove(data);
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
