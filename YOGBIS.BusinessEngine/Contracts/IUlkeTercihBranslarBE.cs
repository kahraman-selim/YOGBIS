using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IUlkeTercihBranslarBE
    {
        Result<List<UlkeTercihBranslarVM>> UlkeTercihBranslariGetir();
        Result<UlkeTercihBranslarVM> UlkeTercihBransGetir(Guid id);
        Result<UlkeTercihBranslarVM> UlkeTercihBransEkle(UlkeTercihBranslarVM model, SessionContext user);
        Result<UlkeTercihBranslarVM> UlkeTercihBransGuncelle(UlkeTercihBranslarVM model, SessionContext user);
        Result<bool> UlkeTercihBransSil(Guid id, SessionContext user);
    }
}
