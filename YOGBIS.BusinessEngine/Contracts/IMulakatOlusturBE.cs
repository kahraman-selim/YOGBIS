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
        Result<List<MulakatlarVM>> MulakatlariGetirViewComponent();
        Result<List<MulakatlarVM>> MulakatlariGetirKontejanSelectedBox();
        Result<List<MulakatlarVM>> MulakatlariGetirSelectedBox();
        Result<MulakatlarVM> MulakatGetir(Guid id);
        Result<MulakatlarVM> MulakatEkle(MulakatlarVM model, SessionContext user);              
        Result<MulakatlarVM> MulakatGuncelle(MulakatlarVM model, SessionContext user);        
        Result<bool> MulakatSil(Guid id);
        //Result<List<MulakatlarVM>> MulakatAdGetirDereceId(Guid dereceId);
        Result<string> MulakatDonemAdGetir(Guid id);
        Result<string> MulakatOnayGetir(Guid id);
        Result<string> MulakatYilGetir(Guid id);
    }
}
