using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class SoruDereceVM : BaseVM
    {
        public Guid SoruId { get; set; }
        public SoruBankasiVM SoruBankasi { get; set; }
        public Guid DereceId { get; set; }
        public SoruDerecelerVM SoruDereceler { get; set; }
        public string KaydedenAdi { get; set; }
    }
}
