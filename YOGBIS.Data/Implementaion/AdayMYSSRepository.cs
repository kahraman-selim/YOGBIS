using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class AdayMYSSRepository:Repository<AdayMYSS>, IAdayMYSSRepository
    {
        private readonly YOGBISContext _ctx;

        public AdayMYSSRepository(YOGBISContext ctx):base(ctx)
        {
            _ctx = ctx;
        }
    }
}
