using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface ISoruBankasiBE
    {
        Result<List<SoruBankasiVM>> GetAllSorular();
        Result<SoruBankasiVM> SoruEkle(SoruBankasiVM model);

        Result<SoruBankasiVM> GetAllSorular(int id);

        Result<SoruBankasiVM> SoruGuncelle(SoruBankasiVM model);
        
        Result<bool> SoruSil(int id);        
    }
}
