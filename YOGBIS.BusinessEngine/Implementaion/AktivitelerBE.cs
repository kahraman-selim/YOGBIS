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
                    Aktiviteler Etkinlik= new Aktiviteler();
                    Etkinlik.KullaniciId = user.LoginId;
                    Etkinlik.MdYrdAdiSoyadi = model.MdYrdAdiSoyadi;
                    Etkinlik.MdYrdDonusYil = model.MdYrdDonusYil;
                    Etkinlik.MdYrdEPosta = model.MdYrdEPosta;
                    Etkinlik.MdYrdTelefon = model.MdYrdTelefon;
                    Etkinlik.MudurAdiSoyadi = model.MudurAdiSoyadi;
                    Etkinlik.MudurDonusYil = model.MudurDonusYil;
                    Etkinlik.MudurEPosta = model.MudurEPosta;
                    Etkinlik.MudurTelefon = model.MudurTelefon;
                    Etkinlik.OkulAdres = model.OkulAdres;
                    Etkinlik.OkulId = model.OkulId;
                    Etkinlik.OkulTelefon = model.OkulTelefon;
                    Etkinlik.UlkeId = model.UlkeId;
                                       
                    _unitOfWork.EtkinlikRepository.Add(Etkinlik);
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
            if (model.EtkinlikId>0)
            {
                try
                {
                    //var Etkinlik = _mapper.Map<EtkinlikVM, Etkinlik>(model);
                    var data = _unitOfWork.EtkinlikRepository.Get(model.EtkinlikId);
                    if (data!=null)
                    {
                        data.KullaniciId = user.LoginId;
                        data.MdYrdAdiSoyadi = model.MdYrdAdiSoyadi;
                        data.MdYrdDonusYil = model.MdYrdDonusYil;
                        data.MdYrdEPosta = model.MdYrdEPosta;
                        data.MdYrdTelefon = model.MdYrdTelefon;
                        data.MudurAdiSoyadi = model.MudurAdiSoyadi;
                        data.MudurDonusYil = model.MudurDonusYil;
                        data.MudurEPosta = model.MudurEPosta;
                        data.MudurTelefon = model.MudurTelefon;
                        data.OkulAdres = model.OkulAdres;
                        data.OkulId = model.OkulId;
                        data.OkulTelefon = model.OkulTelefon;
                        data.UlkeId = model.UlkeId;

                        _unitOfWork.EtkinlikRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<EtkinlikVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<EtkinlikVM>(false, "Lütfen kayıt seçiniz");
                    }

                }
                catch (Exception ex)
                {

                    return new Result<EtkinlikVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<EtkinlikVM>(false, "Lütfen kayıt seçiniz");
            }
        }
        public Result<bool> EtkinlikSil(int id)
        {
            var data = _unitOfWork.EtkinlikRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.EtkinlikRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        public Result<List<AktivitelerVM>> EtkinlikGetirUlkeId(int ulkeId)
        {
            var data = _unitOfWork.EtkinlikRepository.GetAll(u => u.UlkeId == ulkeId, includeProperties: "Kullanici,Okullar,Ulkeler").ToList();
            if (data != null)
            {
                List<EtkinlikVM> returnData = new List<EtkinlikVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EtkinlikVM()
                    {
                        EtkinlikId = item.EtkinlikId,
                        OkulTelefon = item.OkulTelefon,
                        OkulAdres = item.OkulAdres,
                        //*************************                        
                        MudurAdiSoyadi = item.MudurAdiSoyadi,
                        MudurTelefon = item.MudurTelefon,
                        MudurEPosta = item.MudurEPosta,
                        MudurDonusYil = item.MudurDonusYil,
                        //****************************
                        MdYrdAdiSoyadi = item.MdYrdAdiSoyadi,
                        MdYrdTelefon = item.MdYrdTelefon,
                        MdYrdEPosta = item.MdYrdEPosta,
                        MdYrdDonusYil = item.MdYrdDonusYil,
                        //********************************
                        OkulId = item.OkulId,
                        OkulAdi = item.Okullar.OkulAdi,
                        UlkeId = item.UlkeId,
                        UlkeAdi = item.Ulkeler.UlkeAdi,
                        KayitTarihi = item.KayitTarihi,
                        KullaniciId = item.KullaniciId,
                        KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<EtkinlikVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<EtkinlikVM>>(false, ResultConstant.RecordNotFound);
            }
        }
    }
}
