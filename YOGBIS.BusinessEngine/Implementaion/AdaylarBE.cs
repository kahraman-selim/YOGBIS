using AutoMapper;
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
    public class AdaylarBE : IAdaylarBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDerecelerBE _derecelerBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IKomisyonlarBE _komisyonlarBE;
        #endregion

        #region Donustucuruler
        public AdaylarBE(IUnitOfWork unitOfWork, IMapper mapper, IDerecelerBE derecelerBE, IMulakatOlusturBE mulakatOlusturBE, IKomisyonlarBE komisyonlarBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _derecelerBE = derecelerBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _komisyonlarBE = komisyonlarBE;
        }
        #endregion

        #region AdaylariGetir
        public Result<List<AdaylarVM>> AdaylariGetir()
        {
            var data = _unitOfWork.adaylarRepository.GetAll(includeProperties: "Kullanici,Mulakatlar,Komisyonlar").OrderBy(x=>x.KomisyonSN).ThenBy(y=>y.KomisyonGunSN).ToList(); 
            var Adaylar = _mapper.Map<List<Adaylar>, List<AdaylarVM>>(data);

            if (data != null)
            {
                List<AdaylarVM> returnData = new List<AdaylarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new AdaylarVM()
                    {
                        AdayId=item.AdayId,
                        TC=item.TC,
                        Ad=item.Ad,
                        Soyad=item.Soyad,
                        DereceAdi=item.DereceAdi,
                        UlkeTercihi=item.UlkeTercihi,
                        Brans=item.Brans,
                        MYYSTarihi=item.MYYSTarihi, 
                        MYSSSaat=item.MYSSSaat,
                        MYSSKomisyonAdi=item.MYSSKomisyonAdi,
                        KomisyonSN=item.KomisyonSN,
                        KomisyonGunSN = item.KomisyonGunSN,                       
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }

                return new Result<List<AdaylarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else 
            {
                return new Result<List<AdaylarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region AdayGetir(Guid id)
        public Result<AdaylarVM> AdayGetir(Guid id)
        {
            if (id != null) 
            {
                var data = _unitOfWork.adaylarRepository.GetFirstOrDefault(x=>x.AdayId==id, includeProperties: "Kullanici,Mulakatlar,Komisyonlar");

                if (data != null) 
                {
                    AdaylarVM Adaylar = new AdaylarVM();

                    Adaylar.AdayId = data.AdayId;
                    Adaylar.TC=data.TC;
                    Adaylar.Ad=data.Ad;
                    Adaylar.Soyad = data.Soyad;
                    Adaylar.DereceAdi = data.DereceAdi;
                    Adaylar.UlkeTercihi = data.UlkeTercihi;
                    Adaylar.Brans = data.Brans;
                    Adaylar.MYYSTarihi = data.MYYSTarihi;
                    Adaylar.MYSSSaat = data.MYSSSaat;
                    Adaylar.MYSSKomisyonAdi = data.MYSSKomisyonAdi;
                    Adaylar.KomisyonSN = data.KomisyonSN;
                    Adaylar.KomisyonGunSN = data.KomisyonGunSN;
                    Adaylar.KayitTarihi=data.KayitTarihi;
                    Adaylar.KaydedenId = data.KaydedenId;
                    Adaylar.KaydedenAdi = data.Kullanici.Ad + " " + data.Kullanici.Soyad;


                    return new Result<AdaylarVM>(true, ResultConstant.RecordFound, Adaylar);
                }
                else
                {
                    return new Result<AdaylarVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else 
            {
                return new Result<AdaylarVM>(false, ResultConstant.RecordNotFound);
            }           
        }
        #endregion

        #region AdayEkle
        public Result<AdaylarVM> AdayEkle(AdaylarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var Adaylar = _mapper.Map<AdaylarVM, Adaylar>(model);
                    Adaylar.KaydedenId = user.LoginId;

                    _unitOfWork.adaylarRepository.Add(Adaylar);
                    _unitOfWork.Save();
                    return new Result<AdaylarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<AdaylarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<AdaylarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region AdayGuncelle
        public Result<AdaylarVM> AdayGuncelle(AdaylarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var Adaylar = _mapper.Map<AdaylarVM, Adaylar>(model);
                    Adaylar.KaydedenId = user.LoginId;

                    _unitOfWork.adaylarRepository.Update(Adaylar);
                    _unitOfWork.Save();
                    return new Result<AdaylarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<AdaylarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<AdaylarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region AdaySil
        public Result<bool> AdaySil(Guid id)
        {
            var data = _unitOfWork.adaylarRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.adaylarRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region AdayGetirKullaniciId
        public Result<List<AdaylarVM>> AdayGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.adaylarRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,SoruDereceler,Mulakatlar,Komisyonlar").ToList();
            if (data != null)
            {
                List<AdaylarVM> returnData = new List<AdaylarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new AdaylarVM()
                    {
                        AdayId = item.AdayId,
                        TC = item.TC,
                        Ad = item.Ad,
                        Soyad = item.Soyad,
                        DereceAdi = item.DereceAdi,
                        UlkeTercihi = item.UlkeTercihi,
                        Brans = item.Brans,
                        MYYSTarihi = item.MYYSTarihi,
                        MYSSSaat = item.MYSSSaat,
                        MYSSKomisyonAdi = item.MYSSKomisyonAdi,
                        KomisyonSN = item.KomisyonSN,
                        KomisyonGunSN = item.KomisyonGunSN,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<AdaylarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<AdaylarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion
            
    }
}
