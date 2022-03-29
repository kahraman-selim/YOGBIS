using System;
using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IOgrencilerBE
    {
        Result<List<OgrencilerVM>> OgrencileriGetir();
        Result<OgrencilerVM> OgrenciGetir(Guid id);
        Result<OgrencilerVM> OgrenciEkle(OgrencilerVM model, SessionContext user);
        Result<OgrencilerVM> OgrenciGuncelle(OgrencilerVM model, SessionContext user);
        Result<bool> OgrenciSil(Guid id);
        Result<List<OgrencilerVM>> OgrenciGetirKullaniciId(string userId);
        Result<List<OgrencilerVM>> OgrenciGetirUlkeId(Guid ulkeId);
        Result<List<OgrencilerVM>> OgrenciGetirOkulId(Guid okulId);        
    }
}
