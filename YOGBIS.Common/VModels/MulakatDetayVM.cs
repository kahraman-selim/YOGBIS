using System;
using System.Collections.Generic;

namespace YOGBIS.Common.VModels
{
    public class MulakatDetayVM
    {
        public string userId { get; set; }
        public string tc { get; set; }
        public Guid mulakatId { get; set; }
        public Guid dereceId { get; set; }
        public List<KomisyonUyeViewModel> komisyonUyelerVm { get; set; }
    }
}
