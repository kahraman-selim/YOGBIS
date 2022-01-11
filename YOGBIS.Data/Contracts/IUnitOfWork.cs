using System;

namespace YOGBIS.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAdayDDKRepository adayDDKRepository { get; }
        IAdayGorevKaydiRepository adayGorevKaydiRepository { get; }
        IAdaylarRepository adaylarRepository { get; }
        IAdayNotRepository adayNotRepository { get; }
        IAutoHistoryRepository autoHistoryRepository { get; }
        IBranslarRepository branslarRepository { get; }
        IEPostaAdresleriRepository ePostaAdresleriRepository { get; }
        IEtkinliklerRepository etkinliklerRepository { get; }
        IEyaletlerRepository eyaletlerRepository { get; }
        IFotoGaleriRepository fotoGaleriRepository { get; }
        IGorevKararPdfGaleriRepository gorevKararPdfGaleriRepository { get; }
        IIkametAdresleriRepository ikametAdresleriRepository { get; }
        IIlcelerRepository ilcelerRepository { get; }
        IIllerRepository illerRepository { get; }
        IIllerMdEPostaRepository illerMdEPostaRepository { get; }
        IKitalarRepository kitalarRepository { get; }
        IKullaniciRepository kullaniciRepository { get; }
        IMulakatlarRepository mulakatlarRepository { get; }
        IMulakatSorulariRepository mulakatSorulariRepository { get; }
        INotlarRepository notlarRepository { get; }
        IOgrencilerRepository ogrencilerRepository { get; }
        IOkulBilgiRepository okulBilgiRepository { get; }
        IOkullarRepository okullarRepository { get; }
        ISehirlerRepository sehirlerRepository { get; }
        ISiniflarRepository siniflarRepository { get; }
        ISoruBankasiLogRepository soruBankasiLogRepository { get; }
        ISoruBankasiRepository soruBankasiRepository { get; }
        ISoruDerecelerRepository soruDerecelerRepository { get; }
        ISoruDereceRepository soruDereceRepository { get; }
        ISoruKategorilerRepository sorukategorilerRepository { get; }
        ISoruKategoriRepository soruKategoriRepository { get; }
        ISoruOnayRepository soruOnayRepository { get; }
        ISubelerRepository subelerRepository { get; }
        IUlkeGruplariKitalarRepository ulkeGruplariKitalarRepository { get; }
        IUlkeGruplariRepository ulkeGruplariRepository { get; }        
        IUlkelerRepository ulkelerRepository { get; }
        IUniversitelerRepository universitelerRepository { get; }

        void Save();
    }
}
