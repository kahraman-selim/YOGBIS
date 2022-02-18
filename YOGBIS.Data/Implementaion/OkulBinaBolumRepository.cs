using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class OkulBinaBolumRepository : Repository<OkulBinaBolum>, IOkulBinaBolumRepository
    {
        private readonly YOGBISContext _ctx;

        public OkulBinaBolumRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
