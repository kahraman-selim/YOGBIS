using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
            var data = _unitOfWork.adaylarRepository.GetAll(includeProperties: "Kullanici").ToList(); //,Mulakatlar,Komisyonlar
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
                        //DereceAdi=item.DereceAdi,
                        //UlkeTercihi=item.UlkeTercihi,
                        //Brans=item.Brans,
                        //MYYSTarihi=item.MYYSTarihi, 
                        //MYSSSaat=item.MYSSSaat,
                        //MYSSKomisyonAdi=item.MYSSKomisyonAdi,
                        //KomisyonSN=item.KomisyonSN,
                        //KomisyonGunSN = item.KomisyonGunSN,                       
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

        #region AdayTemelBilgileriGetir
        public Result<List<AdaylarVM>> AdayTemelBilgileriGetir()
        {
            try
            {
                // 1. Sorguyu IQueryable olarak oluştur (henüz execute etme)
                var query = _unitOfWork.adaylarRepository.GetAll()
                    .AsNoTracking() // Change tracking'i devre dışı bırak
                    .Include(x => x.Kullanici)
                    .OrderBy(x => x.Ad)
                    .ThenBy(y => y.Soyad);

                // 2. Sadece ihtiyaç duyulan alanları seç
                var data = query.Select(item => new AdaylarVM
                {
                    AdayId=item.AdayId,
                    TC=item.TC,
                    Ad=item.Ad,
                    Soyad=item.Soyad,
                    KayitTarihi = item.KayitTarihi,
                    KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                    KaydedenAdi = item.Kullanici != null ?
                        item.Kullanici.Ad + " " + item.Kullanici.Soyad :
                        string.Empty
                }).ToList();

                if (data != null)
                {
                    return new Result<List<AdaylarVM>>(true, ResultConstant.RecordFound, data);
                }

                return new Result<List<AdaylarVM>>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                // Loglama yapılabilir
                return new Result<List<AdaylarVM>>(false, ex.Message);
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
                    //Adaylar.DereceAdi = data.DereceAdi;
                    //Adaylar.UlkeTercihi = data.UlkeTercihi;
                    //Adaylar.Brans = data.Brans;
                    //Adaylar.MYYSTarihi = data.MYYSTarihi;
                    //Adaylar.MYSSSaat = data.MYSSSaat;
                    //Adaylar.MYSSKomisyonAdi = data.MYSSKomisyonAdi;
                    //Adaylar.KomisyonSN = data.KomisyonSN;
                    //Adaylar.KomisyonGunSN = data.KomisyonGunSN;
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

        #region AdayGetirTC
        public Result<AdaylarVM> AdayGetirTC(string TC)
        {
            try
            {
                var data = _unitOfWork.adaylarRepository.GetFirstOrDefault(x => x.TC == TC);

                if (data != null)
                {
                    var aday = _mapper.Map<AdaylarVM>(data);
                    return new Result<AdaylarVM>(true, "Aday bilgileri başarıyla getirildi.", aday, 1);
                }
                
                return new Result<AdaylarVM>(false, "Aday bulunamadı.", null, 0);
            }
            catch (Exception ex)
            {
                return new Result<AdaylarVM>(false, $"Aday bilgileri getirilirken hata oluştu: {ex.Message}", null, 0);
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
                        //DereceAdi = item.DereceAdi,
                        //UlkeTercihi = item.UlkeTercihi,
                        //Brans = item.Brans,
                        //MYYSTarihi = item.MYYSTarihi,
                        //MYSSSaat = item.MYSSSaat,
                        //MYSSKomisyonAdi = item.MYSSKomisyonAdi,
                        //KomisyonSN = item.KomisyonSN,
                        //KomisyonGunSN = item.KomisyonGunSN,
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

        #region TCKontrol
        public bool TCKontrol(string TC)
        {
            return _unitOfWork.adaylarRepository.GetFirstOrDefault(x => x.TC == TC) != null;
        }
        #endregion

        #region AdayBasvuruBilgileriEkle
        public Result<AdayBasvuruBilgileriVM> AdayBasvuruBilgileriEkle(AdayBasvuruBilgileriVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var Adaylar = _mapper.Map<AdayBasvuruBilgileriVM, AdayBasvuruBilgileri>(model);
                    Adaylar.KaydedenId = user.LoginId;

                    _unitOfWork.adayBasvuruBilgileriRepository.Add(Adaylar);
                    _unitOfWork.Save();
                    return new Result<AdayBasvuruBilgileriVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<AdayBasvuruBilgileriVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<AdayBasvuruBilgileriVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region AdayBasvuruBilgileriGetir(string TC)
        public Result<AdayBasvuruBilgileriVM> AdayBasvuruBilgileriGetir(string TC)
        {
            if (TC != null)
            {
                var data = _unitOfWork.adayBasvuruBilgileriRepository.GetFirstOrDefault(x => x.TC == TC, includeProperties: "Kullanici,Mulakatlar");

                if (data != null)
                {
                    AdayBasvuruBilgileriVM Aday = new AdayBasvuruBilgileriVM();

                    Aday.Id = data.Id;
                    Aday.TC = data.TC;
                    Aday.Askerlik = data.Askerlik;
                    Aday.KurumKod = data.KurumKod;
                    Aday.KurumAdi = data.KurumAdi;
                    Aday.Ogrenim = data.Ogrenim;
                    Aday.MezunOkulKodu = data.MezunOkulKodu;
                    Aday.Mezuniyet = data.Mezuniyet;
                    Aday.HizmetYil = data.HizmetYil;
                    Aday.HizmetAy = data.HizmetAy;
                    Aday.HizmetGun = data.HizmetGun;
                    Aday.Derece = data.Derece;
                    Aday.Kademe = data.Kademe;
                    Aday.Enaz5Yil = data.Enaz5Yil;
                    Aday.DahaOnceYDGorev = data.DahaOnceYDGorev;
                    Aday.YIciGorevBasTar = data.YIciGorevBasTar;
                    Aday.YabanciDilBasvuru = data.YabanciDilBasvuru;
                    Aday.YabanciDilAdi = data.YabanciDilAdi;
                    Aday.YabanciDilTuru = data.YabanciDilTuru;
                    Aday.YabanciDilTarihi = data.YabanciDilTarihi;
                    Aday.YabanciDilPuan = data.YabanciDilPuan;
                    Aday.YabanciDilSeviye = data.YabanciDilSeviye;
                    Aday.IlTercihi1 = data.IlTercihi1;
                    Aday.IlTercihi2 = data.IlTercihi2;
                    Aday.IlTercihi3 = data.IlTercihi3;
                    Aday.IlTercihi4 = data.IlTercihi4;
                    Aday.IlTercihi5 = data.IlTercihi5;
                    Aday.BasvuruTarihi = data.BasvuruTarihi;
                    Aday.SonDegisiklikTarihi = data.SonDegisiklikTarihi;
                    Aday.OnayDurumu = data.OnayDurumu;
                    Aday.OnayDurumuAck = data.OnayDurumuAck;
                    Aday.MYYSTarihi = data.MYYSTarihi;
                    Aday.MYYSSinavTedbiri = data.MYYSSinavTedbiri;
                    Aday.MYYSTedbirAck = data.MYYSTedbirAck;
                    Aday.MYYSPuan = data.MYYSPuan;
                    Aday.MYYSSonuc = data.MYYSSonuc;
                    Aday.MYSSDurum = data.MYSSDurum;
                    Aday.MYSSDurumAck = data.MYSSDurumAck;
                    Aday.IlMemGorus = data.IlMemGorus;
                    Aday.Referans = data.Referans;
                    Aday.ReferansAck = data.ReferansAck;
                    Aday.GorevIptalAck = data.GorevIptalAck;
                    Aday.GorevIptalBrans = data.GorevIptalBrans;
                    Aday.GorevIptalYil = data.GorevIptalYil;
                    Aday.GorevIptalBAOK = data.GorevIptalBAOK;
                    Aday.IlkGorevKaydi = data.IlkGorevKaydi;
                    Aday.YabanciDilALM = data.YabanciDilALM;
                    Aday.YabanciDilING = data.YabanciDilING;
                    Aday.YabanciDilFRS = data.YabanciDilFRS;
                    Aday.YabanciDilDiger = data.YabanciDilDiger;
                    Aday.GorevdenUzaklastirma = data.GorevdenUzaklastirma;
                    Aday.EDurum = data.EDurum;
                    Aday.MDurum = data.MDurum;
                    Aday.PDurum = data.PDurum;
                    Aday.Sendika = data.Sendika;
                    Aday.SendikaAck = data.SendikaAck;
                    Aday.MYYSSoruItiraz = data.MYYSSoruItiraz;
                    Aday.MYYSSonucItiraz = data.MYYSSonucItiraz;
                    Aday.BasvuruBrans = data.BasvuruBrans;
                    Aday.BransId = data.BransId;
                    Aday.AdliSicilBelge = data.AdliSicilBelge;
                    Aday.DereceId = data.DereceId;
                    Aday.DereceAdi = data.DereceAdi;
                    Aday.Unvan = data.Unvan;
                    Aday.UlkeTercihId = data.UlkeTercihId;
                    Aday.MulakatOnayNo = _mulakatOlusturBE.MulakatOnayGetir((Guid)data.MulakatId).Data;
                    Aday.MulakatYil = int.Parse(_mulakatOlusturBE.MulakatYilGetir((Guid)data.MulakatId).Data);
                    Aday.MulakatId = data.MulakatId;

                    Aday.KayitTarihi = data.KayitTarihi;
                    Aday.KaydedenId = data.KaydedenId;
                    Aday.KaydedenAdi = data.Kullanici.Ad + " " + data.Kullanici.Soyad;


                    return new Result<AdayBasvuruBilgileriVM>(true, ResultConstant.RecordFound, Aday);
                }
                else
                {
                    return new Result<AdayBasvuruBilgileriVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<AdayBasvuruBilgileriVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion
    }
}
