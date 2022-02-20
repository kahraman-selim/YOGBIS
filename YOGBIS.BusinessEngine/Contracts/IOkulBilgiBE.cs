using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IOkulBilgiBE
    {
        Result<List<OkulBilgiVM>> OkulBilgileriGetir();
        Result<OkulBilgiVM> OkulBilgiEkle(OkulBilgiVM model, SessionContext user);
        Result<OkulBilgiVM> OkulBilgiGetir(Guid id);
        Result<OkulBilgiVM> OkulBilgiGuncelle(OkulBilgiVM model, SessionContext user);        
        Result<bool> OkulBilgiSil(Guid id);
        Result<List<OkulBilgiVM>> OkulBilgiGetirKullaniciId(string userId);
        Result<List<OkulBilgiVM>> OkulBilgiGetirUlkeId(Guid ulkeId);
        Result<List<OkulBilgiVM>> OkulAdGetirUlkeId(Guid ulkeId);
    }
}
