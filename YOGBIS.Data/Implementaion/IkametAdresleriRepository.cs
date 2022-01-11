using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class IkametAdresleriRepository : Repository<IkametAdresleri>, IIkametAdresleriRepository
    {
        private readonly YOGBISContext _ctx;

        public IkametAdresleriRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
