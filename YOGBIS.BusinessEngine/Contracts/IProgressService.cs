using System;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IProgressService
    {
        void UpdateProgress(string sessionId, Action<ProgressData> updateAction);
        void ResetProgress(string sessionId);
        ProgressData GetProgress(string sessionId);
    }

    public class ProgressData
    {
        private decimal? _myspuan;
        private string _mysstarih;

        public string IslemAsamasi { get; set; }
        public int ToplamKayit { get; set; }
        public int IslemYapilan { get; set; }
        public int Yuzde { get; set; }
        public int BasariliEklenen { get; set; }
        public string Success { get; set; }
        public string Error { get; set; }
        public string Mesaj { get; set; }

        public decimal? MYSPuan
        {
            get => _myspuan;
            set
            {
                if (value.HasValue)
                {
                    _myspuan = Math.Round(value.Value, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    _myspuan = null;
                }
            }
        }

        public string MYSSTarih
        {
            get => _mysstarih;
            set
            {
                if (DateTime.TryParse(value, out DateTime date))
                {
                    _mysstarih = date.ToString("dd.MM.yyyy", new System.Globalization.CultureInfo("tr-TR"));
                }
                else
                {
                    _mysstarih = value;
                }
            }
        }
    }
}
