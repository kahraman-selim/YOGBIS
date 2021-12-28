using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class SiniflarRepository : Repository<Siniflar>, ISiniflarRepository
    {
        private readonly YOGBISContext _ctx;

        public SiniflarRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
