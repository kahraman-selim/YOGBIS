using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface ISoruKategorileriBE
    {
        Result<List<SoruKategorilerVM>> GetAllSoruKategoriler();
        Result<SoruKategorilerVM> SoruKategoriEkle(SoruKategorilerVM model);

        Result<SoruKategorilerVM> GetAllSoruKategoriler(int id);

        Result<SoruKategorilerVM> SoruKategoriGuncelle(SoruKategorilerVM model);
        
        Result<bool> SoruKategoriSil(int id);        
    }
}
