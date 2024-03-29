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
    public class SoruBankasiBE : ISoruBankasiBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IKullaniciBE _kullaniciBE;
        #endregion

        #region Dönüştürücüler
        public SoruBankasiBE(IUnitOfWork unitOfWork, IMapper mapper, IKullaniciBE kullaniciBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _kullaniciBE = kullaniciBE;
        }
        #endregion

        #region SoruGetirKullaniciId
        public Result<List<SoruBankasiVM>> SoruGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.soruBankasiRepository.GetAll(u => u.KaydedenId == userId,
                includeProperties: "Kaydeden").OrderByDescending(s => s.SoruBankasiId).ToList();
            if (data != null)
            {
                List<SoruBankasiVM> returnData = new List<SoruBankasiVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruBankasiVM()
                    {
                        SoruBankasiId = item.SoruBankasiId,
                        Soru = item.Soru,
                        Cevap = item.Cevap,
                        SorulmaSayisi = item.SorulmaSayisi,
                        SoruDurumu = item.SoruDurumu,
                        KaydedenId = item.Kaydeden != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kaydeden != null ? item.Kaydeden.Ad + " " + item.Kaydeden.Soyad : string.Empty,
                        KayitTarihi = item.KayitTarihi
                    });
                }
                return new Result<List<SoruBankasiVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SoruBankasiVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SoruGetirOnaylayanId
        public Result<List<SoruBankasiVM>> SoruGetirOnaylayanId(string userId)
        {
            var data = _unitOfWork.soruBankasiRepository.GetAll(u => u.KaydedenId == userId,
            includeProperties: "Kaydeden,SoruKategoriler").OrderByDescending(s => s.SoruBankasiId).ToList();
            if (data != null)
            {
                List<SoruBankasiVM> returnData = new List<SoruBankasiVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruBankasiVM()
                    {
                        SoruBankasiId = item.SoruBankasiId,
                        Soru = item.Soru,
                        Cevap = item.Cevap,
                        SorulmaSayisi = item.SorulmaSayisi,
                        SoruDurumu = item.SoruDurumu,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kaydeden.Ad + " " + item.Kaydeden.Soyad,
                        KayitTarihi = item.KayitTarihi
                    });
                }
                return new Result<List<SoruBankasiVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SoruBankasiVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SorulariGetir
        public Result<List<SoruBankasiVM>> SorulariGetir()
        {
            var data = _unitOfWork.soruBankasiRepository.GetAll(includeProperties: "SoruKategoriler,Dereceler,Kaydeden,Onaylayan").OrderByDescending(s => s.SoruBankasiId).ToList();
            var soruBankasi = _mapper.Map<List<SoruBankasi>, List<SoruBankasiVM>>(data);

            if (data != null)
            {
                List<SoruBankasiVM> returnData = new List<SoruBankasiVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruBankasiVM()
                    {
                        SoruBankasiId = item.SoruBankasiId,
                        //SoruKategorilerId = item.SoruKategorilerId,
                        //SoruKategorilerAdi = item.SoruKategoriler.SoruKategorilerAdi,
                        Soru = item.Soru,
                        Cevap = item.Cevap,
                        //DereceId = item.DereceId,
                        //DereceAdi = item.Dereceler.DereceAdi,
                        SorulmaSayisi = item.SorulmaSayisi,
                        SoruDurumu = item.SoruDurumu,
                        KaydedenId = item.Kaydeden !=null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kaydeden!=null ? item.Kaydeden.Ad + " " + item.Kaydeden.Soyad:string.Empty,
                        //OnaylayanId = item.Onaylayan != null ? item.OnaylayanId:string.Empty,
                        //OnaylayanAdi = item.Onaylayan != null ? item.Onaylayan.Ad + " " + item.Onaylayan.Soyad : string.Empty,
                        //OnayDurumu = (EnumsSoruOnay)item.OnayDurumu,
                        //OnayDurumuAciklama = EnumExtension<EnumsSoruOnay>.GetDisplayValue((EnumsSoruOnay)item.OnayDurumu),
                        //OnayAciklama = item.OnayAciklama,
                        KayitTarihi = item.KayitTarihi
                    });
                }
                return new Result<List<SoruBankasiVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SoruBankasiVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SoruGetir
        public Result<SoruBankasiVM> SoruGetir(Guid id)
        {
            var data = _unitOfWork.soruBankasiRepository.Get(id);
            if (data != null)
            {
                var soruBankasi = _mapper.Map<SoruBankasi, SoruBankasiVM>(data);
                return new Result<SoruBankasiVM>(true, ResultConstant.RecordFound, soruBankasi);
            }
            else
            {
                return new Result<SoruBankasiVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SoruEkle
        public Result<SoruBankasiVM> SoruEkle(SoruBankasiVM model, SessionContext user, string[] onaylayanId)
        {
            if (model != null)
            {
                try
                {
                    var sorubankasi = new SoruBankasi
                    {
                        Soru = model.Soru,
                        Cevap =model.Cevap,
                        SorulmaSayisi = model.SorulmaSayisi,
                        SoruDurumu = model.SoruDurumu,                        
                        KaydedenId =user.LoginId,
                        KayitTarihi=model.KayitTarihi
                    };
                    _unitOfWork.soruBankasiRepository.Add(sorubankasi);
                    _unitOfWork.Save();
                    ///////////////////////////////////////////////
                    //sorubankasi.SoruOnays = new List<SoruOnay>();
                    foreach (var item in onaylayanId)
                    {
                        var soruonay = new SoruOnay
                        {
                            KayitTarihi = model.KayitTarihi,
                            OnayAciklama = model.OnayDurumuAciklama,
                            OnayDurumu = (int)EnumsSoruOnay.Onaya_Gonderildi,
                            OnaylayanId = item,
                            SoruBankasiId = sorubankasi.SoruBankasiId
                        };
                        _unitOfWork.soruOnayRepository.Add(soruonay);
                        _unitOfWork.Save();
                    }
                    /////////////////////////////////////////////
                    var sorubankasilog = new SoruBankasiLog
                    {
                        Cevap=model.Cevap,
                        DereceId=model.SoruDereceId,
                        KaydedenId=user.LoginId,
                        KayitTarihi=model.KayitTarihi,
                        KayitTuru=(int)EnumKayitTuru.Kayit,
                        SoruBankasiId=sorubankasi.SoruBankasiId,
                        Soru=model.Soru,
                        SoruDurumu=model.SoruDurumu,
                        SorulmaSayisi=model.SorulmaSayisi,
                        SoruKategoriId=model.SoruKategorilerId
                    };
                    _unitOfWork.soruBankasiLogRepository.Add(sorubankasilog);
                    _unitOfWork.Save();
                    /////////////////////////////////////////////
                    var sorukategori = new SoruKategori
                    {
                        SoruId = sorubankasi.SoruBankasiId,
                        KategoriId = model.SoruKategorilerId
                    };
                    _unitOfWork.soruKategoriRepository.Add(sorukategori);
                    _unitOfWork.Save();
                    ////////////////////////////////////////////////
                    var soruderece = new SoruDerece
                    {
                        SoruId = sorubankasi.SoruBankasiId,
                        DereceId = model.SoruDereceId                        
                    };
                    _unitOfWork.soruDereceRepository.Add(soruderece);
                    _unitOfWork.Save();




                    return new Result<SoruBankasiVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SoruBankasiVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SoruBankasiVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SoruGuncelle
        public Result<SoruBankasiVM> SoruGuncelle(SoruBankasiVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var soruBankasi = _mapper.Map<SoruBankasiVM, SoruBankasi>(model);
                    soruBankasi.KaydedenId = user.LoginId;
                    //soruBankasi.OnayDurumu = (int)EnumsSoruOnay.Onaya_Gonderildi;
                    _unitOfWork.soruBankasiRepository.Update(soruBankasi);
                    _unitOfWork.Save();
                    return new Result<SoruBankasiVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SoruBankasiVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SoruBankasiVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SoruSil
        public Result<bool> SoruSil(Guid id)
        {
            var data = _unitOfWork.soruBankasiRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.soruBankasiRepository.Remove(data);
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
