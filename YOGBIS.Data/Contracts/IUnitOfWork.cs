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
         ISoruBankasiLogRepository soruBankasiLogRepository { get;  }
         ISoruBankasiRepository soruBankasiRepository { get;  }
         ISoruKategoriRepository soruKategoriRepository { get;  }
         IKategorilerRepository kategorilerRepository { get;  }
         IMulakatlarRepository mulakatlarRepository { get;  }
         IMulakatSorulariRepository mulakatSorulariRepository { get; }  
         IKullaniciRepository kullaniciRepository { get; }

        void Save();
    }
}
