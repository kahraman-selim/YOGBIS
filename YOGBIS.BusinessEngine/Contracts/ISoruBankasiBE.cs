using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface ISoruBankasiBE
    {
        Result<List<SoruBankasiVM>> SoruGetirKullaniciId(string userId);
        Result<List<SoruBankasiVM>> SoruGetirOnaylayanId(string userId);
        Result<List<SoruBankasiVM>> SorulariGetir();
        Result<SoruBankasiVM> SoruEkle(SoruBankasiVM model, SessionContext user, string[] onaylayanId);
        Result<SoruBankasiVM> SoruGetir(int id);        
        Result<SoruBankasiVM> SoruGuncelle(SoruBankasiVM model, SessionContext user);        
        Result<bool> SoruSil(int id);        
    }
}
