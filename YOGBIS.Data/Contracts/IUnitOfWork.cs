using System;

namespace YOGBIS.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAdaylarRepository adaylarRepository { get; }
        IAutoHistoryRepository autoHistoryRepository { get; }
        IDerecelerRepository derecelerRepository { get; }
        IEyaletlerRepository eyaletlerRepository { get; }
        IGorevKaydiRepository gorevKaydiRepository { get; }
        IKitalarRepository kitalarRepository { get; }
        IKullaniciRepository kullaniciRepository { get; }
        IMulakatlarRepository mulakatlarRepository { get; }
        IMulakatSorulariRepository mulakatSorulariRepository { get; }
        INotlarRepository notlarRepository { get; }
        IOgrencilerRepository ogrencilerRepository { get; }
        IOgretmenlerRepository ogretmenlerRepository { get; }
        IOkulBilgiRepository okulBilgiRepository { get; } // geçici bir tablo sonra silinecek
        IOkullarRepository okullarRepository { get; }
        IOkutmanlarRepository okutmanlarRepository { get; }
        ISehirlerRepository sehirlerRepository { get; }
        ISiniflarRepository siniflarRepository { get; }
        ISoruBankasiRepository soruBankasiRepository { get; }
        ISoruDereceRepository soruDereceRepository { get; }
        ISoruKategorilerRepository sorukategorilerRepository { get; }
        ISoruKategoriRepository soruKategoriRepository { get; }        
        ISubelerRepository subelerRepository { get; }
        IUlkeGruplariKitalarRepository ulkeGruplariKitalarRepository { get; }
        IUlkeGruplariRepository ulkeGruplariRepository { get; }        
        IUlkelerRepository ulkelerRepository { get; }
        IUniversitelerRepository universitelerRepository { get; }

        void Save();
    }
}
