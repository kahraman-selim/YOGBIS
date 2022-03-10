using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface ISiniflarBE
    {
        Result<List<SiniflarVM>> SiniflariGetir();
        Result<SiniflarVM> SinifEkle(SiniflarVM model, SessionContext user);
        Result<SiniflarVM> SinifGetir(Guid id);
        Result<List<SiniflarVM>> SiniflariGetirOkulId(Guid OkulId);
        Result<SiniflarVM> SinifGuncelle(SiniflarVM model, SessionContext user);        
        Result<bool> SinifSil(Guid id);
        Result<List<SiniflarVM>> SinifGetirKullaniciId(string userId);
        Result<string> SinifAdGetir(Guid id);
    }
}
