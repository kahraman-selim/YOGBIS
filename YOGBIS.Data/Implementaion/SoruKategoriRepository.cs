using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class SoruKategoriRepository : Repository<SoruKategori>, ISoruKategoriRepository
    {
        private readonly YOGBISContext _ctx;

        public SoruKategoriRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
