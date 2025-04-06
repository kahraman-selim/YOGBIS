using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IAdaylarBE
    {
        Result<List<AdaylarVM>> AdaylariGetir();
        Result<List<AdaylarVM>> AdayTemelBilgileriGetir();
        Result<AdaylarVM> AdayGetir(Guid id);
        Result<AdayMYSSVM> MYSSAdayGetir(Guid id);
        Result<AdaylarVM> AdayGetirTC(string TC);
        Result<AdaylarVM> AdayEkle(AdaylarVM model, SessionContext user);
        Result<AdaylarVM> AdayGuncelle(AdaylarVM model, SessionContext user);
        Result<AdayBasvuruBilgileriVM> AdayBasvuruGetir(string TC);
        Result<AdayBasvuruBilgileriVM> AdayBasvuruGuncelle(AdayBasvuruBilgileriVM model, SessionContext user);
        Result<AdayMYSSVM> AdayMYSSGuncelle(AdayMYSSVM model, SessionContext user);
        //Task<Result<bool>> UpdateBilgiFormu(string userId, byte[] fileData);
        Result<bool> AdaySil(Guid id);
        Result<List<AdaylarVM>> AdayGetirKullaniciId(string userId);
        bool TCKontrol(string TC);
        Result<AdayBasvuruBilgileriVM> AdayBasvuruBilgileriEkle(AdayBasvuruBilgileriVM model, SessionContext user);
        Result<List<AdayBasvuruBilgileriVM>> AdayBasvuruBilgileriniGetir(string TC);
        Result<List<AdayBasvuruBilgileriVM>> AdayBasvuruBilgileriniGetirMulakat(string TC);
        Result<AdayBasvuruBilgileriVM> AdayBasvuruBilgileriniGetirById(Guid id);
        Result<AdayIletisimBilgileriVM> AdayIletisimBilgileriEkle(AdayIletisimBilgileriVM model, SessionContext user);
        Result<List<AdayIletisimBilgileriVM>> AdayIletisimBilgileriniGetir(string TC);
        Result<AdayIletisimBilgileriVM> AdayIletisimBilgileriniGetirById(Guid id);
        Result<AdayMYSSVM> AdayMYSSBilgileriEkle(AdayMYSSVM model, SessionContext user);
        Result<List<AdayMYSSVM>> AdayMYSSBilgileriniGetir(string TC);
        Result<AdayMYSSVM> AdayMYSSBilgileriniGetirById(Guid id);      
        Result<AdayTYSVM> AdayTYSBilgileriEkle(AdayTYSVM model, SessionContext user);
        Result<List<AdayTYSVM>> AdayTYSBilgileriniGetir(string TC);
        Result<AdayTYSVM> AdayTYSBilgileriniGetirById(Guid id);
        Result<List<AdayMYSSVM>> GetirKomisyonMulakatListesi(string komisyonAdi, string mulakatTarihi);        
        Result<List<AdayMYSSVM>> AdayTakipMulakatListesi();
        Result<bool> AdayCagriDurumGuncelle(Guid id);
        Result<bool> AdaySinavaGelmediGuncelle(Guid id);
        Result<bool> AdaySinavaGeldiGuncelle(Guid id);
        Result<List<AdayMYSSVM>> AdayKabulMulakatListesi();
        Result<bool> AdaySinavKabulGuncelle(Guid id);
        Result<bool> AdaySinavOdaKabulGuncelle(Guid id);
        Result<bool> AdaySinavOdaAlindiGuncelle(Guid id);
        Result<string> AdayIletisimBilgileriGetir(Guid adayId);
        Result<AdayMYSSVM> GetirAdayMYSSBilgileri(Guid id);
        Result<AdayBasvuruBilgileriVM> GetirAdayBasvuruBilgileri(Guid? adayId);
        Result<AdaySinavNotlarVM> AdaySinavNotuKaydet(AdaySinavNotlarVM model,SessionContext user);
        Result<AdayBasvuruBilgileriVM> GetirAdayBasvuruBilgileriByTcAndMulakatId(string TC, Guid mulakatId);
        Result<AdayMYSSVM> AdaySinavIptalGuncelle(AdayMYSSVM model, SessionContext user);
        Result<AdayBasvuruBilgileriVM> AdayTopluGuncelle(AdayBasvuruBilgileriVM model, SessionContext user);
        Result<AdayMYSSVM> AdayMYSSTopluGuncelle(AdayMYSSVM model, SessionContext user);
        Result<List<AdayMYSSVM>> MYSSAdaylariGetir();
    }
}
