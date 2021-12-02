using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.Extentsion;
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
        #endregion

        #region Dönüştürücüler
        public SoruBankasiBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 
        #endregion

        public Result<List<SoruBankasiVM>> SoruGetirKullaniciId(string userId)
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
                        SoruKategorilerId = item.SoruKategorilerId,
                        SoruKategorilerAdi = item.SoruKategoriler.SoruKategorilerAdi,
                        Soru = item.Soru,
                        Cevap = item.Cevap,
                        DereceId = item.DereceId,
                        DereceAdi = item.Dereceler.DereceAdi,
                        SorulmaSayisi = item.SorulmaSayisi,
                        SoruDurumu = item.SoruDurumu,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kaydeden.Ad +" "+ item.Kaydeden.Soyad,
                        OnaylayanId = item.OnaylayanId,
                        //OnaylayanAdi = item.Onaylayan.Ad,
                        OnayDurumu = (EnumsSoruOnay)item.OnayDurumu,
                        OnayDurumuAciklama = EnumExtension<EnumsSoruOnay>.GetDisplayValue((EnumsSoruOnay)item.OnayDurumu),                        
                        OnayAciklama = item.OnayAciklama,
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
                        SoruKategorilerId = item.SoruKategorilerId,
                        SoruKategorilerAdi = item.SoruKategoriler.SoruKategorilerAdi,
                        Soru = item.Soru,
                        Cevap = item.Cevap,
                        DereceId = item.DereceId,
                        DereceAdi = item.Dereceler.DereceAdi,
                        SorulmaSayisi = item.SorulmaSayisi,
                        SoruDurumu = item.SoruDurumu,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kaydeden.Ad + " " + item.Kaydeden.Soyad,
                        OnaylayanId = item.OnaylayanId,
                        OnaylayanAdi = item.Onaylayan != null ? item.Onaylayan.Ad + " " + item.Onaylayan.Soyad : string.Empty,
                        OnayDurumu = (EnumsSoruOnay)item.OnayDurumu,
                        OnayDurumuAciklama = EnumExtension<EnumsSoruOnay>.GetDisplayValue((EnumsSoruOnay)item.OnayDurumu),
                        OnayAciklama = item.OnayAciklama,
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
        public Result<SoruBankasiVM> SoruGetir(int id)
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
        public Result<SoruBankasiVM> SoruEkle(SoruBankasiVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var soruBankasi= _mapper.Map<SoruBankasiVM, SoruBankasi>(model);
                    soruBankasi.KaydedenId = user.LoginId;
                    soruBankasi.OnayDurumu = (int)EnumsSoruOnay.Onaya_Gonderildi;
                    _unitOfWork.soruBankasiRepository.Add(soruBankasi);
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
        public Result<SoruBankasiVM> SoruGuncelle(SoruBankasiVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var soruBankasi = _mapper.Map<SoruBankasiVM, SoruBankasi>(model);
                    soruBankasi.KaydedenId = user.LoginId;
                    soruBankasi.OnayDurumu = (int)EnumsSoruOnay.Onaya_Gonderildi;
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
        public Result<bool> SoruSil(int id)
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
        public Result<List<SoruBankasiVM>> SoruGetirOnaylayanId(string userId)
        {
            var data = _unitOfWork.soruBankasiRepository.GetAll(u => u.OnaylayanId == userId,
            includeProperties: "Kaydeden,SoruKategoriler").OrderByDescending(s => s.SoruBankasiId).ToList();
            if (data != null)
            {
                List<SoruBankasiVM> returnData = new List<SoruBankasiVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruBankasiVM()
                    {
                        SoruBankasiId = item.SoruBankasiId,
                        SoruKategorilerId = item.SoruKategorilerId,
                        SoruKategorilerAdi = item.SoruKategoriler.SoruKategorilerAdi,
                        Soru = item.Soru,
                        Cevap = item.Cevap,
                        DereceId = item.DereceId,
                        DereceAdi = item.Dereceler.DereceAdi,
                        SorulmaSayisi = item.SorulmaSayisi,
                        SoruDurumu = item.SoruDurumu,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kaydeden.Ad + " " + item.Kaydeden.Soyad,
                        OnaylayanId = item.OnaylayanId,
                        //OnaylayanAdi = item.Onaylayan.Ad,
                        OnayDurumu = (EnumsSoruOnay)item.OnayDurumu,
                        OnayDurumuAciklama = EnumExtension<EnumsSoruOnay>.GetDisplayValue((EnumsSoruOnay)item.OnayDurumu),
                        OnayAciklama = item.OnayAciklama,
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

        public object SoruGetirOnaylayanId()
        {
            throw new NotImplementedException();
        }
    }
}
