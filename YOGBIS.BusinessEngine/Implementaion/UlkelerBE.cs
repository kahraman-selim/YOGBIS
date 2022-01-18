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
            //var data = _unitOfWork.ulkelerRepository.GetAll(includeProperties: "Kullanici,Kitalar").OrderBy(u => u.UlkeAdi).ToList();
            //var ulkeler = _mapper.Map<List<Ulkeler>, List<UlkelerVM>>(data);
            //return new Result<List<UlkelerVM>>(true, ResultConstant.RecordFound, ulkeler);

            var data = _unitOfWork.ulkelerRepository.GetAll(includeProperties: "Kitalar,Kullanici").OrderBy(u => u.UlkeAdi).ToList();

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
                        KitaAdi = item.Kitalar.KitaAdi,
                        //VatandasSayisi=item.VatandasSayisi,
                        //UlkeGrupId = item.Kitalar.UlkeGruplariKitalars.Where(x=>x.KitaId==item.KitaId),
                        //UlkeGrupAdi = item.UlkeGruplari.UlkeGrupAdi,
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

        #region UlkeEkle
        public Result<UlkelerVM> UlkeEkle(UlkelerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var ulkeler = _mapper.Map<UlkelerVM, Ulkeler>(model);
                    
                    ulkeler.KitaId = model.KitaId;
                    ulkeler.KaydedenId = user.LoginId;
                    ulkeler.UlkeAdi = model.UlkeAdi;
                    ulkeler.UlkeAciklama = model.UlkeAciklama;
                    ulkeler.UlkeBayrakURL = model.UlkeBayrakURL;
                    //ulkeler.VatandasSayisi = 0;

                    _unitOfWork.ulkelerRepository.Add(ulkeler);
                    _unitOfWork.Save();
                    return new Result<UlkelerVM>(true, ResultConstant.RecordCreateSuccess,model);
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
        public Result<UlkelerVM> UlkeGetir(int id)
        {
            //var data = _unitOfWork.ulkelerRepository.Get(id);
            //if (data != null)
            //{
            //    var ulkeler = _mapper.Map<Ulkeler, UlkelerVM>(data);
            //    return new Result<UlkelerVM>(true, ResultConstant.RecordFound, ulkeler);
            //}
            //else
            //{
            //    return new Result<UlkelerVM>(false, ResultConstant.RecordNotFound);
            //}
            if (id > 0)
            {
                var data = _unitOfWork.ulkelerRepository.GetFirstOrDefault(u => u.UlkeId == id, includeProperties: "Kitalar,Kullanici");
                if (data != null)
                {
                    UlkelerVM ulke = new UlkelerVM();
                    ulke.UlkeId = data.UlkeId;
                    ulke.UlkeAdi = data.UlkeAdi;
                    ulke.UlkeAciklama = data.UlkeAciklama;
                    ulke.UlkeBayrakURL = data.UlkeBayrakURL;
                    //ulke.VatandasSayisi = data.VatandasSayisi;
                    ulke.KitaId = data.KitaId;
                    ulke.KitaAdi = data.Kitalar.KitaAdi;
                    ulke.KaydedenId = data.KaydedenId;
                    ulke.KaydedenAdi = data.Kullanici != null ? data.Kullanici.Ad + " " + data.Kullanici.Soyad : string.Empty;


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
            if (model.UlkeId>0)
            {
                var data = _unitOfWork.ulkelerRepository.Get(model.UlkeId);
                if (data!=null)
                {
                    data.UlkeAdi = model.UlkeAdi;
                    data.UlkeAciklama = model.UlkeAciklama;
                    data.KitaId = model.KitaId;
                    data.UlkeBayrakURL = model.UlkeBayrakURL;
                    data.KaydedenId = user.LoginId;
                    //data.VatandasSayisi = model.VatandasSayisi;

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
        public Result<bool> UlkeSil(int id)
        {
            var data = _unitOfWork.ulkelerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.ulkelerRepository.Remove(data);
                _unitOfWork.Save();
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
                        UlkeBayrakURL=item.UlkeBayrakURL,
                        UlkeAciklama = item.UlkeAciklama,
                        KayitTarihi = item.KayitTarihi,
                        KitaId = item.KitaId,
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
    }
}
