using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class KullaniciRepository : Repository<Kullanici>, IKullaniciRepository
    {
        private readonly YOGBISContext _ctx;

        public KullaniciRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

    }
}
