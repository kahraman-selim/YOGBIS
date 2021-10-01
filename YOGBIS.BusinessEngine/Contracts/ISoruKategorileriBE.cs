using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface ISoruKategorileriBE
    {
        Result<List<SoruKategorilerVM>> SoruKategoriGetir();
        Result<SoruKategorilerVM> SoruKategoriEkle(SoruKategorilerVM model);
        Result<List<SoruKategorilerVM>> SoruKategoriKullaniciId(string userId);        
        Result<SoruKategorilerVM> SoruKategoriGetir(int id);
        Result<SoruKategorilerVM> SoruKategoriGuncelle(SoruKategorilerVM model);        
        Result<bool> SoruKategoriSil(int id);        
    }
}
