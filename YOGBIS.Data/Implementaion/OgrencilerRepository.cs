﻿using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class OgrencilerRepository : Repository<Ogrenciler>, IOgrencilerRepository
    {
        private readonly YOGBISContext _ctx;

        public OgrencilerRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
