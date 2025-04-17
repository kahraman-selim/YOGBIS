using System;
using System.Collections.Generic;
using System.Text;

namespace YOGBIS.Common.VModels
{
    public class KomisyonUyelerVM
    {
        public Guid KomisyonId { get; set; }
        public string KomisyonUyeAdSoyad { get; set; }
        public int KomisyonUyeSiraId { get; set; }
        public int KategoriNot1 { get; set; }
        public int KategoriNot2 { get; set; }
        public int KategoriNot3 { get; set; }
        public int KategoriNot4 { get; set; }
        public int KategoriNot5 { get; set; }
        public string SecilenAdayTCNo { get; set; }
        public string MulakatId { get; set; }
    }
}
