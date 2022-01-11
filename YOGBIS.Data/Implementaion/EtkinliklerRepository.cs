using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class EtkinliklerRepository : Repository<Etkinlikler>, IEtkinliklerRepository
    {
        private readonly YOGBISContext _ctx;

        public EtkinliklerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
