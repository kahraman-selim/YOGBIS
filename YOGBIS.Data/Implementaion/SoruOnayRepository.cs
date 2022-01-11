using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class SoruOnayRepository : Repository<SoruOnay>, ISoruOnayRepository
    {
        private readonly YOGBISContext _ctx;

        public SoruOnayRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
