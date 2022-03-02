using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IOkulBinaBolumBE
    {
        Result<List<OkulBinaBolumVM>> OkulBinaBolumleriGetir();
        Result<OkulBinaBolumVM> OkulBinaBolumEkle(OkulBinaBolumVM model, SessionContext user);
        Result<OkulBinaBolumVM> OkulBinaBolumGetir(Guid id);
        Result<OkulBinaBolumVM> OkulBinaBolumGuncelle(OkulBinaBolumVM model, SessionContext user);        
        Result<bool> OkulBinaBolumSil(Guid id);
        Result<List<OkulBinaBolumVM>> OkulBinaBolumGetirKullaniciId(string userId);
        Result<OkulBinaBolumVM> OkulBinaBolumGetirBolumAdi(string okulbinabolumAdi);
        Result<Guid> OkulBinaBolumIdGetir(string okulbinabolumAdi);
        Result<string> OkulBinaBolumAdGetir(Guid id);
    }
}
