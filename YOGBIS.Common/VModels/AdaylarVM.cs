using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AdaylarVM:BaseVM
    {
        [Key]
        public int AdayId { get; set; }
        public int AdayTC { get; set; }
    }
}
