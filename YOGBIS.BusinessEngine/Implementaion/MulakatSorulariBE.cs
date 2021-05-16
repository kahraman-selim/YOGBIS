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
            var data = _unitOfWork.mulakatSorulariRepository.GetAll().ToList();
            //1. Yöntem
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

            //2. Yöntem
            var mulakatSorulari = _mapper.Map<List<MulakatSorulari>, List<MulakatSorulariVM>>(data);
            return new Result<List<MulakatSorulariVM>>(true, ResultConstant.RecordFound, mulakatSorulari);
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
    }
}
