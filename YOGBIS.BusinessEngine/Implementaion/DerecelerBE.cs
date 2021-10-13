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
    public class DerecelerBE : IDerecelerBE
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DerecelerBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Result<List<DerecelerVM>> DereceleriGetir()
        {
            //var data = _unitOfWork.derecelerRepository.GetAll().ToList();
            //var dereceler = _mapper.Map<List<Dereceler>, List<DerecelerVM>>(data);
            //return new Result<List<DerecelerVM>>(true, ResultConstant.RecordFound, dereceler);

            var data = _unitOfWork.derecelerRepository.GetAll(includeProperties: "Kullanici").ToList();
            var dereceler = _mapper.Map<List<Dereceler>, List<DerecelerVM>>(data);

            if (data != null)
            {
                List<DerecelerVM> returnData = new List<DerecelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new DerecelerVM()
                    {
                        DereceId=item.DereceId,
                        DereceAdi=item.DereceAdi,                        
                        KullaniciAdi = item.Kullanici.Ad + " " + item.Kullanici.Soyad
                    });
                }
                return new Result<List<DerecelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<DerecelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<List<DerecelerVM>> DereceGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.derecelerRepository.GetAll(u => u.KullaniciId == userId, includeProperties: "Kullanici").ToList();
            if (data != null)
            {
                List<DerecelerVM> returnData = new List<DerecelerVM>();

                foreach (var item in data)
                {
                    returnData.Add(new DerecelerVM()
                    {                        
                        DereceId=item.DereceId,
                        DereceAdi=item.DereceAdi,
                        KayitTarihi = item.KayitTarihi,
                        KullaniciAdi=item.Kullanici.Ad+" "+item.Kullanici.Soyad,
                        KullaniciId = item.KullaniciId                        
                    });
                }
                return new Result<List<DerecelerVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<DerecelerVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<DerecelerVM> DereceGetir(int id)
        {
            var data = _unitOfWork.derecelerRepository.Get(id);
            if (data != null)
            {
                var dereceler = _mapper.Map<Dereceler, DerecelerVM>(data);
                return new Result<DerecelerVM>(true, ResultConstant.RecordFound, dereceler);
            }
            else
            {
                return new Result<DerecelerVM>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<DerecelerVM> DereceEkle(DerecelerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var derece = _mapper.Map<DerecelerVM, Dereceler>(model);
                     derece.KullaniciId = user.LoginId;                    
                    _unitOfWork.derecelerRepository.Add(derece);
                    _unitOfWork.Save();
                    return new Result<DerecelerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<DerecelerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<DerecelerVM>(false, "Boş veri olamaz");
            }
        }
        public Result<DerecelerVM> DereceGuncelle(DerecelerVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var derece = _mapper.Map<DerecelerVM, Dereceler>(model);
                    derece.KullaniciId = user.LoginId;
                    _unitOfWork.derecelerRepository.Update(derece);
                    _unitOfWork.Save();
                    return new Result<DerecelerVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<DerecelerVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<DerecelerVM>(false, "Boş veri olamaz");
            }
        }
        public Result<bool> DereceSil(int id)
        {
            var data = _unitOfWork.derecelerRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.derecelerRepository.Remove(data);
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
