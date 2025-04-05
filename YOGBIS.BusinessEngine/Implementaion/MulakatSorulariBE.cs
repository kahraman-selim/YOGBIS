using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    public class MulakatSorulariBE : IMulakatSorulariBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDerecelerBE _derecelerBE;
        private readonly IMulakatOlusturBE _mulakatOlusturBE;
        private readonly ISoruKategorileriBE _soruKategorileriBE;
        #endregion

        #region Donustucuruler
        public MulakatSorulariBE(IUnitOfWork unitOfWork, IMapper mapper, IDerecelerBE derecelerBE, IMulakatOlusturBE mulakatOlusturBE, ISoruKategorileriBE soruKategorileriBE)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _derecelerBE = derecelerBE;
            _mulakatOlusturBE = mulakatOlusturBE;
            _soruKategorileriBE = soruKategorileriBE;
        }
        #endregion

        #region MulakatSorulariGetirEskiMetod
        //public Result<List<MulakatSorulariVM>> MulakatSorulariGetir()
        //{
        //    //1. Yöntem
        //    var data = _unitOfWork.mulakatSorulariRepository.GetAll(includeProperties: "Kullanici,SoruDereceler,SoruKategoriler,Mulakatlar").OrderBy(x=>x.DereceId).ThenBy(y=>y.SoruSiraNo).ThenBy(z=>z.SoruKategoriSiraNo).ToList(); 
        //    var mulakatSorulari = _mapper.Map<List<MulakatSorulari>, List<MulakatSorulariVM>>(data);

        //    if (data != null)
        //    {
        //        List<MulakatSorulariVM> returnData = new List<MulakatSorulariVM>();

        //        foreach (var item in data)
        //        {
        //            returnData.Add(new MulakatSorulariVM()
        //            {
        //                MulakatSorulariId = item.MulakatSorulariId,
        //                SoruSiraNo = item.SoruSiraNo,
        //                SoruNo = item.SoruNo,
        //                DereceId = item.DereceId,
        //                DereceAdi = item.SoruDereceler.DereceAdi,
        //                SoruKategorilerId = item.SoruKategorilerId,
        //                SoruKategoriSiraNo = item.SoruKategoriSiraNo,
        //                SoruKategoriAdi = item.SoruKategoriler.SoruKategorilerAdi,
        //                SoruKategoriTakmaAdi = item.SoruKategoriler.SoruKategorilerTakmaAdi,
        //                Soru = item.Soru,
        //                Cevap = item.Cevap,
        //                SinavKateogoriTurId = item.SinavKateogoriTurId,
        //                SinavKategoriTurAdi = item.SinavKategoriTurAdi,
        //                MulakatId = item.MulakatId,
        //                MulakatDonemi = item.Mulakatlar.MulakatAdi,
        //                KayitTarihi = item.KayitTarihi,
        //                KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
        //                KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
        //            });
        //        }

        //        return new Result<List<MulakatSorulariVM>>(true, ResultConstant.RecordFound, returnData);
        //    }
        //    else 
        //    {
        //        return new Result<List<MulakatSorulariVM>>(false, ResultConstant.RecordNotFound);
        //    }
        //}
        #endregion

        #region MulakatSorulariGetirYeniMetod
        public Result<List<MulakatSorulariVM>> MulakatSorulariGetir()
        {
            try
            {
                // 1. Sorguyu IQueryable olarak oluştur (henüz execute etme)
                var query = _unitOfWork.mulakatSorulariRepository.GetAll()
                    .AsNoTracking() // Change tracking'i devre dışı bırak
                    .Include(x => x.SoruDereceler)
                    .Include(x => x.SoruKategoriler)
                    .Include(x => x.Mulakatlar)
                    .Include(x => x.Kullanici)
                    .Where(x => x.Mulakatlar.Durumu == true)
                    .OrderBy(x => x.DereceId)
                    .ThenBy(y => y.SoruSiraNo)
                    .ThenBy(z => z.SoruKategoriSiraNo);

                // 2. Sadece ihtiyaç duyulan alanları seç
                var data = query.Select(item => new MulakatSorulariVM
                {
                    MulakatSorulariId = item.MulakatSorulariId,
                    SoruSiraNo = item.SoruSiraNo,
                    SoruNo = item.SoruNo,
                    DereceId = item.DereceId,
                    DereceAdi = item.SoruDereceler.DereceAdi,
                    SoruKategorilerId = item.SoruKategorilerId,
                    SoruKategoriSiraNo = item.SoruKategoriSiraNo,
                    SoruKategoriAdi = item.SoruKategoriler.SoruKategorilerAdi,
                    SoruKategoriTakmaAdi = item.SoruKategoriler.SoruKategorilerTakmaAdi,
                    Soru = item.Soru,
                    Cevap = item.Cevap,
                    SinavKateogoriTurId = item.SinavKateogoriTurId,
                    SinavKategoriTurAdi = item.SinavKategoriTurAdi,
                    Iptal=item.Iptal,
                    MulakatId = item.MulakatId,
                    MulakatDonemi = item.Mulakatlar.MulakatAdi,
                    KayitTarihi = item.KayitTarihi,
                    KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                    KaydedenAdi = item.Kullanici != null ?
                        item.Kullanici.Ad + " " + item.Kullanici.Soyad :
                        string.Empty
                }).ToList(); // Asenkron olarak çalıştır

                if (data != null)
                {
                    return new Result<List<MulakatSorulariVM>>(true, ResultConstant.RecordFound, data);
                }

                return new Result<List<MulakatSorulariVM>>(false, ResultConstant.RecordNotFound);
            }
            catch (Exception ex)
            {
                // Loglama yapılabilir
                return new Result<List<MulakatSorulariVM>>(false, ex.Message);
            }
        } 
        #endregion

        // Pagination için overload

        //public Result<List<MulakatSorulariVM>> MulakatSorulariGetir(int pageSize = 50, int pageNumber = 1)
        //{
        //    try
        //    {
        //        var query = _unitOfWork.mulakatSorulariRepository.GetAll()
        //            .AsNoTracking()
        //            .Include(x => x.SoruDereceler)
        //            .Include(x => x.SoruKategoriler)
        //            .Include(x => x.Mulakatlar)
        //            .Include(x => x.Kullanici)
        //            .OrderBy(x => x.DereceId)
        //            .ThenBy(y => y.SoruSiraNo)
        //            .ThenBy(z => z.SoruKategoriSiraNo)
        //            .Skip((pageNumber - 1) * pageSize)
        //            .Take(pageSize);

        //        var data = query.Select(item => new MulakatSorulariVM
        //        {
        //            MulakatSorulariId = item.MulakatSorulariId,
        //            SoruSiraNo = item.SoruSiraNo,
        //            SoruNo = item.SoruNo,
        //            DereceId = item.DereceId,
        //            DereceAdi = item.SoruDereceler.DereceAdi,
        //            SoruKategorilerId = item.SoruKategorilerId,
        //            SoruKategoriSiraNo = item.SoruKategoriSiraNo,
        //            SoruKategoriAdi = item.SoruKategoriler.SoruKategorilerAdi,
        //            SoruKategoriTakmaAdi = item.SoruKategoriler.SoruKategorilerTakmaAdi,
        //            Soru = item.Soru,
        //            Cevap = item.Cevap,
        //            SinavKateogoriTurId = item.SinavKateogoriTurId,
        //            SinavKategoriTurAdi = item.SinavKategoriTurAdi,
        //            MulakatId = item.MulakatId,
        //            MulakatDonemi = item.Mulakatlar.MulakatAdi,
        //            KayitTarihi = item.KayitTarihi,
        //            KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
        //            KaydedenAdi = item.Kullanici != null ?
        //                            item.Kullanici.Ad + " " + item.Kullanici.Soyad :
        //                            string.Empty
        //        }).ToList();

        //        if (data != null && data.Any())
        //        {
        //            return new Result<List<MulakatSorulariVM>>(true, ResultConstant.RecordFound, data);
        //        }

        //        return new Result<List<MulakatSorulariVM>>(false, ResultConstant.RecordNotFound);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Result<List<MulakatSorulariVM>>(false, ex.Message);
        //    }
        //}

        #region MulakatSoruGetir(Guid id)
        public Result<MulakatSorulariVM> MulakatSoruGetir(Guid id)
        {
            if (id != null) 
            {
                var data = _unitOfWork.mulakatSorulariRepository.GetFirstOrDefault(x=>x.MulakatSorulariId==id && x.Mulakatlar.Durumu==true, 
                    includeProperties: "Kullanici,SoruDereceler,SoruKategoriler,Mulakatlar");

                if (data != null) 
                {
                    MulakatSorulariVM mulakatsoru = new MulakatSorulariVM();

                    mulakatsoru.MulakatSorulariId = data.MulakatSorulariId;
                    mulakatsoru.SoruNo=data.SoruNo;
                    mulakatsoru.SoruSiraNo=data.SoruSiraNo;
                    mulakatsoru.DereceId = data.DereceId;
                    mulakatsoru.DereceAdi = data.SoruDereceler.DereceAdi;
                    mulakatsoru.SoruKategorilerId = data.SoruKategorilerId;
                    mulakatsoru.SoruKategoriSiraNo = data.SoruKategoriler.SoruKategorilerSiraNo;
                    mulakatsoru.SoruKategoriTakmaAdi = data.SoruKategoriler.SoruKategorilerTakmaAdi;
                    mulakatsoru.SoruKategoriAdi = data.SoruKategoriler.SoruKategorilerAdi;
                    mulakatsoru.Soru = data.Soru;
                    mulakatsoru.Cevap=data.Cevap;
                    mulakatsoru.SinavKateogoriTurId = data.SinavKateogoriTurId;
                    mulakatsoru.SinavKategoriTurAdi = data.SinavKategoriTurAdi;
                    mulakatsoru.Iptal=data.Iptal;
                    mulakatsoru.MulakatId = data.MulakatId;
                    mulakatsoru.MulakatDonemi = data.Mulakatlar.MulakatAdi;
                    mulakatsoru.KayitTarihi=data.KayitTarihi;
                    mulakatsoru.KaydedenId = data.KaydedenId;
                    mulakatsoru.KaydedenAdi = data.Kullanici.Ad + " " + data.Kullanici.Soyad;


                    return new Result<MulakatSorulariVM>(true, ResultConstant.RecordFound, mulakatsoru);
                }
                else
                {
                    return new Result<MulakatSorulariVM>(false, ResultConstant.RecordNotFound);
                }
            }
            else 
            {
                return new Result<MulakatSorulariVM>(false, ResultConstant.RecordNotFound);
            }           
        }
        #endregion

        #region MulakatSoruGetirSoruSiraNo(Guid id)
        public Result<int> MulakatSoruGetirSoruSiraNo(Guid id)
        {
            var data = _unitOfWork.mulakatSorulariRepository.Get(id);
            if (data != null)
            {
                var sorusirano = data.SoruSiraNo;
                return new Result<int>(true, ResultConstant.RecordFound, sorusirano);
            }
            else
            {
                return new Result<int>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        #region MulakatSoruEkle
        public Result<MulakatSorulariVM> MulakatSoruEkle(MulakatSorulariVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var mulakatSoru = _mapper.Map<MulakatSorulariVM, MulakatSorulari>(model);
                    mulakatSoru.KaydedenId = user.LoginId;

                    _unitOfWork.mulakatSorulariRepository.Add(mulakatSoru);
                    _unitOfWork.Save();
                    return new Result<MulakatSorulariVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<MulakatSorulariVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<MulakatSorulariVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region MulakatSoruGuncelle
        public Result<MulakatSorulariVM> MulakatSoruGuncelle(MulakatSorulariVM model, SessionContext user)
        {
            if (model.MulakatSorulariId != null)
            {
                try
                {
                    var data = _unitOfWork.mulakatSorulariRepository.Get(model.MulakatSorulariId);
                    if (data != null) 
                    {
                        data.SoruKategorilerId = model.SoruKategorilerId;
                        data.Soru = model.Soru;
                        data.Cevap = model.Cevap;
                        data.SoruNo=model.SoruNo;
                        data.KaydedenId = user.LoginId;

                        _unitOfWork.mulakatSorulariRepository.Update(data);
                        _unitOfWork.Save();
                        return new Result<MulakatSorulariVM>(true, ResultConstant.RecordCreateSuccess);
                    }
                    else 
                    {
                        return new Result<MulakatSorulariVM>(false, "Lütfen kayıt seçiniz");
                    }
                }
                catch (Exception ex)
                {

                    return new Result<MulakatSorulariVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<MulakatSorulariVM>(false, "Boş veri olamaz");
            }
        }
        #endregion

        #region SoruNoIleTopluGuncelle
        public Result<string> SoruNoIleTopluGuncelle(MulakatSorulariVM model, SessionContext user)
        {
            try
            {
                // Aynı SoruNo'ya sahip tüm kayıtları getir
                var kayitlar = _unitOfWork.mulakatSorulariRepository.GetAll().Where(x => x.SoruNo == model.SoruNo).ToList();

                if (kayitlar == null || !kayitlar.Any())
                {
                    return new Result<string>(false, "Belirtilen soru numarasına ait kayıt bulunamadı.");
                }

                // Tüm kayıtları güncelle
                foreach (var kayit in kayitlar)
                {
                    //kayit.SoruKategorilerId = model.SoruKategorilerId;
                    kayit.Soru = model.Soru;
                    kayit.Cevap = model.Cevap;
                    //kayit.SoruNo = model.SoruNo; // SoruNo'yu da güncellemek istiyorsanız

                    _unitOfWork.mulakatSorulariRepository.Update(kayit);
                }

                _unitOfWork.Save();

                return new Result<string>(true, $"{kayitlar.Count} adet kayıt başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework)
                // Logger.LogError(ex, "SoruNo ile toplu güncelleme sırasında hata oluştu.");

                return new Result<string>(false, $"Toplu güncelleme sırasında hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region SoruSıraNoIleTopluGuncelle
        public Result<MulakatSorulariVM> SoruSiraNoIleTopluGuncelle(MulakatSorulariVM model, SessionContext user)
        {
            try
            {
                var kayitlar = _unitOfWork.mulakatSorulariRepository
                    .GetAll()
                    .Where(x => x.SoruSiraNo == model.SoruSiraNo && x.DereceId==model.DereceId && x.MulakatId==model.MulakatId)
                    .ToList();

                if (!kayitlar.Any())
                {
                    return new Result<MulakatSorulariVM>(false, "Belirtilen soru numarasına ait kayıt bulunamadı.");
                }

                foreach (var kayit in kayitlar)
                {
                    kayit.Iptal = model.Iptal; // Gelen model'deki Iptal değerini kullan
                    _unitOfWork.mulakatSorulariRepository.Update(kayit);
                }

                _unitOfWork.Save();
                return new Result<MulakatSorulariVM>(true, $"{kayitlar.Count} adet kayıt başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new Result<MulakatSorulariVM>(false, $"Toplu güncelleme sırasında hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region MulakatSorusuSil
        public Result<bool> MulakatSorusuSil(Guid id)
        {
            var data = _unitOfWork.mulakatSorulariRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.mulakatSorulariRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        #endregion

        #region MulakatSoruGetirKullaniciId
        public Result<List<MulakatSorulariVM>> MulakatSoruGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.mulakatSorulariRepository.GetAll(u => u.KaydedenId == userId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<MulakatSorulariVM> returnData = new List<MulakatSorulariVM>();

                foreach (var item in data)
                {
                    returnData.Add(new MulakatSorulariVM()
                    {
                        MulakatSorulariId = item.MulakatSorulariId,
                        SoruSiraNo = item.SoruSiraNo,
                        SoruNo = item.SoruNo,
                        DereceId = item.DereceId,
                        DereceAdi = _derecelerBE.DereceAdGetir(item.DereceId).Data,
                        SoruKategorilerId = item.SoruKategorilerId,
                        SoruKategoriSiraNo = _soruKategorileriBE.SoruKategorilerKategoriSiraNoGetir(item.SoruKategorilerId).Data,
                        SoruKategoriAdi = _soruKategorileriBE.SoruKategorilerKategoriAdGetir(item.SoruKategorilerId).Data,
                        SoruKategoriTakmaAdi = _soruKategorileriBE.SoruKategoriTakmaAdGetir(item.SoruKategorilerId).Data,
                        Soru = item.Soru,
                        Cevap = item.Cevap,
                        SinavKateogoriTurId = item.SinavKateogoriTurId,
                        SinavKategoriTurAdi = item.SinavKategoriTurAdi,
                        MulakatId = item.MulakatId,
                        MulakatDonemi= _mulakatOlusturBE.MulakatDonemAdGetir(item.MulakatId).Data,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }
                return new Result<List<MulakatSorulariVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<MulakatSorulariVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        #endregion

        /*public Result TopluMulakatSoruEkle(List<MulakatSorulariVM> sorular, SessionContext user)
        {
            try
            {
                // Toplu ekleme işlemi (örneğin Entity Framework ile)
                var entities = sorular.Select(soru => new MulakatSorulari
                {
                    SoruSiraNo = soru.SoruSiraNo,
                    SoruNo = soru.SoruNo,
                    DereceId = soru.DereceId,
                    SoruKategorilerId = soru.SoruKategorilerId,
                    SoruKategoriSiraNo = soru.SoruKategoriSiraNo,
                    Soru = soru.Soru,
                    Cevap = soru.Cevap,
                    SinavKateogoriTurId = soru.SinavKateogoriTurId,
                    SinavKategoriTurAdi = soru.SinavKategoriTurAdi,
                    MulakatId = soru.MulakatId,
                    KaydedenId = user.UserId
                }).ToList();

                _context.MulakatSorulari.AddRange(entities);
                _context.SaveChanges();

                return new Result { IsSuccess = true, Message = "Toplu ekleme başarılı." };
            }
            catch (Exception ex)
            {
                return new Result { IsSuccess = false, Message = ex.Message };
            }
        }*/

        #region KullanilmayanYontem MulakatSorulariGetir
        //public Result<List<MulakatSorulariVM>> MulakatSorulariGetir(Guid id, string derece)
        //{
        //    var data = _unitOfWork.mulakatSorulariRepository.GetAll(k => k.SoruSiraNo == id).ToList(); //&& k.Dereceler.DereceAdi == derece (parantez içi eklenecek)
        //    if (data != null)
        //    {
        //        List<MulakatSorulariVM> returnData = new List<MulakatSorulariVM>();
        //        foreach (var item in data)
        //        {
        //            returnData.Add(new MulakatSorulariVM()
        //            {
        //                MulakatSorulariId = item.MulakatSorulariId,
        //                SoruSiraNo = item.SoruSiraNo,
        //                SoruId = item.SoruId,
        //                SoruKategoriId = item.SoruKategoriId,
        //                SoruKategoriAdi = item.SoruKategoriAdi,
        //                //DereceAdi=item.Dereceler.DereceAdi,
        //                Soru = item.Soru,
        //                Cevap = item.Cevap
        //            });
        //        }
        //        return new Result<List<MulakatSorulariVM>>(true, ResultConstant.RecordFound, returnData);
        //    }
        //    else
        //    {
        //        return new Result<List<MulakatSorulariVM>>(false, ResultConstant.RecordNotFound);
        //    }
        //} 
        #endregion

        #region MulakatAdaySoruGetir(int sorusirano)
        public Result<List<MulakatSorulariVM>> MulakatAdaySoruGetir(int sorusirano, Guid? mulakatId, Guid? dereceId)
        {
            var data = _unitOfWork.mulakatSorulariRepository.GetAll(x=>x.SoruSiraNo==sorusirano, 
                includeProperties: "Kullanici,SoruDereceler,SoruKategoriler,Mulakatlar")
                .Where(x => x.MulakatId == mulakatId && x.DereceId == dereceId && x.Mulakatlar.Durumu==true && x.Iptal==true)
                .OrderBy(x => x.DereceId)
                .ThenBy(y => y.SoruSiraNo)
                .ThenBy(z => z.SoruKategoriSiraNo).ToList();
            var mulakatSorulari = _mapper.Map<List<MulakatSorulari>, List<MulakatSorulariVM>>(data);

            if (data != null)
            {
                List<MulakatSorulariVM> returnData = new List<MulakatSorulariVM>();

                foreach (var item in data)
                {
                    returnData.Add(new MulakatSorulariVM()
                    {
                        MulakatSorulariId = item.MulakatSorulariId,
                        SoruSiraNo = item.SoruSiraNo,
                        SoruNo = item.SoruNo,
                        DereceId = item.DereceId,
                        DereceAdi = item.SoruDereceler.DereceAdi,
                        SoruKategorilerId = item.SoruKategorilerId,
                        SoruKategoriSiraNo = item.SoruKategoriSiraNo,
                        SoruKategoriAdi = item.SoruKategoriler.SoruKategorilerAdi,
                        SoruKategoriTakmaAdi = item.SoruKategoriler.SoruKategorilerTakmaAdi,
                        Soru = item.Soru,
                        Cevap = item.Cevap,
                        SinavKateogoriTurId = item.SinavKateogoriTurId,
                        SinavKategoriTurAdi = item.SinavKategoriTurAdi,
                        MulakatId = item.MulakatId,
                        MulakatDonemi = item.Mulakatlar.MulakatAdi,
                        MulakatDurumu= item.Mulakatlar.Durumu,
                        KayitTarihi = item.KayitTarihi,
                        KaydedenId = item.Kullanici != null ? item.KaydedenId : string.Empty,
                        KaydedenAdi = item.Kullanici != null ? item.Kullanici.Ad + " " + item.Kullanici.Soyad : string.Empty,
                    });
                }

                // returnData boş değilse başarılı sonuç dön
                if (returnData.Any())
                {
                    return new Result<List<MulakatSorulariVM>>(true, ResultConstant.RecordFound, returnData);
                }
            }
            // data null ise veya returnData boş ise başarısız sonuç dön
            return new Result<List<MulakatSorulariVM>>(false, ResultConstant.RecordNotFound);
        }
        #endregion
    }
}
