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

        #region MulakatSorulariGetir
        public Result<List<MulakatSorulariVM>> MulakatSorulariGetir()
        {
            //1. Yöntem
            var data = _unitOfWork.mulakatSorulariRepository.GetAll(includeProperties: "Kullanici,SoruDereceler,SoruKategoriler,Mulakatlar").OrderBy(x=>x.DereceId).ThenBy(y=>y.SoruSiraNo).ThenBy(z=>z.SoruKategoriSiraNo).ToList(); 
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

            #region 2.Yöntem
            /*var data = _unitOfWork.mulakatSorulariRepository.GetAll(includeProperties: "SoruBankasi,SoruKategoriler,Dereceler,Mulakatlar,Kullanici").ToList();
            var soruBankasi = _mapper.Map<List<MulakatSorulari>, List<MulakatSorulariVM>>(data);

            if (data != null)
            {
                List<MulakatSorulariVM> returnData = new List<MulakatSorulariVM>();

                foreach (var item in data)
                {
                    returnData.Add(new MulakatSorulariVM()
                    {
                        MulakatSorulariId=item.MulakatSorulariId,
                        SoruSiraNo=item.SoruSiraNo,
                        //SoruId=item.SoruBankasi.SoruBankasiId,
                        //SoruKategoriId=item.SoruKategoriler.SoruKategorilerId,
                        //SoruKategoriAdi=item.SoruKategoriler.SoruKategorilerAdi,
                        //DereceId=item.Dereceler.DereceId,
                        //DereceAdi=item.Dereceler.DereceAdi,
                         Soru=item.Soru,
                        Cevap=item.Cevap,
                        MulakatId=item.Mulakatlar.MulakatId,
                        MulakatAdi=item.Mulakatlar.MulakatAdi,
                        KaydedenId =item.Kullanici.Id,
                        //KullaniciAdi=item.Kullanici.Ad+" "+item.Kullanici.Soyad
                    });
                }
                return new Result<List<MulakatSorulariVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<MulakatSorulariVM>>(false, ResultConstant.RecordNotFound);
            }*/
            #endregion
        }
        #endregion

        #region MulakatSoruGetir(Guid id)
        public Result<MulakatSorulariVM> MulakatSoruGetir(Guid id)
        {
            if (id != null) 
            {
                var data = _unitOfWork.mulakatSorulariRepository.GetFirstOrDefault(x=>x.MulakatSorulariId==id, includeProperties: "Kullanici,SoruDereceler,SoruKategoriler,Mulakatlar");

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
    }
}
