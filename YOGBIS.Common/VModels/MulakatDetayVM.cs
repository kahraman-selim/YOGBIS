using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class MulakatDetayVM
    {
        public string userId { get; set; }
        public string tc { get; set; }
        public Guid mulakatId { get; set; }
        public Guid dereceId { get; set; }

    }
}
