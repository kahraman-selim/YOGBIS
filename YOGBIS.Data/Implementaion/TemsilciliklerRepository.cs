using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class TemsilciliklerRepository : Repository<Temsilcilikler>, ITemsilciliklerRepository
    {
        private readonly YOGBISContext _ctx;

        public TemsilciliklerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
