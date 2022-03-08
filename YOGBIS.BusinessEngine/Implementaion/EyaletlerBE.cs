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
    public class EyaletlerBE : IEyaletlerBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Donusturuculer
        public EyaletlerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region EyaletleriGetir
        public Result<List<EyaletlerVM>> EyaletleriGetir()
        {

            var data = _unitOfWork.eyaletlerRepository.GetAll(includeProperties: "Kullanici,Ulkeler").ToList();
            var eyaletler = _mapper.Map<List<Eyaletler>, List<EyaletlerVM>>(data);

            if (data != null)
            {
                List<EyaletlerVM> returnData = new List<EyaletlerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EyaletlerVM()
                    {
                        EyaletId=item.EyaletId,
                        EyaletAdi= CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.EyaletAdi.ToString()),
                        EyaletAciklama=item.EyaletAciklama,                       
                        UlkeId=item.UlkeId,
                        //UlkeAdi=item.Ulkeler.UlkeAdi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<EyaletlerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<EyaletlerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region EyaletGetirKullaniciId
        public Result<List<EyaletlerVM>> EyaletGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.eyaletlerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici,Ulkeler").ToList();
            if (data != null)
            {
                List<EyaletlerVM> returnData = new List<EyaletlerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new EyaletlerVM()
                    {
                        EyaletId = item.EyaletId,
                        EyaletAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.EyaletAdi.ToString()),
                        EyaletAciklama = item.EyaletAciklama,
                        //EyaletVatandas = item.EyaletVatandas,
                        UlkeId = item.UlkeId,
                        //UlkeAdi = item.Ulkeler.UlkeAdi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<EyaletlerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<EyaletlerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region EyaletGetir(Guid id)
        public Result<EyaletlerVM> EyaletGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.eyaletlerRepository.GetFirstOrDefault(e => e.EyaletId == id, includeProperties:"Ulkeler,Kullanici");
                if (data != null)
                {
                    EyaletlerVM eyalet = new EyaletlerVM();
                    eyalet.EyaletAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.EyaletAdi.ToString());
                    eyalet.EyaletAciklama = data.EyaletAciklama;
                    eyalet.UlkeId = data.UlkeId;
                    //eyalet.UlkeAdi = data.Ulkeler.UlkeAdi;
                    eyalet.KayitTarihi = data.KayitTarihi;
                    eyalet.KaydedenId = data.KaydedenId;
                    eyalet.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    if (eyalet.Sehirler != null)
                    {
                        eyalet.Sehirler = data.Sehirlers.Select(s => new SehirlerVM()
                        {
                            SehirId = s.SehirId,
                            SehirAdi = s.SehirAdi

                        }).ToList();
                    }

                    return new Result<EyaletlerVM>(true, ResultConstant.RecordFound, eyalet);
                }
                else
                {
                    return new Result<EyaletlerVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<EyaletlerVM>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion

        #region EyaletEkle
        public Result<EyaletlerVM> EyaletEkle(EyaletlerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var eyaletler = new Eyaletler()
                    {
                        EyaletAdi = model.EyaletAdi.ToLower(),
                        KaydedenId = user.LoginId,
                        EyaletAciklama = model.EyaletAciklama,
                        KayitTarihi = model.KayitTarihi,
                        UlkeId = model.UlkeId,
                    };

                    _unitOfWork.eyaletlerRepository.Add(eyaletler);
                    _unitOfWork.Save();
                    return new Result<EyaletlerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<EyaletlerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<EyaletlerVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region EyaletGuncelle
        public Result<EyaletlerVM> EyaletGuncelle(EyaletlerVM model, SessionContext user)
        {
            if (model.EyaletId != null)
            {
                var data = _unitOfWork.eyaletlerRepository.Get(model.EyaletId);
                if (data != null)
                {
                    data.EyaletAdi = model.EyaletAdi.ToLower();
                    data.KaydedenId = user.LoginId;
                    data.EyaletAciklama = model.EyaletAciklama;
                    data.UlkeId = model.UlkeId;
                    data.KayitTarihi = model.KayitTarihi;

                    _unitOfWork.eyaletlerRepository.Update(data);
                    _unitOfWork.Save();
                    return new Result<EyaletlerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                else
                {
                    return new Result<EyaletlerVM>(false, "Lütfen kayıt seçiniz");
                }
            }
            else
            {
                return new Result<EyaletlerVM>(false, "Lütfen kayıt seçiniz");
            }
        }
        #endregion

        #region EyaletSil
        public Result<bool> EyaletSil(Guid id)
        {
            var data = _unitOfWork.eyaletlerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.eyaletlerRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region EyaletIdGetir
        public Result<Guid> EyaletIdGetir(string eyaletAdi)
        {
            if (eyaletAdi == null)
            {
                return new Result<Guid>(false, ResultConstant.RecordFound);
            }
            else
            {
                var data = _unitOfWork.eyaletlerRepository.GetFirstOrDefault(x => x.EyaletAdi == eyaletAdi);

                if (data != null)
                {

                    var eyaletId = data.EyaletId;


                    return new Result<Guid>(true, ResultConstant.RecordFound, eyaletId);
                }
                else
                {
                    return new Result<Guid>(false, ResultConstant.RecordNotFound);
                }
            }
        }
        #endregion

        #region EyaletGetirEyaletAdi(string eyaletAdi)
        public Result<EyaletlerVM> EyaletGetirEyaletAdi(string eyaletAdi)
        {
            if (eyaletAdi != null)
            {
                var EyaletId = EyaletIdGetir(eyaletAdi).Data;

                var data = _unitOfWork.eyaletlerRepository.GetFirstOrDefault(e => e.EyaletId == EyaletId, includeProperties: "Ulkeler,Kullanici");
                if (data != null)
                {
                    EyaletlerVM eyalet = new EyaletlerVM();
                    eyalet.EyaletAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.EyaletAdi.ToString());
                    eyalet.EyaletAciklama = data.EyaletAciklama;
                    eyalet.UlkeId = data.UlkeId;
                    //eyalet.UlkeAdi = data.Ulkeler.UlkeAdi;
                    eyalet.KayitTarihi = data.KayitTarihi;
                    eyalet.KaydedenId = data.KaydedenId;
                    eyalet.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    if (eyalet.Sehirler != null)
                    {
                        eyalet.Sehirler = data.Sehirlers.Select(s => new SehirlerVM()
                        {
                            SehirId = s.SehirId,
                            SehirAdi = s.SehirAdi

                        }).ToList();
                    }


                    return new Result<EyaletlerVM>(true, ResultConstant.RecordFound, eyalet);
                }
                else
                {
                    return new Result<EyaletlerVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<EyaletlerVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region EyaletAdGetir(Guid id)
        public Result<string> EyaletAdGetir(Guid id)
        {            

            if (id==null)
            {
                var eyaletadi = "";
                return new Result<string>(true, ResultConstant.RecordFound, eyaletadi);
            }
            else
            {
                var data = _unitOfWork.eyaletlerRepository.Get(id);
                if (data != null)
                {
                    var eyaletadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.EyaletAdi.ToString());
                    return new Result<string>(true, ResultConstant.RecordFound, eyaletadi);
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
