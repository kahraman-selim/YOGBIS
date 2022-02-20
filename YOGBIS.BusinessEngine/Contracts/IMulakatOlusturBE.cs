using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IMulakatOlusturBE
    {              
        Result<List<MulakatlarVM>> MulakatlariGetir();
        Result<MulakatlarVM> MulakatGetir(Guid id);
        Result<MulakatlarVM> MulakatEkle(MulakatlarVM model, SessionContext user);              
        Result<MulakatlarVM> MulakatGuncelle(MulakatlarVM model, SessionContext user);        
        Result<bool> MulakatSil(Guid id);        
    }
}
