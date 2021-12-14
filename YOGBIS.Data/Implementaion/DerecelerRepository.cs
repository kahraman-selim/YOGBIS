using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class DerecelerRepository : Repository<Dereceler>, IDerecelerRepository
    {
        private readonly YOGBISContext _ctx;

        public DerecelerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
