using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class SSSCevapRepository : Repository<SSSCevap>, ISSSCevapRepository
    {
        private readonly YOGBISContext _ctx;

        public SSSCevapRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
