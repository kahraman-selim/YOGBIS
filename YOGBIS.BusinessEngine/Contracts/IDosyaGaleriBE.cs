using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IDosyaGaleriBE
    {
        Result<List<DosyaGaleriVM>> DosyaGaleriGetir();
        Result<List<DosyaGaleriVM>> DosyaGetirKullaniciId(string userId);
        Result<DosyaGaleriVM> DosyaGetir(Guid id);
        Result<string> DosyaAdGetir(Guid id);
        Result<string> DosyaURLGetir(Guid id);
        Result<List<string>> DosyaURLGetirEtkinlikId(Guid etkinlikId);
        Result<DosyaGaleriVM> DosyaEkle(DosyaGaleriVM model, SessionContext user);
        Result<DosyaGaleriVM> DosyaGuncelle(DosyaGaleriVM model, SessionContext user);
        Result<bool> DosyaSil(Guid id);        
    }
}
