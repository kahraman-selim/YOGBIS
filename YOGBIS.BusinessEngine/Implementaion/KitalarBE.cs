using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Implementaion
{
    public class KitalarBE : IKitalarBE
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public KitalarBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result<List<KitalarVM>> KitalariGetir()
        {
            var data = _unitOfWork.kitalarRepository.GetAll().ToList();
            var kitalar = _mapper.Map<List<Kitalar>, List<KitalarVM>>(data);
            return new Result<List<KitalarVM>>(true, ResultConstant.RecordFound, kitalar);
        }

        public Result<KitalarVM> KitaGetir(int id)
        {
            var data = _unitOfWork.kitalarRepository.Get(id);
            if (data != null)
            {
                var kitalar = _mapper.Map<Kitalar, KitalarVM>(data);
                return new Result<KitalarVM>(true, ResultConstant.RecordFound, kitalar);
            }
            else
            {
                return new Result<KitalarVM>(false, ResultConstant.RecordNotFound);
            }
        }
    }
}
