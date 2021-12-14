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
        Result<SoruKategorilerVM> SoruKategoriGetir(int id);
        Result<SoruKategorilerVM> SoruKategoriGuncelle(SoruKategorilerVM model, SessionContext user);        
        Result<bool> SoruKategoriSil(int id);        
    }
}
