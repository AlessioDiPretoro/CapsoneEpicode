namespace Stones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            DetailOrder = new HashSet<DetailOrder>();
        }

        public int id { get; set; }

        public int idBuyer { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(10)]
        public string prov { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        [Required]
        [StringLength(50)]
        public string state { get; set; }

        public DateTime date { get; set; }

        [StringLength(200)]
        public string notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailOrder> DetailOrder { get; set; }

        public virtual Users Users { get; set; }
    }
}
