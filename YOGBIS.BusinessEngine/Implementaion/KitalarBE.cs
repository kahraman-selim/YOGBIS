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
    public class KitalarBE : IKitalarBE
    {
        #region Değişkenler
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Dönüştürücüler
        public KitalarBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region KitalariGetir
        public Result<List<KitalarVM>> KitalariGetir()
        {
            var data = _unitOfWork.kitalarRepository.GetAll().OrderBy(k => k.KitaAdi).ToList();
            var kitalar = _mapper.Map<List<Kitalar>, List<KitalarVM>>(data);
            return new Result<List<KitalarVM>>(true, ResultConstant.RecordFound, kitalar);
        }
        #endregion

        #region KitaGetir
        public Result<KitalarVM> KitaGetir(Guid id)
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
        #endregion

        #region KıtaEkle
        public Result<KitalarVM> KitaEkle(KitalarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var kitalar = _mapper.Map<KitalarVM, Kitalar>(model);
                    kitalar.KitaAdi = model.KitaAdi;
                    kitalar.KitaAciklama = model.KitaAciklama;
                    kitalar.KaydedenId = user.LoginId;

                    _unitOfWork.kitalarRepository.Add(kitalar);
                    _unitOfWork.Save();
                    return new Result<KitalarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<KitalarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<KitalarVM>(false, "Boş veri olamaz");
            }
        }
        #endregion
    }
}
