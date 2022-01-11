using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class AdayDDKRepository : Repository<AdayDDK>, IAdayDDKRepository
    {
        private readonly YOGBISContext _ctx;

        public AdayDDKRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
