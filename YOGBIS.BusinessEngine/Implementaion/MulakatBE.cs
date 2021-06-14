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
    public class MulakatBE : IMulakatBE
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MulakatBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result<List<MulakatSorulariVM>> GetAllMulakatSorulari()
        {
            var data = _unitOfWork.mulakatSorulariRepository.GetAll().ToList();
            var mulakatSorulari = _mapper.Map<List<MulakatSorulari>, List<MulakatSorulariVM>>(data);
            return new Result<List<MulakatSorulariVM>>(true, ResultConstant.RecordFound, mulakatSorulari);
        }

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
                        MulakatSorulariId=item.MulakatSorulariId,
                        SoruSiraNo=item.SoruSiraNo,
                        SoruNo=item.SoruNo,
                        SoruKategoriId=item.SoruKategoriId,
                        SoruKategoriAdi=item.SoruKategoriAdi,
                        Derecesi=item.Derecesi,
                        Soru=item.Soru,
                        Cevap=item.Cevap
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
