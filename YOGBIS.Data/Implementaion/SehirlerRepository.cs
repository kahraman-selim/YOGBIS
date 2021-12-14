using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class SehirlerRepository : Repository<Sehirler>, ISehirlerRepository
    {
        private readonly YOGBISContext _ctx;

        public SehirlerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
