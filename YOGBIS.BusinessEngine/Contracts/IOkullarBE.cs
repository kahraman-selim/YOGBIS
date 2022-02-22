using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IOkullarBE
    {
        Result<List<OkullarVM>> OkullariGetir();
        Result<List<OkullarVM>> OkullariGetirAZ();
        Result<OkullarVM> OkulEkle(OkullarVM model, SessionContext user);
        Result<OkullarVM> OkulGetir(Guid id);
        Result<OkullarVM> OkulGuncelle(OkullarVM model, SessionContext user);
        Result<OkullarVM> OkulDetayGuncelle(OkullarVM model, SessionContext user);
        Result<bool> OkulSil(Guid id);
        Result<List<OkullarVM>> OkulGetirYoneticiId(string userId);
    }
}
