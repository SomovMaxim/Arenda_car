namespace Arenda_car.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contract")]
    public partial class Contract
    {
        public int id { get; set; }

        public int? id_client { get; set; }

        [StringLength(50)]
        public string name_client { get; set; }

        [StringLength(50)]
        public string passport { get; set; }

        public int? id_car { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_start { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_end { get; set; }

        public decimal? price { get; set; }
    }
}
