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

            adaylarRepository = new AdaylarRepository(_ctx);
            aktivitelerRepository = new AktivitelerRepository(_ctx);
            autoHistoryRepository = new AutoHistoryRepository(_ctx);
            derecelerRepository = new DerecelerRepository(_ctx);
            eyaletlerRepository = new EyaletlerRepository(_ctx);
            gorevKaydiRepository = new GorevKaydiRepository(_ctx);
            kitalarRepository = new KitalarRepository(_ctx);
            kullaniciRepository = new KullaniciRepository(_ctx);
            mulakatlarRepository = new MulakatlarRepository(_ctx);
            mulakatSorulariRepository = new MulakatSorulariRepository(_ctx);
            notlarRepository = new NotlarRepository(_ctx);
            ogrencilerRepository = new OgrencilerRepository(_ctx);
            ogretmenlerRepository = new OgretmenlerRepository(_ctx);
            okulBilgiRepository = new OkulBilgiRepository(_ctx);
            okullarRepository = new OkullarRepository(_ctx);
            okutmanlarRepository = new OkutmanlarRepository(_ctx);
            sehirlerRepository = new SehirlerRepository(_ctx);
            siniflarRepository = new SiniflarRepository(_ctx);
            soruBankasiRepository = new SoruBankasiRepository(_ctx);
            soruDereceRepository = new SoruDereceRepository(_ctx);
            sorukategorilerRepository = new SoruKategorilerRepository(_ctx);
            soruKategoriRepository = new SoruKategoriRepository(_ctx);
            subelerRepository = new SubelerRepository(_ctx);
            ulkeGruplariKitalarRepository = new UlkeGruplariKitalarRepository(_ctx);
            ulkeGruplariRepository = new UlkeGruplariRepository(_ctx);            
            ulkelerRepository = new UlkelerRepository(_ctx);
            universitelerRepository = new UniversitelerRepository(_ctx);
            
        }

        public IAdaylarRepository adaylarRepository { get; private set; }
        public IAktivitelerRepository aktivitelerRepository { get; private set; }
        public IAutoHistoryRepository autoHistoryRepository { get; private set; }
        public IDerecelerRepository derecelerRepository { get; private set; }
        public IEyaletlerRepository eyaletlerRepository { get; private set; }
        public IGorevKaydiRepository gorevKaydiRepository { get; private set; }
        public IKitalarRepository kitalarRepository { get; private set; }
        public IKullaniciRepository kullaniciRepository { get; private set; }
        public IMulakatlarRepository mulakatlarRepository { get; private set; }
        public IMulakatSorulariRepository mulakatSorulariRepository { get; private set; }
        public INotlarRepository notlarRepository { get; private set; }
        public IOgrencilerRepository ogrencilerRepository { get; private set; }
        public IOgretmenlerRepository ogretmenlerRepository { get; private set; }
        public IOkulBilgiRepository okulBilgiRepository { get; private set; }
        public IOkullarRepository okullarRepository { get; private set; }
        public IOkutmanlarRepository okutmanlarRepository { get; private set; }
        public ISehirlerRepository sehirlerRepository { get; private set; }
        public ISiniflarRepository siniflarRepository { get; private set; }
        public ISoruBankasiRepository soruBankasiRepository { get; private set; }
        public ISoruDereceRepository soruDereceRepository { get; private set; }
        public ISoruKategorilerRepository sorukategorilerRepository { get; private set; }
        public ISoruKategoriRepository soruKategoriRepository { get; private set; }
        public ISubelerRepository subelerRepository { get; private set; }
        public IUlkeGruplariKitalarRepository ulkeGruplariKitalarRepository { get; private set; }
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
