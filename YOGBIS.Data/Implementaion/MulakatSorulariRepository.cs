using System;
using System.Collections.Generic;
using System.Text;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class MulakatSorulariRepository : Repository<MulakatSorulari>, IMulakatSorulariRepository
    {
        private readonly YOGBISContext _ctx;

        public MulakatSorulariRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
