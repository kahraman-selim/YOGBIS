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
    public class AktivitelerBE : IAktivitelerBE
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AktivitelerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Result<List<AktivitelerVM>> EtkinlikleriGetir()
        {
            var data = _unitOfWork.aktivitelerRepository.GetAll(includeProperties: "Okullar,Kullanici").OrderBy(u => u.Okullar.OkulAdi).ToList();

            if (data != null)
            {
                List<AktivitelerVM> returnData = new List<AktivitelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new AktivitelerVM()
                    {
                        AktiviteId=item.AktiviteId,
                        AktiviteAdi=item.AktiviteAdi,
                        AktiviteBilgi=item.AktiviteBilgi,
                        BasTarihi=item.BasTarihi,
                        BitTarihi=item.BitTarihi,
                        DuzenleyenAdiSoyadi=item.DuzenleyenAdiSoyadi,
                        KatilimciSayisi=item.KatilimciSayisi,
                        OkulId = item.OkulId,
                        OkulAdi = item.Okullar.OkulAdi,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
                    });
                }
                return new Result<List<AktivitelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<AktivitelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<List<AktivitelerVM>> EtkinlikGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.aktivitelerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,Okullar").ToList();
            if (data != null)
            {
                List<AktivitelerVM> returnData = new List<AktivitelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new AktivitelerVM()
                    {
                        AktiviteId = item.AktiviteId,
                        AktiviteAdi = item.AktiviteAdi,
                        AktiviteBilgi = item.AktiviteBilgi,
                        BasTarihi = item.BasTarihi,
                        BitTarihi = item.BitTarihi,
                        DuzenleyenAdiSoyadi = item.DuzenleyenAdiSoyadi,
                        KatilimciSayisi = item.KatilimciSayisi,
                        OkulId = item.OkulId,
                        OkulAdi = item.Okullar.OkulAdi,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<AktivitelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<AktivitelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<AktivitelerVM> EtkinlikGetir(int id)
        {
            if (id>0)
            {
                var data = _unitOfWork.aktivitelerRepository.GetFirstOrDefault(u => u.AktiviteId == id, includeProperties: "Okullar,Kullanici");

                if (data!=null)
                {
                    AktivitelerVM Etkinlik = new AktivitelerVM();
                    Etkinlik.AktiviteId = data.AktiviteId;
                    Etkinlik.AktiviteAdi = data.AktiviteAdi;
                    Etkinlik.AktiviteBilgi = data.AktiviteBilgi;
                    Etkinlik.BasTarihi = data.BasTarihi;
                    Etkinlik.BitTarihi = data.BitTarihi;
                    Etkinlik.DuzenleyenAdiSoyadi = data.DuzenleyenAdiSoyadi;
                    Etkinlik.KatilimciSayisi = data.KatilimciSayisi;
                    Etkinlik.KayitTarihi = data.KayitTarihi;
                    Etkinlik.OkulId = data.OkulId;
                    Etkinlik.OkulAdi = data.Okullar.OkulAdi;
                    Etkinlik.KaydedenId = data.Kullanici.Id;
                    Etkinlik.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    return new Result<AktivitelerVM>(true, ResultConstant.RecordFound, Etkinlik);
                }
                else
                {
                    return new Result<AktivitelerVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<AktivitelerVM>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<AktivitelerVM> EtkinlikEkle(AktivitelerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    Aktiviteler Etkinlik = new Aktiviteler
                    {
                        AktiviteAdi = model.AktiviteAdi,
                        AktiviteBilgi = model.AktiviteBilgi,
                        BasTarihi = model.BasTarihi,
                        BitTarihi = model.BitTarihi,
                        DuzenleyenAdiSoyadi = model.DuzenleyenAdiSoyadi,
                        KatilimciSayisi = model.KatilimciSayisi,
                        KayitTarihi = model.KayitTarihi,
                        OkulId = model.OkulId,
                        KaydedenId = user.LoginId,

                        FotoGaleri = new List<FotoGaleri>()
                    };
                    foreach (var file in Etkinlik.FotoGaleri)
                    {
                        Etkinlik.FotoGaleri.Add(new FotoGaleri()
                        {
                            FotoAdi=file.FotoAdi,
                            FotoURL=file.FotoURL
                        });
                    }
                    _unitOfWork.aktivitelerRepository.Add(Etkinlik);
                    _unitOfWork.Save();
                    return new Result<AktivitelerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<AktivitelerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<AktivitelerVM>(false, "Boş veri olamaz");
            }
        }
        public Result<AktivitelerVM> EtkinlikGuncelle(AktivitelerVM model, SessionContext user)
        {
            if (model.AktiviteId>0)
            {
                try
                {
                    
                    var data = _unitOfWork.aktivitelerRepository.Get(model.AktiviteId);
                    if (data!=null)
                    {
                        data.AktiviteAdi = model.AktiviteAdi;
                        data.AktiviteBilgi = model.AktiviteBilgi;
                        data.BasTarihi = model.BasTarihi;
                        data.BitTarihi = model.BitTarihi;
                        data.DuzenleyenAdiSoyadi = model.DuzenleyenAdiSoyadi;
                        data.KatilimciSayisi = model.KatilimciSayisi;
                        data.KayitTarihi = model.KayitTarihi;
                        data.OkulId = model.OkulId;
                        data.KaydedenId = user.LoginId;

                        data.FotoGaleri = new List<FotoGaleri>();

                        foreach (var file in data.FotoGaleri)
                        {
                            data.FotoGaleri.Add(new FotoGaleri()
                            {
                                FotoAdi = file.FotoAdi,
                                FotoURL = file.FotoURL
                            });
                        }

                        _unitOfWork.aktivitelerRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<AktivitelerVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<AktivitelerVM>(false, "Lütfen kayıt seçiniz");
                    }

                }
                catch (Exception ex)
                {

                    return new Result<AktivitelerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<AktivitelerVM>(false, "Lütfen kayıt seçiniz");
            }
        }
        public Result<bool> EtkinlikSil(int id)
        {
            var data = _unitOfWork.aktivitelerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.aktivitelerRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        public Result<List<AktivitelerVM>> EtkinlikGetirUlkeId(int UlkeId)
        {
            var data = _unitOfWork.aktivitelerRepository.GetAll(u => u.Okullar.Sehirler.Eyaletler.UlkeId == UlkeId, includeProperties: "Kullanici,Okullar").ToList();
            if (data != null)
            {
                List<AktivitelerVM> returnData = new List<AktivitelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new AktivitelerVM()
                    {
                        AktiviteId=item.AktiviteId,
                        AktiviteAdi=item.AktiviteAdi,
                        AktiviteBilgi=item.AktiviteBilgi,
                        BasTarihi=item.BasTarihi,
                        BitTarihi=item.BitTarihi,
                        DuzenleyenAdiSoyadi=item.DuzenleyenAdiSoyadi,
                        KatilimciSayisi=item.KatilimciSayisi,
                        OkulId = item.OkulId,
                        OkulAdi = item.Okullar.OkulAdi,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<AktivitelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<AktivitelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<List<AktivitelerVM>> EtkinlikGetirOkulId(int okulId)
        {
            var data = _unitOfWork.aktivitelerRepository.GetAll(u => u.OkulId == okulId, includeProperties: "Kullanici,Okullar").ToList();
            if (data != null)
            {
                List<AktivitelerVM> returnData = new List<AktivitelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new AktivitelerVM()
                    {
                        AktiviteId = item.AktiviteId,
                        AktiviteAdi = item.AktiviteAdi,
                        AktiviteBilgi = item.AktiviteBilgi,
                        BasTarihi = item.BasTarihi,
                        BitTarihi = item.BitTarihi,
                        DuzenleyenAdiSoyadi = item.DuzenleyenAdiSoyadi,
                        KatilimciSayisi = item.KatilimciSayisi,
                        OkulId = item.OkulId,
                        OkulAdi = item.Okullar.OkulAdi,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<AktivitelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<AktivitelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
    }
}
