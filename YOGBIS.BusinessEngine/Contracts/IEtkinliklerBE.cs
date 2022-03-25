using System;
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
        Result<EtkinliklerVM> EtkinlikGetir(Guid id);
        Result<EtkinliklerVM> EtkinlikGuncelle(EtkinliklerVM model, SessionContext user);
        Result<bool> EtkinlikSil(Guid id);
        Result<List<EtkinliklerVM>> EtkinlikGetirKullaniciId(string userId);
        Result<List<EtkinliklerVM>> EtkinlikGetirUlkeId(Guid ulkeId);
        Result<List<EtkinliklerVM>> EtkinlikGetirOkulId(Guid okulId);
        Result<string> EtkinlikKapakURLGetir(Guid id);
    }
}
