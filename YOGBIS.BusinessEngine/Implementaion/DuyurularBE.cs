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
    public class DuyurularBE : IDuyurularBE
    {
        
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Dönüştürücüler
        public DuyurularBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region DuyurulariGetir
        public Result<List<DuyurularVM>> DuyurulariGetir()
        {
            var data = _unitOfWork.duyurularRepository.GetAll(includeProperties: "Kullanici").ToList().OrderByDescending(d=>d.KayitTarihi);

            if (data != null)
            {
                List<DuyurularVM> returnData = new List<DuyurularVM>();

                foreach (var item in data)
                {
                    returnData.Add(new DuyurularVM()
                    {
                        DuyurularId = (Guid)item.DuyurularId,
                        DuyuruBaslık=item.DuyuruBaslık,
                        DuyuruAltBaslık = item.DuyuruAltBaslık,
                        DuyuruDetay=item.DuyuruDetay,
                        DuyuruKapakResimUrl=item.DuyuruKapakResimUrl,
                        DuyuruLink=item.DuyuruLink,                       
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty
                    });
                }
                return new Result<List<DuyurularVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<DuyurularVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DuyuruGetirKullaniciId
        public Result<List<DuyurularVM>> DuyuruGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.duyurularRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici").ToList().OrderBy(d=>d.KayitTarihi);
            if (data != null)
            {
                List<DuyurularVM> returnData = new List<DuyurularVM>();

                foreach (var item in data)
                {
                    returnData.Add(new DuyurularVM()
                    {
                        DuyurularId = (Guid)item.DuyurularId,
                        DuyuruBaslık = item.DuyuruBaslık,
                        DuyuruAltBaslık = item.DuyuruAltBaslık,
                        DuyuruDetay = item.DuyuruDetay,
                        DuyuruKapakResimUrl = item.DuyuruKapakResimUrl,
                        DuyuruLink = item.DuyuruLink,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty

                    });
                }
                return new Result<List<DuyurularVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<DuyurularVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DuyuruGetir(Guid id)
        public Result<DuyurularVM> DuyuruGetir(Guid id)
        {
            if (id != null)
            {
                var data = _unitOfWork.duyurularRepository.GetFirstOrDefault(u => u.DuyurularId == id, includeProperties: "Kullanici,FotoGaleri,DosyaGaleri");

                if (data != null)
                {
                    DuyurularVM duyuru = new DuyurularVM();
                    duyuru.DuyurularId = data.DuyurularId;
                    duyuru.DuyuruBaslık = data.DuyuruBaslık;
                    duyuru.DuyuruAltBaslık = data.DuyuruAltBaslık;
                    duyuru.DuyuruLink = data.DuyuruLink;
                    duyuru.DuyuruKapakResimUrl = data.DuyuruKapakResimUrl;
                    duyuru.KayitTarihi = data.KayitTarihi;
                    duyuru.KaydedenId = data.Kullanici.Id;
                    duyuru.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;

                    duyuru.FotoGaleri = data.FotoGaleri.Select(f => new FotoGaleriVM()
                    {
                        FotoGaleriId = f.FotoGaleriId,
                        FotoAdi = f.FotoAdi,
                        FotoURL=f.FotoURL,
                        KayitTarihi=f.KayitTarihi,
                        KaydedenId=f.KaydedenId

                    }).ToList();

                    duyuru.DosyaGaleri = data.DosyaGaleri.Select(d => new DosyaGaleriVM()
                    {
                        DosyaGaleriId = d.DosyaGaleriId,
                        DosyaAdi = d.DosyaAdi,
                        DosyaURL = d.DosyaURL,
                        KayitTarihi = d.KayitTarihi,
                        KaydedenId = d.KaydedenId

                    }).ToList();

                    return new Result<DuyurularVM>(true, ResultConstant.RecordFound, duyuru);
                }
                else
                {
                    return new Result<DuyurularVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else
            {
                return new Result<DuyurularVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region DuyuruEkle
        public Result<DuyurularVM> DuyuruEkle(DuyurularVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var duyuru = new Duyurular()
                    {
                        DuyuruBaslık=model.DuyuruBaslık,
                        DuyuruAltBaslık=model.DuyuruAltBaslık,
                        DuyuruDetay=model.DuyuruDetay,
                        DuyuruLink=model.DuyuruLink,
                        DuyuruKapakResimUrl=model.DuyuruKapakResimUrl,
                        KayitTarihi = model.KayitTarihi,
                        KaydedenId = user.LoginId
                        
                    };

                    duyuru.FotoGaleri = new List<FotoGaleri>();
                    if (model.FotoGaleri != null)
                    {
                        foreach (var file in model.FotoGaleri)
                        {
                            duyuru.FotoGaleri.Add(new FotoGaleri()
                            {
                                FotoAdi = file.FotoAdi,
                                FotoURL = file.FotoURL,
                                KaydedenId=user.LoginId,
                                KayitTarihi=file.KayitTarihi
                            });
                        }
                    }

                    duyuru.DosyaGaleri = new List<DosyaGaleri>();
                    if (model.DosyaGaleri != null)
                    {
                        foreach (var file in model.DosyaGaleri)
                        {
                            duyuru.DosyaGaleri.Add(new DosyaGaleri()
                            {
                                DosyaAdi=file.DosyaAdi,
                                DosyaURL=file.DosyaURL,
                                KaydedenId=user.LoginId,
                                KayitTarihi=file.KayitTarihi
                                
                            });
                        }
                    }

                    _unitOfWork.duyurularRepository.Add(duyuru);
                    _unitOfWork.Save();
                    return new Result<DuyurularVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<DuyurularVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<DuyurularVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region DuyuruGuncelle
        public Result<DuyurularVM> DuyuruGuncelle(DuyurularVM model, SessionContext user)
        {
            if (model.DuyurularId != null)
            {
                try
                {

                    var data = _unitOfWork.duyurularRepository.Get(model.DuyurularId);
                    if (data != null)
                    {
                        data.DuyuruBaslık = model.DuyuruBaslık;
                        data.DuyuruAltBaslık = model.DuyuruAltBaslık;
                        data.DuyuruDetay = model.DuyuruDetay;
                        data.DuyuruLink = model.DuyuruLink;
                        data.DuyuruKapakResimUrl = model.DuyuruKapakResimUrl;
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

                        _unitOfWork.duyurularRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<DuyurularVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else
                    {
                        return new Result<DuyurularVM>(false, "Lütfen kayıt seçiniz");
                    }

                }
                catch (Exception ex)
                {

                    return new Result<DuyurularVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<DuyurularVM>(false, "Lütfen kayıt seçiniz");
            }
        }
        #endregion

        #region DuyuruSil
        public Result<bool> DuyuruSil(Guid id)
        {
            var data = _unitOfWork.duyurularRepository.GetFirstOrDefault(e=>e.DuyurularId==id, includeProperties:"FotoGaleri,DosyaGaleri");
            if (data != null)
            {
                _unitOfWork.duyurularRepository.Remove(data);
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

        #region DuyuruKapakURLGetir(id)
        public Result<string> DuyuruKapakURLGetir(Guid id)
        {

            var data = _unitOfWork.duyurularRepository.Get(id);
            if (data != null)
            {
                var duyuruKapakURL = data.DuyuruKapakResimUrl;

                return new Result<string>(true, ResultConstant.RecordFound, duyuruKapakURL);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion
    }
}
