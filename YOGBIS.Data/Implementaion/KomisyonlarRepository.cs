using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class KomisyonlarRepository : Repository<Komisyonlar>, IKomisyonlarRepository
    {
        private readonly YOGBISContext _ctx;

        public KomisyonlarRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
