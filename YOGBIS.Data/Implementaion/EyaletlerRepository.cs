﻿using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class EyaletlerRepository : Repository<Eyaletler>, IEyaletlerRepository
    {
        private readonly YOGBISContext _ctx;

        public EyaletlerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
