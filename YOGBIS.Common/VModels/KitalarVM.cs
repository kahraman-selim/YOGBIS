using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOGBIS.Common.VModels
{
    public class KitalarVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid KitaId { get; set; }
        public string KitaAdi { get; set; }
        public string KitaAciklama { get; set; }
        public string KaydedenId { get; set; }
        public Guid? UlkeGrupId { get; set; }
        public UlkeGruplariVM UlkeGruplari { get; set; }
        public List<UlkelerVM> Ulkeler { get; set; }
    }
}
