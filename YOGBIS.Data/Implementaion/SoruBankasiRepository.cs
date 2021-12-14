using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class SoruBankasiRepository : Repository<SoruBankasi>, ISoruBankasiRepository
    {
        private readonly YOGBISContext _ctx;

        public SoruBankasiRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
