﻿using System.Collections.Generic;
using System.Threading.Tasks;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IKullaniciBE
    {
        Result<List<KullaniciVM>> KullaniciGetir();
        Result<string> KullaniciAdSoyadGetir(string UserId);
        Result<KullaniciVM> KullaniciGuncelle(KullaniciVM model);
        Task<Result<List<KullaniciVM>>> OnayKullaniciGetir();
        Task<Result<List<KullaniciVM>>> OkulMuduruGetir();
        Task<Result<List<KullaniciVM>>> KomisyonGetir();
    }
}
