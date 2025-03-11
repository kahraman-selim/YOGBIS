using System;
using System.Globalization;

namespace YOGBIS.UI.Models
{
    public class AdayMYSSVM
    {
        private string _myspuan;
        private string _mysstarih;

        public string TC { get; set; }
        
        public string MYSSTarih 
        { 
            get => _mysstarih;
            set
            {
                if (DateTime.TryParse(value, out DateTime date))
                {
                    _mysstarih = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));
                }
                else
                {
                    _mysstarih = value;
                }
            }
        }
        
        public string MYSSSaat { get; set; }
        public string MYSSMulakatYer { get; set; }
        public string MYSSDurum { get; set; }
        public string MYSSDurumAck { get; set; }
        public int MYSSKomisyonSiraNo { get; set; }
        public int KomisyonSN { get; set; }
        public int KomisyonGunSN { get; set; }
        
        public string MYSPuan
        {
            get => _myspuan;
            set
            {
                if (decimal.TryParse(value, out decimal puan))
                {
                    _myspuan = Math.Round(puan, 2, MidpointRounding.AwayFromZero)
                        .ToString("F2", new CultureInfo("tr-TR"));
                }
                else
                {
                    _myspuan = value;
                }
            }
        }
        
        public string MYSSonuc { get; set; }
        public string MYSSonucAck { get; set; }
        public int MYSSSorulanSoruNo { get; set; }
        public Guid KomisyonId { get; set; }
        public string MYSSKomisyonAdi { get; set; }
        public Guid MulakatId { get; set; }
        public Guid AdayId { get; set; }
    }
}
