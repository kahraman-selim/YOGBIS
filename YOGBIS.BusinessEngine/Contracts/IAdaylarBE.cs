using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IAdaylarBE
    {
        Result<List<AdaylarVM>> AdaylariGetir();
        Result<List<AdaylarVM>> AdayTemelBilgileriGetir();
        Result<AdaylarVM> AdayGetir(Guid id);
        Result<AdaylarVM> AdayGetirTC(string TC);
        Result<AdaylarVM> AdayEkle(AdaylarVM model, SessionContext user);
        Result<AdaylarVM> AdayGuncelle(AdaylarVM model, SessionContext user);
        Result<bool> AdaySil(Guid id);
        Result<List<AdaylarVM>> AdayGetirKullaniciId(string userId);
        bool TCKontrol(string TC);
        Result<AdayBasvuruBilgileriVM> AdayBasvuruBilgileriEkle(AdayBasvuruBilgileriVM model, SessionContext user);
        Result<List<AdayBasvuruBilgileriVM>> AdayBasvuruBilgileriniGetir(string TC);       
        Result<AdayBasvuruBilgileriVM> AdayBasvuruBilgileriniGetirById(Guid id);
        Result<AdayIletisimBilgileriVM> AdayIletisimBilgileriEkle(AdayIletisimBilgileriVM model, SessionContext user);
        Result<List<AdayIletisimBilgileriVM>> AdayIletisimBilgileriniGetir(string TC);
        Result<AdayIletisimBilgileriVM> AdayIletisimBilgileriniGetirById(Guid id);
        Result<AdayMYSSVM> AdayMYSSBilgileriEkle(AdayMYSSVM model, SessionContext user);
        Result<List<AdayMYSSVM>> AdayMYSSBilgileriniGetir(string TC);
        Result<AdayMYSSVM> AdayMYSSBilgileriniGetirById(Guid id);      
        Result<AdayTYSVM> AdayTYSBilgileriEkle(AdayTYSVM model, SessionContext user);
        Result<List<AdayTYSVM>> AdayTYSBilgileriniGetir(string TC);
        Result<AdayTYSVM> AdayTYSBilgileriniGetirById(Guid id);
    }
}
