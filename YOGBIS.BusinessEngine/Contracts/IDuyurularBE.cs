using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IDuyurularBE
    {
        Result<List<DuyurularVM>> DuyurulariGetir();
        Result<List<DuyurularVM>> DuyuruGetirKullaniciId(string userId);
        Result<DuyurularVM> DuyuruGetir(Guid id);
        Result<DuyurularVM> DuyuruEkle(DuyurularVM model, SessionContext user);        
        Result<DuyurularVM> DuyuruGuncelle(DuyurularVM model, SessionContext user);
        Result<bool> DuyuruSil(Guid id);
        Result<string> DuyuruKapakURLGetir(Guid id);
    }
}
