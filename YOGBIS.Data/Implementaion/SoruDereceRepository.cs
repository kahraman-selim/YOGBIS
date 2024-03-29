﻿using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class SoruDereceRepository : Repository<SoruDerece>, ISoruDereceRepository
    {
        private readonly YOGBISContext _ctx;

        public SoruDereceRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
