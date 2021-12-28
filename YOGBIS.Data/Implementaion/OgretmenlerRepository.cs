using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class OgretmenlerRepository : Repository<Ogretmenler>, IOgretmenlerRepository
    {
        private readonly YOGBISContext _ctx;

        public OgretmenlerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
