using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class BranslarRepository : Repository<Branslar>, IBranslarRepository
    {
        private readonly YOGBISContext _ctx;

        public BranslarRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
