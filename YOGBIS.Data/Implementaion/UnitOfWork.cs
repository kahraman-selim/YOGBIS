using System;
using System.Collections.Generic;
using System.Text;
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
            kategorilerRepository = new KategorilerRepository(_ctx);
            mulakatlarRepository = new MulakatlarRepository(_ctx);
            mulakatSorulariRepository = new MulakatSorulariRepository(_ctx);
            soruBankasiLogRepository = new SoruBankasiLogRepository(_ctx);
            soruBankasiRepository = new SoruBankasiRepository(_ctx);
            soruKategoriRepository = new SoruKategoriRepository(_ctx);
            kullaniciRepository = new KullaniciRepository(_ctx);
        }

        public IUlkeGruplariRepository ulkeGruplariRepository { get; private set; }
        public IKitalarRepository kitalarRepository { get; private set; }
        public IUlkelerRepository ulkelerRepository { get; private set; }
        public IEyaletlerRepository eyaletlerRepository { get; private set; }
        public ISehirlerRepository sehirlerRepository { get; private set; }
        public ISoruBankasiLogRepository soruBankasiLogRepository { get; private set; }
        public ISoruBankasiRepository soruBankasiRepository { get; private set; }
        public ISoruKategoriRepository  soruKategoriRepository { get; private set; }
        public IKategorilerRepository kategorilerRepository { get; private set; }
        public IMulakatlarRepository mulakatlarRepository { get; private set; }
        public IMulakatSorulariRepository mulakatSorulariRepository { get; private set; }
        public  IKullaniciRepository kullaniciRepository { get; private set; }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
