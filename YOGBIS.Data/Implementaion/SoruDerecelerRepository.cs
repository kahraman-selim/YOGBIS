using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class SoruDerecelerRepository : Repository<SoruDereceler>, ISoruDerecelerRepository
    {
        private readonly YOGBISContext _ctx;

        public SoruDerecelerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
