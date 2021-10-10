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
    public class OkulBilgiBE : IOkulBilgiBE
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OkulBilgiBE(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Result<List<OkulBilgiVM>> OkulBilgileriGetir()
        {
            var data = _unitOfWork.okulBilgiRepository.GetAll().ToList();
            var okulbilgiler = _mapper.Map<List<OkulBilgi>, List<OkulBilgiVM>>(data);
            return new Result<List<OkulBilgiVM>>(true, ResultConstant.RecordFound, okulbilgiler);
        }
        public Result<List<OkulBilgiVM>> OkulBilgiGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.okulBilgiRepository.GetAll(u => u.KullaniciId == userId, includeProperties: "Kullanici,Okullar").ToList();
            if (data != null)
            {
                List<OkulBilgiVM> returnData = new List<OkulBilgiVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkulBilgiVM()
                    {                        
                        OkulBilgiId=item.OkulBilgiId,
                        OkulTelefon=item.OkulTelefon,
                        YoneticiGorev=item.YoneticiGorev,
                        YoneticiAdiSoyadi=item.YoneticiAdiSoyadi,
                        YoneticiTelefon=item.YoneticiTelefon,
                        OkulId=item.OkulId,
                        OkulAdi=item.Okullar.OkulAdi,
                        UlkeId=item.UlkeId,
                        UlkeAdi=item.Ulkeler.UlkeAdi,
                        KayitTarihi = item.KayitTarihi,
                        KullaniciId = item.KullaniciId                        
                    });
                }
                return new Result<List<OkulBilgiVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkulBilgiVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<OkulBilgiVM> OkulBilgiGetir(int id)
        {
            var data = _unitOfWork.okulBilgiRepository.Get(id);
            if (data != null)
            {
                var okulbilgi = _mapper.Map<OkulBilgi, OkulBilgiVM>(data);
                return new Result<OkulBilgiVM>(true, ResultConstant.RecordFound, okulbilgi);
            }
            else
            {
                return new Result<OkulBilgiVM>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<OkulBilgiVM> OkulBilgiEkle(OkulBilgiVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var okulbilgi = _mapper.Map<OkulBilgiVM, OkulBilgi>(model);
                     okulbilgi.KullaniciId = user.LoginId;                    
                    _unitOfWork.okulBilgiRepository.Add(okulbilgi);
                    _unitOfWork.Save();
                    return new Result<OkulBilgiVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<OkulBilgiVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<OkulBilgiVM>(false, "Boş veri olamaz");
            }
        }
        public Result<OkulBilgiVM> OkulBilgiGuncelle(OkulBilgiVM model, SessionContext user)
        {
            if (model != null)
            {
                try
                {
                    var okulbilgi = _mapper.Map<OkulBilgiVM, OkulBilgi>(model);
                    okulbilgi.KullaniciId = user.LoginId;
                    _unitOfWork.okulBilgiRepository.Update(okulbilgi);
                    _unitOfWork.Save();
                    return new Result<OkulBilgiVM>(true, ResultConstant.RecordCreateSuccess);
                }
                catch (Exception ex)
                {

                    return new Result<OkulBilgiVM>(false, ResultConstant.RecordCreateNotSuccess + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<OkulBilgiVM>(false, "Boş veri olamaz");
            }
        }
        public Result<bool> OkulBilgiSil(int id)
        {
            var data = _unitOfWork.okulBilgiRepository.Get(id);
            if (data != null)
            {
                _unitOfWork.okulBilgiRepository.Remove(data);
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
