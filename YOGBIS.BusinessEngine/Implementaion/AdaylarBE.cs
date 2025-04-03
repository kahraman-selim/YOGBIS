using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<AdaylarBE> _logger;
        #endregion

        #region Dönüştürücüler
        public AdaylarBE(IUnitOfWork unitOfWork, IMapper mapper, IDerecelerBE derecelerBE, IMulakatOlusturBE mulakatOlusturBE, IKomisyonlarBE komisyonlarBE, ILogger<AdaylarBE> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _derecelerBE = derecelerBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _komisyonlarBE = komisyonlarBE;
            _logger = logger;
        }
        #endregion

        #region AdaylariGetir
        public Result<List<AdaylarVM>> AdaylariGetir()
        {
            try
            {
                // 1. Sorguyu IQueryable olarak oluştur (henüz execute etme)
                var query = _unitOfWork.adaylarRepository.GetAll()
                    .AsNoTracking() // Change tracking'i devre dışı bırak
                    .Include(x => x.Kullanici)
                    //.Include(x => x.FotoGaleri)
                    //.Include(x => x.DosyaGaleri)
                    //.Include(x => x.AdaySinavNotlar)
                    //.Include(x => x.AdayGorevKaydi)
                    //.Include(x => x.AdayBasvuruBilgileri.Where(x=>x.OnayDurumu=="Onaylandı"))
                    //.Include(x => x.AdayIletisimBilgileri)
                    //.Include(x => x.AdayMYSS.Where(z => z.Mulakatlar.Durumu == true && z.MYSSDurum == "GİRECEK"))
                    //.Include(x => x.AdayTYS)
                    .ToList();

                if (query == null || !query.Any())
                {
                    return new Result<List<AdaylarVM>>(false, ResultConstant.RecordNotFound, default(List<AdaylarVM>));
                }

                var returndata = query.Select(item => new AdaylarVM
                {
                    AdayId = item.AdayId,
                    TC = item.TC,
                    Ad = item.Ad,
                    Soyad = item.Soyad,
                    BabaAd = item.BabaAd,
                    AnaAd = item.AnaAd,
                    DogumTarihi = item.DogumTarihi,
                    DogumTarihi2 = item.DogumTarihi2,
                    DogumYeri = item.DogumYeri,
                    Cinsiyet = item.Cinsiyet,
                    MulakatYil = item.MulakatYil,
                    KaydedenId = item.KaydedenId,
                    KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    //FotoGaleri = item.FotoGaleri?
                    //.Select(x => new FotoGaleriVM
                    //{
                    //    FotoGaleriId = x.FotoGaleriId,
                    //    FotoAdi = x.FotoAdi,
                    //    FotoURL = x.FotoURL,
                    //    KayitTarihi = x.KayitTarihi,
                    //    KaydedenId = x.KaydedenId,
                    //    KaydedenAdi = x.Kullanici != null ? x.Kullanici.Ad + " " + x.Kullanici.Soyad : string.Empty

                    //}).ToList() ?? new List<FotoGaleriVM>(),

                    //AdayBasvuruBilgileri = item.AdayBasvuruBilgileri?
                    //.Select(y => new AdayBasvuruBilgileriVM
                    //{
                    //    Id = y.Id,
                    //    AdayId = y.AdayId,
                    //    TC = y.TC,
                    //    Askerlik = y.Askerlik,
                    //    KurumKod = y.KurumKod,
                    //    KurumAdi = y.KurumAdi,
                    //    Ogrenim = y.Ogrenim,
                    //    MezunOkulKodu = y.MezunOkulKodu,
                    //    Mezuniyet = y.Mezuniyet,
                    //    HizmetYil = y.HizmetYil,
                    //    HizmetAy = y.HizmetAy,
                    //    HizmetGun = y.HizmetGun,
                    //    Derece = y.Derece,
                    //    Kademe = y.Kademe,
                    //    Enaz5Yil = y.Enaz5Yil,
                    //    DahaOnceYDGorev = y.DahaOnceYDGorev,
                    //    YIciGorevBasTar = y.YIciGorevBasTar,
                    //    YabanciDilBasvuru = y.YabanciDilBasvuru,
                    //    YabanciDilAdi = y.YabanciDilAdi,
                    //    YabanciDilTuru = y.YabanciDilTuru,
                    //    YabanciDilTarihi = y.YabanciDilTarihi,
                    //    YabanciDilPuan = y.YabanciDilPuan,
                    //    YabanciDilSeviye = y.YabanciDilSeviye,
                    //    IlTercihi1 = y.IlTercihi1,
                    //    IlTercihi2 = y.IlTercihi2,
                    //    IlTercihi3 = y.IlTercihi3,
                    //    IlTercihi4 = y.IlTercihi4,
                    //    IlTercihi5 = y.IlTercihi5,
                    //    BasvuruTarihi = y.BasvuruTarihi,
                    //    SonDegisiklikTarihi = y.SonDegisiklikTarihi,
                    //    OnayDurumu = y.OnayDurumu,
                    //    OnayDurumuAck = y.OnayDurumuAck,
                    //    MYYSTarihi = y.MYYSTarihi,
                    //    MYYSSinavTedbiri = y.MYYSSinavTedbiri,
                    //    MYYSTedbirAck = y.MYYSTedbirAck,
                    //    MYYSPuan = y.MYYSPuan,
                    //    MYYSSonuc = y.MYYSSonuc,
                    //    MYSSDurum = y.MYSSDurum,
                    //    MYSSDurumAck = y.MYSSDurumAck,
                    //    IlMemGorus = y.IlMemGorus,
                    //    Referans = y.Referans,
                    //    ReferansAck = y.ReferansAck,
                    //    GorevIptalAck = y.GorevIptalAck,
                    //    GorevIptalBrans = y.GorevIptalBrans,
                    //    GorevIptalYil = y.GorevIptalYil,
                    //    GorevIptalBAOK = y.GorevIptalBAOK,
                    //    IlkGorevKaydi = y.IlkGorevKaydi,
                    //    YabanciDilALM = y.YabanciDilALM,
                    //    YabanciDilING = y.YabanciDilING,
                    //    YabanciDilFRS = y.YabanciDilFRS,
                    //    YabanciDilDiger = y.YabanciDilDiger,
                    //    GorevdenUzaklastirma = y.GorevdenUzaklastirma,
                    //    EDurum = y.EDurum,
                    //    MDurum = y.MDurum,
                    //    PDurum = y.PDurum,
                    //    GenelDurum = y.GenelDurum,
                    //    YLisans = y.YLisans,
                    //    Doktora = y.Doktora,
                    //    Sendika = y.Sendika,
                    //    SendikaAck = y.SendikaAck,
                    //    MYYSSoruItiraz = y.MYYSSoruItiraz,
                    //    MYYSSonucItiraz = y.MYYSSonucItiraz,
                    //    BasvuruBrans = y.BasvuruBrans,
                    //    AdliSicilBelge = y.AdliSicilBelge,
                    //    BilgiFormu = y.BilgiFormu

                    //}).ToList() ?? new List<AdayBasvuruBilgileriVM>(),

                    //AdayMYSS = item.AdayMYSS?
                    //.Select(z => new AdayMYSSVM
                    //{
                    //    Id = z.Id,
                    //    AdayId = z.AdayId,
                    //    TC = z.TC,
                    //    AdayAdiSoyadi = z.Adaylar != null ? z.Adaylar.Ad.ToString() + " " + z.Adaylar.Soyad.ToString() : string.Empty,
                    //    MYSSTarih = z.MYSSTarih,
                    //    MYSSSaat = z.MYSSSaat,
                    //    MYSSMulakatYer = z.MYSSMulakatYer,
                    //    MYSSDurum = z.MYSSDurum,
                    //    MYSSDurumAck = z.MYSSDurumAck,
                    //    MYSSKomisyonSiraNo = z.MYSSKomisyonSiraNo,
                    //    MYSSKomisyonAdi = z.MYSSKomisyonAdi,
                    //    KomisyonSN = z.KomisyonSN,
                    //    KomisyonGunSN = z.KomisyonGunSN,
                    //    CagriDurum = z.CagriDurum,
                    //    KabulDurum = z.KabulDurum,
                    //    SinavDurum = z.SinavDurum,
                    //    SinavaGelmedi = z.SinavaGelmedi,
                    //    SinavaGelmediAck = z.SinavaGelmediAck,
                    //    SinavaGeldi = z.SinavaGeldi,
                    //    SinavaAlindi = z.SinavaAlindi,
                    //    MYSPuan = z.MYSPuan,
                    //    MYSSonuc = z.MYSSonuc,
                    //    MYSSonucAck = z.MYSSonucAck,
                    //    MYSSSorulanSoruNo = z.MYSSSorulanSoruNo,
                    //    SinavIptal = z.SinavIptal,
                    //    SinavIptalAck = z.SinavIptalAck,
                    //    BransId = z.BransId,
                    //    BransAdi = z.BransAdi,
                    //    DereceId = z.DereceId,
                    //    DereceAdi = z.DereceAdi,
                    //    UlkeTercihId = z.UlkeTercihId,
                    //    UlkeTercihAdi = z.UlkeTercihAdi,
                    //    MulakatId = z.MulakatId,
                    //    KayitTarihi = z.KayitTarihi,
                    //    KaydedenId = z.KaydedenId,
                    //    KaydedenAdi = z.Kullanici != null ? z.Kullanici.Ad + " " + z.Kullanici.Soyad : string.Empty
                    //})                    
                    //.OrderBy(z=>z.MYSSKomisyonSiraNo)
                    //.ThenBy(z => z.KomisyonSN)
                    //.ThenBy(z => z.KomisyonGunSN)
                    //.ToList() ?? new List<AdayMYSSVM>()

                }).ToList();

                if (returndata != null)
                {
                    return new Result<List<AdaylarVM>>(true, ResultConstant.RecordFound, returndata);
                }

                return new Result<List<AdaylarVM>>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                return new Result<List<AdaylarVM>>(false, ex.Message);
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
                    MulakatYil = item.MulakatYil,
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
                    Adaylar.MulakatYil = data.MulakatYil;
                    Adaylar.KayitTarihi=data.KayitTarihi;
                    Adaylar.KaydedenId = data.KaydedenId;
                    Adaylar.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;


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

        #region MYSSAdayGetir(Guid id)
        public Result<AdayMYSSVM> MYSSAdayGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.adayMYSSRepository.GetFirstOrDefault(x => x.Id == id, includeProperties: "Kullanici,Mulakatlar,Komisyonlar,Adaylar");

                if (data != null)
                {
                    AdayMYSSVM Adaylar = new AdayMYSSVM();

                    Adaylar.Id = data.Id;
                    Adaylar.AdayId = data.AdayId;
                    Adaylar.TC = data.TC;
                    Adaylar.AdayAdiSoyadi = data.Adaylar != null ? data.Adaylar.Ad.ToString() + " " + data.Adaylar.Soyad.ToString() : string.Empty;
                    Adaylar.MYSSTarih = data.MYSSTarih;
                    Adaylar.MYSSSaat = data.MYSSSaat;
                    Adaylar.MYSSMulakatYer = data.MYSSMulakatYer;
                    Adaylar.MYSSDurum = data.MYSSDurum;
                    Adaylar.MYSSDurumAck = data.MYSSDurumAck;
                    Adaylar.MYSSKomisyonSiraNo = data.MYSSKomisyonSiraNo;
                    Adaylar.MYSSKomisyonAdi = data.MYSSKomisyonAdi;
                    Adaylar.KomisyonSN = data.KomisyonSN;
                    Adaylar.KomisyonGunSN = data.KomisyonGunSN;
                    Adaylar.CagriDurum = data.CagriDurum;
                    Adaylar.KabulDurum = data.KabulDurum;
                    Adaylar.SinavDurum = data.SinavDurum;
                    Adaylar.SinavaGelmedi = data.SinavaGelmedi;
                    Adaylar.SinavaGelmediAck = data.SinavaGelmediAck;
                    Adaylar.SinavaGeldi = data.SinavaGeldi;
                    Adaylar.SinavaAlindi = data.SinavaAlindi;
                    Adaylar.MYSPuan = data.MYSPuan;
                    Adaylar.MYSSonuc = data.MYSSonuc;
                    Adaylar.MYSSonucAck = data.MYSSonucAck;
                    Adaylar.MYSSSorulanSoruNo = data.MYSSSorulanSoruNo;
                    Adaylar.SinavIptal = data.SinavIptal;
                    Adaylar.SinavIptalAck = data.SinavIptalAck;
                    Adaylar.BransId = data.BransId;
                    Adaylar.BransAdi = data.BransAdi;
                    Adaylar.DereceId = data.DereceId;
                    Adaylar.DereceAdi = data.DereceAdi;
                    Adaylar.UlkeTercihId = data.UlkeTercihId;
                    Adaylar.UlkeTercihAdi = data.UlkeTercihAdi;
                    Adaylar.MulakatId = data.MulakatId;
                    Adaylar.KomisyonGunSN = data.KomisyonGunSN;
                    Adaylar.KomisyonSN = data.KomisyonSN;
                    Adaylar.KomisyonId = data.KomisyonId;
                    Adaylar.MYSSKomisyonAdi = data.Komisyonlar != null ? data.Komisyonlar.KomisyonAdi : string.Empty;
                    Adaylar.KayitTarihi = data.KayitTarihi;
                    Adaylar.KaydedenId = data.KaydedenId;
                    Adaylar.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;


                    return new Result<AdayMYSSVM>(true, ResultConstant.RecordFound, Adaylar);
                }
                else
                {
                    return new Result<AdayMYSSVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<AdayMYSSVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region AdayMYSSGuncelle
        public Result<AdayMYSSVM> AdayMYSSGuncelle(AdayMYSSVM model, SessionContext user)
        {
            if (model.Id != null)
            {
                try
                {
                    var data = _unitOfWork.adayMYSSRepository.Get(model.Id);
                    if (data != null)
                    {
                        data.Id = model.Id;
                        data.KomisyonId = model.KomisyonId;
                        data.MYSSKomisyonSiraNo = model.MYSSKomisyonSiraNo;
                        data.MYSSKomisyonAdi = model.MYSSKomisyonAdi;
                        data.KomisyonSN = model.KomisyonSN;
                        data.KomisyonGunSN = model.KomisyonGunSN;
                        data.MYSSTarih = model.MYSSTarih;
                        data.MYSSSaat = model.MYSSSaat;
                        data.MYSSDurum = model.MYSSDurum;
                        data.MYSSDurumAck = model.MYSSDurumAck;     
                        data.MYSSSorulanSoruNo = model.MYSSSorulanSoruNo;                       
                        data.CagriDurum = model.CagriDurum;
                        data.KabulDurum = model.KabulDurum;
                        data.SinavDurum = model.SinavDurum;
                        data.SinavaGelmedi = model.SinavaGelmedi;
                        data.SinavaGelmediAck = model.SinavaGelmediAck;
                        
                        _unitOfWork.adayMYSSRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<AdayMYSSVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<AdayMYSSVM>(false, "Lütfen kayıt seçiniz");
                    }

                }
                catch (Exception ex)
                {

                    return new Result<AdayMYSSVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<AdayMYSSVM>(false, "Boş veri olamaz");
            }

        }
        #endregion

        #region AdayBasvuruGetir(Guid id)
        public Result<AdayBasvuruBilgileriVM> AdayBasvuruGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.adayBasvuruBilgileriRepository.GetFirstOrDefault(x => x.Id == id, includeProperties: "Kullanici,Mulakatlar,Adaylar");

                if (data != null)
                {
                    AdayBasvuruBilgileriVM Adaylar = new AdayBasvuruBilgileriVM();

                    Adaylar.Id = data.Id;
                    Adaylar.AdayId = data.AdayId;
                    Adaylar.TC = data.TC;
                    Adaylar.AdayAdiSoyadi = data.Adaylar != null ? data.Adaylar.Ad.ToString() + " " + data.Adaylar.Soyad.ToString() : string.Empty;
                    Adaylar.DahaOnceYDGorev=data.DahaOnceYDGorev;
                    Adaylar.YIciGorevBasTar=data.YIciGorevBasTar;
                    Adaylar.YabanciDilBasvuru = data.YabanciDilBasvuru;
                    Adaylar.YabanciDilAdi = data.YabanciDilAdi;
                    Adaylar.YabanciDilTuru = data.YabanciDilTuru;
                    Adaylar.YabanciDilTarihi = data.YabanciDilTarihi;
                    Adaylar.YabanciDilPuan = data.YabanciDilPuan;
                    Adaylar.YabanciDilSeviye = data.YabanciDilSeviye;
                    Adaylar.IlMemGorus = data.IlMemGorus;
                    Adaylar.Referans = data.Referans;
                    Adaylar.ReferansAck= data.ReferansAck;
                    Adaylar.Sendika = data.Sendika;
                    Adaylar.SendikaAck = data.SendikaAck;
                    Adaylar.GorevIptalAck = data.GorevIptalAck;
                    Adaylar.GorevIptalBrans = data.GorevIptalBrans;
                    Adaylar.GorevIptalYil = data.GorevIptalYil;
                    Adaylar.GorevIptalBAOK = data.GorevIptalBAOK;
                    Adaylar.IlkGorevKaydi = data.IlkGorevKaydi;
                    Adaylar.YabanciDilALM = data.YabanciDilALM;
                    Adaylar.YabanciDilING = data.YabanciDilING;
                    Adaylar.YabanciDilFRS = data.YabanciDilFRS;
                    Adaylar.YabanciDilDiger = data.YabanciDilDiger;
                    Adaylar.GorevdenUzaklastirma = data.GorevdenUzaklastirma;
                    Adaylar.EDurum=data.EDurum;
                    Adaylar.MDurum=data.MDurum;
                    Adaylar.PDurum=data.PDurum;
                    Adaylar.GenelDurum=data.GenelDurum;
                    Adaylar.YLisans=data.YLisans;
                    Adaylar.Doktora=data.Doktora;
                    Adaylar.DahaOnceSinav = data.DahaOnceSinav;
                    Adaylar.AdliSicilBelge = data.AdliSicilBelge;
                    Adaylar.BilgiFormu=data.BilgiFormu;
                    Adaylar.BransId = data.BransId;
                    Adaylar.BransAdi = data.BransAdi;
                    Adaylar.DereceId = data.DereceId;
                    Adaylar.DereceAdi = data.DereceAdi;
                    Adaylar.UlkeTercihId = data.UlkeTercihId;
                    Adaylar.UlkeTercihAdi = data.UlkeTercihAdi;
                    Adaylar.MulakatId = data.MulakatId;
                    
                    Adaylar.KayitTarihi = data.KayitTarihi;
                    Adaylar.KaydedenId = data.KaydedenId;
                    Adaylar.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;


                    return new Result<AdayBasvuruBilgileriVM>(true, ResultConstant.RecordFound, Adaylar);
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

        #region AdayBasvuruGuncelle
        public Result<AdayBasvuruBilgileriVM> AdayBasvuruGuncelle(AdayBasvuruBilgileriVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var Adaylar = _mapper.Map<AdayBasvuruBilgileriVM, AdayBasvuruBilgileri>(model);
                    Adaylar.KaydedenId = user.LoginId;

                    _unitOfWork.adayBasvuruBilgileriRepository.Update(Adaylar);
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

        #region AdayTopluGuncelleBasvuruBilgileri
        public Result<AdayBasvuruBilgileriVM> AdayTopluGuncelle(AdayBasvuruBilgileriVM model, SessionContext user)
        {
            if (model.Id != null)
            {
                try
                {
                    var data = _unitOfWork.adayBasvuruBilgileriRepository.Get(model.Id);
                    if (data != null) 
                    {
                        data.MulakatId = model.MulakatId;
                        data.KaydedenId = user.LoginId;

                        _unitOfWork.adayBasvuruBilgileriRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<AdayBasvuruBilgileriVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<AdayBasvuruBilgileriVM>(false, "Lütfen kayıt seçiniz");
                    }
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

        #region AdayTopluGuncelleMYSSBilgileri
        public Result<AdayMYSSVM> AdayMYSSTopluGuncelle(AdayMYSSVM model, SessionContext user)
        {
            if (model.Id != null)
            {
                try
                {
                    var data = _unitOfWork.adayMYSSRepository.Get(model.Id);
                    if (data != null)
                    {
                        //data.MulakatId = (Guid)model.MulakatId;
                        data.UlkeTercihId = model.UlkeTercihId;
                        data.UlkeTercihAdi = model.UlkeTercihAdi;
                        data.BransId = model.BransId;
                        data.BransAdi = model.BransAdi;
                        data.KaydedenId = user.LoginId;

                        _unitOfWork.adayMYSSRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<AdayMYSSVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<AdayMYSSVM>(false, "Lütfen kayıt seçiniz");
                    }
                }
                catch (Exception ex)
                {

                    return new Result<AdayMYSSVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<AdayMYSSVM>(false, "Boş veri olamaz");
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
                        MulakatYil = item.MulakatYil,
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
        
        #region AdayBasvuruBilgileriniGetirAdayTanımlama
        public Result<List<AdayBasvuruBilgileriVM>> AdayBasvuruBilgileriniGetir(string TC)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - TC ile sorgu başladı: {TC}");

                if (string.IsNullOrEmpty(TC))
                {
                    System.Diagnostics.Debug.WriteLine("Business Engine - TC boş");
                    return new Result<List<AdayBasvuruBilgileriVM>>(false, "TC kimlik numarası boş olamaz");
                }
                
                System.Diagnostics.Debug.WriteLine("Business Engine - Repository'den veri çekiliyor");
                var data = _unitOfWork.adayBasvuruBilgileriRepository.GetAll()
                    .Include(x => x.Kullanici)
                    .Include(x => x.Adaylar)                    
                    .Where(x=>x.TC==TC)
                    .OrderBy(x=>x.MYYSTarihi)
                    .ToList();


                if (data != null && data.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"Business Engine - {data.Count} adet veri bulundu. TC: {TC}");
                    
                    var adaylar = data.Select(data => new AdayBasvuruBilgileriVM()
                    {
                        Id = data.Id,
                        TC = data.TC,                       
                        Askerlik =data.Askerlik,
                        KurumKod = data.KurumKod,
                        KurumAdi = data.KurumAdi,
                        Ogrenim = data.Ogrenim,
                        MezunOkulKodu = data.MezunOkulKodu,
                        Mezuniyet = data.Mezuniyet,
                        HizmetYil = data.HizmetYil,
                        HizmetAy = data.HizmetAy,
                        HizmetGun = data.HizmetGun,
                        Derece = data.Derece,
                        Kademe = data.Kademe,
                        Enaz5Yil = data.Enaz5Yil,
                        DahaOnceYDGorev = data.DahaOnceYDGorev,
                        YIciGorevBasTar = data.YIciGorevBasTar,
                        YabanciDilBasvuru = data.YabanciDilBasvuru,
                        YabanciDilAdi = data.YabanciDilAdi,
                        YabanciDilTuru = data.YabanciDilTuru,
                        YabanciDilTarihi = data.YabanciDilTarihi,
                        YabanciDilPuan = data.YabanciDilPuan,
                        YabanciDilSeviye = data.YabanciDilSeviye,
                        IlTercihi1 = data.IlTercihi1,
                        IlTercihi2 = data.IlTercihi2,
                        IlTercihi3 = data.IlTercihi3,
                        IlTercihi4 = data.IlTercihi4,
                        IlTercihi5 = data.IlTercihi5,
                        BasvuruTarihi = data.BasvuruTarihi,
                        SonDegisiklikTarihi = data.SonDegisiklikTarihi,
                        OnayDurumu = data.OnayDurumu,
                        OnayDurumuAck = data.OnayDurumuAck,
                        MYYSTarihi = data.MYYSTarihi,
                        MYYSSinavTedbiri = data.MYYSSinavTedbiri,
                        MYYSTedbirAck = data.MYYSTedbirAck,
                        MYYSPuan = data.MYYSPuan,
                        MYYSSonuc = data.MYYSSonuc,
                        MYSSDurum = data.MYSSDurum,
                        MYSSDurumAck = data.MYSSDurumAck,
                        IlMemGorus = data.IlMemGorus,
                        Referans = data.Referans,
                        ReferansAck = data.ReferansAck,
                        GorevIptalAck = data.GorevIptalAck,
                        GorevIptalBrans = data.GorevIptalBrans,
                        GorevIptalYil = data.GorevIptalYil,
                        GorevIptalBAOK = data.GorevIptalBAOK,
                        IlkGorevKaydi = data.IlkGorevKaydi,
                        YabanciDilALM = data.YabanciDilALM,
                        YabanciDilING = data.YabanciDilING,
                        YabanciDilFRS = data.YabanciDilFRS,
                        YabanciDilDiger = data.YabanciDilDiger,
                        GorevdenUzaklastirma = data.GorevdenUzaklastirma,
                        EDurum = data.EDurum,
                        MDurum = data.MDurum,
                        PDurum = data.PDurum,
                        GenelDurum = data.GenelDurum,
                        DahaOnceSinav = data.DahaOnceSinav,
                        DogumYeri = !string.IsNullOrEmpty(data.Adaylar?.DogumYeri) ? data.Adaylar.DogumYeri : "",

                        Yas = !string.IsNullOrEmpty(data.Adaylar?.DogumTarihi2) && DateTime.TryParse(data.Adaylar.DogumTarihi2, out DateTime dogumTarihi) ? (int)((DateTime.Now - dogumTarihi).TotalDays / 365.25) : 0,
                        
                        IkametBilgisi = data.Adaylar.AdayIletisimBilgileri != null ? 
                        data.Adaylar.AdayIletisimBilgileri.Select(x => x.IkametIl).FirstOrDefault() + "/" + 
                        data.Adaylar.AdayIletisimBilgileri.Select(x => x.IkametIlce).FirstOrDefault()  : "",                       

                        YLisans = data.YLisans,
                        Doktora = data.Doktora,
                        Sendika = data.Sendika,
                        SendikaAck = data.SendikaAck,
                        MYYSSoruItiraz = data.MYYSSoruItiraz,
                        MYYSSonucItiraz = data.MYYSSonucItiraz,
                        BasvuruBrans = data.BasvuruBrans,
                        BransId = data.BransId,
                        AdliSicilBelge = data.AdliSicilBelge,                        
                        DereceId = data.DereceId,
                        DereceAdi = data.DereceAdi,
                        Unvan = data.Unvan,
                        UlkeTercihId = data.UlkeTercihId,                
                        MulakatId = data.MulakatId,
                        KayitTarihi = data.KayitTarihi,
                        KaydedenId = data.KaydedenId
                    }).ToList();

                    foreach (var aday in adaylar)
                    {
                        if (aday.MulakatId.HasValue)
                        {
                            try
                            {
                                System.Diagnostics.Debug.WriteLine($"Business Engine - {aday.TC} için mülakat bilgileri alınıyor. MulakatId: {aday.MulakatId}");

                                // Önce mülakat verisini direkt veritabanından alalım
                                var mulakat = _unitOfWork.mulakatlarRepository.Get((Guid)aday.MulakatId);
                                if (mulakat != null && mulakat.YazılıSinavTarihi != default(DateTime))
                                {
                                    aday.MulakatYil = mulakat.YazılıSinavTarihi.Year;
                                    System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı veritabanından alındı: {aday.MulakatYil}");
                                }
                                else
                                {
                                    // Eğer veritabanından alamazsak servisten deneyelim
                                    var mulakatYil = _mulakatOlusturBE.MulakatYilGetir((Guid)aday.MulakatId);
                                    if (mulakatYil.IsSuccess && !string.IsNullOrEmpty(mulakatYil.Data))
                                    {
                                        aday.MulakatYil = int.Parse(mulakatYil.Data);
                                        System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı servisten alındı: {aday.MulakatYil}");
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı alınamadı");
                                    }
                                }

                                // Ülke tercih bilgisini alalım
                                if (aday.UlkeTercihId != null)
                                {
                                    var ulkeTercih = _unitOfWork.ulkeTercihRepository.Get((Guid)aday.UlkeTercihId);
                                    if (ulkeTercih != null && ulkeTercih.UlkeTercihId != null)
                                    {
                                        var ulke = _unitOfWork.ulkeTercihRepository.Get((Guid)ulkeTercih.UlkeTercihId);
                                        if (ulke != null)
                                        {
                                            aday.UlkeTercihAdi = ulke.UlkeTercihAdi;
                                            System.Diagnostics.Debug.WriteLine($"Business Engine - Ülke adı veritabanından alındı: {aday.UlkeTercihAdi}");
                                        }
                                    }
                                }

                                var mulakatOnay = _mulakatOlusturBE.MulakatOnayGetir((Guid)aday.MulakatId);
                                if (mulakatOnay.IsSuccess && !string.IsNullOrEmpty(mulakatOnay.Data))
                                {
                                    aday.MulakatOnayNo = mulakatOnay.Data;
                                    System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat onay no alındı: {mulakatOnay.Data}");
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat bilgileri alınırken hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                            }
                        }
                    }

                    return new Result<List<AdayBasvuruBilgileriVM>>(true, ResultConstant.RecordFound, adaylar);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Business Engine - TC ile veri bulunamadı: {TC}");
                    return new Result<List<AdayBasvuruBilgileriVM>>(false, "TC numarası ile kayıt bulunamadı");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new Result<List<AdayBasvuruBilgileriVM>>(false, "Veri getirme hatası: " + ex.Message);
            }
        }
        #endregion

        #region AdayBasvuruBilgileriniGetirMulakat
        public Result<List<AdayBasvuruBilgileriVM>> AdayBasvuruBilgileriniGetirMulakat(string TC)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - TC ile sorgu başladı: {TC}");

                if (string.IsNullOrEmpty(TC))
                {
                    System.Diagnostics.Debug.WriteLine("Business Engine - TC boş");
                    return new Result<List<AdayBasvuruBilgileriVM>>(false, "TC kimlik numarası boş olamaz");
                }

                System.Diagnostics.Debug.WriteLine("Business Engine - Repository'den veri çekiliyor");
                var data = _unitOfWork.adayBasvuruBilgileriRepository.GetAll()
                    .Include(x => x.Kullanici)
                    .Include(x => x.Adaylar)
                    .Where(x => x.TC == TC && x.OnayDurumu=="Onaylandı")
                    .ToList();


                if (data != null && data.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"Business Engine - {data.Count} adet veri bulundu. TC: {TC}");

                    var adaylar = data.Select(data => new AdayBasvuruBilgileriVM()
                    {
                        Id = data.Id,
                        TC = data.TC,
                        Askerlik = data.Askerlik,
                        KurumKod = data.KurumKod,
                        KurumAdi = data.KurumAdi,
                        Ogrenim = data.Ogrenim,
                        MezunOkulKodu = data.MezunOkulKodu,
                        Mezuniyet = data.Mezuniyet,
                        HizmetYil = data.HizmetYil,
                        HizmetAy = data.HizmetAy,
                        HizmetGun = data.HizmetGun,
                        Derece = data.Derece,
                        Kademe = data.Kademe,
                        Enaz5Yil = data.Enaz5Yil,
                        DahaOnceYDGorev = data.DahaOnceYDGorev,
                        YIciGorevBasTar = data.YIciGorevBasTar,
                        YabanciDilBasvuru = data.YabanciDilBasvuru,
                        YabanciDilAdi = data.YabanciDilAdi,
                        YabanciDilTuru = data.YabanciDilTuru,
                        YabanciDilTarihi = data.YabanciDilTarihi,
                        YabanciDilPuan = data.YabanciDilPuan,
                        YabanciDilSeviye = data.YabanciDilSeviye,
                        IlTercihi1 = data.IlTercihi1,
                        IlTercihi2 = data.IlTercihi2,
                        IlTercihi3 = data.IlTercihi3,
                        IlTercihi4 = data.IlTercihi4,
                        IlTercihi5 = data.IlTercihi5,
                        BasvuruTarihi = data.BasvuruTarihi,
                        SonDegisiklikTarihi = data.SonDegisiklikTarihi,
                        OnayDurumu = data.OnayDurumu,
                        OnayDurumuAck = data.OnayDurumuAck,
                        MYYSTarihi = data.MYYSTarihi,
                        MYYSSinavTedbiri = data.MYYSSinavTedbiri,
                        MYYSTedbirAck = data.MYYSTedbirAck,
                        MYYSPuan = data.MYYSPuan,
                        MYYSSonuc = data.MYYSSonuc,
                        MYSSDurum = data.MYSSDurum,
                        MYSSDurumAck = data.MYSSDurumAck,
                        IlMemGorus = data.IlMemGorus,
                        Referans = data.Referans,
                        ReferansAck = data.ReferansAck,
                        GorevIptalAck = data.GorevIptalAck,
                        GorevIptalBrans = data.GorevIptalBrans,
                        GorevIptalYil = data.GorevIptalYil,
                        GorevIptalBAOK = data.GorevIptalBAOK,
                        IlkGorevKaydi = data.IlkGorevKaydi,
                        YabanciDilALM = data.YabanciDilALM,
                        YabanciDilING = data.YabanciDilING,
                        YabanciDilFRS = data.YabanciDilFRS,
                        YabanciDilDiger = data.YabanciDilDiger,
                        GorevdenUzaklastirma = data.GorevdenUzaklastirma,
                        EDurum = data.EDurum,
                        MDurum = data.MDurum,
                        PDurum = data.PDurum,
                        GenelDurum = data.GenelDurum,
                        DahaOnceSinav = data.DahaOnceSinav,
                        DogumYeri = !string.IsNullOrEmpty(data.Adaylar?.DogumYeri) ? data.Adaylar.DogumYeri : "",

                        Yas = !string.IsNullOrEmpty(data.Adaylar?.DogumTarihi2) && DateTime.TryParse(data.Adaylar.DogumTarihi2, out DateTime dogumTarihi) ? (int)((DateTime.Now - dogumTarihi).TotalDays / 365.25) : 0,

                        IkametBilgisi = data.Adaylar.AdayIletisimBilgileri != null ?
                        data.Adaylar.AdayIletisimBilgileri.Select(x => x.IkametIl).FirstOrDefault() + "/" +
                        data.Adaylar.AdayIletisimBilgileri.Select(x => x.IkametIlce).FirstOrDefault() : "",

                        YLisans = data.YLisans,
                        Doktora = data.Doktora,
                        Sendika = data.Sendika,
                        SendikaAck = data.SendikaAck,
                        MYYSSoruItiraz = data.MYYSSoruItiraz,
                        MYYSSonucItiraz = data.MYYSSonucItiraz,
                        BasvuruBrans = data.BasvuruBrans,
                        BransId = data.BransId,
                        AdliSicilBelge = data.AdliSicilBelge,
                        DereceId = data.DereceId,
                        DereceAdi = data.DereceAdi,
                        Unvan = data.Unvan,
                        UlkeTercihId = data.UlkeTercihId,
                        MulakatId = data.MulakatId,
                        KayitTarihi = data.KayitTarihi,
                        KaydedenId = data.KaydedenId
                    }).ToList();

                    foreach (var aday in adaylar)
                    {
                        if (aday.MulakatId.HasValue)
                        {
                            try
                            {
                                System.Diagnostics.Debug.WriteLine($"Business Engine - {aday.TC} için mülakat bilgileri alınıyor. MulakatId: {aday.MulakatId}");

                                // Önce mülakat verisini direkt veritabanından alalım
                                var mulakat = _unitOfWork.mulakatlarRepository.Get((Guid)aday.MulakatId);
                                if (mulakat != null && mulakat.YazılıSinavTarihi != default(DateTime))
                                {
                                    aday.MulakatYil = mulakat.YazılıSinavTarihi.Year;
                                    System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı veritabanından alındı: {aday.MulakatYil}");
                                }
                                else
                                {
                                    // Eğer veritabanından alamazsak servisten deneyelim
                                    var mulakatYil = _mulakatOlusturBE.MulakatYilGetir((Guid)aday.MulakatId);
                                    if (mulakatYil.IsSuccess && !string.IsNullOrEmpty(mulakatYil.Data))
                                    {
                                        aday.MulakatYil = int.Parse(mulakatYil.Data);
                                        System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı servisten alındı: {aday.MulakatYil}");
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı alınamadı");
                                    }
                                }

                                // Ülke tercih bilgisini alalım
                                if (aday.UlkeTercihId != null)
                                {
                                    var ulkeTercih = _unitOfWork.ulkeTercihRepository.Get((Guid)aday.UlkeTercihId);
                                    if (ulkeTercih != null && ulkeTercih.UlkeTercihId != null)
                                    {
                                        var ulke = _unitOfWork.ulkeTercihRepository.Get((Guid)ulkeTercih.UlkeTercihId);
                                        if (ulke != null)
                                        {
                                            aday.UlkeTercihAdi = ulke.UlkeTercihAdi;
                                            System.Diagnostics.Debug.WriteLine($"Business Engine - Ülke adı veritabanından alındı: {aday.UlkeTercihAdi}");
                                        }
                                    }
                                }

                                var mulakatOnay = _mulakatOlusturBE.MulakatOnayGetir((Guid)aday.MulakatId);
                                if (mulakatOnay.IsSuccess && !string.IsNullOrEmpty(mulakatOnay.Data))
                                {
                                    aday.MulakatOnayNo = mulakatOnay.Data;
                                    System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat onay no alındı: {mulakatOnay.Data}");
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat bilgileri alınırken hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                            }
                        }
                    }

                    return new Result<List<AdayBasvuruBilgileriVM>>(true, ResultConstant.RecordFound, adaylar);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Business Engine - TC ile veri bulunamadı: {TC}");
                    return new Result<List<AdayBasvuruBilgileriVM>>(false, "TC numarası ile kayıt bulunamadı");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new Result<List<AdayBasvuruBilgileriVM>>(false, "Veri getirme hatası: " + ex.Message);
            }
        }
        #endregion

        #region AdayBasvuruBilgileriniGetirById
        public Result<AdayBasvuruBilgileriVM> AdayBasvuruBilgileriniGetirById(Guid id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Id ile sorgu başladı: {id}");

                var data = _unitOfWork.adayBasvuruBilgileriRepository.Get(id);
                if (data != null)
                {
                    var aday = new AdayBasvuruBilgileriVM()
                    {
                        Id = data.Id,
                        TC = data.TC,
                        AdliSicilBelge = data.AdliSicilBelge,
                        HasAdliSicilBelge = data.AdliSicilBelge != null && data.AdliSicilBelge.Length > 0
                    };

                    System.Diagnostics.Debug.WriteLine($"Business Engine - Veri bulundu. TC: {aday.TC}");
                    return new Result<AdayBasvuruBilgileriVM>(true, ResultConstant.RecordFound, aday);
                }

                System.Diagnostics.Debug.WriteLine($"Business Engine - Veri bulunamadı. Id: {id}");
                return new Result<AdayBasvuruBilgileriVM>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new Result<AdayBasvuruBilgileriVM>(false, ResultConstant.RecordNotFound + " | " + ex.Message);
            }
        }
        #endregion

        #region AdayIletisimBilgileriEkle
        public Result<AdayIletisimBilgileriVM> AdayIletisimBilgileriEkle(AdayIletisimBilgileriVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var Aday= _mapper.Map<AdayIletisimBilgileriVM, AdayIletisimBilgileri>(model);
                    Aday.KaydedenId = user.LoginId;

                    _unitOfWork.adayIletisimBilgileriRepository.Add(Aday);
                    _unitOfWork.Save();
                    return new Result<AdayIletisimBilgileriVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<AdayIletisimBilgileriVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<AdayIletisimBilgileriVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region AdayIletisimBilgileriniGetir
        public Result<List<AdayIletisimBilgileriVM>> AdayIletisimBilgileriniGetir(string TC)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - TC ile sorgu başladı: {TC}");

                if (string.IsNullOrEmpty(TC))
                {
                    System.Diagnostics.Debug.WriteLine("Business Engine - TC boş");
                    return new Result<List<AdayIletisimBilgileriVM>>(false, "TC kimlik numarası boş olamaz");
                }

                System.Diagnostics.Debug.WriteLine("Business Engine - Repository'den veri çekiliyor");
                var data = _unitOfWork.adayIletisimBilgileriRepository.GetAll(x => x.TC == TC).ToList();

                if (data != null && data.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"Business Engine - {data.Count} adet veri bulundu. TC: {TC}");

                    var adaylar = data.Select(data => new AdayIletisimBilgileriVM()
                    {
                        Id = data.Id,
                        TC = data.TC,
                        CepTelNo=data.CepTelNo,
                        EPosta=data.EPosta,
                        NufusIl=data.NufusIl,
                        NufusIlce=data.NufusIlce,
                        IkametAdres=data.IkametAdres,
                        IkametIl=data.IkametIl,
                        IkametIlce=data.IkametIlce,
                        MulakatId = data.MulakatId,
                        KayitTarihi = data.KayitTarihi,
                        KaydedenId = data.KaydedenId
                    }).ToList();

                    foreach (var aday in adaylar)
                    {
                        if (aday.MulakatId.HasValue)
                        {
                            try
                            {
                                System.Diagnostics.Debug.WriteLine($"Business Engine - {aday.TC} için mülakat bilgileri alınıyor. MulakatId: {aday.MulakatId}");

                                // Önce mülakat verisini direkt veritabanından alalım
                                var mulakat = _unitOfWork.mulakatlarRepository.Get((Guid)aday.MulakatId);
                                if (mulakat != null && mulakat.YazılıSinavTarihi != default(DateTime))
                                {
                                    aday.MulakatYil = mulakat.YazılıSinavTarihi.Year;
                                    System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı veritabanından alındı: {aday.MulakatYil}");
                                }
                                else
                                {
                                    // Eğer veritabanından alamazsak servisten deneyelim
                                    var mulakatYil = _mulakatOlusturBE.MulakatYilGetir((Guid)aday.MulakatId);
                                    if (mulakatYil.IsSuccess && !string.IsNullOrEmpty(mulakatYil.Data))
                                    {
                                        aday.MulakatYil = int.Parse(mulakatYil.Data);
                                        System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı servisten alındı: {aday.MulakatYil}");
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı alınamadı");
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat bilgileri alınırken hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                            }
                        }
                    }

                    return new Result<List<AdayIletisimBilgileriVM>>(true, ResultConstant.RecordFound, adaylar);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Business Engine - TC ile veri bulunamadı: {TC}");
                    return new Result<List<AdayIletisimBilgileriVM>>(false, "TC numarası ile kayıt bulunamadı");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new Result<List<AdayIletisimBilgileriVM>>(false, "Veri getirme hatası: " + ex.Message);
            }
        }
        #endregion

        #region AdayIletisimBilgileriniGetirById
        public Result<AdayIletisimBilgileriVM> AdayIletisimBilgileriniGetirById(Guid id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Id ile sorgu başladı: {id}");

                var data = _unitOfWork.adayIletisimBilgileriRepository.Get(id);
                if (data != null)
                {
                    var aday = new AdayIletisimBilgileriVM()
                    {
                        Id = data.Id,
                        TC = data.TC,
                    };

                    System.Diagnostics.Debug.WriteLine($"Business Engine - Veri bulundu. TC: {aday.TC}");
                    return new Result<AdayIletisimBilgileriVM>(true, ResultConstant.RecordFound, aday);
                }

                System.Diagnostics.Debug.WriteLine($"Business Engine - Veri bulunamadı. Id: {id}");
                return new Result<AdayIletisimBilgileriVM>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new Result<AdayIletisimBilgileriVM>(false, ResultConstant.RecordNotFound + " | " + ex.Message);
            }
        }
        #endregion

        #region AdayMYSSBilgileriEkle
        public Result<AdayMYSSVM> AdayMYSSBilgileriEkle(AdayMYSSVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var Aday = _mapper.Map<AdayMYSSVM, AdayMYSS>(model);
                    Aday.KaydedenId = user.LoginId;

                    _unitOfWork.adayMYSSRepository.Add(Aday);
                    _unitOfWork.Save();
                    return new Result<AdayMYSSVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<AdayMYSSVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<AdayMYSSVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region AdayMYSSBilgileriniGetir
        public Result<List<AdayMYSSVM>> AdayMYSSBilgileriniGetir(string TC)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - TC ile sorgu başladı: {TC}");

                if (string.IsNullOrEmpty(TC))
                {
                    System.Diagnostics.Debug.WriteLine("Business Engine - TC boş");
                    return new Result<List<AdayMYSSVM>>(false, "TC kimlik numarası boş olamaz");
                }

                System.Diagnostics.Debug.WriteLine("Business Engine - Repository'den veri çekiliyor");
                var data = _unitOfWork.adayMYSSRepository.GetAll(x => x.TC == TC).ToList();

                if (data != null && data.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"Business Engine - {data.Count} adet veri bulundu. TC: {TC}");

                    var adaylar = data.Select(data => new AdayMYSSVM()
                    {
                        Id = data.Id,
                        TC = data.TC,
                        MYSSTarih = data.MYSSTarih,
                        MYSSSaat = data.MYSSSaat,
                        MYSSMulakatYer = data.MYSSMulakatYer,
                        MYSSDurum = data.MYSSDurum,
                        MYSSDurumAck = data.MYSSDurumAck,
                        MYSSKomisyonSiraNo = data.MYSSKomisyonSiraNo,
                        MYSSKomisyonAdi = data.MYSSKomisyonAdi,
                        KomisyonSN = data.KomisyonSN,
                        KomisyonGunSN = data.KomisyonGunSN,
                        CagriDurum = data.CagriDurum ?? false,
                        KabulDurum = data.KabulDurum ?? false,
                        SinavDurum = data.SinavDurum ?? false,
                        SinavaGelmedi = data.SinavaGelmedi ?? false,
                        SinavaGelmediAck = data.SinavaGelmediAck,
                        MYSPuan= data.MYSPuan,
                        MYSSonuc = data.MYSSonuc,
                        MYSSonucAck = data.MYSSonucAck,
                        MYSSSorulanSoruNo = data.MYSSSorulanSoruNo,
                        SinavIptal = data.SinavIptal ?? false,
                        SinavIptalAck = data.SinavIptalAck,
                        AdayId = data.AdayId,
                        MulakatId = data.MulakatId,
                        KaydedenId = data.KaydedenId
                    }).ToList();

                    foreach (var aday in adaylar)
                    {
                        if (aday.MulakatId.HasValue)
                        {
                            try
                            {
                                System.Diagnostics.Debug.WriteLine($"Business Engine - {aday.TC} için mülakat bilgileri alınıyor. MulakatId: {aday.MulakatId}");

                                // Önce mülakat verisini direkt veritabanından alalım
                                var mulakat = _unitOfWork.mulakatlarRepository.Get((Guid)aday.MulakatId);
                                if (mulakat != null && mulakat.YazılıSinavTarihi != default(DateTime))
                                {
                                    aday.MulakatYil = mulakat.YazılıSinavTarihi.Year;
                                    System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı veritabanından alındı: {aday.MulakatYil}");
                                }
                                else
                                {
                                    // Eğer veritabanından alamazsak servisten deneyelim
                                    var mulakatYil = _mulakatOlusturBE.MulakatYilGetir((Guid)aday.MulakatId);
                                    if (mulakatYil.IsSuccess && !string.IsNullOrEmpty(mulakatYil.Data))
                                    {
                                        aday.MulakatYil = int.Parse(mulakatYil.Data);
                                        System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı servisten alındı: {aday.MulakatYil}");
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı alınamadı");
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat bilgileri alınırken hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                            }
                        }
                    }

                    return new Result<List<AdayMYSSVM>>(true, ResultConstant.RecordFound, adaylar);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Business Engine - TC ile veri bulunamadı: {TC}");
                    return new Result<List<AdayMYSSVM>>(false, "TC numarası ile kayıt bulunamadı");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new Result<List<AdayMYSSVM>>(false, "Veri getirme hatası: " + ex.Message);
            }
        } 
        #endregion

        #region AdayMYSSBilgileriniGetirById
        public Result<AdayMYSSVM> AdayMYSSBilgileriniGetirById(Guid id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Id ile sorgu başladı: {id}");

                var data = _unitOfWork.adayMYSSRepository.Get(id);
                if (data != null)
                {
                    var aday = new AdayMYSSVM()
                    {
                        Id = data.Id,
                        TC = data.TC,
                    };

                    System.Diagnostics.Debug.WriteLine($"Business Engine - Veri bulundu. TC: {aday.TC}");
                    return new Result<AdayMYSSVM>(true, ResultConstant.RecordFound, aday);
                }

                System.Diagnostics.Debug.WriteLine($"Business Engine - Veri bulunamadı. Id: {id}");
                return new Result<AdayMYSSVM>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new Result<AdayMYSSVM>(false, ResultConstant.RecordNotFound + " | " + ex.Message);
            }
        } 
        #endregion

        #region AdayMYSSBilgileriGuncelle
        public Result<AdayMYSSVM> AdayMYSSBilgileriGuncelle(AdayMYSSVM model, SessionContext user)
        {
            var result = new Result<AdayMYSSVM>();
            try
            {
                var data = _unitOfWork.adayMYSSRepository.GetFirstOrDefault(x => x.Id == model.Id);
                if (data != null)
                {
                    data.TC = model.TC;
                    data.MYSSTarih = model.MYSSTarih;
                    data.MYSSSaat = model.MYSSSaat;
                    data.MYSSMulakatYer = model.MYSSMulakatYer;
                    data.MYSSDurum = model.MYSSDurum;
                    data.MYSSDurumAck = model.MYSSDurumAck;
                    data.MYSSKomisyonSiraNo = model.MYSSKomisyonSiraNo;
                    data.MYSSKomisyonAdi = model.MYSSKomisyonAdi;
                    data.KomisyonId = model.KomisyonId;
                    data.KomisyonSN = model.KomisyonSN;
                    data.KomisyonGunSN = model.KomisyonGunSN;
                    data.CagriDurum = model.CagriDurum;
                    data.KabulDurum = model.KabulDurum;
                    data.SinavDurum = model.SinavDurum;
                    data.SinavaGelmedi = model.SinavaGelmedi;
                    data.SinavaGelmediAck = model.SinavaGelmediAck;
                    data.MYSPuan = model.MYSPuan;
                    data.MYSSonuc = model.MYSSonuc;
                    data.MYSSonucAck = model.MYSSonucAck;
                    data.MYSSSorulanSoruNo = model.MYSSSorulanSoruNo;
                    data.SinavIptal = model.SinavIptal;
                    data.SinavIptalAck = model.SinavIptalAck;
                    data.AdayId = model.AdayId.Value;
                    data.MulakatId = model.MulakatId.Value;
                    data.KaydedenId = user.LoginId;

                    _unitOfWork.adayMYSSRepository.Update(data);
                    _unitOfWork.Save();

                    result.Data = model;
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Kayıt bulunamadı!";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Bir hata oluştu: {ex.Message}";
            }
            return result;
        }
        #endregion      

        #region AdayTYSBilgileriEkle
        public Result<AdayTYSVM> AdayTYSBilgileriEkle(AdayTYSVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var Aday = _mapper.Map<AdayTYSVM, AdayTYS>(model);
                    Aday.KaydedenId = user.LoginId;

                    _unitOfWork.adayTYSRepository.Add(Aday);
                    _unitOfWork.Save();
                    return new Result<AdayTYSVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<AdayTYSVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<AdayTYSVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region AdayTYSBilgileriniGetir
        public Result<List<AdayTYSVM>> AdayTYSBilgileriniGetir(string TC)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - TC ile sorgu başladı: {TC}");

                if (string.IsNullOrEmpty(TC))
                {
                    System.Diagnostics.Debug.WriteLine("Business Engine - TC boş");
                    return new Result<List<AdayTYSVM>>(false, "TC kimlik numarası boş olamaz");
                }

                System.Diagnostics.Debug.WriteLine("Business Engine - Repository'den veri çekiliyor");
                var data = _unitOfWork.adayTYSRepository.GetAll(x => x.TC == TC).ToList();

                if (data != null && data.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"Business Engine - {data.Count} adet veri bulundu. TC: {TC}");

                    var adaylar = data.Select(data => new AdayTYSVM()
                    {
                        Id = data.Id,
                        TC = data.TC,
                        TYSTarih = data.TYSTarih,
                        TYSSaat = data.TYSSaat,
                        TYSMulakatYer = data.TYSMulakatYer,
                        TYSDurumu = data.TYSDurumu,
                        TYSDurumAck = data.TYSDurumAck,
                        TYSKomisyonSiraNo = data.TYSKomisyonSiraNo,
                        TYSKomisyonAdi = data.TYSKomisyonAdi,
                        KomisyonId = data.KomisyonId,
                        KomisyonSN = data.KomisyonSN,
                        KomisyonGunSN = data.KomisyonGunSN,
                        CagriDurum = data.CagriDurum ?? false,
                        KabulDurum = data.KabulDurum ?? false,
                        SinavDurum = data.SinavDurum ?? false,
                        SinavaGelmedi = data.SinavaGelmedi ?? false,
                        SinavaGelmediAck = data.SinavaGelmediAck,
                        TYSPuan = data.TYSPuan,
                        TYSSonuc = data.TYSSonuc,
                        TYSSonucAck = data.TYSSonucAck,
                        TYSSorulanSoruNo = data.TYSSorulanSoruNo,
                        SinavIptal = data.SinavIptal ?? false,
                        SinavIptalAck = data.SinavIptalAck,
                        AdayId = data.AdayId,
                        MulakatId = data.MulakatId,
                        KaydedenId = data.KaydedenId
                    }).ToList();

                    foreach (var aday in adaylar)
                    {
                        if (aday.MulakatId.HasValue)
                        {
                            try
                            {
                                System.Diagnostics.Debug.WriteLine($"Business Engine - {aday.TC} için mülakat bilgileri alınıyor. MulakatId: {aday.MulakatId}");

                                // Önce mülakat verisini direkt veritabanından alalım
                                var mulakat = _unitOfWork.mulakatlarRepository.Get((Guid)aday.MulakatId);
                                if (mulakat != null && mulakat.YazılıSinavTarihi != default(DateTime))
                                {
                                    aday.MulakatYil = mulakat.YazılıSinavTarihi.Year;
                                    System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı veritabanından alındı: {aday.MulakatYil}");
                                }
                                else
                                {
                                    // Eğer veritabanından alamazsak servisten deneyelim
                                    var mulakatYil = _mulakatOlusturBE.MulakatYilGetir((Guid)aday.MulakatId);
                                    if (mulakatYil.IsSuccess && !string.IsNullOrEmpty(mulakatYil.Data))
                                    {
                                        aday.MulakatYil = int.Parse(mulakatYil.Data);
                                        System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı servisten alındı: {aday.MulakatYil}");
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat yılı alınamadı");
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Business Engine - Mülakat bilgileri alınırken hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                            }
                        }
                    }

                    return new Result<List<AdayTYSVM>>(true, ResultConstant.RecordFound, adaylar);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Business Engine - TC ile veri bulunamadı: {TC}");
                    return new Result<List<AdayTYSVM>>(false, "TC numarası ile kayıt bulunamadı");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new Result<List<AdayTYSVM>>(false, "Veri getirme hatası: " + ex.Message);
            }
        } 
        #endregion

        #region AdayTYSBilgileriniGetirById
        public Result<AdayTYSVM> AdayTYSBilgileriniGetirById(Guid id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Id ile sorgu başladı: {id}");

                var data = _unitOfWork.adayTYSRepository.Get(id);
                if (data != null)
                {
                    var aday = new AdayTYSVM()
                    {
                        Id = data.Id,
                        TC = data.TC,
                    };

                    System.Diagnostics.Debug.WriteLine($"Business Engine - Veri bulundu. TC: {aday.TC}");
                    return new Result<AdayTYSVM>(true, ResultConstant.RecordFound, aday);
                }

                System.Diagnostics.Debug.WriteLine($"Business Engine - Veri bulunamadı. Id: {id}");
                return new Result<AdayTYSVM>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Business Engine - Hata: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new Result<AdayTYSVM>(false, ResultConstant.RecordNotFound + " | " + ex.Message);
            }
        } 
        #endregion

        #region AdayTYSBilgileriGetir
        public Result<AdayTYSVM> AdayTYSBilgileriGetir(string TC)
        {
            var result = new Result<AdayTYSVM>();
            try
            {
                var data = _unitOfWork.adayTYSRepository.GetFirstOrDefault(x => x.TC == TC);
                if (data != null)
                {
                    var model = new AdayTYSVM
                    {
                        Id = data.Id,
                        TC = data.TC,
                        TYSTarih = data.TYSTarih,
                        TYSSaat = data.TYSSaat,
                        TYSMulakatYer = data.TYSMulakatYer,
                        TYSDurumu = data.TYSDurumu,
                        TYSDurumAck = data.TYSDurumAck,
                        TYSKomisyonSiraNo = data.TYSKomisyonSiraNo,
                        TYSKomisyonAdi = data.TYSKomisyonAdi,
                        KomisyonId = data.KomisyonId,
                        KomisyonSN = data.KomisyonSN,
                        KomisyonGunSN = data.KomisyonGunSN,
                        CagriDurum = data.CagriDurum ?? false,
                        KabulDurum = data.KabulDurum ?? false,
                        SinavDurum = data.SinavDurum ?? false,
                        SinavaGelmedi = data.SinavaGelmedi ?? false,
                        SinavaGelmediAck = data.SinavaGelmediAck,
                        TYSPuan = data.TYSPuan.ToString(),
                        TYSSonuc = data.TYSSonuc,
                        TYSSonucAck = data.TYSSonucAck,
                        TYSSorulanSoruNo = data.TYSSorulanSoruNo,
                        SinavIptal = data.SinavIptal ?? false,
                        SinavIptalAck = data.SinavIptalAck,
                        AdayId = data.AdayId,
                        MulakatId = data.MulakatId,
                        KaydedenId = data.KaydedenId
                    };

                    result.Data = model;
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Kayıt bulunamadı!";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Bir hata oluştu: {ex.Message}";
            }
            return result;
        }
        #endregion

        #region AdayTYSBilgileriGetir
        public Result<List<AdayTYSVM>> AdayTYSBilgileriGetir()
        {
            var result = new Result<List<AdayTYSVM>>();
            try
            {
                var adaylar = (from a in _unitOfWork.adayTYSRepository.GetAll()
                              join k in _unitOfWork.komisyonlarRepository.GetAll() on a.KomisyonId equals k.KomisyonId into komisyonlar
                              from k in komisyonlar.DefaultIfEmpty()
                              select new AdayTYSVM
                              {
                                  TC = a.TC,
                                  TYSTarih = a.TYSTarih,
                                  TYSSaat = a.TYSSaat,
                                  TYSMulakatYer = a.TYSMulakatYer,
                                  TYSDurumu = a.TYSDurumu,
                                  TYSDurumAck = a.TYSDurumAck,
                                  TYSKomisyonSiraNo = a.TYSKomisyonSiraNo,
                                  TYSKomisyonAdi = k != null ? k.KomisyonAdi : null,
                                  KomisyonId = k != null ? (Guid?)k.KomisyonId : (Guid?)null,
                                  KomisyonSN = a.KomisyonSN,
                                  KomisyonGunSN = a.KomisyonGunSN,
                                  CagriDurum = a.CagriDurum ?? false,
                                  KabulDurum = a.KabulDurum ?? false,
                                  SinavDurum = a.SinavDurum ?? false,
                                  SinavaGelmedi = a.SinavaGelmedi ?? false,
                                  SinavaGelmediAck = a.SinavaGelmediAck,
                                  TYSPuan = a.TYSPuan.ToString(),
                                  TYSSonuc = a.TYSSonuc,
                                  TYSSonucAck = a.TYSSonucAck,
                                  TYSSorulanSoruNo = a.TYSSorulanSoruNo,
                                  SinavIptal = a.SinavIptal ?? false,
                                  AdayId = a.AdayId,
                                  MulakatId = a.MulakatId,
                                  KaydedenId = a.KaydedenId,

                              }).ToList();

                if (adaylar != null && adaylar.Any())
                {
                    result.Data = adaylar;
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Kayıt bulunamadı!";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Bir hata oluştu: {ex.Message}";
            }
            return result;
        }
        #endregion

        #region GetirKomisyonMulakatListesi
        public Result<List<AdayMYSSVM>> GetirKomisyonMulakatListesi(string komisyonAdi, string mulakatTarihi)
        {
            try
            {
                _logger.LogInformation($"GetirKomisyonMulakatListesi başladı - Komisyon: {komisyonAdi}, Tarih: {mulakatTarihi}");

                var data = _unitOfWork.adayMYSSRepository.GetAll()
                    .Include(x => x.Adaylar)
                    .Include(x => x.Mulakatlar)
                    .Where(x => x.MYSSKomisyonAdi.ToUpper() == komisyonAdi.ToUpper() &&
                               x.MYSSTarih == mulakatTarihi &&
                               x.Mulakatlar != null && x.Mulakatlar.Durumu == true)
                    .OrderBy(x => x.KomisyonGunSN);

                if (!data.Any())
                {
                    _logger.LogWarning($"Komisyon {komisyonAdi} için {mulakatTarihi} tarihinde kayıt bulunamadı.");
                    return new Result<List<AdayMYSSVM>>(false, "Kayıt bulunamadı");
                }

                var adaylar = data.Select(x => new AdayMYSSVM
                {
                    Id = x.Id,
                    AdayId = x.AdayId,
                    TC = x.TC,
                    AdayAdiSoyadi = x.Adaylar != null ? x.Adaylar.Ad.ToString() + " " + x.Adaylar.Soyad.ToString() : "",
                    MYSSTarih = !string.IsNullOrEmpty(x.MYSSTarih) ? x.MYSSTarih : "",
                    MYSSSaat = x.MYSSSaat,
                    MYSSKomisyonAdi = x.MYSSKomisyonAdi,
                    KomisyonGunSN = x.KomisyonGunSN,
                    MYSSonuc = x.MYSSonuc,
                    MYSPuan = x.MYSPuan,
                    CagriDurum = x.CagriDurum ?? false,
                    KabulDurum = x.KabulDurum ?? false, 
                    SinavDurum = x.SinavDurum ?? false,
                    SinavaGelmedi = x.SinavaGelmedi ?? false,
                    SinavaGelmediAck = x.SinavaGelmediAck,
                    SinavaGeldi = x.SinavaGeldi ?? false,
                    BransId = x.BransId,
                    BransAdi = x.BransAdi.ToString(),
                    UlkeTercihId = x.UlkeTercihId,
                    UlkeTercihAdi = x.UlkeTercihAdi.ToString(),
                    DereceId = x.DereceId,
                    DereceAdi = x.DereceAdi.ToString(),
                    KomisyonId = x.KomisyonId,
                    KomisyonSN = x.KomisyonSN,
                    SinavIptal = x.SinavIptal ?? false,
                    SinavIptalAck = x.SinavIptalAck,
                    MYSSSorulanSoruNo = x.MYSSSorulanSoruNo
                }).ToList();

                foreach (var aday in adaylar)
                {
                    _logger.LogInformation($"Aday Durumları - TC: {aday.TC}, CagriDurum: {aday.CagriDurum}, KabulDurum: {aday.KabulDurum}, SinavaGelmedi: {aday.SinavaGelmedi}");
                }

                return new Result<List<AdayMYSSVM>>(true, ResultConstant.RecordFound, adaylar);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetirKomisyonMulakatListesi - Hata: {ex.Message}", ex);
                return new Result<List<AdayMYSSVM>>(false, $"Hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region AdayTakipMulakatListesiGetir
        public Result<List<AdayMYSSVM>> AdayTakipMulakatListesi()
        {
            try
            {
                var data = _unitOfWork.adayMYSSRepository.GetAll(
                    x => x.CagriDurum == true && x.SinavaGeldi == false && x.SinavaGelmedi==false && x.Mulakatlar.Durumu==true,                    
                     includeProperties: "Adaylar,Mulakatlar")
                    .OrderBy(x => x.KomisyonGunSN);

                if (data != null && data.Any())
                {
                    var adaylar = data.Select(x => new AdayMYSSVM()
                    {
                        Id = x.Id,
                        AdayId = x.AdayId,
                        TC = x.TC,
                        AdayAdiSoyadi = x.Adaylar != null ? x.Adaylar.Ad.ToString() + " " + x.Adaylar.Soyad.ToString() : "",
                        MYSSTarih = !string.IsNullOrEmpty(x.MYSSTarih) ? x.MYSSTarih : "",
                        MYSSSaat = x.MYSSSaat,
                        MYSSKomisyonAdi = x.MYSSKomisyonAdi,
                        KomisyonGunSN = x.KomisyonGunSN,
                        MYSSonuc = x.MYSSonuc,
                        MYSPuan = x.MYSPuan,
                        CagriDurum = x.CagriDurum ?? false,
                        SinavDurum = x.SinavDurum ?? false,
                        SinavaGelmedi = x.SinavaGelmedi ?? false,
                        SinavaGelmediAck = x.SinavaGelmediAck,
                        SinavaGeldi = x.SinavaGeldi ?? false,
                        SinavIptal = x.SinavIptal ?? false,
                        BransId = x.BransId,
                        BransAdi = x.BransAdi.ToString(),
                        UlkeTercihId = x.UlkeTercihId,
                        UlkeTercihAdi = x.UlkeTercihAdi.ToString(),
                        DereceId = x.DereceId,
                        DereceAdi = x.DereceAdi.ToString(),
                        KomisyonId = x.KomisyonId,
                        KomisyonSN = x.KomisyonSN,
                        SinavIptalAck = x.SinavIptalAck,
                        MYSSSorulanSoruNo = x.MYSSSorulanSoruNo

                    }).ToList();

                    return new Result<List<AdayMYSSVM>>(true, ResultConstant.RecordFound, adaylar);
                }

                return new Result<List<AdayMYSSVM>>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdayTakipMulakatListesi - Hata: {ex.Message}", ex);
                return new Result<List<AdayMYSSVM>>(false, ResultConstant.RecordNotFound + " | " + ex.Message);
            }
        }
        #endregion

        #region AdayKabulMulakatListesi
        public Result<List<AdayMYSSVM>> AdayKabulMulakatListesi()
        {
            try
            {
                var data = _unitOfWork.adayMYSSRepository.GetAll(
                    x => x.CagriDurum == true && x.SinavaGeldi == true && x.SinavaAlindi == false && x.Mulakatlar.Durumu == true,
                     includeProperties: "Adaylar,Mulakatlar")
                    .OrderBy(x => x.KomisyonGunSN);

                if (data != null && data.Any())
                {
                    var adaylar = data.Select(x => new AdayMYSSVM()
                    {
                        Id = x.Id,
                        AdayId = x.AdayId,
                        TC = x.TC,
                        AdayAdiSoyadi = x.Adaylar != null ? x.Adaylar.Ad.ToString() + " " + x.Adaylar.Soyad.ToString() : "",
                        MYSSTarih = !string.IsNullOrEmpty(x.MYSSTarih) ? x.MYSSTarih : "",
                        MYSSSaat = x.MYSSSaat,
                        MYSSKomisyonAdi = x.MYSSKomisyonAdi,
                        KabulDurum = x.KabulDurum,
                        KomisyonGunSN = x.KomisyonGunSN,
                        MYSSonuc = x.MYSSonuc,
                        MYSPuan = x.MYSPuan,
                        CagriDurum = x.CagriDurum ?? false,
                        SinavDurum = x.SinavDurum ?? false,
                        SinavaGelmedi = x.SinavaGelmedi ?? false,
                        SinavaGelmediAck = x.SinavaGelmediAck,
                        SinavaGeldi = x.SinavaGeldi ?? false,
                        SinavIptal = x.SinavIptal ?? false,
                        SinavaAlindi = x.SinavaAlindi ?? false,
                        BransId = x.BransId,
                        BransAdi = x.BransAdi.ToString(),
                        UlkeTercihId = x.UlkeTercihId,
                        UlkeTercihAdi = x.UlkeTercihAdi.ToString(),
                        DereceId = x.DereceId,
                        DereceAdi = x.DereceAdi.ToString(),
                        KomisyonId = x.KomisyonId,
                        KomisyonSN = x.KomisyonSN,
                        SinavIptalAck = x.SinavIptalAck,
                        MYSSSorulanSoruNo = x.MYSSSorulanSoruNo

                    }).ToList();

                    return new Result<List<AdayMYSSVM>>(true, ResultConstant.RecordFound, adaylar);
                }

                return new Result<List<AdayMYSSVM>>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdayKabulMulakatListesi - Hata: {ex.Message}", ex);
                return new Result<List<AdayMYSSVM>>(false, ResultConstant.RecordNotFound + " | " + ex.Message);
            }
        }
        #endregion

        #region AdayIletisimBilgileriGetir
        public Result<string> AdayIletisimBilgileriGetir(Guid adayId)
        {
            try
            {
                _logger.LogInformation($"AdayIletisimBilgileriGetir - Başlangıç - AdayId: {adayId}");

                // Tüm kayıtları getir ve logla
                var tumKayitlar = _unitOfWork.adayIletisimBilgileriRepository.GetAll().ToList();
                _logger.LogInformation($"Toplam kayıt sayısı: {tumKayitlar.Count}");
                
                // İletişim bilgilerini getir
                var iletisimBilgisi = tumKayitlar.FirstOrDefault(x => x.AdayId == adayId);
                
                if (iletisimBilgisi == null)
                {
                    _logger.LogWarning($"AdayIletisimBilgileriGetir - İletişim bilgisi bulunamadı - AdayId: {adayId}");
                    return new Result<string>(true, "Kayıt bulunamadı", "İletişim bilgisi sistemde mevcut değil");
                }

                _logger.LogInformation($"AdayIletisimBilgileriGetir - CepTelNo: {iletisimBilgisi.CepTelNo ?? "Null"}");

                if (string.IsNullOrEmpty(iletisimBilgisi.CepTelNo))
                {
                    _logger.LogWarning($"AdayIletisimBilgileriGetir - Telefon numarası boş - AdayId: {adayId}");
                    return new Result<string>(true, "Kayıt bulundu", "Telefon numarası girilmemiş");
                }

                _logger.LogInformation($"AdayIletisimBilgileriGetir - Başarılı - AdayId: {adayId}, Telefon: {iletisimBilgisi.CepTelNo}");
                return new Result<string>(true, "Kayıt bulundu", iletisimBilgisi.CepTelNo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdayIletisimBilgileriGetir - Hata: {ex.Message}", ex);
                return new Result<string>(false, "İletişim bilgileri alınırken bir hata oluştu", null);
            }
        }
        #endregion        

        #region AdayCagriDurumGuncelle
        public Result<bool> AdayCagriDurumGuncelle(Guid id)
        {
            try
            {
                var aday = _unitOfWork.adayMYSSRepository
                    .GetFirstOrDefault(x => x.Id == id && x.Mulakatlar.Durumu == true);

                if (aday == null)
                {
                    return new Result<bool>(false, "Aday bulunamadı veya mülakat aktif değil");
                }

                // Durum kontrolü
                if (aday.CagriDurum == true)
                {
                    return new Result<bool>(false, "Aday zaten çağrılmış durumda");
                }

                aday.CagriDurum = true;

                _unitOfWork.Save();
                //transaction.CommitAsync();

                return new Result<bool>(true, "Aday çağrı durumu güncellendi");
            }
            catch (Exception ex)
            {
                //transaction.Rollback();
                _logger.LogError($"AdayCagriDurumGuncelle - Hata: {ex.Message}", ex);
                return new Result<bool>(false, $"Güncelleme sırasında hata: {ex.Message}");
            }
        }
        #endregion

        #region AdaySinavaGelmediGuncelle
        public Result<bool> AdaySinavaGelmediGuncelle(Guid id)
        {
            try
            {
                var aday = _unitOfWork.adayMYSSRepository.Get(id);

                if (aday != null)
                {
                    aday.SinavaGelmedi = true;
                    aday.MYSSDurumAck = "Sınava Gelmedi";
                    _unitOfWork.Save();

                    return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
                }

                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdayCagriDurumGuncelle - Hata: {ex.Message}", ex);
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region AdaySinavaGeldiGuncelle
        public Result<bool> AdaySinavaGeldiGuncelle(Guid id)
        {
            try
            {
                var aday = _unitOfWork.adayMYSSRepository.Get(id);

                if (aday != null)
                {
                    aday.SinavaGeldi = true;                    
                    _unitOfWork.Save();

                    return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
                }

                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdayCagriDurumGuncelle - Hata: {ex.Message}", ex);
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region AdaySinavKabulGuncelle
        public Result<bool> AdaySinavKabulGuncelle(Guid id)
        {
            try
            {
                var aday = _unitOfWork.adayMYSSRepository.Get(id);

                if (aday != null)
                {
                    aday.KabulDurum = true;
                    _unitOfWork.Save();

                    return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
                }

                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdaySinavKabulGuncelle - Hata: {ex.Message}", ex);
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region AdaySinavOdaKabulGuncelle
        public Result<bool> AdaySinavOdaKabulGuncelle(Guid id)
        {
            try
            {
                var aday = _unitOfWork.adayMYSSRepository.Get(id);

                if (aday != null)
                {
                    aday.SinavDurum = true;
                    _unitOfWork.Save();

                    return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
                }

                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdaySinavOdaKabulGuncelle - Hata: {ex.Message}", ex);
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region AdaySinavOdaAlindiGuncelle
        public Result<bool> AdaySinavOdaAlindiGuncelle(Guid id)
        {
            try
            {
                var aday = _unitOfWork.adayMYSSRepository.Get(id);

                if (aday != null)
                {
                    aday.SinavaAlindi = true;
                    _unitOfWork.Save();

                    return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
                }

                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdaySinavOdaAlindiGuncelle - Hata: {ex.Message}", ex);
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region GetirAdayMYSSBilgileri(Guid id)
        public Result<AdayMYSSVM> GetirAdayMYSSBilgileri(Guid id)
        {
            try
            {
                var aday = _unitOfWork.adayMYSSRepository.GetAll(
                    x => x.Id == id,
                    includeProperties: "Adaylar,Mulakatlar"
                ).FirstOrDefault();

                if (aday != null)
                {
                    var adayBilgileri = new AdayMYSSVM
                    {
                        Id = aday.Id,
                        AdayId = aday.AdayId,
                        TC = aday.TC,
                        AdayAdiSoyadi = aday.Adaylar != null ? $"{aday.Adaylar.Ad} {aday.Adaylar.Soyad}" : "",
                        DereceId = aday.DereceId,
                        MulakatId = aday.MulakatId,
                        DereceAdi = aday.DereceAdi?.ToString() ?? "",
                        BransAdi = aday.BransAdi?.ToString() ?? "",
                        UlkeTercihAdi = aday.UlkeTercihAdi?.ToString() ?? ""
                        
                    };

                    return new Result<AdayMYSSVM>(true, ResultConstant.RecordFound, adayBilgileri);
                }

                return new Result<AdayMYSSVM>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetirAdayMYSSBilgileri - Hata: {ex.Message}", ex);
                return new Result<AdayMYSSVM>(false, ResultConstant.RecordNotFound + " | " + ex.Message);
            }
        }
        #endregion

        #region GetirAdayBasvuruBilgileri(Guid? adayId)
        public Result<AdayBasvuruBilgileriVM> GetirAdayBasvuruBilgileri(Guid? adayId)
        {
            try
            {
                _logger.LogInformation($"GetirAdayBasvuruBilgileri çağrıldı - AdayId: {adayId}");

                var aday = _unitOfWork.adayBasvuruBilgileriRepository.GetAll(
                    x => x.AdayId == adayId,
                    includeProperties: "Adaylar,Mulakatlar,AdayIletisimBilgileri"
                ).FirstOrDefault();

                if (aday == null)
                {
                    _logger.LogWarning($"GetirAdayBasvuruBilgileri - Aday bulunamadı - AdayId: {adayId}");
                    return new Result<AdayBasvuruBilgileriVM>(false, $"Aday başvuru bilgileri bulunamadı. (AdayId: {adayId})");
                }

                var iletisimBilgisi = aday.Adaylar?.AdayIletisimBilgileri?
                    .Where(x => x.AdayId == adayId)
                    .FirstOrDefault();

                var adayBilgileri = new AdayBasvuruBilgileriVM
                {
                    Id = aday.Id,
                    AdayId = aday.AdayId,
                    TC = aday.TC,
                    MulakatId = aday.MulakatId,
                    MulakatYil = aday.MulakatYil,
                    DogumYeri = !string.IsNullOrEmpty(aday.Adaylar?.DogumYeri) ? aday.Adaylar.DogumYeri : "",
                    Yas = !string.IsNullOrEmpty(aday.Adaylar?.DogumTarihi2) && DateTime.TryParse(aday.Adaylar.DogumTarihi2, out DateTime dogumTarihi)? (int)((DateTime.Now - dogumTarihi).TotalDays / 365.25) : 0,
                    IkametBilgisi = iletisimBilgisi != null ? 
                        $"{iletisimBilgisi.IkametIl ?? ""}{(!string.IsNullOrEmpty(iletisimBilgisi.IkametIl) && !string.IsNullOrEmpty(iletisimBilgisi.IkametIlce) ? "/" : "")}{iletisimBilgisi.IkametIlce ?? ""}" 
                        : "",
                    HizmetAy = aday.HizmetAy,
                    HizmetYil = aday.HizmetYil,
                    HizmetGun = aday.HizmetGun,
                    Derece = aday.Derece,
                    Kademe = aday.Kademe,
                    IlkGorevKaydi = aday.IlkGorevKaydi,
                    GorevdenUzaklastirma = aday.GorevdenUzaklastirma,
                    GorevIptalAck = aday.GorevIptalAck,
                    GorevIptalBAOK = aday.GorevIptalBAOK,
                    GorevIptalBrans = aday.GorevIptalBrans,
                    GorevIptalYil = aday.GorevIptalYil,
                    AdliSicilBelge = aday.AdliSicilBelge,
                    YabanciDilAdi = aday.YabanciDilAdi,
                    YabanciDilALM = aday.YabanciDilALM,
                    YabanciDilBasvuru = aday.YabanciDilBasvuru,
                    YabanciDilDiger = aday.YabanciDilDiger,
                    YabanciDilFRS = aday.YabanciDilFRS,
                    YabanciDilING = aday.YabanciDilING,
                    YabanciDilPuan = aday.YabanciDilPuan,
                    YabanciDilSeviye = aday.YabanciDilSeviye,
                    YabanciDilTarihi = aday.YabanciDilTarihi,
                    YabanciDilTuru = aday.YabanciDilTuru,
                    YLisans = aday.YLisans,
                    Doktora = aday.Doktora,
                    GenelDurum = aday.GenelDurum,
                    BilgiFormu = aday.BilgiFormu,
                    DereceAdi = aday.DereceAdi?.ToString() ?? "",
                    BransAdi = aday.BransAdi?.ToString() ?? "",
                    UlkeTercihAdi = aday.UlkeTercihAdi?.ToString() ?? ""
                };

                _logger.LogInformation($"GetirAdayBasvuruBilgileri başarılı - AdayId: {adayId}");
                return new Result<AdayBasvuruBilgileriVM>(true, "Aday başvuru bilgileri başarıyla getirildi.", adayBilgileri);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetirAdayBasvuruBilgileri - Hata: {ex.Message} - AdayId: {adayId}", ex);
                return new Result<AdayBasvuruBilgileriVM>(false, $"Aday başvuru bilgileri getirilirken hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region AdaySinavIptalGuncelle(AdayMYSSVM model, SessionContext user)
        public Result<AdayMYSSVM> AdaySinavIptalGuncelle(AdayMYSSVM model, SessionContext user)
        {
            var result = new Result<AdayMYSSVM>();
            try
            {
                var data = _unitOfWork.adayMYSSRepository.GetFirstOrDefault(x => x.Id == model.Id);
                if (data != null)
                {
                    data.SinavIptal = model.SinavIptal;
                    data.SinavIptalAck = model.SinavIptalAck;           
                    data.KaydedenId = user.LoginId;

                    _unitOfWork.adayMYSSRepository.Update(data);
                    _unitOfWork.Save();

                    result.Data = model;
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Kayıt bulunamadı!";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Bir hata oluştu: {ex.Message}";
            }
            return result;
        }
        #endregion      

        #region MYSSAdaylariGetir
        public Result<List<AdayMYSSVM>> MYSSAdaylariGetir()
        {
            try
            {
                // 1. Sorguyu IQueryable olarak oluştur (henüz execute etme)
                var query = _unitOfWork.adayMYSSRepository.GetAll()
                    .AsNoTracking() // Change tracking'i devre dışı bırak
                    .Include(x => x.Kullanici)
                    .Include(x => x.Adaylar)
                    .Where(z => z.Mulakatlar.Durumu == true && z.MYSSDurum == "GİRECEK")
                    .OrderBy(x => x.MYSSKomisyonSiraNo)
                    .ThenBy(x => x.KomisyonSN)
                    .ThenBy(x => x.KomisyonGunSN);
                    

                //if (query == null || !query.Any())
                //{
                //    return new Result<List<AdayMYSSVM>>(false, ResultConstant.RecordNotFound, default(List<AdayMYSSVM>));
                //}

                var returndata = query.Select(item => new AdayMYSSVM
                {

                    Id = item.Id,
                    AdayId = item.AdayId,
                    TC = item.TC,
                    AdayAdiSoyadi = item.Adaylar != null ? item.Adaylar.Ad.ToString() + " " + item.Adaylar.Soyad.ToString() : string.Empty,
                    MYSSTarih = item.MYSSTarih,
                    MYSSSaat = item.MYSSSaat,
                    MYSSMulakatYer = item.MYSSMulakatYer,
                    MYSSDurum = item.MYSSDurum,
                    MYSSDurumAck = item.MYSSDurumAck,
                    MYSSKomisyonSiraNo = item.MYSSKomisyonSiraNo,
                    MYSSKomisyonAdi = item.MYSSKomisyonAdi,
                    KomisyonSN = item.KomisyonSN,
                    KomisyonGunSN = item.KomisyonGunSN,
                    CagriDurum = item.CagriDurum,
                    KabulDurum = item.KabulDurum,
                    SinavDurum = item.SinavDurum,
                    SinavaGelmedi = item.SinavaGelmedi,
                    SinavaGelmediAck = item.SinavaGelmediAck,
                    SinavaGeldi = item.SinavaGeldi,
                    SinavaAlindi = item.SinavaAlindi,
                    MYSPuan = item.MYSPuan,
                    MYSSonuc = item.MYSSonuc,
                    MYSSonucAck = item.MYSSonucAck,
                    MYSSSorulanSoruNo = item.MYSSSorulanSoruNo,
                    SinavIptal = item.SinavIptal,
                    SinavIptalAck = item.SinavIptalAck,
                    BransId = item.BransId,
                    BransAdi = item.BransAdi,
                    DereceId = item.DereceId,
                    DereceAdi = item.DereceAdi,
                    UlkeTercihId = item.UlkeTercihId,
                    UlkeTercihAdi = item.UlkeTercihAdi,
                    MulakatId = item.MulakatId,
                    KayitTarihi = item.KayitTarihi,
                    KomisyonId = item.KomisyonId,
                    KaydedenId = item.KaydedenId,                    
                    KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                }).ToList();

                if (returndata != null)
                {
                    return new Result<List<AdayMYSSVM>>(true, ResultConstant.RecordFound, returndata);
                }

                return new Result<List<AdayMYSSVM>>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                return new Result<List<AdayMYSSVM>>(false, ex.Message);
            }
        }
        #endregion
    }
}
