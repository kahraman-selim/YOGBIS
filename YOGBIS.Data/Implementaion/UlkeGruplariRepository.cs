using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class UlkeGruplariRepository : Repository<UlkeGruplari>, IUlkeGruplariRepository
    {
        private readonly YOGBISContext _ctx;

        public UlkeGruplariRepository(YOGBISContext ctx):base(ctx)
        {
            _ctx = ctx;
        }
    }
}
