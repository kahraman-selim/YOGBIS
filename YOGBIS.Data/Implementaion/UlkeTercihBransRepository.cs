﻿using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class UlkeTercihBransRepository : Repository<UlkeTercihBranslar>, IUlkeTercihBransRepository
    {
        private readonly YOGBISContext _ctx;

        public UlkeTercihBransRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
