using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class AdayTYSRepository:Repository<AdayTYS>, IAdayTYSRepository
    {
        private readonly YOGBISContext _ctx;

        public AdayTYSRepository(YOGBISContext ctx):base(ctx)
        {
            _ctx = ctx;
        }
    }
}
