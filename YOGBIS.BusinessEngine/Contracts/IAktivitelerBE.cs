using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IAktivitelerBE
    {
        Result<List<AktivitelerVM>> EtkinlikleriGetir();
        Result<AktivitelerVM> EtkinlikEkle(AktivitelerVM model, SessionContext user);
        Result<AktivitelerVM> EtkinlikGetir(int id);
        Result<AktivitelerVM> EtkinlikGuncelle(AktivitelerVM model, SessionContext user);        
        Result<bool> EtkinlikSil(int id);
        Result<List<AktivitelerVM>> EtkinlikleriGetirKullaniciId(string userId);
        Result<List<AktivitelerVM>> EtkinlikGetirUlkeId(int ulkeId);
        Result<List<AktivitelerVM>> EtkinlikGetirOkulId(int okulId);
    }
}
