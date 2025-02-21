using System;
using System.Collections.Generic;
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
        Result<AdaylarVM> AdayEkle(AdaylarVM model, SessionContext user);
        Result<AdaylarVM> AdayGuncelle(AdaylarVM model, SessionContext user);
        Result<bool> AdaySil(Guid id);
        Result<List<AdaylarVM>> AdayGetirKullaniciId(string userId);
    }
}
