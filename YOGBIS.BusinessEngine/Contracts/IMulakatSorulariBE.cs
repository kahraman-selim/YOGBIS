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
        Result<MulakatSorulariVM> MulakatSorusuEkle(MulakatSorulariVM model, SessionContext user); 
        Result<MulakatSorulariVM> MulakatSorulariGetir(Guid id);
        Result<MulakatSorulariVM> MulakatSorusuGuncelle(MulakatSorulariVM model, SessionContext user);        
        Result<bool> MulakatSorusuSil(Guid id);
        //Result<List<MulakatSorulariVM>> MulakatSorulariGetir(Guid id, string derece);
    }
}
