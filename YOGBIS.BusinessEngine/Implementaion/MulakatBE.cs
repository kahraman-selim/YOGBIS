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

        public Result<MulakatSorulariVM> GetAllMulakatSorulari(int id)
        {
            var data = _unitOfWork.mulakatSorulariRepository.Get(id);
            if (data != null)
            {
                var mulakatSoru = _mapper.Map<MulakatSorulari, MulakatSorulariVM>(data);
                return new Result<MulakatSorulariVM>(true, ResultConstant.RecordFound, mulakatSoru);
            }
            else
            {
                return new Result<MulakatSorulariVM>(false, ResultConstant.RecordNotFound);
            }
        }

    }
}
