using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class AdayNotRepository : Repository<AdayNot>, IAdayNotRepository
    {
        private readonly YOGBISContext _ctx;

        public AdayNotRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
