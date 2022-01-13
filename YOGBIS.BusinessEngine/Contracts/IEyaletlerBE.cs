using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IEyaletlerBE
    {
        Result<List<EyaletlerVM>> EyaletleriGetir();
        Result<EyaletlerVM> EyaletEkle(EyaletlerVM model, SessionContext user);
        Result<EyaletlerVM> EyaletGetir(int id);
        Result<EyaletlerVM> EyaletGuncelle(EyaletlerVM model, SessionContext user);        
        Result<bool> EyaletSil(int id);
        Result<List<EyaletlerVM>> EyaletGetirKullaniciId(string userId);
    }
}
