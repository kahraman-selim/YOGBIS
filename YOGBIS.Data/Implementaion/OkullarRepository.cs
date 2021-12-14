using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class OkullarRepository : Repository<Okullar>, IOkullarRepository
    {
        private readonly YOGBISContext _ctx;

        public OkullarRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
