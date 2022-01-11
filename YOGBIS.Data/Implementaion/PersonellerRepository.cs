using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class PersonellerRepository : Repository<Personeller>, IPersonellerRepository
    {
        private readonly YOGBISContext _ctx;

        public PersonellerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
