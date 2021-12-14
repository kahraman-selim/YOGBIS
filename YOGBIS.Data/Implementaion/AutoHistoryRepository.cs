using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class AutoHistoryRepository : Repository<AutoHistory>, IAutoHistoryRepository
    {
        private readonly YOGBISContext _ctx;

        public AutoHistoryRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
