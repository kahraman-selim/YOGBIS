using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class AdayBasvuruBilgileriRepository:Repository<AdayBasvuruBilgileri>, IAdayBasvuruBilgileriRepository
    {
        private readonly YOGBISContext _ctx;

        public AdayBasvuruBilgileriRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
