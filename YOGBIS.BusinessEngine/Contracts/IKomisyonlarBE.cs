using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IKomisyonlarBE
    {
        Result<List<KomisyonlarVM>> KomisyonlariGetir();
        Result<KomisyonlarVM> KomisyonGetir(Guid id);
        Result<KomisyonlarVM> KomisyonEkle(KomisyonlarVM model, SessionContext user);
        Result<KomisyonlarVM> KomisyonGuncelle(KomisyonlarVM model, SessionContext user);  
        Result<bool> KomisyonSil(Guid id);
        //Result<List<KomisyonlarVM>> KomisyonGetirKullaniciId(string userId);

    }
}
