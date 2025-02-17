using Microsoft.EntityFrameworkCore;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;

namespace YOGBIS.Data.Implementaion
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly YOGBISContext _ctx;
        public UnitOfWork(YOGBISContext ctx)
        {
            _ctx = ctx;

            adayBasvuruBilgileriRepository = new AdayBasvuruBilgileriRepository(_ctx);
            adayDDKRepository = new AdayDDKRepository(_ctx);            
            adayGorevKaydiRepository = new AdayGorevKaydiRepository(_ctx);
            adayIletisimBilgileriRepository = new AdayIletisimBilgileriRepository(_ctx);
            adaylarRepository = new AdaylarRepository(_ctx);
            adayMYSSRepository = new AdayMYSSRepository(_ctx);
            adayNotRepository = new AdayNotRepository(_ctx);
            adayTYSRepository = new AdayTYSRepository(_ctx);
            autoHistoryRepository = new AutoHistoryRepository(_ctx);
            branslarRepository = new BranslarRepository(_ctx);
            dosyaGaleriRepository = new DosyaGaleriRepository(_ctx);
            duyurularRepository = new DuyurularRepository(_ctx);            
            ePostaAdresleriRepository = new EPostaAdresleriRepository(_ctx);            
            etkinliklerRepository = new EtkinliklerRepository(_ctx);
            eyaletlerRepository = new EyaletlerRepository(_ctx);
            fotoGaleriRepository = new FotoGaleriRepository(_ctx);
            gorevKararPdfGaleriRepository = new GorevKararPdfGaleriRepository(_ctx);
            ikametAdresleriRepository = new IkametAdresleriRepository(_ctx);
            ilcelerRepository = new IlcelerRepository(_ctx);
            illerMdEPostaRepository = new IllerMdEPostaRepository(_ctx);
            illerRepository = new IllerRepository(_ctx);            
            kitalarRepository = new KitalarRepository(_ctx);
            komisyonlarRepository = new KomisyonlarRepository(_ctx);
            kullaniciRepository = new KullaniciRepository(_ctx);
            mulakatlarRepository = new MulakatlarRepository(_ctx);
            mulakatSorulariRepository = new MulakatSorulariRepository(_ctx);
            notlarRepository = new NotlarRepository(_ctx);
            ogrencilerRepository = new OgrencilerRepository(_ctx);
            okulBinaBolumRepository = new OkulBinaBolumRepository(_ctx);
            okulBilgiRepository = new OkulBilgiRepository(_ctx);
            okullarRepository = new OkullarRepository(_ctx);
            personellerRepository = new PersonellerRepository(_ctx);
            sehirlerRepository = new SehirlerRepository(_ctx);
            siniflarRepository = new SiniflarRepository(_ctx);
            soruBankasiLogRepository = new SoruBankasiLogRepository(_ctx);
            soruBankasiRepository = new SoruBankasiRepository(_ctx);
            soruDerecelerRepository = new SoruDerecelerRepository(_ctx);
            soruDereceRepository = new SoruDereceRepository(_ctx);
            sorukategorilerRepository = new SoruKategorilerRepository(_ctx);
            soruKategoriRepository = new SoruKategoriRepository(_ctx);
            soruOnayRepository = new SoruOnayRepository(_ctx);
            sssRepository = new SSSRepository(_ctx);
            sssCevapRepository = new SSSCevapRepository(_ctx);
            subelerRepository = new SubelerRepository(_ctx);
            temsilciliklerRepository = new TemsilciliklerRepository(_ctx);
            ulkeGruplariRepository = new UlkeGruplariRepository(_ctx);            
            ulkelerRepository = new UlkelerRepository(_ctx);
            universitelerRepository = new UniversitelerRepository(_ctx);
            
        }

        public IAdayBasvuruBilgileriRepository adayBasvuruBilgileriRepository { get; private set; }
        public IAdayDDKRepository adayDDKRepository { get; private set; }
        public IAdayGorevKaydiRepository adayGorevKaydiRepository { get; private set; }
        public IAdayIletisimBilgileriRepository adayIletisimBilgileriRepository { get; private set; }
        public IAdaylarRepository adaylarRepository { get; private set; }
        public IAdayMYSSRepository adayMYSSRepository { get; private set; }
        public IAdayNotRepository adayNotRepository { get; private set; }  
        public IAdayTYSRepository adayTYSRepository { get; private set; }
        public IAutoHistoryRepository autoHistoryRepository { get; private set; }
        public IBranslarRepository branslarRepository { get; private set; }
        public IDosyaGaleriRepository dosyaGaleriRepository { get; private set; }
        public IDuyurularRepository duyurularRepository { get; private set; }
        public IEPostaAdresleriRepository ePostaAdresleriRepository { get; private set; }
        public IEtkinliklerRepository etkinliklerRepository { get; private set; }        
        public IEyaletlerRepository eyaletlerRepository { get; private set; }
        public IFotoGaleriRepository fotoGaleriRepository { get; private set; }
        public IGorevKararPdfGaleriRepository gorevKararPdfGaleriRepository { get; private set; }
        public IIkametAdresleriRepository ikametAdresleriRepository { get; private set; }
        public IIlcelerRepository ilcelerRepository { get; private set; }
        public IIllerRepository illerRepository { get; private set; }
        public IIllerMdEPostaRepository illerMdEPostaRepository { get; private set; }
        public IKitalarRepository kitalarRepository { get; private set; }
        public IKomisyonlarRepository komisyonlarRepository { get; private set; }
        public IKullaniciRepository kullaniciRepository { get; private set; }
        public IMulakatlarRepository mulakatlarRepository { get; private set; }
        public IMulakatSorulariRepository mulakatSorulariRepository { get; private set; }
        public INotlarRepository notlarRepository { get; private set; }
        public IOgrencilerRepository ogrencilerRepository { get; private set; }
        public IOkulBinaBolumRepository okulBinaBolumRepository { get; private set; }
        public IOkulBilgiRepository okulBilgiRepository { get; private set; }
        public IOkullarRepository okullarRepository { get; private set; }
        public IPersonellerRepository personellerRepository { get; private set; }
        public ISehirlerRepository sehirlerRepository { get; private set; }
        public ISiniflarRepository siniflarRepository { get; private set; }
        public ISoruBankasiLogRepository soruBankasiLogRepository { get; private set; }
        public ISoruBankasiRepository soruBankasiRepository { get; private set; }
        public ISoruDerecelerRepository soruDerecelerRepository { get; private set; }
        public ISoruDereceRepository soruDereceRepository { get; private set; }        
        public ISoruKategorilerRepository sorukategorilerRepository { get; private set; }
        public ISoruKategoriRepository soruKategoriRepository { get; private set; }
        public ISoruOnayRepository soruOnayRepository { get; private set; }
        public ISSSRepository sssRepository { get; private set; }
        public ISSSCevapRepository sssCevapRepository { get; private set; }
        public ISubelerRepository subelerRepository { get; private set; }
        public ITemsilciliklerRepository temsilciliklerRepository { get; private set; }
        public IUlkeGruplariRepository ulkeGruplariRepository { get; private set; }        
        public IUlkelerRepository ulkelerRepository { get; private set; }         
        public IUniversitelerRepository universitelerRepository { get; private set; }


        public void Save()
        {
            _ctx.EnsureAutoHistory();
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
