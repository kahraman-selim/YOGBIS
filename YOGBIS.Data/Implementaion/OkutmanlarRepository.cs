using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class OkutmanlarRepository : Repository<Okutmanlar>, IOkutmanlarRepository
    {
        private readonly YOGBISContext _ctx;

        public OkutmanlarRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
