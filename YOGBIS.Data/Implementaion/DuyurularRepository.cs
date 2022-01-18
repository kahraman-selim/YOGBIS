using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class DuyurularRepository : Repository<Duyurular>, IDuyurularRepository
    {
        private readonly YOGBISContext _ctx;

        public DuyurularRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
