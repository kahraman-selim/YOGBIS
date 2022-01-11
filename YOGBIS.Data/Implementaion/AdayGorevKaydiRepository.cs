using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class AdayGorevKaydiRepository : Repository<AdayGorevKaydi>, IAdayGorevKaydiRepository
    {
        private readonly YOGBISContext _ctx;

        public AdayGorevKaydiRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
