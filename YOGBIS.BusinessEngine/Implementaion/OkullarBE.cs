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
    public class OkullarBE : IOkullarBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IEyaletlerBE _eyaletlerBE;
        private readonly ISehirlerBE _sehirlerBE;
        private readonly ISiniflarBE _siniflarBE;
        private readonly ISubelerBE _subelerBE;
        private readonly IOgrencilerBE _ogrencilerBE;
        private readonly IKullaniciBE _kullaniciBE;
        #endregion

        #region Dönüştürücüler
        public OkullarBE(IUnitOfWork unitOfWork, IMapper mapper, IUlkelerBE ulkelerBE, IEyaletlerBE eyaletlerBE, 
            ISehirlerBE sehirlerBE, ISiniflarBE siniflarBE, ISubelerBE subelerBE, IOgrencilerBE ogrencilerBE, IKullaniciBE kullaniciBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ulkelerBE = ulkelerBE;
            _kullaniciBE = kullaniciBE;
            _eyaletlerBE = eyaletlerBE;
            _siniflarBE = siniflarBE;
            _subelerBE = subelerBE;
            _ogrencilerBE = ogrencilerBE;
            _sehirlerBE = sehirlerBE;
        }
        #endregion

        #region OkullarıGetir
        public Result<List<OkullarVM>> OkullariGetir()
        {
            var data = _unitOfWork.okullarRepository.GetAll(x => x.OkulDurumu == true, includeProperties: "Kullanici,OkulBinaBolum,Subeler,Siniflar,Ogrenciler," +
                "AdayGorevKaydi,Etkinlikler,EpostaAdresleri,Telefonlar,FotoGaleri").OrderBy(o => o.OkulAdi).ToList();

            if (data != null)
            {
                List<OkullarVM> returnData = new List<OkullarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkullarVM()
                    {
                        OkulId = (Guid)item.OkulId,
                        OkulKodu = item.OkulKodu,
                        OkulAdi = item.OkulAdi,
                        UlkeId = item.UlkeId,
                        UlkeAdi = _ulkelerBE.UlkeAdGetir(item.UlkeId).Data.ToString(),
                        OkulDurumu = item.OkulDurumu,
                        OkulTuru = item.OkulTuru,
                        OgretimTuru=item.OgretimTuru,
                        OkulAcilisTarihi = item.OkulAcilisTarihi.GetValueOrDefault(),
                        TemsilcilikId = item.TemsilcilikId.GetValueOrDefault(),
                        EyaletId = item.EyaletId.GetValueOrDefault(),
                        EyaletAdi = item.EyaletId.GetValueOrDefault() == Guid.Empty ? string.Empty : _eyaletlerBE.EyaletAdGetir(item.EyaletId.GetValueOrDefault()).Data.ToString(),
                        SehirId = item.SehirId.GetValueOrDefault(),
                        SehirAdi = item.SehirId.GetValueOrDefault() == Guid.Empty ? string.Empty : _sehirlerBE.SehirAdGetir(item.SehirId.GetValueOrDefault()).Data.ToString(),
                        OkulMudurId = item.OkulMudurId,
                        OkulMudurAdiSoyadi = item.OkulMudurId != null ? _kullaniciBE.KullaniciAdSoyadGetir(item.OkulMudurId).Data : string.Empty,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,

                        TCEOgr=_unitOfWork.ogrencilerRepository.GetAll(o=>o.OkulId==item.OkulId && o.BaslamaKayitTarihi != null)
                        .GroupBy(o=>o.BaslamaKayitTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1")
                        .Sum(o=>o.First().KayitSayisi),

                        SubeSayisi = _unitOfWork.subelerRepository.GetAll(g => g.OkulId == item.OkulId).Count()
                    });
                }
                return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkullarVM>>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion

        #region OkullarıGetirAZ
        public Result<List<OkullarVM>> OkullariGetirAZ()
        {
            var data = _unitOfWork.okullarRepository.GetAll(includeProperties: "Kullanici,OkulBinaBolum,Subeler,Siniflar,Ogrenciler," +
                "AdayGorevKaydi,Etkinlikler,EpostaAdresleri,Telefonlar,FotoGaleri").OrderBy(o => o.OkulAdi).ToList();

            if (data != null)
            {
                List<OkullarVM> returnData = new List<OkullarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkullarVM()
                    {
                        OkulId = (Guid)item.OkulId,
                        OkulKodu = item.OkulKodu,
                        OkulAdi = item.OkulAdi,
                        UlkeId = item.UlkeId,
                        UlkeAdi = _ulkelerBE.UlkeAdGetir(item.UlkeId).Data.ToString(),
                        OkulDurumu = item.OkulDurumu,
                        OgretimTuru=item.OgretimTuru,
                        OkulTuru = item.OkulTuru,
                        OkulAcilisTarihi = item.OkulAcilisTarihi.GetValueOrDefault(),
                        TemsilcilikId=item.TemsilcilikId.GetValueOrDefault(),
                        EyaletId = item.EyaletId.GetValueOrDefault(), 
                        EyaletAdi = item.EyaletId.GetValueOrDefault() == Guid.Empty ? string.Empty : _eyaletlerBE.EyaletAdGetir(item.EyaletId.GetValueOrDefault()).Data.ToString(),
                        SehirId = item.SehirId.GetValueOrDefault(), 
                        SehirAdi = item.SehirId.GetValueOrDefault() == Guid.Empty ? string.Empty : _sehirlerBE.SehirAdGetir(item.SehirId.GetValueOrDefault()).Data.ToString(),
                        OkulMudurId = item.OkulMudurId,
                        OkulMudurAdiSoyadi = item.OkulMudurId != null ? _kullaniciBE.KullaniciAdSoyadGetir(item.OkulMudurId).Data : string.Empty,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
                    });
                }
                return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkullarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulGetirYoneticiId
        public Result<List<OkullarVM>> OkulGetirYoneticiId(string userId)
        {
            var data = _unitOfWork.okullarRepository.GetAll(u => u.OkulMudurId == userId, includeProperties: "Kullanici,FotoGaleri,Etkinlikler,OkulBinaBolum,Subeler,AdayGorevKaydi").OrderBy(u => u.OkulAdi).ToList();
            if (data != null)
            {
                List<OkullarVM> returnData = new List<OkullarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkullarVM()
                    {
                        OkulId = (Guid)item.OkulId,
                        OkulKodu = item.OkulKodu,
                        OkulAdi = item.OkulAdi,
                        UlkeId = item.UlkeId,
                        UlkeAdi = _ulkelerBE.UlkeAdGetir(item.UlkeId).Data.ToString(),
                        OkulDurumu = item.OkulDurumu,
                        OkulTuru = item.OkulTuru,
                        OgretimTuru=item.OgretimTuru,
                        OkulAcilisTarihi = item.OkulAcilisTarihi.GetValueOrDefault(),
                        TemsilcilikId = item.TemsilcilikId.GetValueOrDefault(),
                        EyaletId = item.EyaletId.GetValueOrDefault(),
                        EyaletAdi = item.EyaletId.GetValueOrDefault() == Guid.Empty ? string.Empty : _eyaletlerBE.EyaletAdGetir(item.EyaletId.GetValueOrDefault()).Data.ToString(),
                        SehirId = item.SehirId.GetValueOrDefault(),
                        SehirAdi = item.SehirId.GetValueOrDefault() == Guid.Empty ? string.Empty : _sehirlerBE.SehirAdGetir(item.SehirId.GetValueOrDefault()).Data.ToString(),
                        OkulMudurId = item.OkulMudurId,
                        OkulMudurAdiSoyadi = item.OkulMudurId != null ? _kullaniciBE.KullaniciAdSoyadGetir(item.OkulMudurId).Data : string.Empty,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,

                        SubeSayisi = _unitOfWork.subelerRepository.GetAll(g => g.OkulId == item.OkulId).Count()
                    });
                }
                return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkullarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulGetirOkulId(id)
        public Result<List<OkullarVM>> OkulGetirOkulId(Guid id)
        {
            var data = _unitOfWork.okullarRepository.GetAll(u => u.OkulId == id, includeProperties: "Kullanici,FotoGaleri,Etkinlikler,OkulBinaBolum,Subeler,AdayGorevKaydi").OrderBy(u => u.OkulAdi).ToList();
            if (data != null)
            {
                List<OkullarVM> returnData = new List<OkullarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkullarVM()
                    {
                        OkulId = (Guid)item.OkulId,
                        OkulKodu = item.OkulKodu,
                        OkulAdi = item.OkulAdi,
                        UlkeId = item.UlkeId,
                        UlkeAdi = _ulkelerBE.UlkeAdGetir(item.UlkeId).Data.ToString(),
                        OkulDurumu = item.OkulDurumu,
                        OkulTuru = item.OkulTuru,
                        OgretimTuru = item.OgretimTuru,
                        OkulAcilisTarihi = item.OkulAcilisTarihi.GetValueOrDefault(),
                        TemsilcilikId = item.TemsilcilikId.GetValueOrDefault(),
                        EyaletId = item.EyaletId.GetValueOrDefault(),
                        EyaletAdi = item.EyaletId.GetValueOrDefault() == Guid.Empty ? string.Empty : _eyaletlerBE.EyaletAdGetir(item.EyaletId.GetValueOrDefault()).Data.ToString(),
                        SehirId = item.SehirId.GetValueOrDefault(),
                        SehirAdi = item.SehirId.GetValueOrDefault() == Guid.Empty ? string.Empty : _sehirlerBE.SehirAdGetir(item.SehirId.GetValueOrDefault()).Data.ToString(),
                        OkulMudurId = item.OkulMudurId,
                        OkulMudurAdiSoyadi = item.OkulMudurId != null ? _kullaniciBE.KullaniciAdSoyadGetir(item.OkulMudurId).Data : string.Empty,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,

                        SubeSayisi = _unitOfWork.subelerRepository.GetAll(g => g.OkulId == item.OkulId).Count()
                    });
                }
                return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkullarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulGetirUlkeId
        public Result<List<OkullarVM>> OkulGetirUlkeId(Guid UlkeId)
        {
            var data = _unitOfWork.okullarRepository.GetAll(u => u.UlkeId == UlkeId, includeProperties: "Kullanici,FotoGaleri,Etkinlikler,OkulBinaBolum,Subeler,AdayGorevKaydi").OrderBy(u => u.OkulAdi).ToList();
            if (data != null)
            {
                List<OkullarVM> returnData = new List<OkullarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkullarVM()
                    {
                        OkulId = (Guid)item.OkulId,
                        OkulKodu = item.OkulKodu,
                        OkulAdi = item.OkulAdi,
                        UlkeId = item.UlkeId,
                        UlkeAdi = _ulkelerBE.UlkeAdGetir(item.UlkeId).Data.ToString(),
                        OkulDurumu = item.OkulDurumu,
                        OkulTuru = item.OkulTuru,
                        OgretimTuru=item.OgretimTuru,
                        OkulAcilisTarihi = item.OkulAcilisTarihi.GetValueOrDefault(),
                        TemsilcilikId = item.TemsilcilikId.GetValueOrDefault(),
                        EyaletId = item.EyaletId.GetValueOrDefault(),
                        EyaletAdi = item.EyaletId.GetValueOrDefault() == Guid.Empty ? string.Empty : _eyaletlerBE.EyaletAdGetir(item.EyaletId.GetValueOrDefault()).Data.ToString(),
                        SehirId = item.SehirId.GetValueOrDefault(),
                        SehirAdi = item.SehirId.GetValueOrDefault() == Guid.Empty ? string.Empty : _sehirlerBE.SehirAdGetir(item.SehirId.GetValueOrDefault()).Data.ToString(),
                        OkulMudurId = item.OkulMudurId,
                        OkulMudurAdiSoyadi = item.OkulMudurId != null ? _kullaniciBE.KullaniciAdSoyadGetir(item.OkulMudurId).Data : string.Empty,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
                    });
                }
                return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkullarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulGetir(id)
        public Result<OkullarVM> OkulGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.okullarRepository.GetFirstOrDefault(u => u.OkulId == id, includeProperties: "Kullanici,FotoGaleri,Etkinlikler,OkulBinaBolum,Siniflar,Subeler,Ogrenciler,AdayGorevKaydi");
                if (data != null)
                {
                    OkullarVM okul = new OkullarVM();
                    okul.OkulId = data.OkulId;
                    okul.KayitTarihi = data.KayitTarihi;
                    okul.OkulKodu = data.OkulKodu;
                    okul.OkulAdi = data.OkulAdi;
                    okul.OkulLogoURL = data.OkulLogoURL;
                    okul.OkulBilgi = data.OkulBilgi;
                    okul.OkulAcilisTarihi = data.OkulAcilisTarihi.GetValueOrDefault();
                    okul.OkulDurumu = data.OkulDurumu;
                    okul.OgretimTuru = data.OgretimTuru;
                    okul.OkulMudurId = data.OkulMudurId;
                    okul.OkulMudurAdiSoyadi = data.OkulMudurId != null ? _kullaniciBE.KullaniciAdSoyadGetir(data.OkulMudurId).Data : string.Empty;
                    okul.OkulHizmetGecisDonem = data.OkulHizmetGecisDonem;
                    okul.OkulKapaliAlan = data.OkulKapaliAlan;
                    okul.OkulAcikAlan = data.OkulAcikAlan;
                    okul.OkulMulkiDurum = data.OkulMulkiDurum;
                    okul.OkulMulkiDurumAciklama = data.OkulMulkiDurumAciklama;
                    okul.OkulInternetAdresi = data.OkulInternetAdresi;
                    okul.OkulEPostaAdresi = data.OkulEPostaAdresi;
                    okul.OkulTelefon = data.OkulTelefon;
                    okul.UlkeId = data.UlkeId;
                    okul.UlkeAdi = _ulkelerBE.UlkeAdGetir((Guid)data.UlkeId).Data;
                    okul.TemsilcilikId = data.TemsilcilikId.GetValueOrDefault();
                    okul.EyaletId = data.EyaletId.GetValueOrDefault();
                    okul.EyaletAdi = data.EyaletId.GetValueOrDefault() == Guid.Empty ? string.Empty : _eyaletlerBE.EyaletAdGetir(data.EyaletId.GetValueOrDefault()).Data.ToString();
                    okul.SehirId = data.SehirId.GetValueOrDefault();
                    okul.SehirAdi = data.SehirId.GetValueOrDefault() == Guid.Empty ? string.Empty : _sehirlerBE.SehirAdGetir(data.SehirId.GetValueOrDefault()).Data.ToString();
                    okul.OkulTuru = data.OkulTuru;
                    okul.KaydedenId = data.KaydedenId;
                    okul.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    okul.SinifSayisi = _unitOfWork.subelerRepository.GetAll(g => g.OkulId == data.OkulId).Count();

                    okul.FotoGaleri = data.FotoGaleri.Select(g => new FotoGaleriVM()
                    {
                        FotoGaleriId = g.FotoGaleriId,
                        FotoAdi = g.FotoAdi,
                        FotoURL = g.FotoURL,
                        KayitTarihi = g.KayitTarihi,
                        KaydedenId = g.KaydedenId

                    }).ToList();

                    okul.OkulBinaBolum = data.OkulBinaBolum.GroupBy(o=>o.BolumAdi).Select(o => new OkulBinaBolumVM()
                    {
                        BolumAdi = o.First().BolumAdi,
                        BolumAdedi = o.First().BolumAdedi,                        
                        OkulBinaBolumId = o.First().OkulBinaBolumId,
                        OkulId = o.First().OkulId,
                        BolumAdToplam = o.Sum(c=>c.BolumAdedi)

                    }).ToList();

                    okul.Siniflar = data.Siniflar.Select(s => new SiniflarVM()
                    {
                        SinifAdi=s.SinifAdi,
                        SinifId=s.SinifId,
                        Subeler = _unitOfWork.subelerRepository.GetAll(c=>c.SinifId==s.SinifId).OrderBy(a => a.SinifAdi)
                        .ThenBy(b=>b.SubeAdi)
                        .Select(c=> new SubelerVM()
                        {
                            SubeAdi = c.SubeAdi,
                            
                            //OgrenciSayisi = _unitOfWork.ogrencilerRepository.GetAll(o => o.SubeId == c.SubeId && o.BaslamaKayitTarihi != null).Sum(x => x.KayitSayisi) //_ogrencilerBE.OgrenciSayiGetir((Guid)c.SubeId).Data
                            //Ogrenciler = _unitOfWork.ogrencilerRepository.GetAll(o => o.SubeId == c.SubeId).Select(o => new OgrencilerVM()
                            //{
                            //    KayitSayisi = o.KayitSayisi

                            //}).ToList()

                        }).ToList()                        

                    }).ToList();                   
                    

                    okul.AdayGorevKaydi = data.AdayGorevKaydi.Select(a => new AdayGorevKaydiVM()
                    {
                        Gorevi = a.Gorevi,
                        OkulId = a.OkulId,
                        GorevliTC = a.GorevliTC

                    }).ToList();

                    return new Result<OkullarVM>(true, ResultConstant.RecordFound, okul);
                }
                else
                {
                    return new Result<OkullarVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<OkullarVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulEkle
        public Result<OkullarVM> OkulEkle(OkullarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var okullar = new Okullar()
                    {

                        //OkulUlkeId = (Guid)model.OkulUlkeId,
                        OkulKodu = model.OkulKodu,
                        OkulAdi = model.OkulAdi,
                        OkulDurumu = model.OkulDurumu,
                        OkulTuru=model.OkulTuru,
                        KayitTarihi = model.KayitTarihi,
                        KaydedenId = user.LoginId,
                        OkulMudurId = model.OkulMudurId ?? null,
                        EyaletId = null,
                        SehirId = null,
                        UlkeId=model.UlkeId
                    };

                _unitOfWork.okullarRepository.Add(okullar);
                _unitOfWork.Save();

                return new Result<OkullarVM>(true, ResultConstant.RecordCreateSuccess);
            }
                catch (Exception ex)
            {

                return new Result<OkullarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
            }
            }
            else
            {
                return new Result<OkullarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region OkulGuncelle
        public Result<OkullarVM> OkulGuncelle(OkullarVM model, SessionContext user)
        {
            if (model.OkulId != null)
            {
                try
                {
                    var data = _unitOfWork.okullarRepository.Get((Guid)model.OkulId);
                    if (data != null)
                    {
                        data.OkulKodu = model.OkulKodu;
                        data.OkulAdi = model.OkulAdi;
                        data.UlkeId = model.UlkeId;
                        data.OkulDurumu = model.OkulDurumu;
                        data.OkulTuru = model.OkulTuru;
                        data.KayitTarihi = model.KayitTarihi;
                        data.OkulMudurId = model.OkulMudurId != null ? model.OkulMudurId : null;
                        data.KaydedenId = user.LoginId;
                        data.EyaletId = model.EyaletId == null ? null : model.EyaletId;
                        data.SehirId = model.SehirId == null ? null : model.SehirId;
                        //data.EyaletId = (model.EyaletId == null || model.EyaletId.ToString() == "00000000-0000-0000-0000-000000000000") ? null : model.EyaletId;
                        //data.SehirId = (model.SehirId == null || model.SehirId.ToString() == "00000000-0000-0000-0000-000000000000") ? null : model.SehirId;

                        _unitOfWork.okullarRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<OkullarVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<OkullarVM>(false, "Lütfen kayıt seçiniz");
                    }

                }
                catch (Exception ex)
                {

                    return new Result<OkullarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<OkullarVM>(false, "Lütfen kayıt seçiniz");
            }
        }
        #endregion

        #region OkulDetayGuncelle
        public Result<OkullarVM> OkulDetayGuncelle(OkullarVM model, SessionContext user)
        {
            if (model.OkulId != null)
            {
                try
                {
                    var data = _unitOfWork.okullarRepository.Get(model.OkulId);
                    if (data != null)
                    {
                        //data.OkulKodu = model.OkulKodu;
                        //data.OkulAdi = model.OkulAdi;
                        //data.OkulUlkeId = (Guid)model.OkulUlkeId;
                        data.KayitTarihi = model.KayitTarihi;
                        //data.OkulMudurId = model.OkulMudurId;
                        data.KaydedenId = user.LoginId;
                        ///okulmüdürünün ekleyeceği alanlar////////
                        data.OkulLogoURL = model.OkulLogoURL;
                        data.OkulBilgi = model.OkulBilgi;
                        data.OgretimTuru = model.OgretimTuru;
                        data.OkulAcilisTarihi = model.OkulAcilisTarihi;
                        data.OkulHizmetGecisDonem = model.OkulHizmetGecisDonem;
                        data.OkulKapaliAlan = model.OkulKapaliAlan;
                        data.OkulAcikAlan = model.OkulAcikAlan;
                        data.OkulMulkiDurum = true; //model.OkulMulkiDurum;
                        data.OkulMulkiDurumAciklama = model.OkulMulkiDurumAciklama;
                        data.OkulInternetAdresi = model.OkulInternetAdresi;
                        data.OkulEPostaAdresi = model.OkulEPostaAdresi;
                        data.OkulTelefon = model.OkulTelefon;
                        data.EyaletId = model.EyaletId == null ? null : model.EyaletId;
                        //((Guid)model.EyaletId == null || model.EyaletId.ToString() == "00000000-0000-0000-0000-000000000000") ? null : model.EyaletId;
                        data.SehirId = model.SehirId == null ? null : model.SehirId; 
                        //((Guid)model.SehirId == null || model.SehirId.ToString() == "00000000-0000-0000-0000-000000000000") ? null : model.SehirId;

                        if (model.FotoGaleri != null)
                        {
                            data.FotoGaleri = new List<FotoGaleri>();
                            foreach (var file in model.FotoGaleri)
                            {
                                data.FotoGaleri.Add(new FotoGaleri()
                                {
                                    FotoAdi = file.FotoAdi,
                                    FotoURL = file.FotoURL,
                                    KaydedenId = user.LoginId,
                                    KayitTarihi = model.KayitTarihi
                                });
                            }
                        }

                        _unitOfWork.okullarRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<OkullarVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<OkullarVM>(false, "Lütfen kayıt seçiniz");
                    }

                }
                catch (Exception ex)
                {

                    return new Result<OkullarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<OkullarVM>(false, "Lütfen kayıt seçiniz");
            }
        }
        #endregion

        #region Okulsil
        public Result<bool> OkulSil(Guid id)
        {
            var data = _unitOfWork.okullarRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.okullarRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region OkulLogoURLGetir(id)
        public Result<string> OkulLogoURLGetir(Guid id)
        {

            var data = _unitOfWork.okullarRepository.Get(id);
            if (data != null)
            {
                var okulogoURL = data.OkulLogoURL;

                return new Result<string>(true, ResultConstant.RecordFound, okulogoURL);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion

        #region SubeSinifOgrenciGetirOkulId(id)
        public Result<OkullarVM> SubeSinifOgrenciGetirOkulId(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.okullarRepository.GetFirstOrDefault(u => u.OkulId == id, includeProperties: "Kullanici,Subeler,Siniflar,Ogrenciler");
                if (data != null)
                {
                    OkullarVM okul = new OkullarVM();
                    okul.OkulId = data.OkulId;

                    //okul.Subeler = data.Subeler.Select(s => new SubelerVM()
                    //{
                    //    SubeAdi = s.SubeAdi,
                    //    SubeId=s.SubeId,

                    //}).ToList();

                    //okul.Siniflar = data.Siniflar.Select(i => new SiniflarVM()
                    //{
                    //    SinifAdi=i.SinifAdi,
                    //    SinifId=i.SinifId

                    //}).ToList();

                    return new Result<OkullarVM>(true, ResultConstant.RecordFound, okul);
                }
                else
                {
                    return new Result<OkullarVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<OkullarVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region OkulEyaletIdGetir(id)
        public Result<Guid> OkulEyaletIdGetir(Guid id)
        {

            var data = _unitOfWork.okullarRepository.Get(id);
            if (data != null)
            {
                var eyaletId = (Guid)data.EyaletId.GetValueOrDefault();

                return new Result<Guid>(true, ResultConstant.RecordFound, eyaletId);
            }
            else
            {
                return new Result<Guid>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion

        #region OkulSehirIdGetir(id)
        public Result<Guid> OkulSehirIdGetir(Guid id)
        {

            var data = _unitOfWork.okullarRepository.Get(id);
            if (data != null)
            {
                var sehirId = (Guid)data.SehirId.GetValueOrDefault();

                return new Result<Guid>(true, ResultConstant.RecordFound, sehirId);
            }
            else
            {
                return new Result<Guid>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion
    }
}
