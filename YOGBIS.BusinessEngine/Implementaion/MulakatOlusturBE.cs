using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
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
    public class MulakatOlusturBE : IMulakatOlusturBE
    {

        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDerecelerBE _derecelerBE;
        #endregion

        #region Dönüştürücüler
        public MulakatOlusturBE(IUnitOfWork unitOfWork, IMapper mapper, IDerecelerBE derecelerBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _derecelerBE= derecelerBE;
        }
        #endregion

        #region MulakatlariGetir
        public Result<List<MulakatlarVM>> MulakatlariGetir()
        {
            var data = _unitOfWork.mulakatlarRepository.GetAll(u => u.Durumu == true, includeProperties: "Kullanici").OrderBy(s => s.MulakatId).ToList();
            if (data != null)
            {
                List<MulakatlarVM> returnData = new List<MulakatlarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new MulakatlarVM()
                    {
                        MulakatId=item.MulakatId,
                        OnaySayisi=item.OnaySayisi,
                        OnayTarihi=item.OnayTarihi,
                        KararSayisi=item.KararSayisi,
                        KararTarihi=item.KararTarihi,
                        YazılıSinavTarihi=item.YazılıSinavTarihi,
                        MulakatKategoriId=item.MulakatKategoriId,
                        MulakatAdi=item.MulakatAdi,
                        MulakatDonemi=item.MulakatAdi + "-" + item.BaslamaTarihi.Day.ToString() + "/" + item.BaslamaTarihi.Month.ToString() + "-" +
                        item.BitisTarihi.Day.ToString() + "/" + item.BitisTarihi.Month.ToString() + "-" + item.BitisTarihi.Year.ToString(),
                        MulakatYil = Convert.ToInt32(item.YazılıSinavTarihi.Year.ToString()),
                        BaslamaTarihi=item.BaslamaTarihi,
                        BitisTarihi=item.BitisTarihi,
                        AdaySayisi = item.AdaySayisi,
                        SorulanSoruSayisi = (int)item.SorulanSoruSayisi,
                        Durumu =item.Durumu,
                        MulakatAciklama=item.MulakatAciklama,
                        KaydedenId = item.KaydedenId != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,                                              
                        KayitTarihi = item.KayitTarihi
                    });
                }
                return new Result<List<MulakatlarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<MulakatlarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion                

        #region MulakatGetir
        public Result<MulakatlarVM> MulakatGetir(Guid id)
        {
            var data = _unitOfWork.mulakatlarRepository.Get(id);
            if (data != null)
            {
                var mulakatlar = _mapper.Map<Mulakatlar, MulakatlarVM>(data);
                return new Result<MulakatlarVM>(true, ResultConstant.RecordFound, mulakatlar);
            }
            else
            {
                return new Result<MulakatlarVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region MulakatEkle
        public Result<MulakatlarVM> MulakatEkle(MulakatlarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var mulakatlar = _mapper.Map<MulakatlarVM, Mulakatlar>(model);
                    mulakatlar.KaydedenId = user.LoginId;
                    
                    _unitOfWork.mulakatlarRepository.Add(mulakatlar);
                    _unitOfWork.Save();
                    return new Result<MulakatlarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<MulakatlarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<MulakatlarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region MulakatGuncelle
        public Result<MulakatlarVM> MulakatGuncelle(MulakatlarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var mulakatlar = _mapper.Map<MulakatlarVM, Mulakatlar>(model);
                    mulakatlar.KaydedenId = user.LoginId;

                    _unitOfWork.mulakatlarRepository.Update(mulakatlar);
                    _unitOfWork.Save();
                    return new Result<MulakatlarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<MulakatlarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<MulakatlarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region MulakatSil
        public Result<bool> MulakatSil(Guid id)
        {
            var data = _unitOfWork.mulakatlarRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.mulakatlarRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region MulakatlariGetir
        //public Result<List<MulakatlarVM>> MulakatAdGetirDereceId(Guid dereceId)
        //{
        //    var data = _unitOfWork.mulakatlarRepository.GetAll(u=>u.DereceId==dereceId, includeProperties: "Kullanici,Mulakatlar").OrderByDescending(s => s.MulakatKategoriId).ToList();
        //    if (data != null)
        //    {
        //        List<MulakatlarVM> returnData = new List<MulakatlarVM>();

        //        foreach (var item in data)
        //        {
        //            returnData.Add(new MulakatlarVM()
        //            {
        //                MulakatId = item.MulakatId,
        //                OnaySayisi = item.OnaySayisi,
        //                OnayTarihi = item.OnayTarihi,
        //                KararSayisi = item.KararSayisi,
        //                KararTarihi = item.KararTarihi,
        //                YazılıSinavTarihi = item.YazılıSinavTarihi,
        //                MulakatKategoriId = item.MulakatKategoriId,
        //                MulakatAdi = item.MulakatAdi,
        //                MulakatDonemi = item.MulakatAdi + "-" + item.Dereceler.DereceAdi + "-" + item.BaslamaTarihi.Day.ToString() + "/" + item.BaslamaTarihi.Month.ToString() + "-" +
        //                item.BitisTarihi.Day.ToString() + "/" + item.BitisTarihi.Month.ToString() + "-" + item.BitisTarihi.Year.ToString(),
        //                DereceId = item.Dereceler.DereceId,
        //                DereceAdi = item.Dereceler.DereceAdi,
        //                BaslamaTarihi = item.BaslamaTarihi,
        //                BitisTarihi = item.BitisTarihi,
        //                AdaySayisi = item.AdaySayisi,
        //                SorulanSoruSayisi = (int)item.SorulanSoruSayisi,
        //                Durumu = item.Durumu,
        //                MulakatAciklama = item.MulakatAciklama,
        //                KaydedenId = item.KaydedenId != null ? item.KaydedenId : string.Empty,
        //                KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
        //                KayitTarihi = item.KayitTarihi
        //            });
        //        }
        //        return new Result<List<MulakatlarVM>>(true, ResultConstant.RecordFound, returnData);
        //    }
        //    else
        //    {
        //        return new Result<List<MulakatlarVM>>(false, ResultConstant.RecordNotFound);
        //    }
        //}
        #endregion

        #region MulakatDonemAdGetir(Guid id)
        public Result<string> MulakatDonemAdGetir(Guid id)
        {
            var data = _unitOfWork.mulakatlarRepository.Get(id);
            if (data != null)
            {
                var donemadi = data.MulakatAdi + "-" + data.BaslamaTarihi.Day.ToString() + "/" + data.BaslamaTarihi.Month.ToString() + "-" +
                data.BitisTarihi.Day.ToString() + "/" + data.BitisTarihi.Month.ToString() + "-" + data.BitisTarihi.Year.ToString();


                return new Result<string>(true, ResultConstant.RecordFound, donemadi);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region MulakatOnayGetir(id)
        public Result<string> MulakatOnayGetir(Guid id)
        {
            var data = _unitOfWork.mulakatlarRepository.Get(id);
            if (data != null)
            {
                var mulakatonaysayi = data.OnaySayisi;


                return new Result<string>(true, ResultConstant.RecordFound, mulakatonaysayi);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region MulakatYilGetir(id)
        public Result<string> MulakatYilGetir(Guid id)
        {
            var data = _unitOfWork.mulakatlarRepository.Get(id);
            if (data != null)
            {
                var mulakatyil = data.YazılıSinavTarihi.Year.ToString();


                return new Result<string>(true, ResultConstant.RecordFound, mulakatyil);
            }
            else
            {
                return new Result<string>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region MulakatlariGetirViewComponent
        public Result<List<MulakatlarVM>> MulakatlariGetirViewComponent()
        {
            var data = _unitOfWork.mulakatlarRepository.GetAll(includeProperties: "Kullanici").OrderBy(s => s.MulakatId).ToList();
            if (data != null)
            {
                List<MulakatlarVM> returnData = new List<MulakatlarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new MulakatlarVM()
                    {
                        MulakatId = item.MulakatId,
                        OnaySayisi = item.OnaySayisi,
                        OnayTarihi = item.OnayTarihi,
                        KararSayisi = item.KararSayisi,
                        KararTarihi = item.KararTarihi,
                        YazılıSinavTarihi = item.YazılıSinavTarihi,
                        MulakatKategoriId = item.MulakatKategoriId,
                        MulakatAdi = item.MulakatAdi,
                        MulakatDonemi = item.MulakatAdi + "-" + item.BaslamaTarihi.Day.ToString() + "/" + item.BaslamaTarihi.Month.ToString() + "-" +
                        item.BitisTarihi.Day.ToString() + "/" + item.BitisTarihi.Month.ToString() + "-" + item.BitisTarihi.Year.ToString(),
                        MulakatYil = Convert.ToInt32(item.YazılıSinavTarihi.Year.ToString()),
                        BaslamaTarihi = item.BaslamaTarihi,
                        BitisTarihi = item.BitisTarihi,
                        AdaySayisi = item.AdaySayisi,
                        SorulanSoruSayisi = (int)item.SorulanSoruSayisi,
                        Durumu = item.Durumu,
                        MulakatAciklama = item.MulakatAciklama,
                        KaydedenId = item.KaydedenId != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                        KayitTarihi = item.KayitTarihi
                    });
                }
                return new Result<List<MulakatlarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<MulakatlarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion                
    }
}
