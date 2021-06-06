using System;
using System.Collections.Generic;
using System.Text;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class SoruKategorilerRepository : Repository<SoruKategoriler>, ISoruKategorilerRepository
    {
        private readonly YOGBISContext _ctx;

        public SoruKategorilerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
