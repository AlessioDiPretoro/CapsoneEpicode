namespace Stones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailOrder")]
    public partial class DetailOrder
    {
        public int id { get; set; }

        public int idOrder { get; set; }

        public int idProduct { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int quanty { get; set; }

        [Column(TypeName = "money")]
        public decimal priceCad { get; set; }

        [StringLength(50)]
        public string state { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
