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
    public class OgrencilerBE : IOgrencilerBE
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OgrencilerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Result<List<OgrencilerVM>> OgrencileriGetir()
        {
            var data = _unitOfWork.ogrencilerRepository.GetAll(includeProperties: "Kullanici,Ulkeler").OrderBy(u => u.Ulkeler.UlkeAdi).ToList();
                //.ThenBy(o => o.s).ToList(); //GetAll(includeProperties: "Okullar,Ulkeler,Kullanici")

            if (data != null)
            {
                List<OgrencilerVM> returnData = new List<OgrencilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OgrencilerVM()
                    {
                        OgrencilerId = item.OgrencilerId,
                        Cinsiyet=item.Cinsiyet,
                        AyrilmaNedeni=item.AyrilmaNedeni,
                        AyrilmaTarihi=item.AyrilmaTarihi,
                        BaslamaKayitTarihi=item.BaslamaKayitTarihi,
                        EgitimciId=item.EgitimciId,
                        EyaletId=item.EyaletId,
                        KayitNedeni=item.KayitNedeni,
                        OgrenciTuru=item.OgrenciTuru,
                        OkulId=item.OkulId,
                        SehirId=item.SehirId,
                        SinifId=item.SinifId,
                        SubeId=item.SubeId,
                        TemsilcilikId=item.TemsilcilikId,
                        UlkeId=item.UlkeId,
                        UlkeAdi=item.Ulkeler != null ? item.Ulkeler.UlkeAdi : string.Empty,
                        Uyruk=item.Uyruk,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
                    });
                }
                return new Result<List<OgrencilerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OgrencilerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<List<OgrencilerVM>> OgrenciGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.ogrencilerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,Ulkeler").ToList();
            if (data != null)
            {
                List<OgrencilerVM> returnData = new List<OgrencilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OgrencilerVM()
                    {
                        OgrencilerId = item.OgrencilerId,
                        Cinsiyet = item.Cinsiyet,
                        AyrilmaNedeni = item.AyrilmaNedeni,
                        AyrilmaTarihi = item.AyrilmaTarihi,
                        BaslamaKayitTarihi = item.BaslamaKayitTarihi,
                        EgitimciId = item.EgitimciId,
                        EyaletId = item.EyaletId,
                        KayitNedeni = item.KayitNedeni,
                        OgrenciTuru = item.OgrenciTuru,
                        OkulId = item.OkulId,
                        SehirId = item.SehirId,
                        SinifId = item.SinifId,
                        SubeId = item.SubeId,
                        TemsilcilikId = item.TemsilcilikId,
                        UlkeId = item.UlkeId,
                        UlkeAdi = item.Ulkeler != null ? item.Ulkeler.UlkeAdi : string.Empty,
                        Uyruk = item.Uyruk,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<OgrencilerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OgrencilerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<OgrencilerVM> OgrenciEkle(OgrencilerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {

                    Ogrenciler ogrenciler = new Ogrenciler();
                    ogrenciler.KaydedenId = user.LoginId;
                    ogrenciler.OkulId = model.OkulId;
                    ogrenciler.AyrilmaNedeni = model.AyrilmaNedeni;
                    ogrenciler.AyrilmaTarihi = model.AyrilmaTarihi;
                    ogrenciler.UlkeId = model.UlkeId;
                    ogrenciler.BaslamaKayitTarihi = model.BaslamaKayitTarihi;
                    ogrenciler.Cinsiyet = model.Cinsiyet;
                    ogrenciler.EgitimciId = model.EgitimciId;
                    ogrenciler.EyaletId = model.EyaletId;
                    ogrenciler.KayitNedeni = model.KayitNedeni;
                    ogrenciler.OgrenciTuru = model.OgrenciTuru;
                    ogrenciler.SehirId = model.SehirId;
                    ogrenciler.SinifId = model.SinifId;
                    ogrenciler.SubeId = model.SubeId;
                    ogrenciler.TemsilcilikId = model.TemsilcilikId;
                    ogrenciler.Uyruk = model.Uyruk;
                    ogrenciler.KayitTarihi = model.KayitTarihi;


                    _unitOfWork.ogrencilerRepository.Add(ogrenciler);
                    _unitOfWork.Save();
                    return new Result<OgrencilerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<OgrencilerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<OgrencilerVM>(false, "Boş veri olamaz");
            }
        }
        public Result<OgrencilerVM> OgrenciGuncelle(OgrencilerVM model, SessionContext user)
        {
            if (model.OgrencilerId != null)
            {
                try
                {
                    var data = _mapper.Map<OgrencilerVM, Ogrenciler>(model);
                    //var data = _unitOfWork.ogrencilerRepository.Get(model.OgrencilerId);
                    if (data != null)
                    {
                        data.KaydedenId = user.LoginId;
                        data.OkulId = model.OkulId;
                        data.AyrilmaNedeni = model.AyrilmaNedeni;
                        data.AyrilmaTarihi = model.AyrilmaTarihi;
                        data.UlkeId = model.UlkeId;
                        data.BaslamaKayitTarihi = model.BaslamaKayitTarihi;
                        data.Cinsiyet = model.Cinsiyet;
                        data.EgitimciId = model.EgitimciId;
                        data.EyaletId = model.EyaletId;
                        data.KayitNedeni = model.KayitNedeni;
                        data.OgrenciTuru = model.OgrenciTuru;
                        data.SehirId = model.SehirId;
                        data.SinifId = model.SinifId;
                        data.SubeId = model.SubeId;
                        data.TemsilcilikId = model.TemsilcilikId;
                        data.Uyruk = model.Uyruk;
                        data.KayitTarihi = model.KayitTarihi;

                        _unitOfWork.ogrencilerRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<OgrencilerVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<OgrencilerVM>(false, "Lütfen kayıt seçiniz");
                    }

                }
                catch (Exception ex)
                {

                    return new Result<OgrencilerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<OgrencilerVM>(false, "Lütfen kayıt seçiniz");
            }

        }
        public Result<bool> OgrenciSil(Guid id)
        {
            var data = _unitOfWork.ogrencilerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.ogrencilerRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        public Result<List<OgrencilerVM>> OgrenciGetirUlkeId(Guid ulkeId)
        {
            var data = _unitOfWork.ogrencilerRepository.GetAll(u => u.UlkeId == ulkeId, includeProperties: "Kullanici,Ulkeler").ToList();
            if (data != null)
            {
                List<OgrencilerVM> returnData = new List<OgrencilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OgrencilerVM()
                    {
                        OgrencilerId = item.OgrencilerId,
                        Cinsiyet = item.Cinsiyet,
                        AyrilmaNedeni = item.AyrilmaNedeni,
                        AyrilmaTarihi = item.AyrilmaTarihi,
                        BaslamaKayitTarihi = item.BaslamaKayitTarihi,
                        EgitimciId = item.EgitimciId,
                        EyaletId = item.EyaletId,
                        KayitNedeni = item.KayitNedeni,
                        OgrenciTuru = item.OgrenciTuru,
                        OkulId = item.OkulId,
                        SehirId = item.SehirId,
                        SinifId = item.SinifId,
                        SubeId = item.SubeId,
                        TemsilcilikId = item.TemsilcilikId,
                        UlkeId = item.UlkeId,
                        UlkeAdi = item.Ulkeler != null ? item.Ulkeler.UlkeAdi : string.Empty,
                        Uyruk = item.Uyruk,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<OgrencilerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OgrencilerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<OgrencilerVM> OgrenciGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.ogrencilerRepository.GetFirstOrDefault(o => o.OgrencilerId == id, includeProperties: "Kullanici,Ulkeler");

                if (data != null)
                {
                    OgrencilerVM ogrenciler = new OgrencilerVM();

                    ogrenciler.OkulId = data.OkulId;
                    ogrenciler.AyrilmaNedeni = data.AyrilmaNedeni;
                    ogrenciler.AyrilmaTarihi = data.AyrilmaTarihi;
                    ogrenciler.UlkeId = data.UlkeId;
                    ogrenciler.BaslamaKayitTarihi = data.BaslamaKayitTarihi;
                    ogrenciler.Cinsiyet = data.Cinsiyet;
                    ogrenciler.EgitimciId = data.EgitimciId;
                    ogrenciler.EyaletId = data.EyaletId;
                    ogrenciler.KayitNedeni = data.KayitNedeni;
                    ogrenciler.OgrenciTuru = data.OgrenciTuru;
                    ogrenciler.SehirId = data.SehirId;
                    ogrenciler.SinifId = data.SinifId;
                    ogrenciler.SubeId = data.SubeId;
                    ogrenciler.TemsilcilikId = data.TemsilcilikId;
                    ogrenciler.Uyruk = data.Uyruk;
                    ogrenciler.KayitTarihi = data.KayitTarihi;
                    ogrenciler.UlkeId = data.UlkeId;
                    ogrenciler.UlkeAdi = data.Ulkeler.UlkeAdi;
                    ogrenciler.KaydedenId = data.Kullanici.Id;
                    ogrenciler.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    return new Result<OgrencilerVM>(true, ResultConstant.RecordFound, ogrenciler);
                }
                else
                {
                    return new Result<OgrencilerVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<OgrencilerVM>(false, ResultConstant.RecordNotFound);
            }

        }
        public Result<List<OgrencilerVM>> OgrenciGetirOkulId(Guid okulId)
        {
            var data = _unitOfWork.ogrencilerRepository.GetAll(u => u.OkulId == okulId, includeProperties: "Kullanici,Ulkeler").ToList();
            if (data != null)
            {
                List<OgrencilerVM> returnData = new List<OgrencilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OgrencilerVM()
                    {
                        OgrencilerId = item.OgrencilerId,
                        Cinsiyet = item.Cinsiyet,
                        AyrilmaNedeni = item.AyrilmaNedeni,
                        AyrilmaTarihi = item.AyrilmaTarihi,
                        BaslamaKayitTarihi = item.BaslamaKayitTarihi,
                        EgitimciId = item.EgitimciId,
                        EyaletId = item.EyaletId,
                        KayitNedeni = item.KayitNedeni,
                        OgrenciTuru = item.OgrenciTuru,
                        OkulId = item.OkulId,
                        SehirId = item.SehirId,
                        SinifId = item.SinifId,
                        SubeId = item.SubeId,
                        TemsilcilikId = item.TemsilcilikId,
                        UlkeId = item.UlkeId,
                        UlkeAdi = item.Ulkeler != null ? item.Ulkeler.UlkeAdi : string.Empty,
                        Uyruk = item.Uyruk,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<OgrencilerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OgrencilerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
    }
}
