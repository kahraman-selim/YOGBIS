using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IEtkinliklerBE
    {
        Result<List<EtkinliklerVM>> EtkinlikleriGetir();
        Result<EtkinliklerVM> EtkinlikEkle(EtkinliklerVM model, SessionContext user);
        Result<EtkinliklerVM> EtkinlikGetir(int id);
        Result<EtkinliklerVM> EtkinlikGuncelle(EtkinliklerVM model, SessionContext user);
        Result<bool> EtkinlikSil(int id);
        Result<List<EtkinliklerVM>> EtkinlikGetirKullaniciId(string userId);
        Result<List<EtkinliklerVM>> EtkinlikGetirUlkeId(int ulkeId);
        Result<List<EtkinliklerVM>> EtkinlikGetirOkulId(int okulId);
    }
}
