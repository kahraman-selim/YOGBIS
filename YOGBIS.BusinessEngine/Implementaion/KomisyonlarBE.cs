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
    public class KomisyonlarBE : IKomisyonlarBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IKullaniciBE _kullaniciBE;
        #endregion

        #region Dönüştürücüler
        public KomisyonlarBE(IUnitOfWork unitOfWork, IMapper mapper, IKullaniciBE kullaniciBE, IMulakatOlusturBE mulakatOlusturBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mulakatOlusturBE = mulakatOlusturBE;
            _kullaniciBE = kullaniciBE;
        }
        #endregion

        #region KomisyonlariGetir
        public Result<List<KomisyonlarVM>> KomisyonlariGetir()
        {
            var data = _unitOfWork.komisyonlarRepository.GetAll(includeProperties: "Kullanici").OrderBy(x => x.KomisyonSiraNo).ThenBy(x=>x.KomisyonUyeSiraNo).ToList();

            if (data != null)
            {
                List<KomisyonlarVM> returnData = new List<KomisyonlarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new KomisyonlarVM()
                    {
                        KomisyonId=item.KomisyonId,
                        KomisyonKullaniciId=item.KomisyonKullaniciId,
                        KomisyonSiraNo=item.KomisyonSiraNo,
                        KomisyonAdi=item.KomisyonAdi,
                        KomisyonUyeDurum=item.KomisyonUyeDurum,
                        KomisyonUyeSiraNo=item.KomisyonUyeSiraNo,
                        KomisyonUyeGorevi=item.KomisyonUyeGorevi,
                        KomisyonUyeAdiSoyadi=item.KomisyonUyeAdiSoyadi,
                        KomisyonUyeGorevYeri=item.KomisyonUyeGorevi,
                        KomisyoUyeStatu=item.KomisyoUyeStatu,
                        KomisyonUlkeGrubu=item.KomisyonUlkeGrubu,
                        KomisyoUyeTel=item.KomisyoUyeTel,
                        KomisyonUyeEPosta=item.KomisyonUyeEPosta,
                        KomisyonGorevBaslamaTarihi=item.KomisyonGorevBaslamaTarihi,
                        KomisyonGorevBitisTarihi=item.KomisyonGorevBitisTarihi,
                        KayitTarihi=item.KayitTarihi,
                        KaydedenId = item.KaydedenId,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,

                    });
                }
                return new Result<List<KomisyonlarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<KomisyonlarVM>>(false, ResultConstant.RecordNotFound);
            }

        }
        #endregion

        #region KomisyonGetir(Guid id)
        public Result<KomisyonlarVM> KomisyonGetir(Guid id)
        {
            var data = _unitOfWork.komisyonlarRepository.Get(id);
            if (data != null)
            {
                var komisyonlar = _mapper.Map<Komisyonlar, KomisyonlarVM>(data);
                return new Result<KomisyonlarVM>(true, ResultConstant.RecordFound, komisyonlar);
            }
            else
            {
                return new Result<KomisyonlarVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region KomisyonEkle
        public Result<KomisyonlarVM> KomisyonEkle(KomisyonlarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var komisyon = _mapper.Map<KomisyonlarVM, Komisyonlar>(model);
                    komisyon.KaydedenId = user.LoginId;

                    _unitOfWork.komisyonlarRepository.Add(komisyon);
                    _unitOfWork.Save();

                    return new Result<KomisyonlarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<KomisyonlarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<KomisyonlarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region KomisyonGuncelle
        public Result<KomisyonlarVM> KomisyonGuncelle(KomisyonlarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var komisyon = _mapper.Map<KomisyonlarVM, Komisyonlar>(model);
                    komisyon.KaydedenId = user.LoginId;

                    _unitOfWork.komisyonlarRepository.Update(komisyon);
                    _unitOfWork.Save();

                    return new Result<KomisyonlarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<KomisyonlarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<KomisyonlarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region KomisyonSil
        public Result<bool> KomisyonSil(Guid id)
        {
            var data = _unitOfWork.komisyonlarRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.komisyonlarRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion
       
    }
}
