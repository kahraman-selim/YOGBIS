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
    public class EtkinliklerBE : IEtkinliklerBE
    {
        
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUlkelerBE _ulkelerBE;
        private readonly IOkullarBE _okullarBE;
        #endregion

        #region Dönüştürücüler
        public EtkinliklerBE(IUnitOfWork unitOfWork, IMapper mapper, IUlkelerBE ulkelerBE, IOkullarBE okullarBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ulkelerBE = ulkelerBE;
            _okullarBE = okullarBE;
        }
        #endregion

        #region EtkinlikleriGetir
        public Result<List<EtkinliklerVM>> EtkinlikleriGetir()
        {
            var data = _unitOfWork.etkinliklerRepository.GetAll(includeProperties: "Kullanici").ToList();

            if (data != null)
            {
                List<EtkinliklerVM> returnData = new List<EtkinliklerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EtkinliklerVM()
                    {
                        EtkinlikId = (Guid)item.EtkinlikId,
                        EtkinlikAdi = item.EtkinlikAdi,
                        BasTarihi = item.BasTarihi,
                        BitTarihi = item.BitTarihi,
                        EtkinlikBilgi = item.EtkinlikBilgi,
                        HedefKitle = item.HedefKitle,
                        KatilimciSayisi = item.KatilimciSayisi,
                        Sonuc = item.Sonuc,
                        DuzenleyenAdiSoyadi = item.DuzenleyenAdiSoyadi,
                        EtkinlikKapakResimUrl = item.EtkinlikKapakResimUrl,
                        OkulId = item.OkulId.GetValueOrDefault(),
                        OkulAdi = item.OkulId.GetValueOrDefault() == Guid.Empty ? string.Empty : _okullarBE.OkulAdGetir(item.OkulId.GetValueOrDefault()).Data.ToString(),
                        TemsilcilikId = item.TemsilcilikId.GetValueOrDefault(),
                        UlkeId = item.UlkeId.GetValueOrDefault(),
                        UlkeAdi = item.UlkeId.GetValueOrDefault() == Guid.Empty ? string.Empty : _ulkelerBE.UlkeAdGetir(item.UlkeId.GetValueOrDefault()).Data.ToString(),
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
                    });
                }
                return new Result<List<EtkinliklerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<EtkinliklerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region EtkinlikGetirKullaniciId
        public Result<List<EtkinliklerVM>> EtkinlikGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.etkinliklerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<EtkinliklerVM> returnData = new List<EtkinliklerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EtkinliklerVM()
                    {
                        EtkinlikId = (Guid)item.EtkinlikId,
                        EtkinlikAdi = item.EtkinlikAdi,
                        BasTarihi = item.BasTarihi,
                        BitTarihi = item.BitTarihi,
                        EtkinlikBilgi = item.EtkinlikBilgi,
                        HedefKitle = item.HedefKitle,
                        KatilimciSayisi = item.KatilimciSayisi,
                        Sonuc = item.Sonuc,
                        DuzenleyenAdiSoyadi = item.DuzenleyenAdiSoyadi,
                        EtkinlikKapakResimUrl = item.EtkinlikKapakResimUrl,
                        OkulId = item.OkulId.GetValueOrDefault(),
                        OkulAdi = item.OkulId.GetValueOrDefault() == Guid.Empty ? string.Empty : _okullarBE.OkulAdGetir(item.OkulId.GetValueOrDefault()).Data.ToString(),
                        TemsilcilikId = item.TemsilcilikId.GetValueOrDefault(),
                        UlkeId = item.UlkeId.GetValueOrDefault(),
                        UlkeAdi = item.UlkeId.GetValueOrDefault() == Guid.Empty ? string.Empty : _ulkelerBE.UlkeAdGetir(item.UlkeId.GetValueOrDefault()).Data.ToString(),
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<EtkinliklerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<EtkinliklerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region EtkinlikGetir(Guid id)
        public Result<EtkinliklerVM> EtkinlikGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.etkinliklerRepository.GetFirstOrDefault(u => u.EtkinlikId == id, includeProperties: "Kullanici,FotoGaleri,DosyaGaleri");

                if (data != null)
                {
                    EtkinliklerVM Etkinlik = new EtkinliklerVM();
                    Etkinlik.EtkinlikId = data.EtkinlikId;
                    Etkinlik.EtkinlikAdi = data.EtkinlikAdi;
                    Etkinlik.BasTarihi = data.BasTarihi;
                    Etkinlik.BitTarihi = data.BitTarihi;
                    Etkinlik.EtkinlikBilgi = data.EtkinlikBilgi;
                    Etkinlik.HedefKitle = data.HedefKitle;
                    Etkinlik.KatilimciSayisi = data.KatilimciSayisi;
                    Etkinlik.Sonuc = data.Sonuc;
                    Etkinlik.DuzenleyenAdiSoyadi = data.DuzenleyenAdiSoyadi;
                    Etkinlik.EtkinlikKapakResimUrl = data.EtkinlikKapakResimUrl;
                    Etkinlik.OkulId = data.OkulId.GetValueOrDefault();
                    Etkinlik.OkulAdi = data.OkulId.GetValueOrDefault() == Guid.Empty ? string.Empty : _okullarBE.OkulAdGetir(data.OkulId.GetValueOrDefault()).Data.ToString();
                    Etkinlik.TemsilcilikId = data.TemsilcilikId.GetValueOrDefault();
                    Etkinlik.UlkeId = data.UlkeId.GetValueOrDefault();
                    Etkinlik.UlkeAdi = data.UlkeId.GetValueOrDefault() == Guid.Empty ? string.Empty : _ulkelerBE.UlkeAdGetir(data.UlkeId.GetValueOrDefault()).Data.ToString();
                    Etkinlik.KayitTarihi = data.KayitTarihi;
                    Etkinlik.KaydedenId = data.Kullanici.Id;
                    Etkinlik.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    Etkinlik.FotoGaleri = data.FotoGaleri.Select(f => new FotoGaleriVM()
                    {
                        FotoGaleriId = f.FotoGaleriId,
                        FotoAdi = f.FotoAdi,
                        FotoURL=f.FotoURL,
                        KayitTarihi=f.KayitTarihi,
                        KaydedenId=f.KaydedenId

                    }).ToList();

                    Etkinlik.DosyaGaleri = data.DosyaGaleri.Select(d => new DosyaGaleriVM()
                    {
                        DosyaGaleriId = d.DosyaGaleriId,
                        DosyaAdi = d.DosyaAdi,
                        DosyaURL = d.DosyaURL,
                        KayitTarihi = d.KayitTarihi,
                        KaydedenId = d.KaydedenId

                    }).ToList();

                    return new Result<EtkinliklerVM>(true, ResultConstant.RecordFound, Etkinlik);
                }
                else
                {
                    return new Result<EtkinliklerVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<EtkinliklerVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region EtkinlikEkle
        public Result<EtkinliklerVM> EtkinlikEkle(EtkinliklerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var Etkinlik = new Etkinlikler()
                    {
                        EtkinlikAdi = model.EtkinlikAdi,
                        BasTarihi = model.BasTarihi,
                        BitTarihi = model.BitTarihi,
                        EtkinlikBilgi = model.EtkinlikBilgi,
                        HedefKitle=model.HedefKitle,
                        KatilimciSayisi = model.KatilimciSayisi,
                        Sonuc=model.Sonuc,
                        DuzenleyenAdiSoyadi = model.DuzenleyenAdiSoyadi,
                        EtkinlikKapakResimUrl=model.EtkinlikKapakResimUrl,
                        OkulId=model.OkulId == null ? null : model.OkulId,
                        TemsilcilikId=model.TemsilcilikId == null ? null : model.TemsilcilikId,
                        UlkeId=model.UlkeId == null ? null : model.UlkeId,
                        KayitTarihi = model.KayitTarihi,
                        KaydedenId = user.LoginId
                        
                    };

                    Etkinlik.FotoGaleri = new List<FotoGaleri>();
                    if (model.FotoGaleri != null)
                    {
                        foreach (var file in model.FotoGaleri)
                        {
                            Etkinlik.FotoGaleri.Add(new FotoGaleri()
                            {
                                FotoAdi = file.FotoAdi,
                                FotoURL = file.FotoURL,
                                KaydedenId=user.LoginId,
                                KayitTarihi=file.KayitTarihi
                            });
                        }
                    }

                    Etkinlik.DosyaGaleri = new List<DosyaGaleri>();
                    if (model.DosyaGaleri != null)
                    {
                        foreach (var file in model.DosyaGaleri)
                        {
                            Etkinlik.DosyaGaleri.Add(new DosyaGaleri()
                            {
                                DosyaAdi=file.DosyaAdi,
                                DosyaURL=file.DosyaURL,
                                KaydedenId=user.LoginId,
                                KayitTarihi=file.KayitTarihi
                                
                            });
                        }
                    }

                    _unitOfWork.etkinliklerRepository.Add(Etkinlik);
                    _unitOfWork.Save();
                    return new Result<EtkinliklerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<EtkinliklerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<EtkinliklerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region EtkinlikGuncelle
        public Result<EtkinliklerVM> EtkinlikGuncelle(EtkinliklerVM model, SessionContext user)
        {
            if (model.EtkinlikId != null)
            {
                try
                {

                    var data = _unitOfWork.etkinliklerRepository.Get(model.EtkinlikId);
                    if (data != null)
                    {
                        data.EtkinlikAdi = model.EtkinlikAdi;
                        data.BasTarihi = model.BasTarihi;
                        data.BitTarihi = model.BitTarihi;
                        data.EtkinlikBilgi = model.EtkinlikBilgi;
                        data.HedefKitle = model.HedefKitle;
                        data.KatilimciSayisi = model.KatilimciSayisi;
                        data.Sonuc = model.Sonuc;
                        data.DuzenleyenAdiSoyadi = model.DuzenleyenAdiSoyadi;
                        data.EtkinlikKapakResimUrl = model.EtkinlikKapakResimUrl;
                        data.OkulId = model.OkulId == null ? null : model.OkulId;
                        data.TemsilcilikId = model.TemsilcilikId == null ? null : model.TemsilcilikId;
                        data.UlkeId = model.UlkeId == null ? null : model.UlkeId;
                        data.KayitTarihi = model.KayitTarihi;
                        data.KaydedenId = user.LoginId;

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
                                    KayitTarihi = file.KayitTarihi

                                });
                            }
                        }

                        if (model.DosyaGaleri != null)
                        {
                            data.DosyaGaleri = new List<DosyaGaleri>();
                            foreach (var file in model.DosyaGaleri)
                            {
                                data.DosyaGaleri.Add(new DosyaGaleri()
                                {
                                    DosyaAdi = file.DosyaAdi,
                                    DosyaURL = file.DosyaURL,
                                    KaydedenId = user.LoginId,
                                    KayitTarihi = file.KayitTarihi

                                });
                            }
                        }

                        _unitOfWork.etkinliklerRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<EtkinliklerVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<EtkinliklerVM>(false, "Lütfen kayıt seçiniz");
                    }

                }
                catch (Exception ex)
                {

                    return new Result<EtkinliklerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<EtkinliklerVM>(false, "Lütfen kayıt seçiniz");
            }
        }
        #endregion

        #region EtkinlikSil
        public Result<bool> EtkinlikSil(Guid id)
        {
            var data = _unitOfWork.etkinliklerRepository.GetFirstOrDefault(e=>e.EtkinlikId==id, includeProperties:"FotoGaleri,DosyaGaleri");
            if (data != null)
            {
                _unitOfWork.etkinliklerRepository.Remove(data);
                _unitOfWork.Save();

                foreach (var item in data.FotoGaleri.ToList())
                {
                    var fotolar = _unitOfWork.fotoGaleriRepository.GetFirstOrDefault(u => u.FotoGaleriId == item.FotoGaleriId);
                    if (data != null)
                    {
                        _unitOfWork.fotoGaleriRepository.Remove(fotolar);
                        _unitOfWork.Save();
                    }
                }

                foreach (var item in data.DosyaGaleri.ToList())
                {
                    var dosyalar = _unitOfWork.dosyaGaleriRepository.GetFirstOrDefault(u => u.DosyaGaleriId == item.DosyaGaleriId);
                    if (data != null)
                    {
                        _unitOfWork.dosyaGaleriRepository.Remove(dosyalar);
                        _unitOfWork.Save();
                    }
                }

                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region EtkinlikGetirUlkeId(Guid UlkeId)
        public Result<List<EtkinliklerVM>> EtkinlikGetirUlkeId(Guid UlkeId)
        {
            var data = _unitOfWork.etkinliklerRepository.GetAll(e=>e.UlkeId==UlkeId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<EtkinliklerVM> returnData = new List<EtkinliklerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EtkinliklerVM()
                    {
                        EtkinlikId = (Guid)item.EtkinlikId,
                        EtkinlikAdi = item.EtkinlikAdi,
                        BasTarihi = item.BasTarihi,
                        BitTarihi = item.BitTarihi,
                        EtkinlikBilgi = item.EtkinlikBilgi,
                        HedefKitle = item.HedefKitle,
                        KatilimciSayisi = item.KatilimciSayisi,
                        Sonuc = item.Sonuc,
                        DuzenleyenAdiSoyadi = item.DuzenleyenAdiSoyadi,
                        EtkinlikKapakResimUrl = item.EtkinlikKapakResimUrl,
                        OkulId = item.OkulId.GetValueOrDefault(),
                        OkulAdi = item.OkulId.GetValueOrDefault() == Guid.Empty ? string.Empty : _okullarBE.OkulAdGetir(item.OkulId.GetValueOrDefault()).Data.ToString(),
                        TemsilcilikId = item.TemsilcilikId.GetValueOrDefault(),
                        UlkeId = item.UlkeId.GetValueOrDefault(),
                        UlkeAdi = item.UlkeId.GetValueOrDefault() == Guid.Empty ? string.Empty : _ulkelerBE.UlkeAdGetir(item.UlkeId.GetValueOrDefault()).Data.ToString(),
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<EtkinliklerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<EtkinliklerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region EtkinlikGetirOkulId(Guid okulId)
        public Result<List<EtkinliklerVM>> EtkinlikGetirOkulId(Guid okulId)
        {
            var data = _unitOfWork.etkinliklerRepository.GetAll(e=>e.OkulId==okulId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<EtkinliklerVM> returnData = new List<EtkinliklerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EtkinliklerVM()
                    {
                        EtkinlikId = (Guid)item.EtkinlikId,
                        EtkinlikAdi = item.EtkinlikAdi,
                        BasTarihi = item.BasTarihi,
                        BitTarihi = item.BitTarihi,
                        EtkinlikBilgi = item.EtkinlikBilgi,
                        HedefKitle = item.HedefKitle,
                        KatilimciSayisi = item.KatilimciSayisi,
                        Sonuc = item.Sonuc,
                        DuzenleyenAdiSoyadi = item.DuzenleyenAdiSoyadi,
                        EtkinlikKapakResimUrl = item.EtkinlikKapakResimUrl,
                        OkulId = item.OkulId.GetValueOrDefault(),
                        OkulAdi = item.OkulId.GetValueOrDefault() == Guid.Empty ? string.Empty : _okullarBE.OkulAdGetir(item.OkulId.GetValueOrDefault()).Data.ToString(),
                        TemsilcilikId = item.TemsilcilikId.GetValueOrDefault(),
                        UlkeId = item.UlkeId.GetValueOrDefault(),
                        UlkeAdi = item.UlkeId.GetValueOrDefault() == Guid.Empty ? string.Empty : _ulkelerBE.UlkeAdGetir(item.UlkeId.GetValueOrDefault()).Data.ToString(),
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<EtkinliklerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<EtkinliklerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region EtkinlikKapakURLGetir(id)
        public Result<string> EtkinlikKapakURLGetir(Guid id)
        {

            var data = _unitOfWork.etkinliklerRepository.Get(id);
            if (data != null)
            {
                var etkinlikKapakURL = data.EtkinlikKapakResimUrl;

                return new Result<string>(true, ResultConstant.RecordFound, etkinlikKapakURL);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion
    }
}
