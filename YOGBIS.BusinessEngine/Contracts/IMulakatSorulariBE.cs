using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IMulakatSorulariBE
    {
        Result<List<MulakatSorulariVM>> MulakatSorulariGetir();
        Result<MulakatSorulariVM> MulakatSoruGetir(Guid id);
        Result<int> MulakatSoruGetirSoruSiraNo(Guid id);
        Result<MulakatSorulariVM> MulakatSoruEkle(MulakatSorulariVM model, SessionContext user);        
        Result<MulakatSorulariVM> MulakatSoruGuncelle(MulakatSorulariVM model, SessionContext user);        
        Result<bool> MulakatSorusuSil(Guid id);
        Result<List<MulakatSorulariVM>> MulakatSoruGetirKullaniciId(string userId);
        Result<string> SoruNoIleTopluGuncelle(MulakatSorulariVM model, SessionContext user);
        Result<MulakatSorulariVM> SoruSiraNoIleTopluGuncelle(MulakatSorulariVM model, SessionContext user);
        Result<List<MulakatSorulariVM>> MulakatAdaySoruGetir(int sorusirano, Guid? mulakatId, Guid? dereceId);
    }
}
