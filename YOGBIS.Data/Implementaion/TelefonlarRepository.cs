using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class TelefonlarRepository : Repository<Telefonlar>, ITelefonlarRepository
    {
        private readonly YOGBISContext _ctx;

        public TelefonlarRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
