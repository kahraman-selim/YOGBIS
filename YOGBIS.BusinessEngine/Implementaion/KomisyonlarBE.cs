using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

namespace YOGBIS.BusinessEngine.Implementaion
{
    public class KomisyonlarBE : IKomisyonlarBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<KomisyonlarBE> _logger;
        private readonly IKullaniciBE _kullaniciBE; 
        #endregion

        #region Dönüştürücüler
        public KomisyonlarBE(IUnitOfWork unitOfWork, IMapper mapper, ILogger<KomisyonlarBE> logger,IKullaniciBE kullaniciBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _kullaniciBE = kullaniciBE;
        } 
        #endregion

        #region KomisyonlariGetir
        public Result<List<KomisyonlarVM>> KomisyonlariGetir(string mulakatId = null)
        {
            try
            {
                _logger.LogInformation($"Komisyonlar getiriliyor. MulakatId: {mulakatId ?? "Tümü"}");

                var query = _unitOfWork.komisyonlarRepository.GetAll();

                if (!string.IsNullOrEmpty(mulakatId))
                {
                    var mulakatGuid = Guid.Parse(mulakatId);
                    query = query.Where(k => k.MulakatId == mulakatGuid);
                }

                var data = query
                    .OrderBy(x => x.KomisyonSiraNo)
                    .ThenBy(y => y.KomisyonUyeSiraNo)
                    .ThenByDescending(z => z.KomisyonGorevDurum)
                    .ToList();

                if (data != null && data.Any())
                {
                    List<KomisyonlarVM> returnData = new List<KomisyonlarVM>();

                    foreach (var item in data)
                    {
                        returnData.Add(new KomisyonlarVM()
                        {
                            KomisyonId = item.KomisyonId,
                            KomisyonAdi = item.KomisyonAdi,
                            KomisyonSiraNo = item.KomisyonSiraNo,
                            KomisyonUyeAdiSoyadi = item.KomisyonUyeAdiSoyadi,
                            KomisyonUyeDurum = item.KomisyonUyeDurum,
                            KomisyonGorevDurum = item.KomisyonGorevDurum,
                            KomisyonUyeGorevi = item.KomisyonUyeGorevi,
                            KomisyonUyeGorevYeri = item.KomisyonUyeGorevYeri,
                            KomisyonUyeSiraNo = item.KomisyonUyeSiraNo,
                            KomisyoUyeStatu = item.KomisyoUyeStatu,
                            KomisyonUlkeGrubu = item.KomisyonUlkeGrubu,
                            KomisyoUyeTel = item.KomisyoUyeTel,
                            KomisyonUyeEPosta = item.KomisyonUyeEPosta,
                            KomisyonGorevBaslamaTarihi = item.KomisyonGorevBaslamaTarihi,
                            KomisyonGorevBitisTarihi = item.KomisyonGorevBitisTarihi,
                            KayitTarihi = item.KayitTarihi,
                            KaydedenId = item.KaydedenId,                                                        
                            MulakatId = item.MulakatId
                        });
                    }

                    _logger.LogInformation($"Komisyonlar başarıyla getirildi. Toplam kayıt: {returnData.Count}");
                    return new Result<List<KomisyonlarVM>>(true, ResultConstant.RecordFound, returnData);
                }
                else
                {
                    _logger.LogWarning("Komisyon kaydı bulunamadı");
                    return new Result<List<KomisyonlarVM>>(false, ResultConstant.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Komisyonlar getirilirken hata oluştu: {ex.Message}");
                return new Result<List<KomisyonlarVM>>(false, "Komisyonlar getirilirken bir hata oluştu");
            }
        }
        #endregion

        #region KomisyonGetir
        public Result<KomisyonlarVM> KomisyonGetir(Guid id)
        {
            var data = _unitOfWork.komisyonlarRepository.Get(id);
            if (data != null)
            {
                var returnData = _mapper.Map<Komisyonlar, KomisyonlarVM>(data);
                return new Result<KomisyonlarVM>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<KomisyonlarVM>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region KomisyonGetirById
        public Result<KomisyonlarVM> KomisyonGetirById(Guid id)
        {
            try
            {
                var data = _unitOfWork.komisyonlarRepository.GetFirstOrDefault(k => k.KomisyonId == id);
                
                if (data != null)
                {
                    var komisyon = _mapper.Map<Komisyonlar, KomisyonlarVM>(data);
                    return new Result<KomisyonlarVM>(true, ResultConstant.RecordFound, komisyon);
                }
                
                return new Result<KomisyonlarVM>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                return new Result<KomisyonlarVM>(false, ResultConstant.RecordNotFound + " " + ex.Message);
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
                    komisyon.KayitTarihi = DateTime.Now;

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

        #region DurumDegistir
        public Result<KomisyonlarVM> DurumDegistir(Guid komisyonId, SessionContext user)
        {
            try
            {
                _logger.LogInformation($"Komisyon durumu değiştiriliyor: KomisyonId={komisyonId}");

                var mevcutKomisyon = _unitOfWork.komisyonlarRepository.Get(komisyonId);
                if (mevcutKomisyon == null)
                {
                    _logger.LogWarning($"Komisyon bulunamadı: KomisyonId={komisyonId}");
                    return new Result<KomisyonlarVM>(false, "Komisyon bulunamadı");
                }

                mevcutKomisyon.KomisyonGorevDurum = !mevcutKomisyon.KomisyonGorevDurum;

                _unitOfWork.komisyonlarRepository.Update(mevcutKomisyon);
                _unitOfWork.Save();

                _logger.LogInformation($"Komisyon durumu başarıyla değiştirildi: KomisyonId={komisyonId}, Yeni Durum={mevcutKomisyon.KomisyonGorevDurum}");
                return new Result<KomisyonlarVM>(true, "Komisyon durumu başarıyla değiştirildi");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError($"Veritabanı hatası: {dbEx.Message}, InnerException: {dbEx.InnerException?.Message}");
                return new Result<KomisyonlarVM>(false, $"Veritabanı hatası: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Durum değiştirme hatası: {ex.Message}, StackTrace: {ex.StackTrace}");
                return new Result<KomisyonlarVM>(false, $"Durum değiştirme işlemi sırasında hata oluştu: {ex.Message}");
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
            var komisyon = await _unitOfWork.komisyonlarRepository.GetAll()
                .FirstOrDefaultAsync(k => k.KomisyonSiraNo == siraNo);
            return komisyon?.KomisyonId;
        }
        #endregion

        #region KomisyonPersonelKaydet
        public Result<KomisyonPersonellerVM> KomisyonPersonelKaydet(List<KomisyonPersonellerVM> model, SessionContext user)
        {
            try
            {
                // Yeni komisyon personeli oluştur
                //personel var mı yok mu kontrolü kayıtların çoklu olmaması için
                var data = _unitOfWork.komisyonPersonellerRepository.GetAll(x=>x.PersonelId==model.FirstOrDefault().PersonelId);
                if (data != null)
                {
                    foreach (var item in data) 
                    {
                        item.KayitAktifMi = false;
                    }

                    _unitOfWork.komisyonPersonellerRepository.UpdateRange(data);
                    _unitOfWork.Save();
                }

                if (model !=null)
                {
                    var komisyonpersonelliste = new List<KomisyonPersoneller>();

                    foreach (var kayit in model) 
                    {
                        komisyonpersonelliste.Add(new KomisyonPersoneller
                        {
                            KomisyonKullaniciId = kayit.KomisyonKullaniciId,
                            PersonelId = kayit.PersonelId,
                            KayitAktifMi=true,
                            RolId = kayit.RolId,
                            KaydedenId = user.LoginId,
                        });
                    }

                    _unitOfWork.komisyonPersonellerRepository.AddRange(komisyonpersonelliste);
                }


                //_unitOfWork.komisyonPersonellerRepository.Add(komisyonPersonel);
                _unitOfWork.Save();
                
                return new Result<KomisyonPersonellerVM>(true, ResultConstant.RecordCreateSuccess);
            }
            catch (Exception ex)
            {
                return new Result<KomisyonPersonellerVM>(false, $"Kayıt sırasında hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region KomisyonPersonelleriGetir
        public Result<List<KomisyonPersonellerVM>> KomisyonPersonelleriGetir(string personelId)
        {
            try
            {
                // Her repository'den veriyi ayrı ayrı alalım ve kontrol edelim
                var komisyonPersonellerList = _unitOfWork.komisyonPersonellerRepository.GetAll(x=>x.PersonelId == Guid.Parse(personelId) && x.KayitAktifMi==true).ToList();
                if (!komisyonPersonellerList.Any())
                {
                    return new Result<List<KomisyonPersonellerVM>>(false, "Komisyon personeli bulunamadı");
                }

                var komisyonlarList = _unitOfWork.komisyonlarRepository.GetAll().ToList();
                if (!komisyonlarList.Any())
                {
                    return new Result<List<KomisyonPersonellerVM>>(false, "Komisyon bulunamadı");
                }

                var kullanicilarList = _unitOfWork.kullaniciRepository.GetAll().ToList();
                if (!kullanicilarList.Any())
                {
                    return new Result<List<KomisyonPersonellerVM>>(false, "Kullanıcı bulunamadı");
                }

                // Önce her personel için tek bir kayıt oluşturalım
                //var personelGruplari = komisyonPersonellerList
                //    .GroupBy(kp => kp.PersonelId)
                //    .Select(g => new
                //    {
                //        PersonelId = g.Key,
                //        KomisyonKullaniciIds = g.Select(x => x.KomisyonKullaniciId).ToList(),
                //        g.First().RolId,
                //        g.First().Id
                //    });

                var komisyonPersoneller = new List<KomisyonPersonellerVM>();

                foreach (var personel in komisyonPersonellerList)
                {
                    var kullanici = kullanicilarList.FirstOrDefault(u => u.Id == personel.PersonelId.ToString());
                    if (kullanici != null)
                    {
                        var komisyonlar = komisyonlarList
                            .Where(k => personel.KomisyonKullaniciId.Contains(k.KomisyonKullaniciId))
                            .Select(k => k.KomisyonAdi)
                            .ToList();

                        komisyonPersoneller.Add(new KomisyonPersonellerVM
                        {
                            Id = personel.Id,
                            PersonelId = personel.PersonelId,
                            RolId = personel.RolId,
                            PersonelAdSoyad = kullanici.Ad + " " + kullanici.Soyad,
                            KomisyonAdi = string.Join(", ", komisyonlar),
                            KomisyonKullaniciId=personel.KomisyonKullaniciId,
                        });
                    }
                }

                if (!komisyonPersoneller.Any())
                {
                    return new Result<List<KomisyonPersonellerVM>>(false, "Eşleşen kayıt bulunamadı");
                }

                return new Result<List<KomisyonPersonellerVM>>(true, ResultConstant.RecordFound, komisyonPersoneller);
            }
            catch (Exception ex)
            {
                return new Result<List<KomisyonPersonellerVM>>(false, "Hata: " + ex.Message);
            }
        }
        #endregion

        #region KomisyonAdlarıGetir
        public Result<List<KomisyonlarVM>> KomisyonAdlariGetir()
        {

            try
            {
                var data = _unitOfWork.komisyonlarRepository.GetAll()
                    .Include(x=>x.Kullanici)
                    .Include(x=>x.Mulakat)
                    .Where(x=>x.Mulakat != null && x.Mulakat.Durumu == true)
                    .OrderBy(t => t.KomisyonSiraNo)                                      
                    .ToList();

                // Veri yoksa hata fırlat
                if (data == null || !data.Any())
                {
                    return new Result<List<KomisyonlarVM>>(false, ResultConstant.RecordNotFound);
                }

                // Mapping işlemi
                var komisyonlar = _mapper.Map<List<Komisyonlar>, List<KomisyonlarVM>>(data);

                // Mapping sonrası kontrol
                if (komisyonlar == null || !komisyonlar.Any())
                {
                    return new Result<List<KomisyonlarVM>>(false, ResultConstant.RecordNotFound);
                }

                // Verileri işle ve döndür
                var returnData = data.Select(item => new KomisyonlarVM
                {
                    KomisyonId = item.KomisyonId,
                    KomisyonSiraNo = item.KomisyonSiraNo,
                    KomisyonAdi = item.KomisyonAdi,
                })
                                .GroupBy(x => new {
                                    x.KomisyonId,
                                    x.KomisyonSiraNo,
                                    x.KomisyonAdi
                                })
                                .Select(g => g.First())  // Her gruptan bir tane eleman al
                                .ToList();

                return new Result<List<KomisyonlarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            catch (Exception ex)
            {
                return new Result<List<KomisyonlarVM>>(false, $"Dereceler getirilirken bir hata oluştu: {ex.Message}");
            }
        }
        #endregion
    }
}
