using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Implementaion
{
    public class OkullarBE : IOkullarBE
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OkullarBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Result<List<OkullarVM>> OkullariGetir()
        {
            //var data = _unitOfWork.okullarRepository.GetAll().ToList();
            //var okullar = _mapper.Map<List<Okullar>, List<OkullarVM>>(data);
            //return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, okullar);

            var data = _unitOfWork.okullarRepository.GetAll(includeProperties: "Kullanici").ToList();
            var dereceler = _mapper.Map<List<Okullar>, List<OkullarVM>>(data);

            if (data != null)
            {
                List<OkullarVM> returnData = new List<OkullarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkullarVM()
                    {
                        OkulId=item.OkulId,
                        OkulKodu=item.OkulKodu,
                        OkulAdi=item.OkulAdi,
                        KullaniciId=item.Kullanici.Id,
                        KullaniciAdi = item.Kullanici.Ad + " " + item.Kullanici.Soyad
                    });
                }
                return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkullarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<List<OkullarVM>> OkulGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.okullarRepository.GetAll(u => u.KullaniciId == userId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<OkullarVM> returnData = new List<OkullarVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkullarVM()
                    {
                        OkulId = item.OkulId,
                        OkulKodu=item.OkulKodu,
                        OkulAdi=item.OkulAdi,
                        KullaniciAdi=item.Kullanici.Ad+" "+item.Kullanici.Soyad
                    });
                }
                return new Result<List<OkullarVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkullarVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<OkullarVM> OkulGetir(int id)
        {
            var data = _unitOfWork.okullarRepository.Get(id);
            if (data != null)
            {
                var dereceler = _mapper.Map<Okullar, OkullarVM>(data);
                return new Result<OkullarVM>(true, ResultConstant.RecordFound, dereceler);
            }
            else
            {
                return new Result<OkullarVM>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<OkullarVM> OkulEkle(OkullarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var okul = _mapper.Map<OkullarVM, Okullar>(model);
                    okul.KullaniciId = user.LoginId;                    
                    _unitOfWork.okullarRepository.Add(okul);
                    _unitOfWork.Save();
                    return new Result<OkullarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<OkullarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<OkullarVM>(false, "Boş veri olamaz");
            }
        }
        public Result<OkullarVM> OkulGuncelle(OkullarVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var okul = _mapper.Map<OkullarVM, Okullar>(model);
                    okul.KullaniciId = user.LoginId;
                    _unitOfWork.okullarRepository.Update(okul);
                    _unitOfWork.Save();
                    return new Result<OkullarVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<OkullarVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<OkullarVM>(false, "Boş veri olamaz");
            }
        }
        public Result<bool> OkulSil(int id)
        {
            var data = _unitOfWork.okullarRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.okullarRepository.Remove(data);
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
