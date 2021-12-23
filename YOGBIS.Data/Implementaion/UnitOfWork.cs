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
            ulkeGruplariRepository = new UlkeGruplariRepository(_ctx);
            kitalarRepository = new KitalarRepository(_ctx);
            ulkelerRepository = new UlkelerRepository(_ctx);
            eyaletlerRepository = new EyaletlerRepository(_ctx);
            sehirlerRepository = new SehirlerRepository(_ctx);
            sorukategorilerRepository = new SoruKategorilerRepository(_ctx);
            mulakatlarRepository = new MulakatlarRepository(_ctx);
            mulakatSorulariRepository = new MulakatSorulariRepository(_ctx);            
            soruBankasiRepository = new SoruBankasiRepository(_ctx);
            soruKategoriRepository = new SoruKategoriRepository(_ctx);
            kullaniciRepository = new KullaniciRepository(_ctx);
            autoHistoryRepository = new AutoHistoryRepository(_ctx);
            ulkeGruplariKitalarRepository = new UlkeGruplariKitalarRepository(_ctx);
            derecelerRepository = new DerecelerRepository(_ctx);
            okullarRepository = new OkullarRepository(_ctx);
            okulBilgiRepository = new OkulBilgiRepository(_ctx);
            ogrencilerRepository = new OgrencilerRepository(_ctx);
            notlarRepository = new NotlarRepository(_ctx);
            soruDereceRepository = new SoruDereceRepository(_ctx);
        }

        public IUlkeGruplariRepository ulkeGruplariRepository { get; private set; }
        public IKitalarRepository kitalarRepository { get; private set; }
        public IUlkelerRepository ulkelerRepository { get; private set; }
        public IEyaletlerRepository eyaletlerRepository { get; private set; }
        public ISehirlerRepository sehirlerRepository { get; private set; }        
        public ISoruBankasiRepository soruBankasiRepository { get; private set; }
        public ISoruKategoriRepository  soruKategoriRepository { get; private set; }
        public ISoruKategorilerRepository sorukategorilerRepository { get; private set; }
        public IMulakatlarRepository mulakatlarRepository { get; private set; }
        public IMulakatSorulariRepository mulakatSorulariRepository { get; private set; }
        public  IKullaniciRepository kullaniciRepository { get; private set; }
        public IAutoHistoryRepository autoHistoryRepository { get; private set; }
        public IUlkeGruplariKitalarRepository ulkeGruplariKitalarRepository { get; private set; }
        public IDerecelerRepository derecelerRepository { get; private set; }
        public IOkullarRepository okullarRepository { get; private set; }
        public IOkulBilgiRepository okulBilgiRepository { get; private set; }
        public IOgrencilerRepository ogrencilerRepository { get; private set; }
        public INotlarRepository notlarRepository { get; private set; }
        public ISoruDereceRepository soruDereceRepository { get; private set; }
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
