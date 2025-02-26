using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.Exceptions;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Implementaion
{
    public class DerecelerBE : IDerecelerBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Donusturuculer
        public DerecelerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region DereceleriGetir
        public Result<List<SoruDerecelerVM>> DereceleriGetir()
        {
            try
            {
                // Veritabanından verileri çek
                var data = _unitOfWork.soruDerecelerRepository.GetAll(includeProperties: "Kullanici").ToList();

                // Veri yoksa hata fırlat
                if (data == null || !data.Any())
                {
                    return new Result<List<SoruDerecelerVM>>(false, ResultConstant.RecordNotFound);
                }

                // Mapping işlemi
                var dereceler = _mapper.Map<List<SoruDereceler>, List<SoruDerecelerVM>>(data);

                // Mapping sonrası kontrol
                if (dereceler == null || !dereceler.Any())
                {
                    return new Result<List<SoruDerecelerVM>>(false, ResultConstant.RecordNotFound);
                }

                // Verileri işle ve döndür
                var returnData = data.Select(item => new SoruDerecelerVM
                {
                    DereceId = item.DereceId,
                    DereceAdi = item.DereceAdi,
                    DereceKodu=item.DereceKodu,
                    KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                    KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                }).ToList();

                return new Result<List<SoruDerecelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            catch (Exception ex)
            {
                return new Result<List<SoruDerecelerVM>>(false, $"Dereceler getirilirken bir hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region DereceGetirKullaniciId
        public Result<List<SoruDerecelerVM>> DereceGetirKullaniciId(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new YogbisValidationException("Kullanıcı ID boş olamaz.");
                }

                var data = _unitOfWork.soruDerecelerRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici").ToList();
                
                if (data == null || !data.Any())
                {
                    throw new YogbisNotFoundException($"Kullanıcı için derece kaydı bulunamadı. (KullanıcıID: {userId})");
                }

                List<SoruDerecelerVM> returnData = new List<SoruDerecelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruDerecelerVM()
                    {
                        DereceId = item.DereceId,
                        DereceAdi = item.DereceAdi,
                        DereceKodu=item.DereceKodu,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<SoruDerecelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            catch (YogbisValidationException)
            {
                throw new YogbisValidationException("Kullanıcı ID boş olamaz.");
            }
            catch (YogbisNotFoundException)
            {
                throw new YogbisNotFoundException($"Kullanıcı için derece kaydı bulunamadı. (KullanıcıID: {userId})");
            }
            catch (Exception ex)
            {
                throw new YogbisBusinessException($"Kullanıcı dereceleri getirilirken bir hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region DereceGetir(Guid id)
        public Result<SoruDerecelerVM> DereceGetir(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new YogbisValidationException("Geçersiz derece ID'si.");
                }

                var data = _unitOfWork.soruDerecelerRepository.Get(id);
                var dereceler = _mapper.Map<SoruDereceler, SoruDerecelerVM>(data);
                return new Result<SoruDerecelerVM>(true, ResultConstant.RecordFound, dereceler);
            }
            catch (YogbisValidationException)
            {
                throw new YogbisValidationException("Geçersiz derece ID'si.");
            }
            catch (YogbisNotFoundException)
            {
                throw new YogbisNotFoundException("Herhangi bir derece kaydı bulunamadı.");
            }
            catch (Exception ex)
            {
                throw new YogbisBusinessException($"Derece getirilirken bir hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region DereceAdGetir(Guid id)
        public Result<string> DereceAdGetir(Guid DereceId)
        {
            if (DereceId == null)
            {
                var dereceadi = "";
                return new Result<string>(true, ResultConstant.RecordFound, dereceadi);
            }
            else
            {
                var data = _unitOfWork.soruDerecelerRepository.Get(DereceId);
                if (data != null)
                {
                    var dereceadi = data.DereceAdi.ToString();
                    return new Result<string>(true, ResultConstant.RecordFound, dereceadi);
                }
                else
                {
                    return new Result<string>(false, ResultConstant.RecordNotFound);
                }
            }
        }
        #endregion

        #region DereceEkle
        public Result<SoruDerecelerVM> DereceEkle(SoruDerecelerVM model, SessionContext user)
        {
            try
            {
                if (model == null)
                {
                    throw new YogbisValidationException("Derece modeli boş olamaz.");
                }

                if (string.IsNullOrEmpty(model.DereceAdi))
                {
                    throw new YogbisValidationException("Derece adı boş olamaz.");
                }

                if (user == null || string.IsNullOrEmpty(user.LoginId))
                {
                    throw new YogbisValidationException("Geçersiz kullanıcı bilgisi.");
                }

                var derece = _mapper.Map<SoruDerecelerVM, SoruDereceler>(model);
                if (derece.DereceAdi == "Öğretmen")
                    derece.DereceKodu = 1;
                else
                    derece.DereceKodu = 2;
                    derece.KaydedenId = user.LoginId;
                _unitOfWork.soruDerecelerRepository.Add(derece);
                _unitOfWork.Save();

                return new Result<SoruDerecelerVM>(true, ResultConstant.RecordCreateSuccess);
            }
            catch (YogbisValidationException)
            {
                throw new YogbisValidationException("Derece modeli boş olamaz.");
            }
            catch (Exception ex)
            {
                throw new YogbisBusinessException($"Derece eklenirken bir hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region DereceGuncelle
        public Result<SoruDerecelerVM> DereceGuncelle(SoruDerecelerVM model, SessionContext user)
        {
            try
            {
                if (model == null)
                {
                    throw new YogbisValidationException("Derece modeli boş olamaz.");
                }

                if (string.IsNullOrEmpty(model.DereceAdi))
                {
                    throw new YogbisValidationException("Derece adı boş olamaz.");
                }

                if (user == null || string.IsNullOrEmpty(user.LoginId))
                {
                    throw new YogbisValidationException("Geçersiz kullanıcı bilgisi.");
                }

                var derece = _mapper.Map<SoruDerecelerVM, SoruDereceler>(model);
                if (derece.DereceAdi == "Öğretmen")
                    derece.DereceKodu = 1;
                else
                    derece.DereceKodu = 2;
                derece.KaydedenId = user.LoginId;
                _unitOfWork.soruDerecelerRepository.Update(derece);
                _unitOfWork.Save();

                return new Result<SoruDerecelerVM>(true, ResultConstant.RecordCreateSuccess);
            }
            catch (YogbisValidationException)
            {
                throw new YogbisValidationException("Derece modeli boş olamaz.");
            }
            catch (Exception ex)
            {
                throw new YogbisBusinessException($"Derece güncellenirken bir hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region DereceSil
        public Result<bool> DereceSil(Guid id)
        {
            var data = _unitOfWork.soruDerecelerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.soruDerecelerRepository.Remove(data);
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
