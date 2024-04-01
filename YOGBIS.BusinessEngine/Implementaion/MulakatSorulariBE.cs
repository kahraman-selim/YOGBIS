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
        #endregion

        #region Donustucuruler
        public MulakatSorulariBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region MulakatSorulariGetir
        public Result<List<MulakatSorulariVM>> MulakatSorulariGetir()
        {
            //1. Yöntem
            var data = _unitOfWork.mulakatSorulariRepository.GetAll(includeProperties: "Kullanici").ToList();
            var mulakatSorulari = _mapper.Map<List<MulakatSorulari>, List<MulakatSorulariVM>>(data);

            if (data != null)
            {
                List<MulakatSorulariVM> returnData = new List<MulakatSorulariVM>();

                foreach (var item in data)
                {
                    returnData.Add(new MulakatSorulariVM()
                    {
                        MulakatSorulariId=item.MulakatSorulariId,
                        SoruSiraNo = item.SoruSiraNo,
                        SoruNo = item.SoruNo,
                        DereceId=item.DereceId,
                        SoruDereceId = item.SoruDereceId,
                        SoruDereceAdi = item.SoruDereceAdi,
                        SoruKategorilerId=item.SoruKategorilerId,
                        SoruKategoriId=item.SoruKategoriId,
                        SoruKategoriAdi=item.SoruKategoriAdi,
                        Soru = item.Soru,
                        Cevap =item.Cevap,
                        SinavKategoriID = item.SinavKategoriID,
                        SinavKategoriAdi = item.SinavKategoriAdi,
                        //MulakatId = item.MulakatId,
                        KayitTarihi =item.KayitTarihi,
                        KaydedenId= item.Kullanici != null ? item.KaydedenId : string.Empty,
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
            var data = _unitOfWork.mulakatSorulariRepository.Get(id);
            if (data != null)
            {
                var mulakatsorulari = _mapper.Map<MulakatSorulari, MulakatSorulariVM>(data);
                return new Result<MulakatSorulariVM>(true, ResultConstant.RecordFound, mulakatsorulari);
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
            if (model != null)
            {
                try
                {
                    var mulakatSoru = _mapper.Map<MulakatSorulariVM, MulakatSorulari>(model);
                    mulakatSoru.KaydedenId = user.LoginId;
                    _unitOfWork.mulakatSorulariRepository.Update(mulakatSoru);
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
                        SoruDereceId = item.SoruDereceId,
                        SoruDereceAdi = item.SoruDereceAdi,
                        SoruKategorilerId = item.SoruKategorilerId,
                        SoruKategoriId = item.SoruKategoriId,
                        SoruKategoriAdi = item.SoruKategoriAdi,
                        Soru = item.Soru,
                        Cevap = item.Cevap,
                        SinavKategoriID = item.SinavKategoriID,
                        SinavKategoriAdi = item.SinavKategoriAdi,
                        //MulakatId = item.MulakatId,
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

              

        #region KullanilmayanYontem
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
