using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Implementaion
{
    public class SoruBankasiBE : ISoruBankasiBE
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SoruBankasiBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result<List<SoruBankasiVM>> SoruGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.soruBankasiRepository.GetAll(u => u.KaydedenId == userId, 
                includeProperties: "Kaydeden,SoruKategoriler").ToList();
            if (data != null)
            {
                List<SoruBankasiVM> returnData = new List<SoruBankasiVM>();

                foreach (var item in data)
                {
                    returnData.Add(new SoruBankasiVM()
                    {
                        SoruBankasiId = item.SoruBankasiId,
                        SoruKategoriId = item.SoruKategoriId,                        
                        Soru =item.Soru,
                        Cevap=item.Cevap,
                        Derecesi=item.Derecesi,
                        SorulmaSayisi=item.SorulmaSayisi,
                        SoruDurumu=item.SoruDurumu,
                        KaydedenId=item.KaydedenId,
                        OnaylayanId=item.OnaylayanId,
                        OnayDurumu=item.OnayDurumu,
                        OnayAciklama=item.OnayAciklama,
                        KayitTarihi=DateTime.Now
                    });
                }
                return new Result<List<SoruBankasiVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<SoruBankasiVM>>(false, ResultConstant.RecordNotFound);
            }
        }

        public Result<List<SoruBankasiVM>> SoruGetir()
        {
            var data = _unitOfWork.soruBankasiRepository.GetAll().ToList();
            var soruBankasi = _mapper.Map<List<SoruBankasi>, List<SoruBankasiVM>>(data);
            return new Result<List<SoruBankasiVM>>(true, ResultConstant.RecordFound, soruBankasi);
        }

        public Result<SoruBankasiVM> SoruGetir(int id)
        {
            var data = _unitOfWork.soruBankasiRepository.Get(id);
            if (data != null)
            {
                var soruBankasi = _mapper.Map<SoruBankasi, SoruBankasiVM>(data);
                return new Result<SoruBankasiVM>(true, ResultConstant.RecordFound, soruBankasi);
            }
            else
            {
                return new Result<SoruBankasiVM>(false, ResultConstant.RecordNotFound);
            }
        }

        public Result<SoruBankasiVM> SoruEkle(SoruBankasiVM model)
        {
            if (model != null)
            {
                try
                {
                    var soruBankasi= _mapper.Map<SoruBankasiVM, SoruBankasi>(model);
                    _unitOfWork.soruBankasiRepository.Add(soruBankasi);
                    _unitOfWork.Save();
                    return new Result<SoruBankasiVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SoruBankasiVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SoruBankasiVM>(false, "Boş veri olamaz");
            }
        }

        public Result<SoruBankasiVM> SoruGuncelle(SoruBankasiVM model)
        {
            if (model != null)
            {
                try
                {
                    var soruBankasi = _mapper.Map<SoruBankasiVM, SoruBankasi>(model);
                    _unitOfWork.soruBankasiRepository.Update(soruBankasi);
                    _unitOfWork.Save();
                    return new Result<SoruBankasiVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SoruBankasiVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SoruBankasiVM>(false, "Boş veri olamaz");
            }
        }

        public Result<bool> SoruSil(int id)
        {
            var data = _unitOfWork.soruBankasiRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.soruBankasiRepository.Remove(data);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultConstant.RecordRemoveSuccessfully);
            }
            else
            {
                return new Result<bool>(false, ResultConstant.RecordRemoveNotSuccessfully);
            }
        }
        
    }
}
