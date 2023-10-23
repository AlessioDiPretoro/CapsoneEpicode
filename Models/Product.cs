namespace Stones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            DetailOrder = new HashSet<DetailOrder>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string name { get; set; }

        [Column(TypeName = "money")]
        public decimal price { get; set; }

        public string descripton { get; set; }

        public int? idCategory { get; set; }

        public int? idSubject { get; set; }

        public string photo1 { get; set; }

        public string photo2 { get; set; }

        public string photo3 { get; set; }

        public string photo4 { get; set; }

        public string photo5 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailOrder> DetailOrder { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ProductSubject ProductSubject { get; set; }
    }
}