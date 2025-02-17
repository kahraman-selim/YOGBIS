using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class UlkeTercihRepository:Repository<UlkeTercih>, IUlkeTercihRepository
    {
        private readonly YOGBISContext _ctx;

        public UlkeTercihRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
