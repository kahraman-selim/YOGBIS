using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IBranslarBE
    {
        Result<List<BranslarVM>> BranslariGetir();
        Result<BranslarVM> BransGetir(Guid id);
        Result<BranslarVM> BransEkle(BranslarVM model, SessionContext user);
        Result<BranslarVM> BransGuncelle(BranslarVM model, SessionContext user);
        Result<bool> BransSil(Guid id);
        //Result<List<BranslarVM>> BransGetirKullaniciId(string userId);
    }
}
