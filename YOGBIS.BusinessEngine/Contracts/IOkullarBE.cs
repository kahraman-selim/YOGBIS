﻿using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IOkullarBE
    {
        Result<List<OkullarVM>> OkullariGetir();
        Result<OkullarVM> OkulEkle(OkullarVM model, SessionContext user);

        Result<OkullarVM> OkulGetir(int id);

        Result<OkullarVM> OkulGuncelle(OkullarVM model, SessionContext user);
        
        Result<bool> OkulSil(int id);

        //Result<List<OkullarVM>> OkulGetirKullaniciId(string userId);
    }
}