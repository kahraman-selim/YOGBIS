using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface ISoruKategorileriBE
    {
        Result<List<SoruKategorilerVM>> SoruKategorileriGetir();
        Result<SoruKategorilerVM> SoruKategoriEkle(SoruKategorilerVM model, SessionContext user);
        Result<List<SoruKategorilerVM>> SoruKategoriKullaniciId(string userId);
        Result<List<SoruKategorilerVM>> SoruKategorileriGetirDereceId(Guid dereceId);
        Result<SoruKategorilerVM> SoruKategoriGetir(Guid SoruKategorilerId);
        Result<SoruKategorilerVM> SoruKategoriGuncelle(SoruKategorilerVM model, SessionContext user);        
        Result<bool> SoruKategoriSil(Guid id);
        Result<string> SoruKategoriTakmaAdGetir(Guid id);
        Result<string> SoruKategorilerKategoriAdGetir(Guid id);
        Result<int> SoruKategorilerKategoriSiraNoGetir(Guid id);
    }
}
