using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IKomisyonlarBE
    {
        Result<List<KomisyonlarVM>> KomisyonlariGetir(string mulakatId = null);
        Result<KomisyonlarVM> KomisyonGetir(Guid id);
        Result<KomisyonlarVM> KomisyonEkle(KomisyonlarVM model, SessionContext user);
        Result<KomisyonlarVM> KomisyonGuncelle(KomisyonlarVM model, SessionContext user);
        Result<KomisyonlarVM> DurumDegistir(Guid komisyonId, SessionContext user);
        Result<bool> KomisyonSil(Guid id);
        Task<Guid?> GetKomisyonIdBySiraNo(int siraNo);       
    }
}
