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
        //public Result<List<OgrencilerVM>> OgrencileriGetir()
        //{
        //    var data = _unitOfWork.ogrencilerRepository.GetAll()
        //        .OrderBy(u => u.Siniflar.Subeler.Okullar.Sehirler.Eyaletler.Ulkeler.UlkeAdi)
        //        .ThenBy(o=>o.Siniflar.Subeler.Okullar.OkulAdi).ToList(); //GetAll(includeProperties: "Okullar,Ulkeler,Kullanici")

        //    if (data != null)
        //    {
        //        List<OgrencilerVM> returnData = new List<OgrencilerVM>();

        //        foreach (var item in data)
        //        {
        //            returnData.Add(new OgrencilerVM()
        //            {
        //                OgrencilerId=item.OgrencilerId,
                        
        //                KayitTarihi = item.KayitTarihi,
        //                KaydedenId = item.KaydedenId,
        //                KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
        //            });
        //        }
        //        return new Result<List<OgrencilerVM>>(true, ResultConstant.RecordFound, returnData);
        //    }
        //    else
        //    {
        //        return new Result<List<OgrencilerVM>>(false, ResultConstant.RecordNotFound);
        //    }
        //}
        //public Result<List<OgrencilerVM>> OgrenciGetirKullaniciId(string userId)
        //{
        //    var data = _unitOfWork.ogrencilerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici") //Okullar,Ulkeler
        //        .OrderBy(u => u.Siniflar.Subeler.Okullar.OkulAdi).ToList();
        //    if (data != null)
        //    {
        //        List<OgrencilerVM> returnData = new List<OgrencilerVM>();

        //        foreach (var item in data)
        //        {
        //            returnData.Add(new OgrencilerVM()
        //            {
        //                OgrencilerId = item.OgrencilerId,

        //                KayitTarihi = item.KayitTarihi,
        //                KaydedenId = item.KaydedenId,
        //                KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty                        
                        
        //            });
        //        }
        //        return new Result<List<OgrencilerVM>>(true, ResultConstant.RecordFound, returnData);
        //    }
        //    else
        //    {
        //        return new Result<List<OgrencilerVM>>(false, ResultConstant.RecordNotFound);
        //    }
        //}        
        //public Result<OgrencilerVM> OgrenciEkle(OgrencilerVM model, SessionContext user)
        //{
        //    if (model != null)
        //    {
        //        try
        //        {
                    
        //            Ogrenciler ogrenciler= new Ogrenciler();
        //            ogrenciler.KaydedenId = user.LoginId;
 

        //            _unitOfWork.ogrencilerRepository.Add(ogrenciler);
        //            _unitOfWork.Save();
        //            return new Result<OgrencilerVM>(true, ResultConstant.RecordCreateSuccess);
        //        }
        //        catch (Exception ex)
        //        {

        //            return new Result<OgrencilerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
        //        }
        //    }
        //    else
        //    {
        //        return new Result<OgrencilerVM>(false, "Boş veri olamaz");
        //    }
        //}
        //public Result<OgrencilerVM> OgrenciGuncelle(OgrencilerVM model, SessionContext user)
        //{
        //    if (model.OgrencilerId != null)
        //    {
        //        try
        //        {
        //            var data = _mapper.Map<OgrencilerVM, Ogrenciler>(model);
        //            //var data = _unitOfWork.ogrencilerRepository.Get(model.OgrencilerId);
        //            if (data != null)
        //            {
        //                data.KaydedenId = user.LoginId;


        //                _unitOfWork.ogrencilerRepository.Update(data);
        //                _unitOfWork.Save();
        //                return new Result<OgrencilerVM>(true, ResultConstant.RecordCreateSuccess);
        //            }
        //            else
        //            {
        //                return new Result<OgrencilerVM>(false, "Lütfen kayıt seçiniz");
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            return new Result<OgrencilerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
        //        }
        //    }
        //    else
        //    {
        //        return new Result<OgrencilerVM>(false, "Lütfen kayıt seçiniz");
        //    }

        //}
        //public Result<bool> OgrenciSil(Guid id)
        //{
        //    var data = _unitOfWork.ogrencilerRepository.Get(id);
        //    if (data != null)
        //    {
        //        _unitOfWork.ogrencilerRepository.Remove(data);
        //        _unitOfWork.Save();
        //        return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
        //    }
        //    else
        //    {
        //        return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
        //    }
        //}
        //public Result<List<OgrencilerVM>> OgrenciGetirUlkeId(int? ulkeId)
        //{
        //    var data = _unitOfWork.ogrencilerRepository.GetAll(u => u.Siniflar.Subeler.Okullar.Sehirler.Eyaletler.UlkeId == ulkeId, includeProperties: "Kullanici,Okullar,Ulkeler").ToList();
        //    if (data != null)
        //    {
        //        List<OgrencilerVM> returnData = new List<OgrencilerVM>();

        //        foreach (var item in data)
        //        {
        //            returnData.Add(new OgrencilerVM()
        //            {
        //                OgrencilerId = item.OgrencilerId,

        //                KayitTarihi = item.KayitTarihi,
        //                KaydedenId = item.KaydedenId,
        //                KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

        //            });
        //        }
        //        return new Result<List<OgrencilerVM>>(true, ResultConstant.RecordFound, returnData);
        //    }
        //    else
        //    {
        //        return new Result<List<OgrencilerVM>>(false, ResultConstant.RecordNotFound);
        //    }
        //}
        //public Result<OgrencilerVM> OgrenciGetir(int id)
        //{
        //    //if (id > 0)
        //    //{
        //    //    var data = _unitOfWork.ogrencilerRepository.GetFirstOrDefault(o => o.OgrencilerId == id, includeProperties: "Okullar,Ulkeler,Kullanici");

        //    //    if (data != null)
        //    //    {
        //    //        OgrencilerVM ogrenciler = new OgrencilerVM();
        //    //        ogrenciler.TCEOg = data.TCEOg;
        //    //        ogrenciler.TCKOg = data.TCKOg;
        //    //        ogrenciler.DEOg = data.DEOg;
        //    //        ogrenciler.DKOg = data.DKOg;
        //    //        ogrenciler.Yil = data.Yil;
        //    //        ogrenciler.Ay = data.Ay;
        //    //        ogrenciler.OkulId = data.OkulId;
        //    //        ogrenciler.OkulAdi = data.Okullar.OkulAdi;
        //    //        ogrenciler.UlkeId = data.UlkeId;
        //    //        ogrenciler.UlkeAdi = data.Ulkeler.UlkeAdi;
        //    //        ogrenciler.KullaniciId = data.Kullanici.Id;
        //    //        ogrenciler.KullaniciAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

        //    //        return new Result<OgrencilerVM>(true, ResultConstant.RecordFound, ogrenciler);
        //    //    }
        //    //    else
        //    //    {
        //    //        return new Result<OgrencilerVM>(false, ResultConstant.RecordNotFound);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    return new Result<OgrencilerVM>(false, ResultConstant.RecordNotFound);
        //    //}

        //    var data = _unitOfWork.ogrencilerRepository.Get(id);
        //    if (data != null)
        //    {
        //        var ogrenciler = _mapper.Map<Ogrenciler, OgrencilerVM>(data);
        //        return new Result<OgrencilerVM>(true, ResultConstant.RecordFound, ogrenciler);
        //    }
        //    else
        //    {
        //        return new Result<OgrencilerVM>(false, ResultConstant.RecordNotFound);
        //    }
        //}
        //public Result<List<OgrencilerVM>> OgrenciGetirOkulId(int? okulId)
        //{
        //    var data = _unitOfWork.ogrencilerRepository.GetAll(u => u.Siniflar.Subeler.OkulId == okulId, includeProperties: "Kullanici,Okullar,Ulkeler").ToList();
        //    if (data != null)
        //    {
        //        List<OgrencilerVM> returnData = new List<OgrencilerVM>();

        //        foreach (var item in data)
        //        {
        //            returnData.Add(new OgrencilerVM()
        //            {
        //                OgrencilerId = item.OgrencilerId,

        //                //OkulId = item.Siniflar.Subeler.OkulId,
        //                //OkulAdi = item.Siniflar.Subeler.Okullar.OkulAdi,
        //                //UlkeId = item.Siniflar.Subeler.Okullar.Sehirler.Eyaletler.UlkeId,
        //                //UlkeAdi = item.Siniflar.Subeler.Okullar.Sehirler.Eyaletler.Ulkeler.UlkeAdi,
        //                KayitTarihi = item.KayitTarihi,
        //                KaydedenId = item.KaydedenId,
        //                KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

        //            });
        //        }
        //        return new Result<List<OgrencilerVM>>(true, ResultConstant.RecordFound, returnData);
        //    }
        //    else
        //    {
        //        return new Result<List<OgrencilerVM>>(false, ResultConstant.RecordNotFound);
        //    }
        //}
    }
}
