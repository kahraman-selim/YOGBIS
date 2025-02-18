using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IUlkeTercihleriBE
    {
        Result<List<UlkeTercihVM>> UlkeTercihleriGetir();
        Result<UlkeTercihVM> UlkeTercihGetir(Guid id);
        Result<UlkeTercihVM> UlkeTercihEkle(UlkeTercihVM model, SessionContext user);
        Result<UlkeTercihVM> UlkeTercihGuncelle(UlkeTercihVM moddel, SessionContext user);
        Result<bool> UlkeTercihSil(Guid id);
    }
}
