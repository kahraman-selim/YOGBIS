using System;

namespace YOGBIS.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAdayBasvuruBilgileriRepository adayBasvuruBilgileriRepository { get; }
        IAdayDDKRepository adayDDKRepository { get; }
        IAdayGorevKaydiRepository adayGorevKaydiRepository { get; }
        IAdayIletisimBilgileriRepository adayIletisimBilgileriRepository { get; }
        IAdaylarRepository adaylarRepository { get; }
        IAdayMYSSRepository adayMYSSRepository { get; }
        IAdayNotRepository adayNotRepository { get; }
        IAdayTYSRepository adayTYSRepository { get; }
        IAutoHistoryRepository autoHistoryRepository { get; }
        IBranslarRepository branslarRepository { get; }
        IDosyaGaleriRepository dosyaGaleriRepository { get; }
        IDuyurularRepository duyurularRepository { get; }
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
        IKomisyonlarRepository komisyonlarRepository { get; }
        IKullaniciRepository kullaniciRepository { get; }
        IMulakatlarRepository mulakatlarRepository { get; }
        IMulakatSorulariRepository mulakatSorulariRepository { get; }
        INotlarRepository notlarRepository { get; }
        IOgrencilerRepository ogrencilerRepository { get; }
        IOkulBilgiRepository okulBilgiRepository { get; }
        IOkulBinaBolumRepository okulBinaBolumRepository { get; }
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
        ISSSRepository sssRepository { get; }
        ISSSCevapRepository sssCevapRepository { get; }
        ISubelerRepository subelerRepository { get; }
        ITemsilciliklerRepository temsilciliklerRepository { get; }
        IUlkeGruplariRepository ulkeGruplariRepository { get; }        
        IUlkelerRepository ulkelerRepository { get; }
        IUlkeTercihRepository ulkeTercihRepository { get; }
        IUlkeTercihBransRepository ulkeTercihBransRepository { get; }   
        IUniversitelerRepository universitelerRepository { get; }

        void Save();
    }
}
