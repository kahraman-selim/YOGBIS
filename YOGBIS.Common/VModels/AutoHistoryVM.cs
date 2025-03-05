using System;
using System.ComponentModel.DataAnnotations;

namespace YOGBIS.Common.VModels
{
    public class AutoHistoryVM
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Satır ID zorunludur")]
        [Display(Name = "Satır ID")]
        public string RowId { get; set; }

        [Required(ErrorMessage = "Tablo adı zorunludur")]
        [Display(Name = "Tablo Adı")]
        public string TableName { get; set; }

        [Display(Name = "Değişiklikler")]
        public string Changed { get; set; }

        [Display(Name = "İşlem Türü")]
        public int Kind { get; set; }

        [Required(ErrorMessage = "Oluşturulma tarihi zorunludur")]
        [Display(Name = "Oluşturulma Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
