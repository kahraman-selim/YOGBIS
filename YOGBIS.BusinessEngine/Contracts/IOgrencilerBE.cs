using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IOgrencilerBE
    {
        Result<List<OgrencilerVM>> OgrencileriGetir();
        Result<OgrencilerVM> OgrenciGetir(int id);
        Result<OgrencilerVM> OgrenciEkle(OgrencilerVM model, SessionContext user);
        Result<OgrencilerVM> OgrenciGuncelle(OgrencilerVM model, SessionContext user);        
        Result<bool> OgrenciSil(int id);
        Result<List<OgrencilerVM>> OgrenciGetirKullaniciId(string userId);
        Result<List<OgrencilerVM>> OgrenciGetirUlkeId(int ulkeId);
    }
}
