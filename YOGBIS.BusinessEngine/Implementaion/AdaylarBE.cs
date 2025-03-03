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
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDerecelerBE _derecelerBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IKomisyonlarBE _komisyonlarBE;
        #endregion

        #region Constructors
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
        
        #region AdayBasvuruBilgileriniGetir
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
                var data = _unitOfWork.adayBasvuruBilgileriRepository.GetAll(x => x.TC == TC).ToList();
                
                if (data != null && data.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"Business Engine - {data.Count} adet veri bulundu. TC: {TC}");
                    
                    var adaylar = data.Select(data => new AdayBasvuruBilgileriVM()
                    {
                        Id = data.Id,
                        TC = data.TC,
                        Askerlik=data.Askerlik,
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
                        Sendika = data.Sendika,
                        SendikaAck = data.SendikaAck,
                        MYYSSoruItiraz = data.MYYSSoruItiraz,
                        MYYSSonucItiraz = data.MYYSSonucItiraz,
                        BasvuruBrans = data.BasvuruBrans,
                        BransId = data.BransId,
                        //AdliSicilBelge = data.AdliSicilBelge,
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
                        IkametIl=data.IkametAdres,
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
    }
}
