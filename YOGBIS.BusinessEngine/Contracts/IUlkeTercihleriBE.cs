using System;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using System.Collections.Generic;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IUlkeTercihleriBE
    {
        Result<List<UlkeTercihVM>> UlkeTercihleriGetir(int year);
        Result<UlkeTercihVM> UlkeTercihGetir(Guid id);
        Result<UlkeTercihVM> UlkeTercihEkle(UlkeTercihVM model, SessionContext user);
        Result<bool> UlkeTercihGuncelle(UlkeTercihVM model, SessionContext user);
        Result<bool> UlkeTercihSil(Guid id);
        Result<bool> BransEkle(Guid ulkeTercihId, Guid tercihBransId, string yabancıDil, int bransKontSayi, string bransCinsiyet, bool esitBrans, SessionContext user);
        Result<List<UlkeTercihVM>> UlkeTercihAdlariGetir();
    }
}
