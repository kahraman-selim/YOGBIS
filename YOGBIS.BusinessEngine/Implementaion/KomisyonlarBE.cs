using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;
using Microsoft.Extensions.Logging;

namespace YOGBIS.BusinessEngine.Implementaion
{
    public class KomisyonlarBE : IKomisyonlarBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly IKullaniciBE _kullaniciBE;
        private readonly ILogger<KomisyonlarBE> _logger;
        #endregion

        #region Dönüştürücüler
        public KomisyonlarBE(IUnitOfWork unitOfWork, IMapper mapper, IKullaniciBE kullaniciBE, IMulakatOlusturBE mulakatOlusturBE, ILogger<KomisyonlarBE> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mulakatOlusturBE = mulakatOlusturBE;
            _kullaniciBE = kullaniciBE;
            _logger = logger;
        }
        #endregion        

        #region KomisyonlarıGetir
        public Result<List<KomisyonlarVM>> KomisyonlariGetir()
        {
            try
            {
                var query = _unitOfWork.komisyonlarRepository.GetAll()
                    .AsNoTracking()
                    .Include(x => x.Kullanici)
                    .OrderBy(x => x.KomisyonSiraNo)
                    .ThenBy(y => y.KomisyonUyeSiraNo)
                    .ThenBy(z=>z.KomisyonUyeDurum);

                var data = query.Select(item => new KomisyonlarVM
                {
                    KomisyonId = item.KomisyonId,
                    KomisyonKullaniciId = item.KomisyonKullaniciId,
                    KomisyonSiraNo = item.KomisyonSiraNo,
                    KomisyonAdi = item.KomisyonAdi,
                    KomisyonUyeDurum = item.KomisyonUyeDurum,
                    KomisyonGorevDurum = item.KomisyonGorevDurum,
                    KomisyonUyeSiraNo = item.KomisyonUyeSiraNo,
                    KomisyonUyeGorevi = item.KomisyonUyeGorevi,
                    KomisyonUyeAdiSoyadi = item.KomisyonUyeAdiSoyadi,
                    KomisyonUyeGorevYeri = item.KomisyonUyeGorevi,
                    KomisyoUyeStatu = item.KomisyoUyeStatu,
                    KomisyonUlkeGrubu = item.KomisyonUlkeGrubu,
                    KomisyoUyeTel = item.KomisyoUyeTel,
                    KomisyonUyeEPosta = item.KomisyonUyeEPosta,
                    KomisyonGorevBaslamaTarihi = item.KomisyonGorevBaslamaTarihi,
                    KomisyonGorevBitisTarihi = item.KomisyonGorevBitisTarihi,
                    KayitTarihi = item.KayitTarihi,
                    KaydedenId = item.KaydedenId,
                    KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,

                }).ToList();

                if (data != null)
                {
                    return new Result<List<KomisyonlarVM>>(true, ResultConstant.RecordFound, data);
                }

                return new Result<List<KomisyonlarVM>>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {

                return new Result<List<KomisyonlarVM>>(false, ex.Message);
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
                    _logger.LogInformation($"Komisyon ekleniyor: KomisyonAdi={model.KomisyonAdi}, UyeDurum={model.KomisyonUyeDurum}, GorevDurum={model.KomisyonGorevDurum}");

                    var komisyon = _mapper.Map<KomisyonlarVM, Komisyonlar>(model);
                    komisyon.KaydedenId = user.LoginId;
                   
                    _unitOfWork.komisyonlarRepository.Add(komisyon);
                    _unitOfWork.Save();

                    _logger.LogInformation($"Komisyon başarıyla eklendi: KomisyonAdi={model.KomisyonAdi}");
                    return new Result<KomisyonlarVM>(true, "Komisyon başarıyla eklendi");
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError($"Veritabanı hatası: {dbEx.Message}, InnerException: {dbEx.InnerException?.Message}");
                    return new Result<KomisyonlarVM>(false, $"Veritabanı hatası: {dbEx.InnerException?.Message ?? dbEx.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Komisyon eklenirken hata: {ex.Message}, StackTrace: {ex.StackTrace}");
                    return new Result<KomisyonlarVM>(false, $"Komisyon eklenirken hata oluştu: {ex.Message}");
                }
            }
            else
            {
                _logger.LogWarning("Komisyon eklenemedi: Model boş");
                return new Result<KomisyonlarVM>(false, "Komisyon bilgileri boş olamaz");
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

                    return new Result<KomisyonlarVM>(true, "Komisyon başarıyla güncellendi");
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError($"Veritabanı hatası: {dbEx.Message}, InnerException: {dbEx.InnerException?.Message}");
                    return new Result<KomisyonlarVM>(false, $"Veritabanı hatası: {dbEx.InnerException?.Message ?? dbEx.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Komisyon güncellenirken hata: {ex.Message}, StackTrace: {ex.StackTrace}");
                    return new Result<KomisyonlarVM>(false, $"Komisyon güncellenirken hata oluştu: {ex.Message}");
                }
            }
            else
            {
                _logger.LogWarning("Komisyon güncellenemedi: Model boş");
                return new Result<KomisyonlarVM>(false, "Komisyon bilgileri boş olamaz");
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
                return new Result<bool>(true, "Komisyon başarıyla silindi");
            }
            else
            {
                return new Result<bool>(false, "Komisyon silinirken hata oluştu");
            }
        }
        #endregion

        #region GetKomisyonIdBySiraNo
        public async Task<Guid?> GetKomisyonIdBySiraNo(int siraNo)
        {
            try
            {
                var komisyon = _unitOfWork.komisyonlarRepository.GetFirstOrDefault(x => x.KomisyonSiraNo == siraNo);
                return await Task.FromResult(komisyon?.KomisyonId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetKomisyonIdBySiraNo {ex.Message}");
                return null;
            }
        }
        #endregion
       
    }
}
