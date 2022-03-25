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
    public class UlkelerBE : IUlkelerBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Dönüştürücüler
        public UlkelerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region UlkeleriGetir
        public Result<List<UlkelerVM>> UlkeleriGetir()
        {

            var data = _unitOfWork.ulkelerRepository.GetAll(includeProperties: "Kitalar,Kullanici,Eyaletler,Sehirler,Okullar").OrderBy(o => o.UlkeAdi).ToList();

            if (data != null)
            {
                List<UlkelerVM> returnData = new List<UlkelerVM>();
                foreach (var item in data)
                {
                    if (item.Aktif==true)
                    {
                        returnData.Add(new UlkelerVM()
                        {
                            UlkeId = item.UlkeId,
                            UlkeAdi = item.UlkeAdi,
                            UlkeBayrakURL = item.UlkeBayrakURL,
                            UlkeBayrakAdi = item.UlkeBayrakAdi,
                            UlkeAciklama = item.UlkeAciklama,
                            KayitTarihi = item.KayitTarihi,
                            KitaId = item.KitaId,
                            KitaAdi = item.Kitalar.KitaAdi,
                            Aktif = item.Aktif,
                            UlkeKodu = item.UlkeKodu,
                            //VatandasSayisi=item.VatandasSayisi,
                            //UlkeGrupId = item.Kitalar.UlkeGruplariKitalars.Where(x=>x.KitaId==item.KitaId),
                            //UlkeGrupAdi = item.UlkeGruplari.UlkeGrupAdi,
                            KaydedenId = item.KaydedenId,
                            KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,

                            OkulSayisi = _unitOfWork.okullarRepository.GetAll(g => g.UlkeId==item.UlkeId).Count(),

                            OgrenciOkulSayisi = ((_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                            .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1")
                            .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                            .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1")
                            .Select(x => x.First().AyrilanSayisi).Sum()) + (_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                            .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2")
                            .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                            .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2")
                            .Select(x => x.First().AyrilanSayisi).Sum())) + ((_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                            .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1")
                            .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                            .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1")
                            .Select(x => x.First().AyrilanSayisi).Sum()) + (_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                            .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2")
                            .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                            .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2")
                            .Select(x => x.First().AyrilanSayisi).Sum()))

                        });
                    }

                }
                return new Result<List<UlkelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<UlkelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region UlkeleriGetirUlkeId
        public Result<List<UlkelerVM>> UlkeleriGetirUlkeId(Guid ulkeId)
        {

            var data = _unitOfWork.ulkelerRepository.GetAll(u => u.UlkeId == ulkeId, includeProperties: "Kitalar,Kullanici,Eyaletler,Sehirler,Okullar").OrderBy(o => o.UlkeAdi).ToList();

            if (data != null)
            {
                List<UlkelerVM> returnData = new List<UlkelerVM>();
                foreach (var item in data)
                {
                    if (item.Aktif == true)
                    {
                        returnData.Add(new UlkelerVM()
                        {
                            UlkeId = item.UlkeId,
                            UlkeAdi = item.UlkeAdi,
                            UlkeBayrakURL = item.UlkeBayrakURL,
                            UlkeBayrakAdi = item.UlkeBayrakAdi,
                            UlkeAciklama = item.UlkeAciklama,
                            KayitTarihi = item.KayitTarihi,
                            KitaId = item.KitaId,
                            KitaAdi = item.Kitalar.KitaAdi,
                            Aktif = item.Aktif,
                            UlkeKodu = item.UlkeKodu,
                            //VatandasSayisi=item.VatandasSayisi,
                            //UlkeGrupId = item.Kitalar.UlkeGruplariKitalars.Where(x=>x.KitaId==item.KitaId),
                            //UlkeGrupAdi = item.UlkeGruplari.UlkeGrupAdi,
                            KaydedenId = item.KaydedenId,
                            KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,

                            OkulSayisi = _unitOfWork.okullarRepository.GetAll(g => g.UlkeId == item.UlkeId).Count(),

                            OgrenciOkulSayisi = ((_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                            .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1")
                            .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                            .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1")
                            .Select(x => x.First().AyrilanSayisi).Sum()) + (_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                            .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2")
                            .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                            .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2")
                            .Select(x => x.First().AyrilanSayisi).Sum())) + ((_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                            .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1")
                            .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                            .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1")
                            .Select(x => x.First().AyrilanSayisi).Sum()) + (_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                            .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2")
                            .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == item.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                            .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2")
                            .Select(x => x.First().AyrilanSayisi).Sum()))

                        });
                    }

                }
                return new Result<List<UlkelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<UlkelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region UlkeleriGetirUlkeleriGetirViewComponent
        public Result<List<UlkelerVM>> UlkeleriGetirViewComponent()
        {

            var data = _unitOfWork.ulkelerRepository.GetAll(includeProperties: "Kitalar,Kullanici").OrderBy(o => o.UlkeAdi).ToList();

            if (data != null)
            {
                List<UlkelerVM> returnData = new List<UlkelerVM>();
                foreach (var item in data)
                {
                    returnData.Add(new UlkelerVM()
                    {
                        UlkeId = item.UlkeId,
                        UlkeAdi = item.UlkeAdi,
                        //UlkeBayrakURL = item.UlkeBayrakURL,
                        //UlkeBayrakAdi = item.UlkeBayrakAdi,
                        //UlkeAciklama = item.UlkeAciklama,
                        //KayitTarihi = item.KayitTarihi,
                        KitaId = item.KitaId,
                        KitaAdi = item.Kitalar.KitaAdi,
                        Aktif = item.Aktif,
                        //UlkeKodu = item.UlkeKodu,
                        //KaydedenId = item.KaydedenId,
                        //KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
                    });
                }
                return new Result<List<UlkelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<UlkelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region UlkeEkle
        public Result<UlkelerVM> UlkeEkle(UlkelerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var ulkeler = new Ulkeler()
                    {
                        Aktif = model.Aktif,
                        KaydedenId = user.LoginId,
                        KayitTarihi = model.KayitTarihi,
                        KitaId = model.KitaId,
                        UlkeAciklama = model.UlkeAciklama,
                        UlkeAdi = model.UlkeAdi,
                        UlkeBayrakURL = model.UlkeBayrakURL,
                        UlkeBayrakAdi = model.UlkeBayrakAdi,
                        UlkeKodu = model.UlkeKodu
                    };

                    ulkeler.FotoGaleri = new List<FotoGaleri>();
                    if (model.FotoGaleri != null)
                    {
                        foreach (var file in model.FotoGaleri)
                        {
                            ulkeler.FotoGaleri.Add(new FotoGaleri()
                            {
                                FotoAdi = file.FotoAdi,
                                FotoURL = file.FotoURL,
                                KaydedenId = user.LoginId,
                                KayitTarihi = model.KayitTarihi
                            });
                        }
                    }

                    _unitOfWork.ulkelerRepository.Add(ulkeler);
                    _unitOfWork.Save();

                    return new Result<UlkelerVM>(true, ResultConstant.RecordCreateSuccess, model);
                }
                catch (Exception ex)
                {

                    return new Result<UlkelerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<UlkelerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region UlkeGetir(id)
        public Result<UlkelerVM> UlkeGetir(Guid id)
        {

            if (id != null)
            {
                var data = _unitOfWork.ulkelerRepository.GetFirstOrDefault(u => u.UlkeId == id, includeProperties: "Kitalar,Eyaletler,Sehirler,Okullar,Kullanici,FotoGaleri");
                if (data != null)
                {
                    UlkelerVM ulke = new UlkelerVM();
                    ulke.UlkeId = data.UlkeId;
                    ulke.UlkeKodu = data.UlkeKodu;
                    ulke.UlkeAdi = data.UlkeAdi;
                    ulke.UlkeBayrakURL = data.UlkeBayrakURL;
                    ulke.UlkeBayrakAdi = data.UlkeBayrakAdi;
                    ulke.UlkeAciklama = data.UlkeAciklama;
                    ulke.Aktif = data.Aktif;
                    //ulke.VatandasSayisi = data.VatandasSayisi;
                    ulke.KitaId = data.KitaId;
                    ulke.KitaAdi = data.Kitalar.KitaAdi;
                    ulke.KaydedenId = data.KaydedenId;
                    ulke.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    ulke.FotoGaleri = data.FotoGaleri.Select(g => new FotoGaleriVM()
                    {
                        FotoGaleriId = g.FotoGaleriId,
                        FotoAdi = g.FotoAdi,
                        FotoURL = g.FotoURL,
                        KayitTarihi = g.KayitTarihi,
                        KaydedenId = g.KaydedenId

                    }).ToList();

                    ulke.Okullar = _unitOfWork.okullarRepository.GetAll(u=>u.UlkeId== data.UlkeId).Select(o => new OkullarVM()
                    {
                        OkulId=o.OkulId,
                        OkulAdi=o.OkulAdi,
                        OkulKodu=o.OkulKodu
                        
                    }).ToList();

                    ulke.OkulSayisi = _unitOfWork.okullarRepository.GetAll(g => g.UlkeId == data.UlkeId).Count();

                    ulke.OgrenciOkulSayisi = ((_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                    .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1")
                    .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                    .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1")
                    .Select(x => x.First().AyrilanSayisi).Sum()) + (_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                    .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2")
                    .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                    .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2")
                    .Select(x => x.First().AyrilanSayisi).Sum())) + ((_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                    .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1")
                    .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                    .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1")
                    .Select(x => x.First().AyrilanSayisi).Sum()) + (_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                    .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2")
                    .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                    .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2")
                    .Select(x => x.First().AyrilanSayisi).Sum()));

                    return new Result<UlkelerVM>(true, ResultConstant.RecordFound, ulke);
                }
                else
                {
                    return new Result<UlkelerVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<UlkelerVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region UlkeGetirUlkeKodu(ulkeKodu)
        public Result<UlkelerVM> UlkeGetirUlkeKodu(string ulkeKodu)
        {

            if (ulkeKodu != null)
            {
                var ulkeId = UlkeIdGetir(ulkeKodu).Data;

                var data = _unitOfWork.ulkelerRepository.GetFirstOrDefault(u => u.UlkeId == ulkeId, includeProperties: "Kitalar,Kullanici,FotoGaleri");
                if (data != null)
                {
                    UlkelerVM ulke = new UlkelerVM();
                    ulke.UlkeId = data.UlkeId;
                    ulke.UlkeKodu = data.UlkeKodu;
                    ulke.UlkeAdi = data.UlkeAdi;
                    ulke.UlkeBayrakURL = data.UlkeBayrakURL;
                    ulke.UlkeBayrakAdi = data.UlkeBayrakAdi;
                    ulke.UlkeAciklama = data.UlkeAciklama;
                    ulke.Aktif = data.Aktif;
                    //ulke.VatandasSayisi = data.VatandasSayisi;
                    ulke.KitaId = data.KitaId;
                    ulke.KitaAdi = data.Kitalar.KitaAdi;
                    ulke.KaydedenId = data.KaydedenId;
                    ulke.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    ulke.FotoGaleri = data.FotoGaleri.Select(g => new FotoGaleriVM()
                    {
                        FotoGaleriId = g.FotoGaleriId,
                        FotoAdi = g.FotoAdi,
                        FotoURL = g.FotoURL,
                        KayitTarihi = g.KayitTarihi,
                        KaydedenId = g.KaydedenId

                    }).ToList();

                    ulke.Okullar = _unitOfWork.okullarRepository.GetAll(u => u.UlkeId == data.UlkeId).Select(o => new OkullarVM()
                    {
                        OkulId = o.OkulId,
                        OkulAdi = o.OkulAdi,
                        OkulKodu = o.OkulKodu

                    }).ToList();

                    ulke.OkulSayisi = _unitOfWork.okullarRepository.GetAll(g => g.UlkeId == data.UlkeId).Count();

                    ulke.OgrenciOkulSayisi = ((_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                    .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1")
                    .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                    .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "1")
                    .Select(x => x.First().AyrilanSayisi).Sum()) + (_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                    .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2")
                    .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                    .GroupBy(o => o.Cinsiyet == false && o.OgrenciTuru == "1" && o.Uyruk == "2")
                    .Select(x => x.First().AyrilanSayisi).Sum())) + ((_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                    .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1")
                    .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1").ToList()
                    .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "1")
                    .Select(x => x.First().AyrilanSayisi).Sum()) + (_unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.BaslamaKayitTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                    .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2")
                    .Select(x => x.First().KayitSayisi).Sum() - _unitOfWork.ogrencilerRepository.GetAll(o => o.UlkeId == data.UlkeId && o.AyrilmaTarihi != null && o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2").ToList()
                    .GroupBy(o => o.Cinsiyet == true && o.OgrenciTuru == "1" && o.Uyruk == "2")
                    .Select(x => x.First().AyrilanSayisi).Sum()));

                    return new Result<UlkelerVM>(true, ResultConstant.RecordFound, ulke);
                }
                else
                {
                    return new Result<UlkelerVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<UlkelerVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region UlkeGuncelle
        public Result<UlkelerVM> UlkeGuncelle(UlkelerVM model, SessionContext user)
        {
            if (model.UlkeId != null)
            {
                var data = _unitOfWork.ulkelerRepository.Get(model.UlkeId);
                if (data != null)
                {
                    data.UlkeAdi = model.UlkeAdi;
                    data.UlkeAciklama = model.UlkeAciklama;
                    data.KitaId = model.KitaId;
                    data.Aktif = model.Aktif;
                    data.UlkeKodu = model.UlkeKodu;
                    data.KaydedenId = user.LoginId;
                    data.KayitTarihi = model.KayitTarihi;
                    data.UlkeBayrakURL = model.UlkeBayrakURL;
                    data.UlkeBayrakAdi = model.UlkeBayrakAdi;

                   
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

                    _unitOfWork.ulkelerRepository.Update(data);
                    _unitOfWork.Save();

                    return new Result<UlkelerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                else
                {
                    return new Result<UlkelerVM>(false, "Lütfen kayıt seçiniz");
                }
            }
            else
            {
                return new Result<UlkelerVM>(false, "Lütfen kayıt seçiniz");
            }

            #region diğeryontem
            //if (model != null)
            //{
            //    try
            //    {
            //        var ulkeler = _mapper.Map<UlkelerVM, Ulkeler>(model);
            //        ulkeler.KullaniciId = user.LoginId;
            //        ulkeler.UlkeBayrak = uniqueFileName;
            //        _unitOfWork.ulkelerRepository.Update(ulkeler);
            //        _unitOfWork.Save();
            //        return new Result<UlkelerVM>(true, ResultConstant.RecordCreateSuccess);
            //    }
            //    catch (Exception ex)
            //    {

            //        return new Result<UlkelerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
            //    }
            //}
            //else
            //{
            //    return new Result<UlkelerVM>(false, "Boş veri olamaz");
            //} 
            #endregion
        }
        #endregion

        #region UlkeSil
        public Result<bool> UlkeSil(Guid id)
        {
            var data = _unitOfWork.ulkelerRepository.GetFirstOrDefault(u => u.UlkeId == id, includeProperties: "FotoGaleri");
            if (data != null)
            {

                _unitOfWork.ulkelerRepository.Remove(data);
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

                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region UlkeGetirKullaniciId
        public Result<List<UlkelerVM>> UlkeGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.ulkelerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,Kitalar").OrderBy(u => u.UlkeAdi).ToList();

            if (data != null)
            {
                List<UlkelerVM> returnData = new List<UlkelerVM>();
                foreach (var item in data)
                {
                    returnData.Add(new UlkelerVM()
                    {
                        UlkeId = item.UlkeId,
                        UlkeAdi = item.UlkeAdi,
                        UlkeBayrakURL = item.UlkeBayrakURL,
                        UlkeAciklama = item.UlkeAciklama,
                        KayitTarihi = item.KayitTarihi,
                        KitaId = item.KitaId,
                        UlkeKodu = item.UlkeKodu,
                        //VatandasSayisi=item.VatandasSayisi,
                        KitaAdi = item.Kitalar.KitaAdi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
                    }); 
                }
                return new Result<List<UlkelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<UlkelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region UlkeFotoSil
        public Result<bool> UlkeFotoSil(Guid id)
        {
            var data = _unitOfWork.fotoGaleriRepository.GetFirstOrDefault(u => u.FotoGaleriId == id);
            if (data != null)          {
                
                _unitOfWork.fotoGaleriRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
     
        #endregion

        #region UlkeBayrakURLGetir(id)
        public Result<string> UlkeBayrakURLGetir(Guid id)
        {

            var data = _unitOfWork.ulkelerRepository.Get(id);
            if (data != null)
            {
                var ulkeBayrakURL = data.UlkeBayrakURL;

                return new Result<string>(true, ResultConstant.RecordFound, ulkeBayrakURL);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion

        #region UlkeAdGetir(id)
        public Result<string> UlkeAdGetir(Guid id)
        {

            var data = _unitOfWork.ulkelerRepository.Get(id);
            if (data != null)
            {
                var ulkeAd = data.UlkeAdi;

                return new Result<string>(true, ResultConstant.RecordFound, ulkeAd);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion

        #region UlkeIdGetir(id)
        public Result<Guid> UlkeIdGetir(Guid id)
        {

            var data = _unitOfWork.ulkelerRepository.Get(id);
            if (data != null)
            {
                var ulkeId = data.UlkeId;

                return new Result<Guid>(true, ResultConstant.RecordFound, ulkeId);
            }
            else
            {
                return new Result<Guid>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion

        #region UlkeIdGetir(ulkeKodu)
        public Result<Guid> UlkeIdGetir(string ulkeKodu)
        {

            if (ulkeKodu == null)
            {
                return new Result<Guid>(false, ResultConstant.RecordFound);
            }
            else
            {
                var data = _unitOfWork.ulkelerRepository.GetFirstOrDefault(x => x.UlkeKodu == ulkeKodu);

                if (data != null)
                {

                    var ulkeId = data.UlkeId;


                    return new Result<Guid>(true, ResultConstant.RecordFound, ulkeId);
                }
                else
                {
                    return new Result<Guid>(false, ResultConstant.RecordNotFound);
                }
            }


        }
        #endregion
    }
}
