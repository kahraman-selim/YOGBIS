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
    public class MulakatSorulariBE : IMulakatSorulariBE
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MulakatSorulariBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result<List<MulakatSorulariVM>> GetAllMulakatSorulari()
        {
            //1. Yöntem
            var data = _unitOfWork.mulakatSorulariRepository.GetAll().ToList();            
            var mulakatSorulari = _mapper.Map<List<MulakatSorulari>, List<MulakatSorulariVM>>(data);
            return new Result<List<MulakatSorulariVM>>(true, ResultConstant.RecordFound, mulakatSorulari);

            #region 2.Yöntem
            //2. Yöntem
            //if (data != null)
            //{
            //    List<MulakatSorulari> returnData = new List<MulakatSorulari>();

            //    foreach (var item in data)
            //    {
            //        returnData.Add(new MulakatSorulari()
            //        {
            //            MulakatSorulariId = item.MulakatSorulariId,
            //            SoruSiraNo = item.SoruSiraNo,
            //            SoruNo = item.SoruNo,
            //            Soru = item.Soru,
            //            Cevap = item.Cevap,
            //            Derecesi = item.Derecesi,
            //            KategoriId = item.KategoriId,
            //            MulakatId = item.MulakatId
            //        });
            //    }
            //    return new Result<List<MulakatSorulari>>(true, ResultConstant.RecordFound, returnData);
            //}
            //else
            //{
            //    return new Result<List<MulakatSorulari>>(false, ResultConstant.RecordNotFound);
            //} 
            #endregion
        }

        public Result<MulakatSorulariVM> GetAllMulakatSorulari(int id)
        {
            var data = _unitOfWork.mulakatSorulariRepository.Get(id);
            if (data!=null)
            {
                var mulakatSoru = _mapper.Map<MulakatSorulari, MulakatSorulariVM>(data);
                return new Result<MulakatSorulariVM>(true, ResultConstant.RecordFound, mulakatSoru);
            }
            else
            {
                return new Result<MulakatSorulariVM>(false, ResultConstant.RecordNotFound);
            }
        }

        public Result<MulakatSorulariVM> MulakatSorusuEkle(MulakatSorulariVM model)
        {
            if (model!=null)
            {
                try
                {
                    var mulakatSoru = _mapper.Map<MulakatSorulariVM, MulakatSorulari>(model);
                    _unitOfWork.mulakatSorulariRepository.Add(mulakatSoru);
                    _unitOfWork.Save();
                    return new Result<MulakatSorulariVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<MulakatSorulariVM>(false, ResultConstant.RecordCreateNotSuccess +" "+ ex.Message.ToString());
                }
            }
            else
            {
                return new Result<MulakatSorulariVM>(false, "Boş veri olamaz");
            }
        }

        public Result<MulakatSorulariVM> MulakatSorusuGuncelle(MulakatSorulariVM model)
        {
            if (model != null)
            {
                try
                {
                    var mulakatSoru = _mapper.Map<MulakatSorulariVM, MulakatSorulari>(model);
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

        #region MulakatSoruSil
        public Result<bool> MulakatSorusuSil(int id)
        {
            var data = _unitOfWork.mulakatSorulariRepository.Get(id);
            if (data!=null)
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

        public Result<List<MulakatSorulariVM>> GetAllMulakatSorulariById(int id, string derece)
        {
            var data = _unitOfWork.mulakatSorulariRepository.GetAll(k => k.SoruSiraNo == id && k.Derecesi == derece).ToList();
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
                        SoruKategoriId = item.SoruKategoriId,
                        SoruKategoriAdi = item.SoruKategoriAdi,
                        Derecesi = item.Derecesi,
                        Soru = item.Soru,
                        Cevap = item.Cevap
                    });
                }
                return new Result<List<MulakatSorulariVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<MulakatSorulariVM>>(false, ResultConstant.RecordNotFound);
            }
        }
    }
}
