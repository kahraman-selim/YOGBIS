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
    public class SSSBE : ISSSBE
    {
        #region Degiskenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        #endregion

        #region Donusturuculer
        public SSSBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion        

        #region SSSEkle
        public Result<SSSVM> SSSEkle(SSSVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var sss = _mapper.Map<SSSVM, SSS>(model);
                    sss.KaydedenId = user.LoginId;
                    _unitOfWork.sssRepository.Add(sss);
                    _unitOfWork.Save();
                    return new Result<SSSVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<SSSVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<SSSVM>(false, "Boş veri olamaz");
            }
        }
        #endregion        
    }
}
