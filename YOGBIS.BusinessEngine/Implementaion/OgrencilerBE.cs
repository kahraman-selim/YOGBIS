using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.Extentsion;
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
            var data = _unitOfWork.ogrencilerRepository.GetAll(includeProperties: "Okullar,Ulkeler,Kullanici").OrderBy(u => u.Ulkeler.UlkeAdi).ToList();

            if (data != null)
            {
                List<OgrencilerVM> returnData = new List<OgrencilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OgrencilerVM()
                    {
                        OgrencilerId=item.OgrencilerId,
                        TCEOg=item.TCEOg,
                        TCKOg=item.TCKOg,
                        DEOg=item.DEOg,
                        DKOg=item.DKOg,
                        //Ay= EnumExtension<EnumAylar>.GetDisplayValue((EnumAylar)item.Ay).ToString(),
                        Yil =item.Yil,
                        OkulId = item.OkulId,
                        OkulAdi = item.Okullar.OkulAdi,
                        UlkeId = item.UlkeId,
                        UlkeAdi = item.Ulkeler.UlkeAdi,
                        KayitTarihi = item.KayitTarihi,
                        KullaniciId = item.KullaniciId,
                        KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
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
            var data = _unitOfWork.ogrencilerRepository.GetAll(u => u.KullaniciId == userId, includeProperties: "Kullanici,Okullar,Ulkeler").ToList();
            if (data != null)
            {
                List<OgrencilerVM> returnData = new List<OgrencilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OgrencilerVM()
                    {
                        OgrencilerId = item.OgrencilerId,
                        TCEOg = item.TCEOg,
                        TCKOg = item.TCKOg,
                        DEOg = item.DEOg,
                        DKOg = item.DKOg,
                        //Ay = item.Ay,
                        Yil = item.Yil,
                        OkulId =item.OkulId,
                        OkulAdi=item.Okullar.OkulAdi,
                        UlkeId=item.UlkeId,
                        UlkeAdi=item.Ulkeler.UlkeAdi,
                        KayitTarihi = item.KayitTarihi,
                        KullaniciId = item.KullaniciId,
                        KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty                        
                        
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
                    
                    Ogrenciler ogrenciler= new Ogrenciler();
                    ogrenciler.KullaniciId = user.LoginId;
                    ogrenciler.TCEOg = model.TCEOg;
                    ogrenciler.TCKOg = model.TCKOg;
                    ogrenciler.DEOg = model.DEOg;
                    ogrenciler.DKOg = model.DKOg;
                    //ogrenciler.Ay = model.Ay;
                    ogrenciler.Yil = model.Yil;
                    ogrenciler.OkulId = model.OkulId;
                    ogrenciler.UlkeId = model.UlkeId;

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
            if (model.OgrencilerId>0)
            {
                try
                {                    
                    var data = _unitOfWork.ogrencilerRepository.Get(model.OgrencilerId);
                    if (data!=null)
                    {
                        data.KullaniciId = user.LoginId;
                        data.TCEOg = model.TCEOg;
                        data.TCKOg = model.TCKOg;
                        data.DEOg = model.DEOg;
                        data.DKOg = model.DKOg;
                        //data.Ay = model.Ay;
                        data.Yil = model.Yil;
                        data.OkulId = model.OkulId;
                        data.UlkeId = model.UlkeId;

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
        public Result<bool> OgrenciSil(int id)
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
        public Result<List<OgrencilerVM>> OgrenciGetirUlkeId(int ulkeId)
        {
            var data = _unitOfWork.ogrencilerRepository.GetAll(u => u.UlkeId == ulkeId, includeProperties: "Kullanici,Okullar,Ulkeler").ToList();
            if (data != null)
            {
                List<OgrencilerVM> returnData = new List<OgrencilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OgrencilerVM()
                    {
                        OgrencilerId = item.OgrencilerId,
                        TCEOg = item.TCEOg,
                        TCKOg = item.TCKOg,
                        DEOg = item.DEOg,
                        DKOg = item.DKOg,
                        //Ay = item.Ay,
                        Yil = item.Yil,
                        OkulId = item.OkulId,
                        OkulAdi = item.Okullar.OkulAdi,
                        UlkeId = item.UlkeId,
                        UlkeAdi = item.Ulkeler.UlkeAdi,
                        KayitTarihi = item.KayitTarihi,
                        KullaniciId = item.KullaniciId,
                        KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<OgrencilerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OgrencilerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<OgrencilerVM> OgrenciGetir(int id)
        {
            if (id > 0)
            {
                var data = _unitOfWork.ogrencilerRepository.GetFirstOrDefault(u => u.OgrencilerId == id, includeProperties: "Okullar,Ulkeler,Kullanici");

                if (data != null)
                {
                    OgrencilerVM ogrenciler = new OgrencilerVM();
                    ogrenciler.TCEOg = data.TCEOg;
                    ogrenciler.TCKOg = data.TCKOg;
                    ogrenciler.DEOg = data.DEOg;
                    ogrenciler.DKOg = data.DKOg;
                    ogrenciler.Yil = data.Yil;
                    //ogrenciler.Ay = data.Ay;
                    ogrenciler.OkulId = data.OkulId;
                    ogrenciler.OkulAdi = data.Okullar.OkulAdi;
                    ogrenciler.UlkeId = data.UlkeId;
                    ogrenciler.UlkeAdi = data.Ulkeler.UlkeAdi;
                    ogrenciler.KullaniciId = data.Kullanici.Id;
                    ogrenciler.KullaniciAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

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
        public Result<List<OgrencilerVM>> OgrenciGetirOkulId(int okulId)
        {
            var data = _unitOfWork.ogrencilerRepository.GetAll(u => u.OkulId == okulId, includeProperties: "Kullanici,Okullar,Ulkeler").ToList();
            if (data != null)
            {
                List<OgrencilerVM> returnData = new List<OgrencilerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OgrencilerVM()
                    {
                        OgrencilerId = item.OgrencilerId,
                        TCEOg = item.TCEOg,
                        TCKOg = item.TCKOg,
                        DEOg = item.DEOg,
                        DKOg = item.DKOg,
                        //Ay = item.Ay,
                        Yil = item.Yil,
                        OkulId = item.OkulId,
                        OkulAdi = item.Okullar.OkulAdi,
                        UlkeId = item.UlkeId,
                        UlkeAdi = item.Ulkeler.UlkeAdi,
                        KayitTarihi = item.KayitTarihi,
                        KullaniciId = item.KullaniciId,
                        KullaniciAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

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
