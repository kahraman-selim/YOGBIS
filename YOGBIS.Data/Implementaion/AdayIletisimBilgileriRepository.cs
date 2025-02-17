using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class AdayIletisimBilgileriRepository : Repository<AdayIletisimBilgileri>, IAdayIletisimBilgileriRepository
    {
        private readonly YOGBISContext _ctx;

        public AdayIletisimBilgileriRepository(YOGBISContext ctx):base(ctx)
        {
            _ctx = ctx;
        }
    }
}
