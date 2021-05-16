using System;
using System.Collections.Generic;
using System.Text;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class MulakatlarRepository : Repository<Mulakatlar>, IMulakatlarRepository
    {
        private readonly YOGBISContext _ctx;

        public MulakatlarRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
