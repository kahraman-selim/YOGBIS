using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class AdaySinavNotlarRepository : Repository<AdayNot>, IAdaySinavNotlarRepository
    {
        private readonly YOGBISContext _ctx;

        public AdaySinavNotlarRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
