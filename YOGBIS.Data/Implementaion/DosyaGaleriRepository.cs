using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class DosyaGaleriRepository : Repository<DosyaGaleri>, IDosyaGaleriRepository
    {
        private readonly YOGBISContext _ctx;

        public DosyaGaleriRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
