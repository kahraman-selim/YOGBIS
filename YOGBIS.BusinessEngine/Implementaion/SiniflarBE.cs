using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class SiniflarBE : ISiniflarBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Dönüştürücüler
        public SiniflarBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region SiniflariGetir
        public Result<List<SiniflarVM>> SiniflariGetir()
        {
            var data = _unitOfWork.siniflarRepository.GetAll(includeProperties: "Kullanici,Subeler,Ogrenciler").OrderBy(c => c.SinifAdi).ToList();

            if (data != null)
            {
                List<SiniflarVM> returnData = new List<SiniflarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SiniflarVM()
                    {
                        SinifId = item.SinifId,
                        SinifAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.SinifAdi.ToString()),
                        SinifAcilisTarihi = item.SinifAcilisTarihi,
                        SinifGrubu = item.SinifGrubu,
                        OkulId = item.OkulId,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SiniflarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SiniflarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SiniflariGetirOkulId
        public Result<List<SiniflarVM>> SiniflariGetirOkulId(Guid OkulId)
        {
            var data = _unitOfWork.siniflarRepository.GetAll(u => u.OkulId == OkulId, includeProperties: "Kullanici,Subeler,Ogrenciler").OrderByDescending(s => s.SinifAdi).ToList();
            if (data != null)
            {
                List<SiniflarVM> returnData = new List<SiniflarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SiniflarVM()
                    {
                        SinifId = item.SinifId,
                        SinifAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.SinifAdi.ToString()),
                        SinifGrubu = item.SinifGrubu,
                        SinifAcilisTarihi = item.SinifAcilisTarihi,
                        OkulId = item.OkulId,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SiniflarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SiniflarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SinifGetirKullaniciId
        public Result<List<SiniflarVM>> SinifGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.siniflarRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,Subeler,Ogrenciler").ToList();
            if (data != null)
            {
                List<SiniflarVM> returnData = new List<SiniflarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SiniflarVM()
                    {
                        SinifId = item.SinifId,
                        SinifAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.SinifAdi.ToString()),
                        SinifGrubu = item.SinifGrubu,
                        SinifAcilisTarihi = item.SinifAcilisTarihi,
                        OkulId = item.OkulId,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SiniflarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SiniflarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SinifGetir(Guid id)
        public Result<SiniflarVM> SinifGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.siniflarRepository.GetFirstOrDefault(s => s.SinifId == id, includeProperties: "Kullanici,Subeler,Ogrenciler");
                if (data != null)
                {
                    SiniflarVM sinif = new SiniflarVM();
                    sinif.SinifId = data.SinifId;
                    sinif.SinifAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.SinifAdi.ToString());
                    sinif.SinifGrubu = data.SinifGrubu;
                    sinif.OkulId = data.OkulId;
                    sinif.KayitTarihi = data.KayitTarihi;
                    sinif.SinifAcilisTarihi = data.SinifAcilisTarihi;
                    sinif.KaydedenId = data.KaydedenId;
                    sinif.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    return new Result<SiniflarVM>(true, ResultConstant.RecordFound, sinif);
                }
                else
                {
                    return new Result<SiniflarVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<SiniflarVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region SinifEkle
        public Result<SiniflarVM> SinifEkle(SiniflarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var siniflar = new Siniflar()
                    {
                        SinifAdi = model.SinifAdi.ToLower(),
                        SinifAcilisTarihi = model.SinifAcilisTarihi,
                        SinifGrubu = model.SinifGrubu,
                        OkulId = model.OkulId,
                        KayitTarihi = model.KayitTarihi,
                        KaydedenId = user.LoginId
                    };

                    _unitOfWork.siniflarRepository.Add(siniflar);
                    _unitOfWork.Save();
                    return new Result<SiniflarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SiniflarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SiniflarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SinifGuncelle
        public Result<SiniflarVM> SinifGuncelle(SiniflarVM model, SessionContext user)
        {
            if (model.SinifId != null)
            {
                var data = _unitOfWork.siniflarRepository.Get(model.SinifId);
                if (data != null)
                {
                    data.SinifAdi = model.SinifAdi.ToLower();
                    data.SinifAcilisTarihi = model.SinifAcilisTarihi;
                    data.SinifGrubu = model.SinifGrubu;
                    data.OkulId = model.OkulId;
                    data.KayitTarihi = model.KayitTarihi;
                    data.KaydedenId = user.LoginId;

                    _unitOfWork.siniflarRepository.Update(data);
                    _unitOfWork.Save();
                    return new Result<SiniflarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                else
                {
                    return new Result<SiniflarVM>(false, "Lütfen kayıt seçiniz");
                }

            }
            else
            {
                return new Result<SiniflarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SinifSil
        public Result<bool> SinifSil(Guid id)
        {
            var data = _unitOfWork.siniflarRepository.GetFirstOrDefault(s => s.SinifId == id, includeProperties: "Kullanici");
            if (data != null)
            {

                var ogrenciler = _unitOfWork.ogrencilerRepository.GetAll(o => o.SinifId == data.SinifId).ToList();

                if (ogrenciler.Count() > 0)
                {
                    foreach (var item in ogrenciler)
                    {
                        var ogrenci = _unitOfWork.ogrencilerRepository.GetFirstOrDefault(o => o.OgrencilerId == item.OgrencilerId);
                        if (data != null)
                        {
                            _unitOfWork.ogrencilerRepository.Remove(ogrenci);
                            _unitOfWork.Save();
                        }
                    }
                }

                if (data.Subeler != null)
                {
                    foreach (var item in data.Subeler.ToList())
                    {
                        var subeler = _unitOfWork.subelerRepository.GetFirstOrDefault(c => c.SubeId == item.SubeId);

                        if (subeler != null)
                        {
                            _unitOfWork.subelerRepository.Remove(subeler);
                            _unitOfWork.Save();

                        }
                    }
                }

                _unitOfWork.siniflarRepository.Remove(data);
                _unitOfWork.Save();

                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region SinifAdGetir(Guid id)
        public Result<string> SinifAdGetir(Guid id)
        {
            if (id == null)
            {
                var sinifadi = "";
                return new Result<string>(true, ResultConstant.RecordFound, sinifadi);
            }
            else
            {
                var data = _unitOfWork.siniflarRepository.Get(id);
                if (data != null)
                {
                    var sinifadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.SinifAdi.ToString());
                    return new Result<string>(true, ResultConstant.RecordFound, sinifadi);
                }
                else
                {
                    return new Result<string>(false, ResultConstant.RecordNotFound);
                }
            }
        } 
        #endregion

    }
}
