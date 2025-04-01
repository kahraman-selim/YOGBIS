using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class KomisyonPersonellerRepository : Repository<KomisyonPersoneller>, IKomisyonPersonellerRepository
    {
        private readonly YOGBISContext _ctx;

        public KomisyonPersonellerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
