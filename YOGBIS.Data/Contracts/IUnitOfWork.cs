using System;
using System.Collections.Generic;
using System.Text;

namespace YOGBIS.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
         IUlkeGruplariRepository ulkeGruplariRepository { get;  }
         IKitalarRepository kitalarRepository { get;  }
         IUlkelerRepository ulkelerRepository { get;  }
         IEyaletlerRepository eyaletlerRepository { get;  }
         ISehirlerRepository sehirlerRepository { get;  }
         ISoruBankasiRepository soruBankasiRepository { get;  }
         ISoruKategoriRepository soruKategoriRepository { get;  }
         ISoruKategorilerRepository sorukategorilerRepository { get;  }
         IMulakatlarRepository mulakatlarRepository { get;  }
         IMulakatSorulariRepository mulakatSorulariRepository { get; }  
         IKullaniciRepository kullaniciRepository { get; }
         IAutoHistoryRepository autoHistoryRepository { get; }
         IUlkeGruplariKitalarRepository ulkeGruplariKitalarRepository { get; }
         IDerecelerRepository derecelerRepository { get; }
         IOkullarRepository okullarRepository { get; }
         IOkulBilgiRepository okulBilgiRepository { get; }

        void Save();
    }
}
