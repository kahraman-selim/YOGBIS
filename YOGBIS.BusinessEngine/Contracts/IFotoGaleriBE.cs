using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IFotoGaleriBE
    {
        Result<List<FotoGaleriVM>> FotoGaleriGetir();
        Result<List<FotoGaleriVM>> FotoGetirKullaniciId(string userId);
        Result<FotoGaleriVM> FotoGetir(Guid id);
        Result<string> FotoAdGetir(Guid id);
        Result<string> FotoURLGetir(Guid id);
        Result<List<string>> FotoURLGetirUlkeId(Guid ulkeId);
        Result<FotoGaleriVM> FotoEkle(FotoGaleriVM model, SessionContext user);
        Result<FotoGaleriVM> FotoGuncelle(FotoGaleriVM model, SessionContext user);
        Result<bool> FotoSil(Guid id);        
    }
}
