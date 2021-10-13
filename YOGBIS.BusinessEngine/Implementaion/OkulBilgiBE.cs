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
            var data = _unitOfWork.okulBilgiRepository.GetAll(includeProperties: "Okullar,Ulkeler,Kullanici").OrderBy(u => u.Ulkeler.UlkeAdi).ToList();
            //var okulbilgiler = _mapper.Map<List<OkulBilgi>, List<OkulBilgiVM>>(data);
            //return new Result<List<OkulBilgiVM>>(true, ResultConstant.RecordFound, okulbilgiler);
            if (data != null)
            {
                List<OkulBilgiVM> returnData = new List<OkulBilgiVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkulBilgiVM()
                    {
                        OkulBilgiId = item.OkulBilgiId,
                        OkulTelefon = item.OkulTelefon,
                        OkulAdres = item.OkulAdres,
                        //*************************                        
                        MudurAdiSoyadi = item.MudurAdiSoyadi,
                        MudurTelefon = item.MudurTelefon,
                        MudurEPosta = item.MudurEPosta,
                        MudurDonusYil = item.MudurDonusYil,
                        //****************************
                        MdYrdAdiSoyadi = item.MdYrdAdiSoyadi,
                        MdYrdTelefon = item.MdYrdTelefon,
                        MdYrdEPosta = item.MdYrdEPosta,
                        MdYrdDonusYil = item.MdYrdDonusYil,
                        //********************************
                        OkulId = item.OkulId,
                        OkulAdi = item.Okullar.OkulAdi,
                        UlkeId = item.UlkeId,
                        UlkeAdi = item.Ulkeler.UlkeAdi,
                        KayitTarihi = item.KayitTarihi,
                        KullaniciId = item.KullaniciId,
                        KullaniciAdi =item.Kullanici.Ad+" "+item.Kullanici.Soyad                        
                    });
                }
                return new Result<List<OkulBilgiVM>>(true, ResultConstant.RecordFound, returnData);
            }
            else
            {
                return new Result<List<OkulBilgiVM>>(false, ResultConstant.RecordNotFound);
            }
        }
        public Result<List<OkulBilgiVM>> OkulBilgiGetirKullaniciId(string userId)
        {
            var data = _unitOfWork.okulBilgiRepository.GetAll(u => u.KullaniciId == userId, includeProperties: "Kullanici,Okullar,Ulkeler").ToList();
            if (data != null)
            {
                List<OkulBilgiVM> returnData = new List<OkulBilgiVM>();

                foreach (var item in data)
                {
                    returnData.Add(new OkulBilgiVM()
                    {                        
                        OkulBilgiId=item.OkulBilgiId,
                        OkulTelefon=item.OkulTelefon,
                        OkulAdres = item.OkulAdres,
                        //*************************                        
                        MudurAdiSoyadi=item.MudurAdiSoyadi,
                        MudurTelefon = item.MudurTelefon,
                        MudurEPosta = item.MudurEPosta,
                        MudurDonusYil = item.MudurDonusYil,
                        //****************************
                        MdYrdAdiSoyadi=item.MdYrdAdiSoyadi,
                        MdYrdTelefon=item.MdYrdTelefon,
                        MdYrdEPosta=item.MdYrdEPosta,
                        MdYrdDonusYil=item.MdYrdDonusYil,
                        //********************************
                        OkulId =item.OkulId,
                        OkulAdi=item.Okullar.OkulAdi,
                        UlkeId=item.UlkeId,
                        UlkeAdi=item.Ulkeler.UlkeAdi,
                        KayitTarihi = item.KayitTarihi,
                        KullaniciAdi=item.Kullanici.Ad+" "+item.Kullanici.Soyad,
                        KullaniciId = item.KullaniciId,
                        
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
            //var data = _unitOfWork.okulBilgiRepository.Get(id);
            //if (data != null)
            //{
            //    var okulbilgi = _mapper.Map<OkulBilgi, OkulBilgiVM>(data);
            //    return new Result<OkulBilgiVM>(true, ResultConstant.RecordFound, okulbilgi);
            //}
            //else
            //{
            //    return new Result<OkulBilgiVM>(false, ResultConstant.RecordNotFound);
            //}

            if (id>0)
            {
                var data = _unitOfWork.okulBilgiRepository.GetFirstOrDefault(u => u.OkulBilgiId == id, includeProperties: "Okullar,Ulkeler,Kullanici");

                if (data!=null)
                {
                    OkulBilgiVM okulbilgi = new OkulBilgiVM();
                    okulbilgi.OkulBilgiId = data.OkulBilgiId;
                    okulbilgi.OkulTelefon = data.OkulTelefon;
                    okulbilgi.OkulAdres = data.OkulAdres;
                    //************************************
                    okulbilgi.MudurAdiSoyadi = data.MudurAdiSoyadi;
                    okulbilgi.MudurTelefon = data.MudurTelefon;
                    okulbilgi.MudurEPosta = data.MudurEPosta;
                    okulbilgi.MudurDonusYil = data.MudurDonusYil;
                    //***************************************
                    okulbilgi.MdYrdAdiSoyadi = data.MdYrdAdiSoyadi;
                    okulbilgi.MdYrdTelefon = data.MdYrdTelefon;
                    okulbilgi.MdYrdEPosta = data.MdYrdEPosta;
                    okulbilgi.MdYrdDonusYil = data.MdYrdDonusYil;
                    //******************************************
                    okulbilgi.OkulId = data.Okullar.OkulId;
                    okulbilgi.OkulAdi = data.Okullar.OkulAdi;
                    okulbilgi.UlkeId = data.Ulkeler.UlkeId;
                    okulbilgi.UlkeAdi = data.Ulkeler.UlkeAdi;
                    okulbilgi.KullaniciId = data.Kullanici.Id;
                    okulbilgi.KullaniciAdi = data.Kullanici.Ad + " " + data.Kullanici.Soyad;

                    return new Result<OkulBilgiVM>(true, ResultConstant.RecordFound, okulbilgi);
                }
                else
                {
                    return new Result<OkulBilgiVM>(false, ResultConstant.RecordNotFound);
                }
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
