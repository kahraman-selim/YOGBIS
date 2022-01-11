﻿using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Data.Implementaion
{
    public class IllerMdEPostaRepository : Repository<IllerMdEPosta>, IIllerMdEPostaRepository
    {
        private readonly YOGBISContext _ctx;

        public IllerMdEPostaRepository(YOGBISContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
