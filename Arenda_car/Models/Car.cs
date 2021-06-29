namespace Arenda_car.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Car")]
    public partial class Car
    {
        public int id { get; set; }

        [StringLength(50)]
        public string vin { get; set; }

        [StringLength(50)]
        public string state_number { get; set; }

        [StringLength(50)]
        public string brand { get; set; }

        [StringLength(1)]
        public string category { get; set; }

        [StringLength(50)]
        public string age { get; set; }

        [StringLength(50)]
        public string body { get; set; }

        [StringLength(50)]
        public string color_body { get; set; }

        [StringLength(50)]
        public string power_engine { get; set; }

        [StringLength(50)]
        public string type_engine { get; set; }

        public int? Insurance { get; set; }

        public decimal? price { get; set; }

        public int? count { get; set; }

        [StringLength(50)]
        public string name { get; set; }
    }
}
