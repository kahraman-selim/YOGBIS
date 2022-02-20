using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface ISSSBE
    {
        //Result<List<SSSVM>> SSSGetir();
        Result<SSSVM> SSSEkle(SSSVM model, SessionContext user);
        //Result<SSSVM> SSSGetir(Guid id);
        //Result<SSSVM> SSSGuncelle(SSSVM model, SessionContext user);        
        //Result<bool> SSSSil(Guid id);
        //Result<List<SSSVM>> SSSGetirKullaniciId(string userId);
    }
}
