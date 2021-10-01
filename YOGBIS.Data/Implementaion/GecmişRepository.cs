using System;
using System.Collections.Generic;
using System.Text;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class GecmişRepository : Repository<Gecmis>, IGecmişRepository
    {
        private readonly YOGBISContext _ctx;

        public GecmişRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
