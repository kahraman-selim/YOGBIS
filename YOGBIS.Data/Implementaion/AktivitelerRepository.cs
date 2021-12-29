using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class AktivitelerRepository : Repository<Aktiviteler>, IAktivitelerRepository
    {
        private readonly YOGBISContext _ctx;

        public AktivitelerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
