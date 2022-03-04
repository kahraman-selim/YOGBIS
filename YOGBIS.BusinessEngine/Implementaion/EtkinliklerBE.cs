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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EtkinliklerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Result<List<EtkinliklerVM>> EtkinlikleriGetir()
        {
            var data = _unitOfWork.etkinliklerRepository.GetAll(includeProperties: "Okullar,Kullanici").ToList();

            if (data != null)
            {
                List<EtkinliklerVM> returnData = new List<EtkinliklerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EtkinliklerVM()
                    {
                        EtkinlikId = item.EtkinlikId,
                        EtkinlikAdi = item.EtkinlikAdi,
                        EtkinlikBilgi = item.EtkinlikBilgi,
                        BasTarihi = item.BasTarihi,
                        BitTarihi = item.BitTarihi,
                        DuzenleyenAdiSoyadi = item.DuzenleyenAdiSoyadi,
                        KatilimciSayisi = item.KatilimciSayisi,
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
        public Result<List<EtkinliklerVM>> EtkinlikGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.etkinliklerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,Okullar").ToList();
            if (data != null)
            {
                List<EtkinliklerVM> returnData = new List<EtkinliklerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EtkinliklerVM()
                    {
                        EtkinlikId = item.EtkinlikId,
                        EtkinlikAdi = item.EtkinlikAdi,
                        EtkinlikBilgi = item.EtkinlikBilgi,
                        BasTarihi = item.BasTarihi,
                        BitTarihi = item.BitTarihi,
                        DuzenleyenAdiSoyadi = item.DuzenleyenAdiSoyadi,
                        KatilimciSayisi = item.KatilimciSayisi,
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
        public Result<EtkinliklerVM> EtkinlikGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.etkinliklerRepository.GetFirstOrDefault(u => u.EtkinlikId == id, includeProperties: "Okullar,Kullanici");

                if (data != null)
                {
                    EtkinliklerVM Etkinlik = new EtkinliklerVM();
                    Etkinlik.EtkinlikId = data.EtkinlikId;
                    Etkinlik.EtkinlikAdi = data.EtkinlikAdi;
                    Etkinlik.EtkinlikBilgi = data.EtkinlikBilgi;
                    Etkinlik.BasTarihi = data.BasTarihi;
                    Etkinlik.BitTarihi = data.BitTarihi;
                    Etkinlik.DuzenleyenAdiSoyadi = data.DuzenleyenAdiSoyadi;
                    Etkinlik.KatilimciSayisi = data.KatilimciSayisi;
                    Etkinlik.KayitTarihi = data.KayitTarihi;
                    Etkinlik.KaydedenId = data.Kullanici.Id;
                    Etkinlik.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

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
        public Result<EtkinliklerVM> EtkinlikEkle(EtkinliklerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    Etkinlikler Etkinlik = new Etkinlikler
                    {
                        EtkinlikAdi = model.EtkinlikAdi,
                        EtkinlikBilgi = model.EtkinlikBilgi,
                        BasTarihi = model.BasTarihi,
                        BitTarihi = model.BitTarihi,
                        DuzenleyenAdiSoyadi = model.DuzenleyenAdiSoyadi,
                        KatilimciSayisi = model.KatilimciSayisi,
                        KayitTarihi = model.KayitTarihi,
                        KaydedenId = user.LoginId,

                        FotoGaleri = new List<FotoGaleri>()
                    };
                    foreach (var file in Etkinlik.FotoGaleri)
                    {
                        Etkinlik.FotoGaleri.Add(new FotoGaleri()
                        {
                            FotoAdi = file.FotoAdi,
                            FotoURL = file.FotoURL
                        });
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
        public Result<EtkinliklerVM> EtkinlikGuncelle(EtkinliklerVM model, SessionContext user)
        {
            if (model.EtkinlikId != null)
            {
                try
                {

                    var data = _unitOfWork.etkinliklerRepository.Get(model.EtkinlikId);
                    if (data != null)
                    {
                        data.EtkinlikId = model.EtkinlikId;
                        data.EtkinlikBilgi = model.EtkinlikBilgi;
                        data.BasTarihi = model.BasTarihi;
                        data.BitTarihi = model.BitTarihi;
                        data.DuzenleyenAdiSoyadi = model.DuzenleyenAdiSoyadi;
                        data.KatilimciSayisi = model.KatilimciSayisi;
                        data.KayitTarihi = model.KayitTarihi;
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
        public Result<bool> EtkinlikSil(Guid id)
        {
            var data = _unitOfWork.etkinliklerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.etkinliklerRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        public Result<List<EtkinliklerVM>> EtkinlikGetirUlkeId(Guid UlkeId)
        {
            var data = _unitOfWork.etkinliklerRepository.GetAll(includeProperties: "Kullanici,Okullar").ToList();
            if (data != null)
            {
                List<EtkinliklerVM> returnData = new List<EtkinliklerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EtkinliklerVM()
                    {
                        EtkinlikId = item.EtkinlikId,
                        EtkinlikAdi = item.EtkinlikAdi,
                        EtkinlikBilgi = item.EtkinlikBilgi,
                        BasTarihi = item.BasTarihi,
                        BitTarihi = item.BitTarihi,
                        DuzenleyenAdiSoyadi = item.DuzenleyenAdiSoyadi,
                        KatilimciSayisi = item.KatilimciSayisi,
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
        public Result<List<EtkinliklerVM>> EtkinlikGetirOkulId(Guid okulId)
        {
            var data = _unitOfWork.etkinliklerRepository.GetAll(includeProperties: "Kullanici,Okullar").ToList();
            if (data != null)
            {
                List<EtkinliklerVM> returnData = new List<EtkinliklerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EtkinliklerVM()
                    {
                        EtkinlikId = item.EtkinlikId,
                        EtkinlikAdi = item.EtkinlikAdi,
                        EtkinlikBilgi = item.EtkinlikBilgi,
                        BasTarihi = item.BasTarihi,
                        BitTarihi = item.BitTarihi,
                        DuzenleyenAdiSoyadi = item.DuzenleyenAdiSoyadi,
                        KatilimciSayisi = item.KatilimciSayisi,
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

    }
}
