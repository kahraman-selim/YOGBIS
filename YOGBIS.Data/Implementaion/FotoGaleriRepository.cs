using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class FotoGaleriRepository : Repository<FotoGaleri>, IFotoGaleriRepository
    {
        private readonly YOGBISContext _ctx;

        public FotoGaleriRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
