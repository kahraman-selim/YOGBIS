using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class UniversitelerRepository : Repository<Universiteler>, IUniversitelerRepository
    {
        private readonly YOGBISContext _ctx;

        public UniversitelerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
