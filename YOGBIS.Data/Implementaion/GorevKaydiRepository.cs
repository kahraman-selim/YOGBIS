using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class GorevKaydiRepository : Repository<GorevKaydi>, IGorevKaydiRepository
    {
        private readonly YOGBISContext _ctx;

        public GorevKaydiRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
